using UnityEngine;
using UnityEngine.EventSystems;

public class SpellComponent : MonoBehaviour, Clickable
{
    [SerializeField] private GameObject _disk;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private bool _isInteractable = true;

    [SerializeField] private SpellPosition _spellPosition;
    [SerializeField] private SpellType _spellType;


    // Getters
    public SpellPosition SpellPosition => _spellPosition;
    public SpellType SpellType => _spellType;

    public void OnClick()
    {
        if (!_isInteractable)
            return;

        Vector3 tempRotation = new Vector3(0f, 120f, 0f);
            
        switch (_spellPosition)
        {
            case SpellPosition.Left:
                _disk.transform.Rotate(tempRotation);
                _spellPosition = SpellPosition.Front;
                break;

            case SpellPosition.Right:
                _disk.transform.Rotate(tempRotation * -1f);
                _spellPosition = SpellPosition.Front;
                break;
        }
    }
}