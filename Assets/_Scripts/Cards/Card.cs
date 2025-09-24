using UnityEngine;


[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards")]
public class Card : ScriptableObject, IDroppable
{
    public string cardName;
    public string cardDescription;

    public CardType _type;

    public virtual void Play(Disk _targetDisk)
    {
        //To be overriden by different card types
        Debug.Log($"Applied card effect to {_targetDisk}");
    }
}
