using UnityEngine;

namespace CapsuleSurvival.Impl
{
    [CreateAssetMenu(fileName = "ArenaParticipantsSettings", menuName = "Config/ArenaParticipantsSettings")]
    public class ArenaParticipantsSettings : ScriptableObject
    {
        [Min(0f)]
        [SerializeField] private float _playerBaseSpeed;

        [Min(0f)]
        [SerializeField] private float _enemyBaseSpeed;

        public float PlayerBaseSpeed => _playerBaseSpeed;
        public float EnemyBaseSpeed => _enemyBaseSpeed;
    }
}