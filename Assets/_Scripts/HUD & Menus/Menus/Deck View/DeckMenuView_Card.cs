using TMPro;
using UnityEngine;

public class DeckMenuView_Card : MonoBehaviour
{
    PhysicalCard _physCard;
    [SerializeField] TMP_Text _cardCount;

    public void SetCard(Card card, int count)
    {
        _physCard.SetAssociatedCard(card);
        _cardCount.text = $"x{count}";
    }
}
