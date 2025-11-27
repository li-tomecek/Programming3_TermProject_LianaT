using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Singleton<PauseMenu>
{
    [SerializeField] Button _resumeButton, _viewDeckButton, _mainMenuButton;
    [SerializeField] GameObject _mainMenuConfirmationPopup;

    private bool _isPaused;

    void Start()
    {
        _mainMenuButton.onClick.AddListener(() =>
        {
            _mainMenuConfirmationPopup.SetActive(true);
        });

        _resumeButton.onClick.AddListener(UnpauseGame);
    }



    public void OpenDeckView()
    {
        //Open deck view menu here
    }

    public void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0;

    }

    public void UnpauseGame()
    {
        _isPaused = false;
        Time.timeScale = 1;
    }

}

