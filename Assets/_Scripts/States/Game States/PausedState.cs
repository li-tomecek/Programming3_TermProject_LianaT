using UnityEngine;

public class PausedState : IState
{
    public void Enter()
    {
        InputManager.Instance.OnPauseHit.RemoveAllListeners();
        InputManager.Instance.OnPauseHit.AddListener(() => GameStateManager.Instance.ChangeState(GameStateManager.Instance.DefaultState));
        
        PauseMenu.Instance.PauseGame();
    }

    public void Exit()
    {
        PauseMenu.Instance.UnpauseGame();
    }

    public void Update()
    {
        //do nothing
    }
}
