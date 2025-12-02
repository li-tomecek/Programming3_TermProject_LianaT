using UnityEngine;

public class DefaultState : IState
{
    public void Enter()
    {
        InputManager.Instance.OnPauseHit.RemoveAllListeners();
        InputManager.Instance.OnPauseHit.AddListener(() => GameStateManager.Instance.ChangeState(GameStateManager.Instance.PausedState));
    }

    public void Exit()
    {
        //do nothing
    }

    public void Update()
    {
        //do nothing
    }
}
