using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards/ForceRotation")]
public class ForceRotationCard : Card
{
    [SerializeField] SpellPosition _position;
    
    public override void Play(Disk castingDisk)
    {
        base.Play(castingDisk);

        TargetDisk.RotateByPosition(_position);
        TargetDisk.LockRotation();

    }
}
