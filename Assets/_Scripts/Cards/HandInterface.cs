using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public class HandInterface : MonoBehaviour
{
    private RectTransform _rectTransform;

    //Hand
    private const int MAX_HAND_SIZE = 5;
    private List<PhysicalCard> _physicalCards = new List<PhysicalCard>();
    private ObjectPool _physicalCardPool;
    [SerializeField] GameObject PhysCardPrefab;

    //Info Card
    [SerializeField] private GameObject _infoCard;
    [SerializeField] private float _infoCardScale = 1.75f;
    [SerializeField] private float _infoCardY = 100f;
    [SerializeField] private float _infoCardTransitionTime = 0.5f;
    private DG.Tweening.Sequence infoCardSeq;

    private bool isHoveringCard;
    
    //-----------------------------------------------------
    //-----------------------------------------------------

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _physicalCardPool = new ObjectPool(PhysCardPrefab, MAX_HAND_SIZE, this.gameObject);   //create pool of card prefabs

        //InfoCard
        _infoCard.SetActive(false);

        infoCardSeq = DOTween.Sequence();
        infoCardSeq.Append(_infoCard.transform.DOLocalMoveY(_infoCardY, _infoCardTransitionTime));
        infoCardSeq.Join(_infoCard.transform.DOScale(_infoCardScale, _infoCardTransitionTime));
        infoCardSeq.SetAutoKill(false);
    }

    //-------------------------------
    //        Managing Hand
    //-------------------------------
    public void AddPhysicalCardToHand(Card card)
    {
        if (_physicalCards.Count >= MAX_HAND_SIZE)
            throw new Exception("Trying to add cards to a full hand!");


        PhysicalCard temp = _physicalCardPool.GetActivePooledObject().GetComponent<PhysicalCard>();
        temp.SetAssociatedCard(card);
        RegisterCardEvents(temp);
        _physicalCards.Add(temp);
    }

    public void RemovePhysicalCardFromHand(PhysicalCard card)
    {
        _physicalCards.Remove(card);
        card.gameObject.SetActive(false);   //To put it back into the pool
    }

    private void RegisterCardEvents(PhysicalCard card)
    {
        card.HoverStartEvent += ShowInfoCard;
        card.HoverEndEvent += HideInfoCard;
    }

    public void SetCardPositionsInHand()
    {
        if (_physicalCards.Count == 0) return;

        //evenly space out the cards in the area provided. May be overlap/overflow over edge of space
        float slotWidth = _rectTransform.rect.width / _physicalCards.Count;

        Vector3 newPosition = new Vector3();
        for (int i = 0; i < _physicalCards.Count; i++)
        {
            newPosition.x = (i * slotWidth) + (slotWidth / 2f);
            _physicalCards[i].gameObject.transform.localPosition = newPosition;
            _physicalCards[i].SetDockedPosition(newPosition);
        }

        Debug.Log("reset hand card positions");
    }

    //-------------------------------
    //  Managing Enlarged Info Card
    //-------------------------------

    private void ShowInfoCard(Card card)
    {
        isHoveringCard = true;
        _infoCard.GetComponentInChildren<PhysicalCard>().SetDisplayInformation(card);
        
        if (!_infoCard.activeSelf)
        {
            _infoCard.SetActive(true);
            infoCardSeq.Restart();
        }
    }

    private void HideInfoCard()
    {
        isHoveringCard = false;
        StartCoroutine(DelayedClose());
    }

    private IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(0.01f);
        if (!isHoveringCard)    //check to see if we immediately started hovering over a new card
        {
            infoCardSeq.PlayBackwards();
            yield return new WaitForSeconds(_infoCardTransitionTime);
            _infoCard.SetActive(false);
        }
    }

}
