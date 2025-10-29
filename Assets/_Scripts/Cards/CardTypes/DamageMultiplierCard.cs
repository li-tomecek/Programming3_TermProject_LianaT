using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards/DamageMultiplier")]
public class DamageMultiplierCard : Card
{
    [SerializeField] float _multiplier;
    
    public override void Play(Disk castingDisk)
    {
        base.Play(castingDisk);
        TargetDisk.ApplyDamageMultiplier(_multiplier);
    }
}
