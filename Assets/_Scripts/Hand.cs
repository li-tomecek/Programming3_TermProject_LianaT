using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public class Hand : MonoBehaviour
{
    private const int MAX_HAND_SIZE = 4;
    private RectTransform _rectTransform;

    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private List<GameObject> physicalCards = new List<GameObject>();

    public int handSize = 2;    //FOR TESTING!


    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        //FOR TESTING
        for (int i = 0; i < handSize; i++)
        {
            physicalCards.Add(Instantiate(_cardPrefab, gameObject.transform));
        }

        SetCardPositionsInHand();
    }


    private void SetCardPositionsInHand()
    {
        float slotWidth = _rectTransform.rect.width / physicalCards.Count;

        Vector3 newPosition = new Vector3();
        for(int i = 0; i < physicalCards.Count; i++)
        {
            newPosition.x = (i * slotWidth) + (slotWidth / 2f);
            physicalCards[i].transform.localPosition = newPosition; 
        }
    }
}
