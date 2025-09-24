public class PreparationPhase : IState
{
    public void Enter()
    {
        //1. Draw card (if applicable)

        //2. Enable click controls?
    }

    public void Update()
    {
        // Wait for player to hit "ready" after rotating disks and applying up to one card per disk
    }
    
    public void Exit()
    {
        //Disable click controls?
    }
}
