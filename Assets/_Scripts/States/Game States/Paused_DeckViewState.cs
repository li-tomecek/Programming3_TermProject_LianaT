using UnityEngine;

public class Paused_DeckViewState : IState
{
    public void Enter()
    {
        InputManager.Instance.OnPauseHit.RemoveAllListeners();
        InputManager.Instance.OnPauseHit.AddListener(() => GameStateManager.Instance.ChangeState(GameStateManager.Instance.PausedState));
        
        //Open Deck view Menu here
    }

    public void Exit()
    {
        //close Deck view Menu here
    }

    public void Update()
    {
        //Do nothing
    }
}
