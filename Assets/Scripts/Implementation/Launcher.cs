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
        [SerializeField] private GameEffectsController _effectsController;

        [SerializeField] private Curtain _sceneCurtain;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameOverListener _gameOverListener;

        [SerializeField] private GameSettings _settings;

        [Min(30)]
        [SerializeField] private int _targetFrameRate = 120;

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;

            SetupGame();

            _gameOverListener.Setup();

            _sceneCurtain.OnOpeningCompleted += OnSceneOpened;
            _sceneCurtain.Open();
        }

        private void SetupGame()
        {
            _arena.Setup();
            _gameContext.RegisterArena(_arena);

            IUserInputReader inputsReader = _inputReaderGetter.GetUserInputReader();
            _gameContext.RegisterUserInputReader(inputsReader);

            _gameContext.RegisterEffectsController(_effectsController);

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