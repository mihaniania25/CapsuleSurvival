using System;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class SphereBomb : GameParticipant, IVulnerable
    {
        public override event Action OnAppeared;
        public override event Action OnDisappeared;
        public event Action<IVulnerable> OnBeingHitted;

        [SerializeField] private SphereCollider _collider;

        private bool _isActive = false;

        public override float Radius { get; protected set; }

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
            _isActive = true;
        }

        public override void Stop()
        {
            _isActive = false;
        }

        public void TakeHit(GameParticipant fromParticipant)
        {
            IVulnerable vulnerableDamageDealer = fromParticipant as IVulnerable;

            OnBeingHitted(this);
            vulnerableDamageDealer?.TakeHit(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isActive == false)
                return;

            IVulnerable vulnerable = collision.gameObject.GetComponent(typeof(IVulnerable)) as IVulnerable;

            if (vulnerable != null)
            {
                OnBeingHitted?.Invoke(this);
                vulnerable.TakeHit(this);
            }
        }
    }
}