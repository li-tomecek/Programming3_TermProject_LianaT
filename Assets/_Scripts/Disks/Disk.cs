using System;
using System.Data.Common;
using System.Transactions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Disk : DropTarget
{
    [SerializeField] private Disk _targetDisk;
    [SerializeField] private float _timeToRotate = 0.6f;

    private Card _activeCard;

    private SpellComponent[] _spellList;

    void Awake()
    {
        _spellList = gameObject.GetComponentsInChildren<SpellComponent>();
    }

    //-----------------------------
    //          Rotation
    //-----------------------------
    #region 
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
    #endregion

    //-----------------------------
    //      Drag and Drop        
    //-----------------------------
    #region
    public override void OnDragStartHover(IDroppable droppedObject)
    {
        //highlight disk here (if object is card and disk is interactable)
    }

    public override void OnDragEndHover()
    {
        //stop highlighting disk here
    }

    public override void OnDrop(IDroppable droppedObject)
    {
        if (droppedObject is Card && _isInteractable)
            _activeCard = (Card)droppedObject;
    }
    #endregion

    //-----------------------------
    //      Getters/Setters         
    //-----------------------------
    #region 
    public void ApplyCard(Card card) { _activeCard = card; }
    public Card GetActiveCard() { return _activeCard; }

    public SpellComponent GetSpellAtFront()
    {
        foreach (SpellComponent spell in _spellList)
        {
            if (spell.SpellPosition == SpellPosition.Front)
                return spell;
        }

        throw new Exception("There are no spells set to the front position!");
    }
    #endregion
}
