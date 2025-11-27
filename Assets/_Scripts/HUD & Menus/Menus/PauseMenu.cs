using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Singleton<PauseMenu>
{
    [SerializeField] Button _resumeButton, _viewDeckButton, _mainMenuButton;
    [SerializeField] GameObject _pauseMenuObject, _mainMenuConfirmationPopup;

    private bool _isPaused;

    void Start()
    {
        _pauseMenuObject.SetActive(false);              //just in case they aren't already deactivated
        _mainMenuConfirmationPopup.SetActive(false);

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
        _pauseMenuObject.SetActive(true);
    }

    public void UnpauseGame()
    {
        _isPaused = false;
        Time.timeScale = 1;
        _pauseMenuObject.SetActive(false);

    }

    public void TogglePause()
    {
        if (_isPaused)
            UnpauseGame();
        else
            PauseGame();
    }

}

