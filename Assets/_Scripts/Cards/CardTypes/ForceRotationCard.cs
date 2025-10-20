using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards/ForceRotation")]
public class ForceRotationCard : Card
{
    [SerializeField] SpellPosition _position;
    
    public override void Play(Disk castingDisk)
    {
        base.Play(castingDisk);
        //Will have to figure out how to treat player vs enemy disks. Players rotate the disks before the resolution phase which makes this tricky.
        //Maybe instead of force left/right/no movement, it forces it to a specific spell type.

        TargetDisk.RotateByPosition(_position);
        TargetDisk.LockRotation();

    }
}
