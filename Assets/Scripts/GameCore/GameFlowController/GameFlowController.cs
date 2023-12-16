using System;

namespace CapsuleSurvival.Core
{
    public class GameFlowController
    {
        public event Action OnGameOver;

        private ParticipantsBehaviourController _participantsBehaviorController = new ParticipantsBehaviourController();
        private GameSpawner _spawner = new GameSpawner();
        private GameTimer _timer = new GameTimer();

        private IConfigsProvider _configsProvider;
        private GameSettings _gameSettings;

        private IGameContext _gameContext;
        private IGameEffectsController _effectsController => _gameContext.EffectsController;

        public void Setup(GameManagerParams gameManagerParams)
        {
            _gameContext = gameManagerParams.GameContext;
            _configsProvider = gameManagerParams.ConfigsProvider;
            _gameSettings = gameManagerParams.GameSettings;

            _spawner.Setup(_gameContext, _configsProvider);
            _participantsBehaviorController.Setup(_gameContext);
            _timer.Setup(_gameContext);

            _participantsBehaviorController.OnPlayerDying += OnPlayerDying;
            _participantsBehaviorController.OnParticipantsCleaned += OnArenaCleanedAfterGame;
        }

        public void Launch()
        {
            _participantsBehaviorController.Launch();

            _spawner.OnPlayerAppearingCompleted += OnPlayerAppearingCompleted;
            _spawner.SpawnPlayer();
        }

        private void OnPlayerAppearingCompleted()
        {
            _spawner.OnPlayerAppearingCompleted -= OnPlayerAppearingCompleted;

            _timer.Launch();
            _spawner.LaunchGameSpawning(_gameSettings.GenerationTypes);
        }

        private void OnPlayerDying()
        {
            _participantsBehaviorController.Stop();
            _spawner.StopSpawning();
            _effectsController.EnableGameOverEffect();
            _timer.Stop();
        }

        private void OnArenaCleanedAfterGame()
        {
            _effectsController.DisableGameOverEffect();
            OnGameOver?.Invoke();
        }

        public void Dispose()
        {
            _spawner.StopSpawning();

            _participantsBehaviorController.OnPlayerDying -= OnPlayerDying;
            _participantsBehaviorController.OnParticipantsCleaned -= OnArenaCleanedAfterGame;
            _spawner.OnPlayerAppearingCompleted -= OnPlayerAppearingCompleted;

            _participantsBehaviorController.Dispose();
            _spawner.Dispose();
        }
    }
}