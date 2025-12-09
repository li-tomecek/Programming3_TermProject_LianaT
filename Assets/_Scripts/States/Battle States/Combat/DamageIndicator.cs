using DG.Tweening;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _damageText;
    [SerializeField] private float _activeTime = 2f;
    [SerializeField] private float _heightOffset = 0f;
    [SerializeField] private float _heightTravelled = 1.5f;

    public void ShowIndicatorAtTarget(string str, Transform target, Color color)
    {
        _damageText.SetText(str);
        _damageText.outlineColor = color;
        
        transform.position = target.position;
        
        if(GetComponentInParent<Canvas>().renderMode == RenderMode.WorldSpace) 
            transform.rotation = Quaternion.LookRotation(gameObject.transform.position - Camera.main.transform.position);   //spacial UI needs to face camera
        else
            transform.position = Camera.main.WorldToScreenPoint(transform.position);            //non-spatial UI has to be converted to screen coordinates
        
        transform.Translate(0f, _heightOffset, 0f);

        transform.DOMoveY(transform.position.y + _heightTravelled, _activeTime).OnComplete(() => gameObject.SetActive(false));
    }
}
