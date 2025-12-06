using TMPro;
using UnityEngine;
using DG.Tweening;

public class ResultsScreen : MonoBehaviour
{
    [SerializeField] GameObject _buttonPanel;

    [Header("Game Over")]
    [SerializeField] TextMeshProUGUI _gameOverText;
    [SerializeField] float _gameOverFadeInDuration = 1f;
    [SerializeField] float _gameOverExpandMultiplier = 1.25f;


    [Header("Game Won")]
    [SerializeField] TextMeshProUGUI _gameWonText;
    [SerializeField] float _gameWonExpandDuration = 1f;
    [SerializeField] float _gameWonExpandMultiplier = 1.5f;

    void Start()
    {
        _gameOverText.alpha = 0;
        _gameWonText.alpha = 0;
        _buttonPanel.SetActive(false);
        gameObject.SetActive(false); 
    }

    public void PlayResultsSequence(bool gameWon)
    {
        Tween tween;
        if (gameWon)
        {
            //expand vistory text
            _gameWonText.alpha = 1;
            tween = _gameWonText.transform.DOScale(_gameWonExpandMultiplier, _gameWonExpandDuration);
        } else
        {
            //fade-in loss text
            tween = _gameOverText.DOFade(1f, _gameOverFadeInDuration);
            _gameOverText.transform.DOScale(_gameOverExpandMultiplier, _gameOverFadeInDuration).SetEase(Ease.OutQuad);
        }

        tween.OnComplete(() => _buttonPanel.SetActive(true));
    }

    public void PlayAgain() {LevelManager.Instance.LoadGameScene();}
    public void MainMenu() {LevelManager.Instance.LoadMainMenu();}

}
