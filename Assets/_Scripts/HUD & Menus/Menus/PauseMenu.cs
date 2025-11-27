using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button _resumeButton, _viewDeckButton, _mainMenuButton;
    [SerializeField] GameObject _mainMenuConfirmationPopup;

    void Start()
    {
        _mainMenuButton.onClick.AddListener(() =>
        {
            _mainMenuConfirmationPopup.SetActive(true);
        });
    }
}

