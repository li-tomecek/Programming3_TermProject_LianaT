using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;

public class StateManager : Singleton<StateManager>
{
    private IState _currentState;

    //States
    private PreparationPhase _prepPhase = new PreparationPhase();
    private ResolutionPhase _resolutionPhase = new ResolutionPhase();

    public IState CurrentState => _currentState;
    public PreparationPhase PreparationPhase => _prepPhase;
    public ResolutionPhase ResolutionPhase => _resolutionPhase;


    void Start()
    {
        ChangeState(_prepPhase);
    }

    void Update()
    {
        _currentState?.Update();
    }

    public void ChangeState(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}
