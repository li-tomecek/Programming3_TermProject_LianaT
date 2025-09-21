using UnityEngine;


[CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards")]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;

    public CardType _type;
}
