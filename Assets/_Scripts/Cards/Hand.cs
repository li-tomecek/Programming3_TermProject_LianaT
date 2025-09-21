using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public class Hand : MonoBehaviour
{
    private RectTransform _rectTransform;

    private const int MAX_HAND_SIZE = 5;
    [SerializeField] private List<Card> cards = new List<Card>();

    [SerializeField] private GameObject _infoCard;
    [SerializeField] private float _infoCardScale = 1.75f;
    [SerializeField] private float _infoCardY = 100f;
    [SerializeField] private float _infoCardTransitionTime = 0.5f;
    private Sequence infoCardSeq;
    private bool isHoveringCard;
    
    //-----------------------------------------------------
    //-----------------------------------------------------

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        //Cards
        foreach (Card card in cards)            //In case we added cards through the editor (for testing)
            RegisterCard(card);
        
        SetCardPositionsInHand();

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
    private void AddCardToHand(Card card)
    {
        if (cards.Count >= MAX_HAND_SIZE)
            throw new Exception("Trying to add cards to a full hand!");

        cards.Add(card);
        RegisterCard(card);
    }

    private void RegisterCard(Card card)
    {
        card.gameObject.GetComponentInChildren<PhysicalCard>().HoverStartEvent += ShowInfoCard;
        card.gameObject.GetComponentInChildren<PhysicalCard>().HoverEndEvent += HideInfoCard;
    }

    private void SetCardPositionsInHand()
    {
        //evenly space out the cards in the area provided. May be overlap/overflow over edge of space
        float slotWidth = _rectTransform.rect.width / cards.Count;

        Vector3 newPosition = new Vector3();
        for(int i = 0; i < cards.Count; i++)
        {
            newPosition.x = (i * slotWidth) + (slotWidth / 2f); 
            cards[i].gameObject.transform.localPosition = newPosition;
            cards[i].gameObject.GetComponentInChildren<PhysicalCard>().SetDockedPosition(newPosition);
        }
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
