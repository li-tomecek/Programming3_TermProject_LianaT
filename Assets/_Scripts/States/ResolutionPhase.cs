using System.Collections;
using System.Collections.Generic;

public class ResolutionPhase : IState
{
    public void Enter()
    {
        //1. Apply any "OnStart" cards
        foreach (Disk playerDisk in Player.Instance.GetDisks())
        {
            if (playerDisk.GetActiveCard()?.Type == CardType.OnStart)
                playerDisk.PlayCard();
        }

        //2. Rotate Disks
        RoutineSequencer.Instance.AddSimultaneous(
            Opponent.Instance.ChooseRotations(),
            Player.Instance.RotateDisksToSelected());
    
        //3. Resolve each duel separately
        RoutineSequencer.Instance.AddRoutine(DuelManager.Instance.ResolveBothDuels());
    }
    
    public void Update()
    {
        //nothing
    }
    
    public void Exit()
    {
        foreach (Disk disk in Player.Instance.GetDisks())
            disk.ResetState();        
        
        foreach (Disk disk in Opponent.Instance.GetDisks())
            disk.ResetState();
    }
}
