using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public class Hand : MonoBehaviour
{
    private RectTransform _rectTransform;

    private const int MAX_HAND_SIZE = 5;
    [SerializeField] private List<Card> physicalCards = new List<Card>();

    [SerializeField] private GameObject _infoCard;
    private Sequence infoCardSeq;
    
    //-----------------------------------------------------
    //-----------------------------------------------------

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        //Cards
        foreach (Card card in physicalCards)            //In case we added cards through the editor (for testing)
            RegisterCard(card);
        
        SetCardPositionsInHand();

        //InfoCard
        _infoCard.SetActive(false);

        infoCardSeq = DOTween.Sequence();
        infoCardSeq.Append(_infoCard.transform.DOMoveY(100f, 0.5f));    //magic numbers to fix
        infoCardSeq.Join(_infoCard.transform.DOScale(1.75f, 0.5f));
        infoCardSeq.SetAutoKill(false);
    }


    //-------------------------------
    //        Managing Hand
    //-------------------------------
    private void AddCardToHand(Card card)
    {
        
        physicalCards.Add(card);
        RegisterCard(card);
    }

    private void RegisterCard(Card card)
    {
        card.gameObject.GetComponentInChildren<PhysicalCard>().HoverStartEvent += ShowInfoCard;
        card.gameObject.GetComponentInChildren<PhysicalCard>().HoverEndEvent += HideInfoCard;
    }

    private void SetCardPositionsInHand()
    {
        float slotWidth = _rectTransform.rect.width / physicalCards.Count;

        Vector3 newPosition = new Vector3();
        for(int i = 0; i < physicalCards.Count; i++)
        {
            newPosition.x = (i * slotWidth) + (slotWidth / 2f);
            physicalCards[i].gameObject.transform.localPosition = newPosition; 
        }
    }


    //-------------------------------
    //  Managing Enlarged Info Card
    //-------------------------------

    private void ShowInfoCard(Card card)
    {
        _infoCard.SetActive(true);
        _infoCard.GetComponentInChildren<PhysicalCard>().SetDisplayInformation(card);

        infoCardSeq.Restart();
    }

    private void HideInfoCard()
    {
        _infoCard.SetActive(false);
    }


}
