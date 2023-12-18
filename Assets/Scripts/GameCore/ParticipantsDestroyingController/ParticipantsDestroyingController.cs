using System;

namespace CapsuleSurvival.Core
{
    public class ParticipantsDestroyingController
    {
        public event Action OnPlayerPreDestroy;
        public event Action OnPlayerDestroyed;

        private PlayerDestroyingController _playerDestroyingController = new PlayerDestroyingController();
        private VulnerablesDestroyingController _vulnerablesDestroyingController = new VulnerablesDestroyingController();

        public void Setup(IGameContext gameContext)
        {
            _playerDestroyingController.Setup(gameContext);
            _vulnerablesDestroyingController.Setup(gameContext);

            _playerDestroyingController.OnPlayerPreDestroy += OnPlayerDestroyingStarted;
            _playerDestroyingController.OnPlayerDestroyed += OnPlayerDestroyingCompleted;
        }

        private void OnPlayerDestroyingStarted()
        {
            OnPlayerPreDestroy?.Invoke();
        }

        private void OnPlayerDestroyingCompleted()
        {
            OnPlayerDestroyed?.Invoke();
        }

        public void Launch()
        {
            _playerDestroyingController.Launch();
            _vulnerablesDestroyingController.Launch();
        }

        public void Stop()
        {
            _playerDestroyingController.Stop();
            _vulnerablesDestroyingController.Stop();
        }

        public void Dispose()
        {
            _playerDestroyingController.OnPlayerPreDestroy -= OnPlayerDestroyingStarted;
            _playerDestroyingController.OnPlayerDestroyed -= OnPlayerDestroyingCompleted;

            _playerDestroyingController.Dispose();
            _vulnerablesDestroyingController.Dispose();
        }
    }
}