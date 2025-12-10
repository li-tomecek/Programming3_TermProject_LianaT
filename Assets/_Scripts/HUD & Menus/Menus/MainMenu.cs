using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _startButton, _quitButton;
    [SerializeField] ConfirmationPopup _quitConfirmationPopup;
    [SerializeField] SoundEffect _menuMusic;

    void Start()
    {
        _quitConfirmationPopup.gameObject.SetActive(false);
        
        if(_menuMusic)
            AudioManager.Instance?.SetMusic(_menuMusic);

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
