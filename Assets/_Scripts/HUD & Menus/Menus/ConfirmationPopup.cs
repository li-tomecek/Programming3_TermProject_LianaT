using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    [SerializeField] Button _cancelButton, _quitButton;
    
    [Header("Scroll Animation")]
    [SerializeField] RectTransform _scrollRect;
    [SerializeField] GameObject _content;
    [SerializeField] float _minAnchorClosed;// = new Vector2();
    [SerializeField] float _maxAnchorClosed;// = new Vector2();
    [SerializeField] float _timeToOpen = 0.7f;
    Vector2 _minAnchorOpen, _maxAnchorOpen;

    void Awake()
    {
        _minAnchorOpen = _scrollRect.anchorMin;     //get the open position anchors
        _maxAnchorOpen = _scrollRect.anchorMax;

        _cancelButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }

    public void QuitGame() { Application.Quit(); }                              //these are to be assigned in-editor as the popup may not always be to quit game

    public void ReturnToMainMenu() 
    {
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.DefaultState);
        LevelManager.Instance.LoadMainMenu();
        gameObject.SetActive(false);
    }

    public void OpenPopup()
    {
        _scrollRect.anchorMin = new Vector2(_minAnchorClosed, _scrollRect.anchorMin.y);   //close the scroll
        _scrollRect.anchorMax = new Vector2(_maxAnchorClosed, _scrollRect.anchorMax.y); 

        gameObject.SetActive(true);
        _content.SetActive(false);

         _scrollRect.DOAnchorMax(_maxAnchorOpen, _timeToOpen)
             .SetUpdate(true);                                    //setUpdate(true) allows the animation to be independant of unity's timescale
        _scrollRect.DOAnchorMin(_minAnchorOpen, _timeToOpen)
            .SetUpdate(true)                                   
            .OnComplete(() => _content.SetActive(true)); 
    }
}
