using CapsuleSurvival.Core;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class Launcher : MonoBehaviour
    {
        private GameContext _gameContext => AppContext.GameContext;
        private GameManager _gameManager => _gameContext.GameManager;
        private UnityConfigsProvider _configsProvider => AppContext.ConfigsProvider;

        [SerializeField] private PlaneArena _arena;
        [SerializeField] private InputReaderGetter _inputReaderGetter;
        [SerializeField] private GameSettings _settings;

        [SerializeField] private Curtain _sceneCurtain;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameOverListener _gameOverListener;

        private void Awake()
        {
            SetupGame();

            _gameOverListener.Setup();

            _sceneCurtain.OnOpeningCompleted += OnSceneOpened;
            _sceneCurtain.Open();
        }

        private void SetupGame()
        {
            _gameContext.RegisterArena(_arena);

            IUserInputReader inputsReader = _inputReaderGetter.GetUserInputReader();
            _gameContext.RegisterUserInputReader(inputsReader);

            _gameManager.Setup(new GameManagerParams
            {
                ConfigsProvider = _configsProvider,
                GameContext = _gameContext,
                GameSettings = _settings
            });
        }

        private void OnSceneOpened()
        {
            _sceneCurtain.OnOpeningCompleted -= OnSceneOpened;
            _startPanel.SetActive(true);
        }

        private void OnApplicationQuit()
        {
            _gameManager.Dispose();
            _gameOverListener.Dispose();

            _sceneCurtain.OnOpeningCompleted -= OnSceneOpened;
        }
    }
}