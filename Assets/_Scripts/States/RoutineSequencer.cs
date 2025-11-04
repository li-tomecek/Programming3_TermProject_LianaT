using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoutineSequencer : Singleton<RoutineSequencer>
{
    private Queue<IEnumerator> _routineQueue = new Queue<IEnumerator>();
    public Queue<IEnumerator> RoutineQueue => _routineQueue;

    private bool _routineActive;
    public Coroutine ActiveRoutine;
    
    //-------------------
    //  Play the queue
    //-------------------
    void Update()
    {
        if (!_routineActive && _routineQueue.Count > 0)
        {
            ActiveRoutine = StartCoroutine(PlayNext());
        }
            
    }

    public IEnumerator PlayNext()
    {
        _routineActive = true;
        yield return StartCoroutine(_routineQueue.Dequeue());
        ActiveRoutine = null;
        _routineActive = false;
    }
    
    
    //-------------------
    //   Add to Queue
    //-------------------
    public void AddRoutine(IEnumerator routine)
    {
        _routineQueue.Enqueue(routine);
    }

    public void AddSimultaneous(params IEnumerator[] routines)
    {
        _routineQueue.Enqueue(SimultaneousRoutines(routines));
    }
    
    private IEnumerator SimultaneousRoutines(params IEnumerator[] routines)
    {
        List<Coroutine> startedRoutines = new List<Coroutine>();     //references the routines

        foreach (var routine in routines)
            startedRoutines.Add(StartCoroutine(routine));            //starts all routines

        foreach (var routine in startedRoutines)
            yield return routine;                                   //waits for each routine
    }  
}
