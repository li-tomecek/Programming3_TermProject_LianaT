using System;
using DG.Tweening;
using UnityEngine;

public class Disk : DropTarget
{
    [SerializeField] private Disk _targetDisk;
    public float TimeToRotate = 0.6f;

    private Card _activeCard;

    private SpellComponent[] _spellList;
    private SpellComponent _activeSpell;        //the spell in the 'FRONT' position

    private Participant _parentParticipant;
    public bool IsPlayer { get; private set; }
    private float _damageMultiplier = 1f;

    void Awake()
    {
        _spellList = gameObject.GetComponentsInChildren<SpellComponent>();
        _activeSpell = FindSpellAtFront();
        _parentParticipant = gameObject.GetComponentInParent<Participant>();
        
        IsPlayer = _parentParticipant is Player;
    }

    //-----------------------------
    //          Rotation
    //-----------------------------
    #region
    public void RotateToFront(SpellComponent toFront)
    {
        if (!_isInteractable)
            return;

        if (toFront.SpellPosition == SpellPosition.Left)
        {
            foreach (SpellComponent spell in _spellList)
                spell.RotateRight();

            gameObject.transform.DOLocalRotate(new Vector3(0f, 120f, 0f), TimeToRotate)
            .SetRelative(true)
            .OnUpdate(UpdateAllSprites);

        }
        else if (toFront.SpellPosition == SpellPosition.Right)
        {
            foreach (SpellComponent spell in _spellList)
                spell.RotateLeft();

            gameObject.transform.DOLocalRotate(new Vector3(0f, -120f, 0f), TimeToRotate)
            .SetRelative(true)
            .OnUpdate(UpdateAllSprites);

        }

        _activeSpell = toFront;
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
    //      Combat State       
    //-----------------------------
    public void PlayCard()
    {
        _activeCard?.Play(this);
        _activeCard = null;
    }
    
    public void ResetState()
    {
        _damageMultiplier = 1f;
    }


    //-----------------------------
    //      Getters/Setters         
    //-----------------------------
    #region 
    public void ApplyCard(Card card) { _activeCard = card; }
    public Card GetActiveCard() { return _activeCard; }
    public SpellComponent GetActiveSpell() { return _activeSpell; }
    public Participant GetParticipant()
    {
        return _parentParticipant;
        //return gameObject.GetComponentInParent<Participant>();
    }
    public float GetDamageMultiplier() { return _damageMultiplier; }
    public void ApplyDamageMultiplier(float mult)
    {
        _damageMultiplier *= mult;  //We multiply and not directly set, as multiple multipliers may be added via different sounrces during one turn
    }
    public Disk GetOpposingDisk() { return _targetDisk; }
    public SpellComponent FindSpellAtFront()
    {
        foreach (SpellComponent spell in _spellList)
        {
            if (spell.SpellPosition == SpellPosition.Front)
                return spell;
        }

        throw new Exception("There are no spells set to the front position!");
    }
    public void ResetSpellInteractability()
    {
        foreach (SpellComponent spell in _spellList)
        {
            spell.SetInteractable(true);
        }
    }
    #endregion
}
