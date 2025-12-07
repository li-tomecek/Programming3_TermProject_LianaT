using TMPro;
using UnityEngine;

public class DeckMenuView_Card : MonoBehaviour
{
    [SerializeField] TMP_Text _cardCount;

    public void SetCard(Card card, int count)
    {
        gameObject.GetComponentInChildren<CardView>().SetAssociatedCard(card);
        _cardCount.text = $"x{count}";
    }
}
