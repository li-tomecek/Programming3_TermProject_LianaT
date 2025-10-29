public class ResolutionPhase : IState
{
    public void Enter()
    {
        //Apply any "OnStart" cards
        foreach (Disk playerDisk in Player.Instance.GetDisks())
        {
            if (playerDisk.GetActiveCard()?.Type == CardType.OnStart)
                playerDisk.PlayCard();
        }

        DuelManager.Instance.ResolveTurn();
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
