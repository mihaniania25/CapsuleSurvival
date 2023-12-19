using System;
using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class CubeEnemy : GameParticipant, IVulnerable
    {
        private const string APPEAR_ANIMPARAM = "Appear";
        private const string DISAPPEAR_ANIMPARAM = "Disappear";

        public override event Action OnAppeared;
        public override event Action OnDisappeared;
        public event Action<IVulnerable> OnBeingHitted;

        [SerializeField] private BoxCollider _collider;
        [SerializeField] private EnemyMovementController _movementController;
        [SerializeField] private EnemyRotationController _rotationController;

        [SerializeField] private ParticipantAnimatorListener _animatorListener;
        [SerializeField] private Animator _animator;

        private bool _isActive = false;

        public override float Radius { get; protected set; }

        public override void Setup()
        {
            float xDimSize = _collider.transform.lossyScale.x * _collider.size.x;
            float zDimSize = _collider.transform.lossyScale.z * _collider.size.z;

            Radius = Mathf.Sqrt(xDimSize * xDimSize + zDimSize * zDimSize) / 2.0f;
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
            _movementController.Launch();
            _rotationController.Launch();
            _isActive = true;
        }

        public override void Stop()
        {
            _movementController.Stop();
            _rotationController.Stop();
            _isActive = false;
        }

        public void TakeHit(GameParticipant fromParticipant)
        {
            if (_isActive)
                OnBeingHitted?.Invoke(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isActive == false)
                return;

            if (collision.gameObject.GetComponent<CubeEnemy>() != null)
                return;

            IVulnerable vulnerable = collision.gameObject.GetComponent(typeof(IVulnerable)) as IVulnerable;
            vulnerable?.TakeHit(this);
        }

        private void OnDestroy()
        {
            _animatorListener.OnAnimationCompleted -= OnAppearingCompleted;
            _animatorListener.OnAnimationCompleted -= OnDisappearingCompleted;
        }
    }
}