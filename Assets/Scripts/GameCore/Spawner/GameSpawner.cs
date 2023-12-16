using System;
using System.Collections.Generic;
using UnityEngine;

namespace CapsuleSurvival.Core
{
    public class GameSpawner
    {
        public event Action OnPlayerAppearingCompleted;

        private ParticipantGeneratorGetter _generatorGetter = new ParticipantGeneratorGetter();
        private List<IParticipantGenerator> _generators = new List<IParticipantGenerator>();

        private IGameContext _gameContext;
        private IArena _arena => _gameContext.Arena;
        private PlayerBase _player => _gameContext.Player;

        private IConfigsProvider _configsProvider;
        private IPlayerConfig _playerConfig => _configsProvider.PlayerConfig;
        private IGeneratorConfig _generatorConfig => _configsProvider.GeneratorConfig;

        public void Setup(IGameContext gameContext, IConfigsProvider configsProvider)
        {
            _gameContext = gameContext;
            _configsProvider = configsProvider;
        }

        public void SpawnPlayer()
        {
            GameObject playerGO = GameObject.Instantiate(_playerConfig.Prefab.gameObject);
            Vector3 spawnPosition = _arena.GetPlayerSpawnPosition();
            playerGO.transform.position = spawnPosition;

            PlayerBase player = playerGO.GetComponent<PlayerBase>();
            _gameContext.RegisterPlayer(player);

            player.OnAppeared += OnPlayerAppeared;
            player.Appear();
        }

        private void OnPlayerAppeared()
        {
            OnPlayerAppearingCompleted?.Invoke();

            _player.OnAppeared -= OnPlayerAppeared;
            _player.Launch();
        }

        public void LaunchGameSpawning(List<GenerationType> generationTypes)
        {
            foreach (GenerationType generationType in generationTypes)
            {
                IParticipantGenerator generator = _generatorGetter.GetParticipantGenerator(generationType);

                generator.Setup(new ParticipantGeneratorParams
                {
                    GameContext = _gameContext,
                    GenerationSettings = _generatorConfig.GetSettings(generationType)
                });

                generator.Launch();
                _generators.Add(generator);
            }
        }

        public void StopSpawning()
        {
            _generators.ForEach(g => g.Stop());
        }

        public void Dispose()
        {
            StopSpawning();

            _generators.ForEach(g => g.Dispose());
            _generators.Clear();

            if (_player != null)
            {
                _player.OnAppeared -= OnPlayerAppeared;
                _gameContext.UnregisterPlayer();
            }
        }
    }
}