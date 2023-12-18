using System;

namespace CapsuleSurvival.Core
{
    public class ParticipantsBehaviourController
    {
        public event Action OnPlayerDying;
        public event Action OnParticipantsCleaned;

        private ParticipantsDestroyingController _participantsDestroyingController = new ParticipantsDestroyingController();
        private ParticipantsCleaner _participantsCleaner = new ParticipantsCleaner();

        private IGameContext _gameContext;
        private ParticipantsRegister _participantsRegister => _gameContext.ParticipantsRegister;

        public void Setup(IGameContext gameContext)
        {
            _gameContext = gameContext;
            _participantsDestroyingController.Setup(gameContext);
            _participantsCleaner.Setup(gameContext);

            _participantsDestroyingController.OnPlayerPreDestroy += OnPlayerPreDestroy;
            _participantsDestroyingController.OnPlayerDestroyed += OnPlayerDestroyingCompleted;
            _participantsCleaner.OnCleaningCompleted += OnParticipantsCleaningCompleted;
        }

        private void OnPlayerPreDestroy()
        {
            _participantsRegister.AllParticipants.ForEach(p => p.Stop());
            OnPlayerDying?.Invoke();
        }

        private void OnPlayerDestroyingCompleted()
        {
            _participantsCleaner.CleanUp();
        }

        private void OnParticipantsCleaningCompleted()
        {
            OnParticipantsCleaned?.Invoke();
        }

        public void Launch()
        {
            _participantsDestroyingController.Launch();
        }

        public void Stop()
        {
            _participantsDestroyingController.Stop();
        }

        public void Dispose()
        {
            _participantsDestroyingController.OnPlayerPreDestroy -= OnPlayerPreDestroy;
            _participantsDestroyingController.OnPlayerDestroyed -= OnPlayerDestroyingCompleted;
            _participantsCleaner.OnCleaningCompleted -= OnParticipantsCleaningCompleted;

            _participantsDestroyingController.Dispose();
            _participantsCleaner.Dispose();

            _gameContext = null;
        }
    }
}