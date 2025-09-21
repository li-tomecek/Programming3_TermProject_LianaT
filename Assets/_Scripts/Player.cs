using System.Collections.Generic;
using UnityEngine;

public class Player : Participant
{
    [SerializeField] private List<Card> _deck = new List<Card>();
    [SerializeField] private List<Card> _hand = new List<Card>();
    [SerializeField] private HandInterface _handInterface;


    void Start()
    {
        foreach (Card card in _hand)
        {
            _handInterface.AddPhysicalCardToHand(card);
        }

        _handInterface.SetCardPositionsInHand();
    }

}
