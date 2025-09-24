using System.Collections.Generic;
using UnityEngine;

public class Player : Participant
{
    private const int MAX_HAND_SIZE = 3;

    [SerializeField] private List<Card> _deck = new List<Card>();
    [SerializeField] private List<Card> _hand = new List<Card>();
    private List<Card> _discardPile = new List<Card>();

    [SerializeField] private HandInterface _handInterface;

    void Start()
    {
        for (int i = 0; (i < MAX_HAND_SIZE && _deck.Count > 0); i++)
        {
            DrawNewCard();
        }
    }

    public void DrawNewCard()
    {
        if (_deck.Count == 0)
            ShuffleDeck();

        if (_hand.Count < MAX_HAND_SIZE)
        {
            int rand = Random.Range(0, _deck.Count);
            _hand.Add(_deck[rand]);
            _handInterface.AddPhysicalCardToHand(_deck[rand]);

            _deck.RemoveAt(rand);
        }
    }

    private void ShuffleDeck()
    {
        _deck = _discardPile;
        _discardPile.Clear();
    }

}
