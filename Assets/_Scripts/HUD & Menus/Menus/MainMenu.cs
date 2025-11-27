using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _startButton, _quitButton;
    [SerializeField] GameObject _quitConfirmationPopup;

    void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.LoadGameScene();
        });

        _quitButton.onClick.AddListener(() =>
        {
            _quitConfirmationPopup.SetActive(true);
        });   
    } 
}
