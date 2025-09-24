using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

public class PhysicalCard : MonoBehaviour,
    IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private LayerMask _dropTargetMask;

    private Card _associatedCard;

    private bool _IsHeld;
    private Vector3 _dockedLocalPosition;
    private Ray ray;
    private IDropTarget _currentTarget, _newTarget;

    private Sequence _hoverSequence;            //This needs to be its own sequence in order to pause it when a card is being dragged

    public event System.Action<Card> HoverStartEvent;
    public event System.Action HoverEndEvent;

    //-----------------------------------------------------
    //-----------------------------------------------------

    private void Start()
    {
        _dockedLocalPosition = gameObject.transform.localPosition;

        _hoverSequence = DOTween.Sequence();
        _hoverSequence.Pause();
        _hoverSequence.SetAutoKill(false);
        _hoverSequence.Join(gameObject.transform.DOLocalMoveY(_dockedLocalPosition.y + 15f, 0.5f));
    }

    public void SetDisplayInformation(Card card)
    {
        _nameText.text = card.cardName;
        _descriptionText.text = card.cardDescription;
    }

    // -----------------
    // Dragging
    // -----------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        _IsHeld = true;
        _hoverSequence?.Pause();
        HoverEndEvent?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position += (Vector3)eventData.delta;
        CheckCardTargeting();
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _IsHeld = false;
        if (_currentTarget != null)
        {
            _currentTarget.OnDrop(_associatedCard);
            Player.Instance.DiscardCard(_associatedCard, this);
        }
        else
        {
            gameObject.transform.DOLocalMove(_dockedLocalPosition, 0.5f);
        }
    }

    private void CheckCardTargeting()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, _dropTargetMask))  //Check for potential drop target
        {
            _newTarget = hit.collider.gameObject.GetComponentInParent<IDropTarget>();

            if (_newTarget != null && _newTarget != _currentTarget)     //If this is a new drop target, change the _current target
            {
                _currentTarget?.OnDragEndHover();
                _currentTarget = _newTarget;
                _currentTarget.OnDragStartHover();
            }
        }
        else                                                            //If we are no longer hovering over any target
        {
            _currentTarget?.OnDragEndHover();
            _currentTarget = null;
        }
    }

    // -----------------
    // Hovering
    // -----------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_IsHeld)
            return;

        _hoverSequence.Restart();
        HoverStartEvent?.Invoke(_associatedCard);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_IsHeld)
            return;

        gameObject.transform.DOLocalMove(_dockedLocalPosition, 0.5f);
        HoverEndEvent?.Invoke();
    }

    public void SetDockedPosition(Vector3 position)
    {
        _dockedLocalPosition = position;
    }

    // -----------------
    // Get/Set
    // -----------------
    public Card AssociatedCard => _associatedCard;

    public void SetAssociatedCard(Card card)
    {
        _associatedCard = card;
        SetDisplayInformation(card);
    }
}
