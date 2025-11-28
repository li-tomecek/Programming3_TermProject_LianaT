using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    [SerializeField] Button _cancelButton, _quitButton;

    void Start()
    {
        _cancelButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void QuitGame() { Application.Quit(); }                              //these are to be assigned in-editor as the popup may not always be to quit game

    public void ReturnToMainMenu() 
    {
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.DefaultState);
        LevelManager.Instance.LoadMainMenu();
        gameObject.SetActive(false);
    }
}
