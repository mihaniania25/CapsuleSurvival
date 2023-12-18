using System;
using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class SphereBomb : GameParticipant, IVulnerable
    {
        private const string APPEAR_ANIMPARAM = "Appear";
        private const string DISAPPEAR_ANIMPARAM = "Disappear";

        public override event Action OnAppeared;
        public override event Action OnDisappeared;
        public event Action<IVulnerable> OnBeingHitted;

        [SerializeField] private SphereCollider _collider;
        [SerializeField] private ParticipantAnimatorListener _animatorListener;
        [SerializeField] private Animator _animator;

        private bool _isActive = false;

        public override float Radius { get; protected set; }

        public override void Setup()
        {
            float horizontalScale = Mathf.Max(_collider.transform.lossyScale.x, _collider.transform.lossyScale.z);
            Radius = _collider.radius * horizontalScale;
        }

        public override void Appear()
        {
            _animatorListener.OnAnimationCompleted += OnAppearingCompleted;
            _animator.SetTrigger(APPEAR_ANIMPARAM);
        }

        private void OnAppearingCompleted()
        {
            _animatorListener.OnAnimationCompleted -= OnAppearingCompleted;
            OnAppeared?.Invoke();
        }

        public override void Dissapear()
        {
            Destroy(_collider);
            _animatorListener.OnAnimationCompleted += OnDisappearingCompleted;
            _animator.SetTrigger(DISAPPEAR_ANIMPARAM);
        }

        private void OnDisappearingCompleted()
        {
            _animatorListener.OnAnimationCompleted -= OnDisappearingCompleted;
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
            if (_isActive == false)
                return;

            if (fromParticipant is SphereBomb)
                return;

            IVulnerable vulnerableDamageDealer = fromParticipant as IVulnerable;

            OnBeingHitted?.Invoke(this);
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

        private void OnDestroy()
        {
            _animatorListener.OnAnimationCompleted -= OnAppearingCompleted;
            _animatorListener.OnAnimationCompleted -= OnDisappearingCompleted;
        }
    }
}