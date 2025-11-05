using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards/ForceSpellType")]
public class ForceSpellType : Card
{
    [SerializeField] SpellType _type;
    
    public override void Play(Disk castingDisk)
    {
        base.Play(castingDisk);

        TargetDisk.RotateByType(_type);
        TargetDisk.LockRotation();
    }
}
