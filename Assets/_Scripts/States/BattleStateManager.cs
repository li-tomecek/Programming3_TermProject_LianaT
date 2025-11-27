using System.Collections;
using System.Linq;
using UnityEngine;

public class BattleStateManager : Singleton<BattleStateManager>
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
        StartCoroutine(Setup());    //need to add a delay to make sure everyone's start functions have happened
    }

    private IEnumerator Setup()
    {
        PreparationPhase.Exit();    //set buttons and disks to inactive temporarily

        //Rotate each disk randomly to start
        int index;
        foreach (Disk disk in Player.Instance.GetDisks().Concat(Opponent.Instance.GetDisks()))
        {
            index = Random.Range(0, disk.GetSpellList().Length);
            disk.SetActiveSpell(disk.GetSpellList()[index]);            //choose a random spell to rotate to
        }

        RoutineSequencer.Instance.AddSimultaneous(
            Opponent.Instance.RotateDisksToSelected(),
            Player.Instance.RotateDisksToSelected());
        
        yield return RoutineSequencer.Instance.ActiveRoutine;
        ChangeState(_prepPhase);
    }

    // ----------------------------------------------------
    
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
