using UnityEngine;
using UnityEngine.UI;
/*
    Handles logic related to smaller interface elements (rather than larger systems such as the PhysicalHand, which is managed directly through the player)
    For example:
     - 'Ready' button
     - Results Screen


*/
public class InterfaceManager : Singleton<InterfaceManager>
{
    [SerializeField] HandInterface _handInterface;
    [SerializeField] Button _readyButton;
    [SerializeField] ResultsScreen _resultsScreen;

    public HandInterface HandInterface => _handInterface;
    public Button ReadyButton => _readyButton;

    //-----------------------------
    //       Ready Button       
    //-----------------------------
    public void ReadyButtonPressed()
    {
        BattleStateManager.Instance.ChangeState(BattleStateManager.Instance.ResolutionPhase);
    }

    public void ShowResultsScreen(bool gameWon)
    {
        _resultsScreen.gameObject.SetActive(true);
        _resultsScreen.PlayResultsSequence(gameWon);
    }
}
