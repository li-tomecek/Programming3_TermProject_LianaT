using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellComponent : MonoBehaviour, IClickable
{
    [Header("Sprite")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite _defaultSprite, _selectedSprite, _disabledSprite;
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
    }

    public void OnClick()
    {
        if (_isInteractable)
            gameObject.GetComponentInParent<Disk>()?.RotateToFront(this);
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

    public bool IsInteractable() { return _isInteractable; }

    public void SetInteractable(bool interactable)
    {
        _isInteractable = interactable;

        if (interactable)
        {
            if (_defaultSprite)
                _spriteRenderer.sprite = _defaultSprite;

            _spriteColor.a = 1f;
            _spriteRenderer.color = _spriteColor;
        }
        else
        {
            if (_disabledSprite)
                _spriteRenderer.sprite = _disabledSprite;

            _spriteColor.a = _disabledAlpha;
            _spriteRenderer.color = _spriteColor;

        }
    }

    public void SetSelectedSprite()
    {
        if (_selectedSprite)
            _spriteRenderer.sprite = _selectedSprite;
    }
}