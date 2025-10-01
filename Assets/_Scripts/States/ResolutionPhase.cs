public class ResolutionPhase : IState
{
    public void Enter()
    {
        //Apply any "OnStart" cards
        foreach (Disk playerDisk in Player.Instance.GetDisks())
        {
            if (playerDisk.GetActiveCard()?.Type == CardType.OnStart)
                playerDisk.GetActiveCard().Play();
        }

        DuelManager.Instance.ResolveTurn();
    }
    
    public void Update()
    {
        //nothing
    }
    
    public void Exit()
    {
        //Cleanup if needed
    }
}
