using UnityEngine;
using UnityEngine.EventSystems;

public class SpellComponent : MonoBehaviour, Clickable
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private bool _isInteractable = true;

    public void OnClick()
    {
        Debug.Log($"Clicked on {gameObject.name}");
    }
}


public enum CardPosition
{
    Front,
    Left,
    Right
}
public enum SpellType
{
    Holy,
    Dark,
    Arcane
}