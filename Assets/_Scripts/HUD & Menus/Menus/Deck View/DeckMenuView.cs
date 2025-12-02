using System.Collections.Generic;
using UnityEngine;

public class DeckMenuView : MonoBehaviour
{
    [SerializeField] GameObject _menuPanel;

    [SerializeField] Transform _deckHolder;
    [SerializeField] Transform _discardHolder;
    [SerializeField] DeckMenuView_Card _cardViewPrefab;


    public void RefreshView(Dictionary<Card, int> deck, Dictionary<Card, int> discardPile)
    {
        // Destroy all current cards
        foreach(Transform child in _deckHolder)
        {
            Destroy(child.gameObject);
        }
        foreach(Transform child in _discardHolder)
        {
            Destroy(child.gameObject);
        }

        //Create new cards
        foreach (var card in deck.Keys)
        {
            var cardView = Instantiate(_cardViewPrefab, _deckHolder);
            cardView.SetCard(card, deck[card]);
        }

        foreach (var card in discardPile.Keys)
        {
            var cardView = Instantiate(_cardViewPrefab, _deckHolder);
            cardView.SetCard(card, discardPile[card]);
        }
    }

    public void SetMenuVisibility(bool isActive)
    {
        _menuPanel.SetActive(isActive);
    }
}
