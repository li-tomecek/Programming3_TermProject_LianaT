using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelManager : Singleton<DuelManager>
{

    // ---------------------
    //      Coroutines
    // ---------------------
    public IEnumerator ResolveBothDuels()
    {
        yield return StartCoroutine(ResolveDuel(0));
        yield return StartCoroutine(ResolveDuel(1));
        
        BattleStateManager.Instance.ChangeState(BattleStateManager.Instance.PreparationPhase);
    }

    public IEnumerator ResolveDuel(int diskIndex)
    {
        Disk winner, loser = null;
        bool duelTied = false;

        //1. Compare disks
        //-------------------------------
        winner = GetWinner(Player.Instance.GetDisks()[diskIndex], Opponent.Instance.GetDisks()[diskIndex]);

        if (winner == null)
            duelTied = true;
        else
            loser = (winner == Player.Instance.GetDisks()[diskIndex]) ? Opponent.Instance.GetDisks()[diskIndex] : Player.Instance.GetDisks()[diskIndex];


        //2. Play animation here!
        //-------------------------------
        if(!duelTied)
            yield return StartCoroutine(winner.EnlargeSpellOnWin());      //temp

        
        //3. Apply cards
        //-------------------------------
        if (!duelTied && winner.GetActiveCard()?.Type == CardType.OnWin)    //a. Apply 'OnWin' cards
            yield return winner.PlayCardAnimation();

        if (!duelTied && loser.GetActiveCard()?.Type == CardType.OnLoss)    //b. Apply 'OnLoss' cards
            yield return loser.PlayCardAnimation();


        //4. Deal damage & reset effects
        //-------------------------------
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
