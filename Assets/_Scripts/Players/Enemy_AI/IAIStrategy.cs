using UnityEngine;

public interface IAIStrategy
{
    SpellComponent ChooseSpell(SpellComponent disabledSpell, SpellComponent opposingDisabled);
    //if opponent uses cards, include a "choose card" function here.
}
