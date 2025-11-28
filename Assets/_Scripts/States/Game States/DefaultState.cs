using UnityEngine;

public class DefaultState : IState
{
    public void Enter()
    {
        Debug.Log("Pause button pressed");


        InputManager.Instance.OnPauseHit.RemoveAllListeners();
        InputManager.Instance.OnPauseHit.AddListener(() => GameStateManager.Instance.ChangeState(GameStateManager.Instance.PausedState));

        Debug.Log("Pause Setup");
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
