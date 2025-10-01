using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [SerializeField] Participant _participant;
    [SerializeField] Slider _slider;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] float _timeToAnimate = 0.6f;

    public void Start()
    {
        _participant.OnHealthChanged += UpdateSlider;
        _nameText.text = _participant.name;
    }

    private void UpdateSlider(int totalHealth)
    {
        //don't really need to pass totalHealth
        //_slider.value = (totalHealth / (float)_participant.GetMaxHealth());
        StartCoroutine(AnimateSlider((totalHealth / (float)_participant.GetMaxHealth())));
    }

    private IEnumerator AnimateSlider(float endValue)
    {
        float valuePerSecond = (_slider.value - endValue) / _timeToAnimate;
        while (_slider.value > endValue)
        {
            yield return 0;
            _slider.value -= Math.Min(valuePerSecond * Time.deltaTime, _slider.value);
        }
    }
}
