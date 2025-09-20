using UnityEngine;
using UnityEngine.EventSystems;

public class SpellComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClicked()
    {
        Debug.Log($"Clicked on the {gameObject.name}");
    }

}