using UnityEngine;


[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards")]
public class Card : ScriptableObject, IDroppable
{
    public string cardName;
    public string cardDescription;

    public TargetType _targetType;
    public CardType Type;

    protected Disk TargetDisk;

    public virtual void Play(Disk castingDisk)
    {
        TargetDisk = _targetType == TargetType.Opponent ? castingDisk.GetOpposingDisk() : castingDisk;
        
        //To be overriden by different card types
        Debug.Log($"Applied card: {cardName}");
    }

}
