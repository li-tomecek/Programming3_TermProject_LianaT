using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    private IState _currentState;

    //States
    private PreparationPhase _prepPhase = new PreparationPhase();
    private ResolutionPhase _resolutionPhase = new ResolutionPhase();

    public PreparationPhase PreparationPhase => _prepPhase;
    public ResolutionPhase ResolutionPhase => _resolutionPhase;


    void Start()
    {
        ChangeState(_prepPhase);
    }

    void ChangeState(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}
