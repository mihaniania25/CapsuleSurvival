using UnityEngine;
using UnityEngine.UI;
using CapsuleSurvival.Core;
using TMPro;

namespace CapsuleSurvival.Impl
{
    public class GameOverPanel : MonoBehaviour
    {
        private string HIDE_ANIMPARAM = "Hide";

        [SerializeField] private Animator _animator;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private TextMeshProUGUI _elapsedTimeLabel;

        private GameContext _gameContext => AppContext.GameContext;
        private GameManager _gameManager => _gameContext.GameManager;
        private GameSessionModel _gameSessionModel => _gameContext.SessionModel;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _quitButton.onClick.AddListener(QuitGame);
        }

        private void OnEnable()
        {
            _elapsedTimeLabel.text = string.Format("{0:0.00} sec", _gameSessionModel.PlayingTimeElapsed.Value);
        }

        private void OnStartButtonClick()
        {
            _gameManager.Launch();
            _animator.SetTrigger(HIDE_ANIMPARAM);
        }

        public void OnPanelHiddingCompleted()
        {
            gameObject.SetActive(false);
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _quitButton.onClick.RemoveListener(QuitGame);
        }
    }
}