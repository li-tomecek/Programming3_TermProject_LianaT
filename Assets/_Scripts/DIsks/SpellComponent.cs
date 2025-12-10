using Unity.VisualScripting;
using UnityEngine;

public class SpellComponent : MonoBehaviour, IClickable
{
    [Header("Sprite")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite _defaultSprite, _selectedSprite, _disabledSprite;
    [SerializeField] ParticleSystem _particles;
    [SerializeField] bool _isInteractable = true;
    [SerializeField] float _disabledAlpha = 0.5f;
    private Color _spriteColor = Color.white;

    [Header("Spell")]
    [SerializeField] SpellPosition _spellPosition;
    [SerializeField] SpellType _spellType;

    [DoNotSerialize] public bool IsRotating;

    // Getters
    public SpellPosition SpellPosition => _spellPosition;
    public SpellType SpellType => _spellType;

    public void Awake()
    {
        gameObject.transform.LookAt(Camera.main.transform, Vector3.up);
        _spriteRenderer.color = _spriteColor;
        _particles.Stop();
    }

    #region Rotation & Interaction

    public void OnClick()
    {
        if (_isInteractable && BattleStateManager.Instance.CurrentState is PreparationPhase)
        {
            SpellComponent activeSpell = gameObject.GetComponentInParent<Disk>()?.GetActiveSpell();
            if (activeSpell != null && activeSpell.IsInteractable())
                activeSpell.SetSprite(SpriteVariant.Default);

            gameObject.GetComponentInParent<Disk>()?.SetActiveSpell(this);
            SetSprite(SpriteVariant.Selected);
        }
    }

    public void RotateLeft()
    {
        switch (_spellPosition)
        {
            case SpellPosition.Left:
                _spellPosition = SpellPosition.Right;
                break;

            case SpellPosition.Front:
                _spellPosition = SpellPosition.Left;
                break;

            case SpellPosition.Right:
                _spellPosition = SpellPosition.Front;
                break;
        }
    }

    public void RotateRight()
    {
        switch (_spellPosition)
        {
            case SpellPosition.Left:
                _spellPosition = SpellPosition.Front;
                break;

            case SpellPosition.Front:
                _spellPosition = SpellPosition.Right;
                break;

            case SpellPosition.Right:
                _spellPosition = SpellPosition.Left;
                break;
        }
    }
    #endregion

    #region Getters / Setters

    public bool IsInteractable() { return _isInteractable; }
    public void SetInteractable(bool interactable)
    {
        _isInteractable = interactable;

        if (interactable)
        {
            SetSprite(SpriteVariant.Default);

            _spriteColor.a = 1f;
            _spriteRenderer.color = _spriteColor;
        }
        else
        {
            SetSprite(SpriteVariant.Disabled);

            _spriteColor.a = _disabledAlpha;
            _spriteRenderer.color = _spriteColor;

        }
    }

    public void SetSprite(SpriteVariant variant)
    {
        switch (variant)
        {
            case SpriteVariant.Default:
                if (_defaultSprite)
                    _spriteRenderer.sprite = _defaultSprite;
                    _particles.Stop();
                    _particles.Clear();
                break;

            case SpriteVariant.Selected:
                if (_selectedSprite)
                    _spriteRenderer.sprite = _selectedSprite;
                    _particles.Play();
                break;

            case SpriteVariant.Disabled:
                if (_disabledSprite)
                    _spriteRenderer.sprite = _disabledSprite;
                    _particles.Stop();
                    _particles.Clear();
                break;
        }
    }

    #endregion

    #region Static functions: Spell Matchups
    public static SpellType GetWinningType(SpellType type)
    {
        switch (type)
        {
            case (SpellType.Holy):
                return SpellType.Arcane;
            case (SpellType.Arcane):
                return SpellType.Dark;
            case (SpellType.Dark):
                return SpellType.Holy;
            default:
                return SpellType.Arcane;      //this should never be reached
        }
    }

    public static SpellType GetLosingType(SpellType type)
    {
        switch (type)
        {
            case (SpellType.Holy):
                return SpellType.Dark;
            case (SpellType.Arcane):
                return SpellType.Holy;
            case (SpellType.Dark):
                return SpellType.Arcane;
            default:
                return SpellType.Dark;      //this should never be reached
        }
    }
    #endregion
}

public enum SpriteVariant
{
    Default, Selected, Disabled
}