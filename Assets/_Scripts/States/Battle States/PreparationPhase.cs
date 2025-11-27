using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreparationPhase : IState
{
    private bool _readyToStartTurn;

    public void Enter()
    {
        //0. Reset ready tag
        _readyToStartTurn = false;

        //1. Draw card (if applicable)
        Player.Instance.TryDrawNewCard();

        //2. Set player disks as interactable
        foreach (Disk playerDisk in Player.Instance.GetDisks())
        {
            playerDisk.SetTargetable(true);
            playerDisk.GetActiveSpell().SetInteractable(false);
        }
    }

    public void Update()
    {
        //Wait for player to hit "ready" after rotating disks and applying up to one card per disk
        //Note: may be better to use events rather than check every frame.
        if (_readyToStartTurn)
            return;

        if (Player.Instance.GetDisks()[0].GetActiveSpell().IsInteractable() &&
            Player.Instance.GetDisks()[1].GetActiveSpell().IsInteractable())
        {
            _readyToStartTurn = true;
            InterfaceManager.Instance.ReadyButton.gameObject.SetActive(true);
        }
    }

    public void Exit()
    {
        InterfaceManager.Instance.ReadyButton.gameObject.SetActive(false);

        //Set all disks as non-interactable
        foreach (Disk playerDisk in Player.Instance.GetDisks())
        {
            playerDisk.SetTargetable(false);
            playerDisk.ResetSpellInteractability();
        }
    }
}
