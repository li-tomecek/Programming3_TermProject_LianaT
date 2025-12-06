using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _startButton, _quitButton;
    [SerializeField] ConfirmationPopup _quitConfirmationPopup;

    void Start()
    {
        _quitConfirmationPopup.gameObject.SetActive(false);

        _startButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.LoadGameScene();
        });

        _quitButton.onClick.AddListener(() =>
        {
            _quitConfirmationPopup.OpenPopup();
        });   
    } 
}
