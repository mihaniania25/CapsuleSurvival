using CapsuleSurvival.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CapsuleSurvival.Impl
{
    public class StartPanel : MonoBehaviour
    {
        private string HIDE_ANIMPARAM = "Hide";

        [SerializeField] private Button _startButton;
        [SerializeField] private Animator _animator;

        private GameContext _gameContext => AppContext.GameContext;
        private GameManager _gameManager => _gameContext.GameManager;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
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

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClick);
        }
    }
}