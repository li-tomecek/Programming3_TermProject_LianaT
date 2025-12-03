using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckMenuView : MonoBehaviour
{
    [SerializeField] GameObject _menuCanvas;

    [SerializeField] Transform _deckHolder;
    [SerializeField] Transform _discardHolder;
    [SerializeField] DeckMenuView_Card _cardViewPrefab;

    [SerializeField] TMP_Text _deckTotal, _discardTotal;

    void Start()
    {
        _menuCanvas.SetActive(false);   
    }


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

        int deckCount = 0;
        int discardCount = 0;
        //Create new cards
        foreach (var card in deck.Keys)
        {
            var cardView = Instantiate(_cardViewPrefab, _deckHolder);
            cardView.SetCard(card, deck[card]);
            deckCount += deck[card];
        }

        foreach (var card in discardPile.Keys)
        {
            var cardView = Instantiate(_cardViewPrefab, _deckHolder);
            cardView.SetCard(card, discardPile[card]);
            discardCount += discardPile[card];
        }

        //Set deck count text
        _deckTotal.text = $"Total: {deckCount}";   
        _discardTotal.text = $"Total: {discardCount}";   
    }

    public void SetMenuVisibility(bool isActive)
    {
        _menuCanvas.SetActive(isActive);
    }

    public void ExitButton()
    {
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.PausedState);
    }
}
