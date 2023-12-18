using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Core
{
    public class ObstacleGenerator : IParticipantGenerator
    {
        private GenerationSettings _genSettings;
        private CoroutineTask _generationTask;
        private Dictionary<GameParticipant, ParticipantAppearingHelper> _appearingHelpers =
            new Dictionary<GameParticipant, ParticipantAppearingHelper>();

        private IGameContext _gameContext;
        private IArena _arena => _gameContext.Arena;
        private ParticipantsRegister _participantsRegister => _gameContext.ParticipantsRegister;

        public void Setup(ParticipantGeneratorParams generatorParams)
        {
            _gameContext = generatorParams.GameContext;
            _genSettings = generatorParams.GenerationSettings;
        }

        public void Launch()
        {
            Stop();

            if (IsGenerationSettingsValid())
                _generationTask = new CoroutineTask(GenerationCoroutine());
        }

        private bool IsGenerationSettingsValid()
        {
            return _genSettings.ParticipantPrefab != null;
        }

        private IEnumerator GenerationCoroutine()
        {
            yield return new WaitForSeconds(_genSettings.StartDelay);

            while (true)
            {
                if (IsAbleToGenerateObstacle())
                {
                    GameObject obstacleGO = GameObject.Instantiate(_genSettings.ParticipantPrefab.gameObject);
                    GameParticipant obstacleView = obstacleGO.GetComponent<GameParticipant>();

                    Vector3 position = _arena.GetFreeRandomPosition(obstacleView);
                    obstacleGO.transform.position = position;

                    _participantsRegister.RegisterParticipant(this, obstacleView);
                    MakeObstacleAppear(obstacleView);
                }

                yield return new WaitForSeconds(_genSettings.GenInterval);
            }
        }

        private void MakeObstacleAppear(GameParticipant obstacleView)
        {
            ParticipantAppearingHelper appearingHelper = new ParticipantAppearingHelper();
            appearingHelper.Setup(obstacleView);
            appearingHelper.OnAppearingCompleted += OnObstacleAppeared;
            _appearingHelpers[obstacleView] = appearingHelper;

            appearingHelper.MakeParticipantAppear();
        }

        private void OnObstacleAppeared(GameParticipant participant)
        {
            ParticipantAppearingHelper appearingHelper = _appearingHelpers[participant];
            appearingHelper.OnAppearingCompleted -= OnObstacleAppeared;
            appearingHelper.Dispose();

            _appearingHelpers.Remove(participant);

            participant.Launch();
        }

        private bool IsAbleToGenerateObstacle()
        {
            return _participantsRegister.GetGeneratorAliveParticipants(this) < _genSettings.MaxGeneratedSimultaneously;
        }

        public void Stop()
        {
            _generationTask?.Stop();
            _generationTask = null;
        }

        public void Dispose()
        {
            Stop();
            DisposeAppearingHelpers();

            _gameContext = null;
            _genSettings = null;
        }

        private void DisposeAppearingHelpers()
        {
            foreach (ParticipantAppearingHelper appearingHelper in _appearingHelpers.Values)
                appearingHelper.Dispose();

            _appearingHelpers.Clear();
        }
    }
}