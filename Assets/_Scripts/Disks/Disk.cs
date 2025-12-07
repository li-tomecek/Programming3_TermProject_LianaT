using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Disk : DropTarget
{
    [SerializeField] Disk _opposingDisk;
    
    [Header("Disk Rotation")]
    public float TimeToRotate = 0.6f;
    
    [Header("Spell Enlarge on Win")]
    [SerializeField] float _spellExpansionFactor = 0.5f;
    [SerializeField] float _timeToExpand = 0.3f;
    [SerializeField] float _timeExpanded = 0.5f;

    private Card _activeCard;

    private SpellComponent[] _spellList;
    private SpellComponent _activeSpell;        //the spell in that has been chosen to rotate to the front

    private float _damageMultiplier = 1f;
    private bool _isRotationLocked;

    private Participant _parentParticipant;
    public bool IsPlayer { get; private set; }

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
        if (_isRotationLocked)  //don't actually need this here but keep it as a safeguard
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

    public void RotateByType(SpellType type)
    {
        foreach (SpellComponent spell in _spellList)
        {
            if (spell.SpellType == type)
            {
                RotateToFront(spell);
                return;
            }
        }
    }
    
    public void RotateByPosition(SpellPosition positionToFront)
    {
        if (positionToFront == SpellPosition.Front)
            return;
        
        foreach (SpellComponent spell in _spellList)
        {
            if (spell.SpellPosition == positionToFront)
            {
                RotateToFront(spell);
                return;
            }
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
        if (droppedObject is Card && _isTargetable)
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

    public IEnumerator PlayCardAnimation()
    {
        yield return 0;
    }

    public void ResetState()
    {
        _damageMultiplier = 1f;
        _isRotationLocked = false;
    }
    
    public IEnumerator EnlargeSpellOnWin()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_activeSpell.transform.DOScale(_spellExpansionFactor, _timeToExpand).SetRelative(true));      //TODO: Fix magic numbers
        sequence.AppendInterval(_timeExpanded);
        sequence.Append(_activeSpell.transform.DOScale(-_spellExpansionFactor, _timeToExpand).SetRelative(true));

        yield return sequence.WaitForCompletion();
    }

    //-----------------------------
    //      Getters/Setters         
    //-----------------------------
    #region 
    public void ApplyCard(Card card) { _activeCard = card; }
    public Card GetActiveCard() { return _activeCard; }
    
    public SpellComponent GetActiveSpell() { return _activeSpell; }
    public void SetActiveSpell(SpellComponent spell) { _activeSpell = spell; }
    public SpellComponent FindSpellAtFront()
    {
        foreach (SpellComponent spell in _spellList)
        {
            if (spell.SpellPosition == SpellPosition.Front)
                return spell;
        }

        throw new Exception("There are no spells set to the front position!");
    }
    public SpellComponent[] GetSpellList() { return _spellList; }
    public void ResetSpellInteractability()
    {
        foreach (SpellComponent spell in _spellList)
        {
            spell.SetInteractable(true);
        }
    }

    public Participant GetParticipant()
    {
        return _parentParticipant;
        //return gameObject.GetComponentInParent<Participant>();
    }
    public Disk GetOpposingDisk() { return _opposingDisk; }

    public float GetDamageMultiplier() { return _damageMultiplier; }
    public void ApplyDamageMultiplier(float mult)
    {
        _damageMultiplier *= mult;  //We multiply and not directly set, as multiple multipliers may be added via different sounrces during one turn
    }

    public void LockRotation() { _isRotationLocked = true; }
    public bool IsRotationLocked() { return _isRotationLocked; }
    #endregion
}
