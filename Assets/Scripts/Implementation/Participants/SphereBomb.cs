using System;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class SphereBomb : GameParticipant
    {
        [SerializeField] private SphereCollider _collider;

        public override float Radius { get; protected set; }

        public override event Action OnAppeared;
        public override event Action OnDisappeared;

        public override void Setup()
        {
            float horizontalScale = Mathf.Max(_collider.transform.lossyScale.x, _collider.transform.lossyScale.z);
            Radius = _collider.radius * horizontalScale;
        }

        public override void Appear()
        {
            GameLog.Error("[SphereBomb] 'Appear' not implemented");
            OnAppeared?.Invoke();
        }

        public override void Dissapear()
        {
            GameLog.Error("[SphereBomb] 'Dissapear' not implemented");
            OnDisappeared?.Invoke();
        }

        public override void Launch()
        {
            GameLog.Error("[SphereBomb] 'Launch' not implemented");
        }

        public override void Stop()
        {
            GameLog.Error("[SphereBomb] 'Stop' not implemented");
        }
    }
}