using System.Collections.Generic;
using UnityEngine;

public class Player : Participant
{
    private const int MAX_HAND_SIZE = 4;
    public static Player Instance { get; private set; }


    [SerializeField] private List<Card> _deck = new List<Card>();
    [SerializeField] private List<Card> _hand = new List<Card>();
    private List<Card> _discardPile = new List<Card>();

    void Awake()                //Because of inheritance, cannot use the singleton class. Maybe this could be changed later.
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();
        for (int i = 0; (i < MAX_HAND_SIZE - 1 && _deck.Count > 0); i++)
        {
            TryDrawNewCard();
        }
    }

    public void TryDrawNewCard()
    {
        if (_deck.Count == 0)
            ReShuffleDeck();

        if (_hand.Count < MAX_HAND_SIZE)
        {
            int rand = Random.Range(0, _deck.Count);
            _hand.Add(_deck[rand]);
            InterfaceManager.Instance.HandInterface.AddPhysicalCardToHand(_deck[rand]);

            _deck.RemoveAt(rand);
        }
    }

    private void ReShuffleDeck()
    {
        _deck.AddRange(_discardPile);
        _discardPile.Clear();
    }

    public void DiscardCard(Card card, PhysicalCard physCard)
    {
        InterfaceManager.Instance.HandInterface.RemovePhysicalCardFromHand(physCard);
        _hand.Remove(card);
        _discardPile.Add(card);
    }

}
