using System;

public class DefensiveStrategy : IAIStrategy
{
    public SpellComponent ChooseSpell(Disk disk, SpellComponent opposingDisabled)
    {
        /**
            Try to use the spell type that would lose to the opponents diabled spell (cannot lose HP this way).
            If not available, use the spell type that matches the current type (equal chance of losing, but would win instead of draw)
        **/

        SpellType losingType = SpellComponent.GetLosingType(opposingDisabled.SpellType);
        
        if(disk.GetActiveSpell().SpellType != losingType)
        {
            return Array.Find(disk.GetSpellList(), spell => spell.SpellType == losingType);
        } else
        {
            return Array.Find(disk.GetSpellList(), spell => spell.SpellType == opposingDisabled.SpellType);
        }
    }
}
