using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards/RecoilDamage")]
public class RecoilDamageCard : Card
{
    [SerializeField] int _damage;
    
    public override void Play(Disk castingDisk)
    {
        base.Play(castingDisk);
        TargetDisk.GetParticipant().TakeDamage(_damage);
    }
}
