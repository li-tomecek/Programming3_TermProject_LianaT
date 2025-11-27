using System.Collections;
using System.Linq;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    private IState _currentState;

    //States
    private PausedState _pausedState = new PausedState();
    private Paused_DeckViewState _deckViewState = new Paused_DeckViewState();
    private DefaultState _defaultState = new DefaultState();

    public IState CurrentState => _currentState;
    public PausedState PausedState => _pausedState;
    public Paused_DeckViewState DeckViewState => _deckViewState;
    public DefaultState DefaultState => _defaultState;

    private void Start()
    {
        ChangeState(_defaultState);
    }

    public void ChangeState(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}
