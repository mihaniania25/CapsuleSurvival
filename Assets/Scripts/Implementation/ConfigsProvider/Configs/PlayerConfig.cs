using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
    public class PlayerConfig : ScriptableObject, IPlayerConfig
    {
        [SerializeField] private PlayerBase _prefab;
        public PlayerBase Prefab => _prefab;

        [SerializeField] private float _baseSpeed;
        public float BaseSpeed => _baseSpeed;
    }
}