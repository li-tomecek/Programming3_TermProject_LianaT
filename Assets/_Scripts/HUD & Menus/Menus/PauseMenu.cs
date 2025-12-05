using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Singleton<PauseMenu>
{
    [SerializeField] Button _resumeButton, _viewDeckButton, _mainMenuButton;
    [SerializeField] GameObject _pauseMenuObject, _mainMenuConfirmationPopup;

    void Start()
    {
        _pauseMenuObject.SetActive(false);              //just in case they aren't already deactivated
        _mainMenuConfirmationPopup.SetActive(false);

        _mainMenuButton.onClick.AddListener(() =>
        {
            _mainMenuConfirmationPopup.SetActive(true);
        });

        _resumeButton.onClick.AddListener(() => GameStateManager.Instance.ChangeState(GameStateManager.Instance.DefaultState));
        _viewDeckButton.onClick.AddListener(OpenDeckView);
    }



    public void OpenDeckView()
    {
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.DeckViewState);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _pauseMenuObject.SetActive(true);
        
        if (DeckMenuController.Instance != null && DeckMenuController.Instance.IsValid())
            _viewDeckButton.interactable = true;
        else
            _viewDeckButton.interactable = false;

    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        _pauseMenuObject.SetActive(false);

    }

}

