using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _damageText;
    [SerializeField] private float _activeTime = 2f;
    [SerializeField] private float _heightOffset = 1f;
    [SerializeField] private float _heightTravelled = 1.5f;

    public void ShowIndicatorAtTarget(string str, Transform target, Color color)
    {
        _damageText.SetText(str);
        _damageText.outlineColor = color;
        
        transform.position = target.position;
        transform.Translate(0f, _heightOffset, 0f);
        transform.rotation = Quaternion.LookRotation(gameObject.transform.position - Camera.main.transform.position);   

        transform.DOMoveY(transform.position.y + _heightTravelled, _activeTime).OnComplete(() => gameObject.SetActive(false));
    }
}
