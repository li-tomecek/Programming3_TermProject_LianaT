using System.Collections.Generic;
using UnityEngine;

public class Player : Participant
{
    [SerializeField] private List<Card> _deck = new List<Card>();
    [SerializeField] private Hand _hand;
}
