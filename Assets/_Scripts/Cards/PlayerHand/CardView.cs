using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

public class CardView : MonoBehaviour
{
    [SerializeField] protected TMP_Text _nameText;
    [SerializeField] protected TMP_Text _descriptionText;
    protected Card _associatedCard;


    //-----------------------------------------------------
    //-----------------------------------------------------

    public void SetDisplayInformation(Card card)
    {
        _nameText.text = card.cardName;
        _descriptionText.text = card.cardDescription;
    }

    // -----------------
    // Get/Set
    // -----------------
    public Card AssociatedCard => _associatedCard;

    public void SetAssociatedCard(Card card)
    {
        _associatedCard = card;
        SetDisplayInformation(card);
    }
}
