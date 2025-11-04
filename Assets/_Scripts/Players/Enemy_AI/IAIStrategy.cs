using UnityEngine;

public interface IAIStrategy
{
    SpellComponent ChooseSpell(Disk disk, SpellComponent opposingDisabled);
    //if opponent uses cards, include a "choose card" function here.
}
