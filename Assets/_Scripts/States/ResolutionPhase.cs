using UnityEngine;

public class ResolutionPhase : IState
{
    public void Enter()
    {
        //1. Rotate opponent's disks (random for now)

        //2. Apply any "OnStart" cards

        //3. Resolve Combat

        //4. Apply any "OnWin/OnLoss" cards
    }
    
    public void Update()
    {
        //nothing
    }
    
    public void Exit()
    {
        // Cleanup if needed
    }
}
