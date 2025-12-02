using UnityEngine;

public class DeckMenuController : Singleton<DeckMenuController>
{
    [SerializeField] DeckMenuView _deckView;

    public bool IsValid()
    {
        return (_deckView != null);
    }
    
    public void OpenDeckMenu()
    {
        (var deck, var discardPile) = Player.Instance.GetCardsAsDictionaries();
        _deckView.RefreshView(deck, discardPile);
        _deckView.SetMenuVisibility(true);
    }
    
    public void CloseDeckMenu()
    {
        _deckView.SetMenuVisibility(false);
    }
}
