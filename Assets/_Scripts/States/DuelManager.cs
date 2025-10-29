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
        //Rotate opponent's disks
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
        Disk winner, loser = null;
        bool duelTied = false;

        //Compare disks
        winner = GetWinner(Player.Instance.GetDisks()[diskIndex], Opponent.Instance.GetDisks()[diskIndex]);

        if (winner == null)
            duelTied = true;
        else
            loser = (winner == Player.Instance.GetDisks()[diskIndex]) ? Opponent.Instance.GetDisks()[diskIndex] : Player.Instance.GetDisks()[diskIndex];

        //Play any animation here!
        yield return new WaitForSeconds(0.5f);      //temp

        //Apply 'OnWin' cards
        if (!duelTied && winner.GetActiveCard()?.Type == CardType.OnWin)
            winner.PlayCard();

        //Apply 'OnLoss' cards
        if (!duelTied && loser.GetActiveCard()?.Type == CardType.OnLoss)
            loser.PlayCard();

        //Deal damage & reset effects
        if (!duelTied)
        {
            loser.GetParticipant().TakeDamage((int)(winner.GetParticipant().GetDamage() * winner.GetDamageMultiplier()));
        }
    }

    private Disk GetWinner(Disk first, Disk second)
    {
        if (first.GetActiveSpell().SpellType == second.GetActiveSpell().SpellType)
            return null;        //tied

        switch (first.GetActiveSpell().SpellType)
        {
            case SpellType.Holy:
                if (second.GetActiveSpell().SpellType == SpellType.Dark)
                    return first;
                break;

            case SpellType.Dark:
                if (second.GetActiveSpell().SpellType == SpellType.Arcane)
                    return first;
                break;

            case SpellType.Arcane:
                if (second.GetActiveSpell().SpellType == SpellType.Holy)
                    return first;
                break;
        }

        return second;        //second disk won the matchup
    }
}
