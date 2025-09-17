using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public class Hand : MonoBehaviour
{
    private const int MAX_HAND_SIZE = 5;
    private RectTransform _rectTransform;

    [SerializeField] private GameObject _infoCard;          //This card opens will show an enlarged view of the currently-selected card when being hovered
    [SerializeField] private List<Card> physicalCards = new List<Card>();

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _infoCard.SetActive(false);

        foreach (Card card in physicalCards)            //In case we added cards through the editor (for testing)
            RegisterCard(card);
        
        SetCardPositionsInHand();
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
        //DOTween stuff
    }

    private void HideInfoCard()
    {
        //DOTween stuff
        _infoCard.SetActive(false);
    }


}
