using CapsuleSurvival.Core;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class GameOverListener : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;

        private GameContext _gameContext => AppContext.GameContext;
        private GameManager _gameManager => _gameContext.GameManager;

        public void Setup()
        {
            _gameManager.OnGameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            _gameOverPanel.SetActive(true);
        }

        public void Dispose()
        {
            _gameManager.OnGameOver -= OnGameOver;
        }
    }
}