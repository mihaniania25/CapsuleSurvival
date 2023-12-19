using CapsuleSurvival.Core;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class EnemyMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isMovementEnabled;

        private UnityConfigsProvider _configsProvider => AppContext.ConfigsProvider;
        private ArenaParticipantsSettings _arenaParticipantsSettings => _configsProvider.ArenaParticipantsSettings;
        private float _baseSpeed => _arenaParticipantsSettings.EnemyBaseSpeed;

        private GameContext _gameContext => AppContext.GameContext;
        private PlayerBase _player => _gameContext.Player.Value;

        public void Launch()
        {
            _isMovementEnabled = true;
        }

        public void Update()
        {
            _rigidbody.velocity = Vector3.zero;

            if (_isMovementEnabled && _player != null && _player.IsAlive)
            {
                Vector3 direction = (_player.transform.position - transform.position).normalized;
                transform.position += direction * _baseSpeed * Time.deltaTime;
            }
        }

        public void Stop()
        {
            _isMovementEnabled = false;
        }
    }
}