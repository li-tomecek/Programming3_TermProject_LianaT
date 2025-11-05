using System;

public class AggressiveStrategy : IAIStrategy
{
    public SpellComponent ChooseSpell(Disk disk, SpellComponent opposingDisabled)
    {
        /**
            Try to use the spell type that would lose to or match the opponents disabled spell (both of these possibly end in a win)
            If both are available, choose randomly between them (we play aggressively, not conservatively, otherwise we'd pick the one that would lose to current)
        **/

        SpellType losingType = SpellComponent.GetLosingType(opposingDisabled.SpellType);
        SpellType chosenType;

        if (disk.GetActiveSpell().SpellType == opposingDisabled.SpellType)  //chose losing type (50/50 W/T)
            chosenType = losingType;

        else if (disk.GetActiveSpell().SpellType == losingType)             //chose losing type (50/50 W/L)
            chosenType = opposingDisabled.SpellType;

        else                                                                //chose random of available (50/25/25 W/T/L)
            chosenType = UnityEngine.Random.Range(0, 2) == 0 ? losingType : opposingDisabled.SpellType;
        
        
        return Array.Find(disk.GetSpellList(), spell => spell.SpellType == chosenType);
    }
}
