using UnityEngine;

public class Paused_DeckViewState : IState
{
    public void Enter()
    {
        InputManager.Instance.OnPauseHit.RemoveAllListeners();
        InputManager.Instance.OnPauseHit.AddListener(() => GameStateManager.Instance.ChangeState(GameStateManager.Instance.PausedState));
        
        Time.timeScale = 0;
        DeckMenuController.Instance.OpenDeckMenu();
    }

    public void Exit()
    {
        Time.timeScale = 1;
        DeckMenuController.Instance.CloseDeckMenu();
    }

    public void Update()
    {
        //Do nothing
    }
}
