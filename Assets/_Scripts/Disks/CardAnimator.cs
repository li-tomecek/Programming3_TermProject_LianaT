using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CardAnimator : MonoBehaviour
{
    [SerializeField] private CardView _cardView;
    [SerializeField] float _timeToMinimize, _timeToExpand;
    [SerializeField] float _minimizedScale, _expandedScale;
    [SerializeField] float _timeExpanded = 0.7f;

    [SerializeField] Transform _dockedPosition;
    
    
    public IEnumerator PlayCardApplicationAnimation(Card card, Vector3 startScreenPosition)
    {
        _cardView.SetAssociatedCard(card);
        _cardView.transform.position = startScreenPosition;
        
        _cardView.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_cardView?.gameObject.transform.DOMove(Camera.main.WorldToScreenPoint(_dockedPosition.position), _timeToMinimize));
        sequence.Join(_cardView?.gameObject.transform.DOScale(_minimizedScale, _timeToMinimize));

        yield return sequence.WaitForCompletion();          //This one doesnt have to be a coroutine, as there is nothing that needs to wait for it.
    }

    public IEnumerator PlayCardUsedAnimation(Vector3 targetScreenPosition)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(_cardView?.gameObject.transform.DOMove(targetScreenPosition, _timeToExpand));
        sequence.Join(_cardView?.gameObject.transform.DOScale(_expandedScale, _timeToExpand));
        sequence.AppendInterval(_timeExpanded);

        yield return sequence.WaitForCompletion();

        HideCard();
    }

    public void HideCard()
    {

        _cardView.gameObject.SetActive(false);
    }
}
