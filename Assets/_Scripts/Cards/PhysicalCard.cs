using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

[RequireComponent (typeof(Card))]
public class PhysicalCard : MonoBehaviour, 
    IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    private bool _IsHeld;
    private Vector3 _dockedLocalPosition;
    private Sequence _hoverSequence;            //This needs to be its own sequence in order to pause it when a card is being dragged
    
    public event System.Action<Card> HoverStartEvent;
    public event System.Action HoverEndEvent;

    //-----------------------------------------------------
    //-----------------------------------------------------

    private void Start()
    {
        SetDisplayInformation(gameObject.GetComponentInChildren<Card>());
        _dockedLocalPosition = gameObject.transform.localPosition;

        _hoverSequence = DOTween.Sequence();
        _hoverSequence.Pause();
        _hoverSequence.SetAutoKill(false);
        _hoverSequence.Join(gameObject.transform.DOLocalMoveY(_dockedLocalPosition.y + 15f, 0.5f));
    }

    public void SetDisplayInformation(Card card)
    {
        nameText.text = card.cardName;
        descriptionText.text = card.cardDescription;
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _IsHeld = false;

        gameObject.transform.DOLocalMove(_dockedLocalPosition, 0.5f); 
    }

    // -----------------
    // Hovering
    // -----------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_IsHeld)
            return;

        _hoverSequence.Restart();
        HoverStartEvent?.Invoke(gameObject.GetComponentInChildren<Card>());
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
        this._dockedLocalPosition = position;
    }
}
