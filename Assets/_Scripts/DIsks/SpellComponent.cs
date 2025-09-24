using UnityEngine;
using UnityEngine.EventSystems;

public class SpellComponent : MonoBehaviour, IClickable
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private bool _isInteractable = true;

    [SerializeField] private SpellPosition _spellPosition;
    [SerializeField] private SpellType _spellType;

    public bool IsRotating;

    // Getters
    public SpellPosition SpellPosition => _spellPosition;
    public SpellType SpellType => _spellType;

    public void Awake()
    {
        gameObject.transform.LookAt(Camera.main.transform, Vector3.up);
    }

    public void OnClick()
    {
        if (_isInteractable)
            gameObject.GetComponentInParent<Disk>()?.RotateToFront(_spellPosition);
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

    public void SetInteractable(bool interactable) { _isInteractable = interactable; }
}