using System;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class CubeEnemy : GameParticipant
    {
        public override event Action OnAppeared;
        public override event Action OnDisappeared;

        [SerializeField] private BoxCollider _collider;

        public override float Radius { get; protected set; }

        public override void Setup()
        {
            float xDimSize = _collider.transform.lossyScale.x * _collider.size.x;
            float zDimSize = _collider.transform.lossyScale.z * _collider.size.z;

            Radius = Mathf.Sqrt(xDimSize * xDimSize + zDimSize * zDimSize) / 2.0f;
        }

        public override void Appear()
        {
            GameLog.Error("[CubeEnemy] 'Appear' not implemented");
            OnAppeared?.Invoke();
        }

        public override void Dissapear()
        {
            GameLog.Error("[CubeEnemy] 'Dissapear' not implemented");
            OnDisappeared?.Invoke();
        }

        public override void Launch()
        {
            GameLog.Error("[CubeEnemy] 'Launch' not implemented");
        }

        public override void Stop()
        {
            GameLog.Error("[CubeEnemy] 'Stop' not implemented");
        }
    }
}