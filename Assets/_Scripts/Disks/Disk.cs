using System.Data.Common;
using DG.Tweening;
using UnityEngine;

public class Disk : MonoBehaviour
{
    [SerializeField] private Disk _targetDisk;
    [SerializeField] private float _timeToRotate = 0.6f;

    private Card _activeCard;
    [SerializeField] private bool _isInteractable;

    private SpellComponent[] _spellList;

    void Awake()
    {
        _spellList = gameObject.GetComponentsInChildren<SpellComponent>();
    }

    //-----------------------------
    //          Rotation
    //-----------------------------

    public void RotateToFront(SpellPosition position)
    {
        if (!_isInteractable)
            return;
            
        if (position == SpellPosition.Left)
        {
            foreach (SpellComponent spell in _spellList)
                spell.RotateRight();

            gameObject.transform.DOLocalRotate(new Vector3(0f, 120f, 0f), _timeToRotate)
            .SetRelative(true)
            .OnUpdate(UpdateAllSprites);

        }
        else if (position == SpellPosition.Right)
        {
            foreach (SpellComponent spell in _spellList)
                spell.RotateLeft();

            gameObject.transform.DOLocalRotate(new Vector3(0f, -120f, 0f), _timeToRotate)
            .SetRelative(true)
            .OnUpdate(UpdateAllSprites);

        }
    }

    public void UpdateAllSprites()
    {
        foreach (SpellComponent spell in _spellList)
        {
            spell.gameObject.transform.LookAt(Camera.main.transform, Vector3.up);
        }
    }

    //-----------------------------
    //      Getters/Setters         
    //-----------------------------
    
    public void ApplyCard(Card card) { _activeCard = card; }
    public Card GetActiveCard() { return _activeCard; }

    public void SetInteractable(bool interactable) { _isInteractable = interactable; }
    public bool IsInteractable() { return _isInteractable; }

}
