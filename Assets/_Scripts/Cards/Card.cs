using UnityEngine;

[RequireComponent(typeof(PhysicalCard))]
public class Card : MonoBehaviour
{
    // I think I should make this into a scriptable object and make sub-classes like my skills
    public string cardName;
    public string cardDescription;
}
