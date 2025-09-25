using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelManager : Singleton<DuelManager>
{

    List<Coroutine> _routines = new List<Coroutine>();

    public void ResolveTurn()
    {
        StartCoroutine(RotateDisksAndCheckDuels());
    }

    // ---------------------
    //      Coroutines
    // ---------------------
    IEnumerator RotateDisksAndCheckDuels()
    {
        //Rotate opponent's disks (random for now)
        yield return Opponent.Instance.ChooseRotations();   //wait for opponents disks to rotate

        _routines.Add(StartCoroutine(ResolveDuel(0)));
        _routines.Add(StartCoroutine(ResolveDuel(1)));

        for (int i = 0; i < _routines.Count; i++)
        {
            yield return _routines[i];
        }
        _routines.Clear();

        StateManager.Instance.ChangeState(StateManager.Instance.PreparationPhase);
    }

    IEnumerator ResolveDuel(int diskIndex)
    {
        Disk winner, loser;
        //Compare disks
        //TODO
        winner = Player.Instance.GetDisks()[diskIndex];     //TEMP
        loser = Opponent.Instance.GetDisks()[diskIndex];

        //Play any animation here!
        yield return new WaitForSeconds(0.5f);      //temp

        //Apply 'OnWin' cards
        if (winner.GetActiveCard()?.Type == CardType.OnWin)
            winner.GetActiveCard().Play();

        //Apply 'OnLoss' cards
        if (loser.GetActiveCard()?.Type == CardType.OnLoss)
            loser.GetActiveCard().Play(); 

        //Deal damage
    }
}
