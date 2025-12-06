using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MenuButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    private Vector3 _defaultScale;
    [SerializeField] float _scaleAmount = 1.25f;
    [SerializeField] float _scaleTime = 0.5f;
    Tween tween;

    void Start()
    {
        _button = gameObject.GetComponent<Button>();
        _defaultScale = transform.localScale;

        _button.onClick.AddListener(() => 
        { 
            _button.transform.localScale = _defaultScale;
        });  
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_button.interactable == false) return; 
        tween = _button.transform.DOScale(_defaultScale * _scaleAmount, _scaleTime).SetUpdate(true);        //SetUpdate(true) should allow the animations to happen even if the timescale is set to 0
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tween = _button.transform.DOScale(_defaultScale, _scaleTime).SetUpdate(true);
    }

    public void OnDisable()
    {
        tween.Kill();
    }



}
