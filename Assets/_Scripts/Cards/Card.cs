using UnityEngine;


[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards")]
public class Card : ScriptableObject, IDroppable
{
    public string cardName;
    public string cardDescription;

    public CardType Type;

    public virtual void Play()
    {
        //To be overriden by different card types
        Debug.Log($"Applied card: {cardName}");
    }
}
