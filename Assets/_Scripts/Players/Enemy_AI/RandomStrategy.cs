
public class RandomStrategy : IAIStrategy
{
    public SpellComponent ChooseSpell(Disk disk, SpellComponent opposingDisabled)
    {
        SpellComponent chosenSpell;
        SpellComponent[] spells = disk.GetSpellList();
            
        do
        {
            chosenSpell = spells[UnityEngine.Random.Range(0, spells.Length)];

        } while (chosenSpell == disk.GetActiveSpell());

        return chosenSpell;
    }
}
