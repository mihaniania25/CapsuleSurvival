using CapsuleSurvival.Utility;
using System;
using UnityEngine;

namespace CapsuleSurvival.Core
{
    public class PlayerDestroyingController
    {
        public event Action OnPlayerPreDestroy;
        public event Action OnPlayerDestroyed;

        private IGameContext _gameContext;
        private PropagationField<PlayerBase> _playerHolder => _gameContext.Player;
        private PlayerBase _player => _playerHolder.Value;

        public void Setup(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Launch()
        {
            _playerHolder.Subscribe(OnPlayerRegister);
        }

        private void OnPlayerRegister(PlayerBase player)
        {
            if (player != null)
            {
                _playerHolder.Unsubscribe(OnPlayerRegister);
                player.OnBeingHitted += OnPlayerHitted;
            }
        }

        private void OnPlayerHitted(IVulnerable playerVulnerable)
        {
            OnPlayerPreDestroy?.Invoke();

            _player.Stop();

            _player.OnBeingHitted -= OnPlayerHitted;
            _player.OnDisappeared += OnPlayerDisappeared;
            _player.Dissapear();
        }

        private void OnPlayerDisappeared()
        {
            _player.OnDisappeared -= OnPlayerDisappeared;

            _gameContext.UnregisterPlayer();
            GameObject.Destroy(_player.gameObject);

            OnPlayerDestroyed?.Invoke();
        }

        public void Stop()
        {
            _playerHolder.Unsubscribe(OnPlayerRegister);

            if (_player != null)
                _player.OnBeingHitted -= OnPlayerHitted;
        }

        public void Dispose()
        {
            Stop();

            if (_player != null)
                _player.OnDisappeared -= OnPlayerDisappeared;

            _gameContext = null;
        }
    }
}