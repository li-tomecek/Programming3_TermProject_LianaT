using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Singleton<PauseMenu>
{
    [SerializeField] Button _resumeButton, _viewDeckButton, _mainMenuButton;
    [SerializeField] GameObject _pauseMenuObject;
    [SerializeField] ConfirmationPopup _mainMenuConfirmationPopup;
    
    [Header("Scroll Animation")]
    [SerializeField] RectTransform _scrollRect;
    [SerializeField] GameObject _menuContent;
    [SerializeField] float _minAnchorClosed = 0.741f;
    [SerializeField] float _timeToOpen = 0.7f;
    Vector2 _minAnchorOpen;

    void Start()
    {
        //Deactivate game objects
        _pauseMenuObject.SetActive(false);                      //just in case they aren't already deactivated
        _mainMenuConfirmationPopup.gameObject.SetActive(false);

        //Set anchors for scroll open/closing
        _minAnchorOpen = _scrollRect.anchorMin;                                         //get the anchor position in the open position
        _scrollRect.anchorMin = new Vector2(_scrollRect.anchorMin.x,_minAnchorClosed);  //close the scroll

        //Assign button listeners
        _mainMenuButton.onClick.AddListener(() =>
        {
            _mainMenuConfirmationPopup.OpenPopup();
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

        OpenScroll();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        CloseScroll();
    }

    private void OpenScroll()
    {  
        _menuContent.SetActive(false);
        _scrollRect.DOAnchorMin(_minAnchorOpen, _timeToOpen)
            .SetUpdate(true)
            .OnComplete(() => _menuContent.SetActive(true));
    }

    private void CloseScroll()
    {
        _menuContent.SetActive(false);
        Vector2 closedAnchor = new Vector2(_scrollRect.anchorMin.x, _minAnchorClosed);
        _scrollRect.DOAnchorMin(closedAnchor, _timeToOpen)
            .SetUpdate(true)
            .OnComplete(() => _pauseMenuObject.SetActive(false));
    }
}

