using System.Collections.Generic;
using System.Linq;

public class PreparationPhase : IState
{

    public void Enter()
    {
        //1. Draw card (if applicable)
        Player.Instance.TryDrawNewCard();

        //2. Set player disks as interactable
        foreach (Disk playerDisk in Player.Instance.GetDisks())
        {
            playerDisk.SetInteractable(true);
            playerDisk.GetSpellAtFront().SetInteractable(false);
        }
    }

    public void Update()
    {
        // Wait for player to hit "ready" after rotating disks and applying up to one card per disk
    }
    
    public void Exit()
    {
        //Set all disks as non-interactable
        foreach (Disk playerDisk in Player.Instance.GetDisks())
        {
            playerDisk.SetInteractable(false);
            playerDisk.GetSpellAtFront().SetInteractable(true);
        }
    }
}
