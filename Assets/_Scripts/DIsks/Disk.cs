using UnityEngine;
using UnityEngine.EventSystems;

public class Disk : MonoBehaviour, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begun draggin up in this bitch");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
