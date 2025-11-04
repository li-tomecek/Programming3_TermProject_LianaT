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
        StartCoroutine(AnimateSlider(((float) _participant.GetCurrentHealth() / (float)_participant.GetMaxHealth())));
    }

    private IEnumerator AnimateSlider(float endValue)
    {
        float difference = (endValue - _slider.value);
        float valuePerSecond = difference / _timeToAnimate;

        while (Mathf.Abs(difference) > 0.01f)
        {
            yield return 0;
            _slider.value += valuePerSecond * Time.deltaTime;
            difference = (endValue - _slider.value);
        }

        Debug.Log(_slider.value);
    }
}
