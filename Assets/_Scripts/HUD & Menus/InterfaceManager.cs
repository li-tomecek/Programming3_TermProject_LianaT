using UnityEngine;
using UnityEngine.UI;
/*
    Handles logic related to smaller interface elements (rather than larger systems such as the PhysicalHand, which is managed directly through the player)
    For example:
     - 'Ready' button
     - Card popups (TODO)
     - Damage indicators(TODO)

*/
public class InterfaceManager : Singleton<InterfaceManager>
{
    [SerializeField] HandInterface _handInterface;
    [SerializeField] Button _readyButton;

    public HandInterface HandInterface => _handInterface;
    public Button ReadyButton => _readyButton;

    //-----------------------------
    //       Ready Button       
    //-----------------------------
    public void ReadyButtonPressed()
    {
        BattleStateManager.Instance.ChangeState(BattleStateManager.Instance.ResolutionPhase);
    }
}
