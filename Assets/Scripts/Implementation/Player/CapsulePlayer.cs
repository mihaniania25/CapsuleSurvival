using System;
using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class CapsulePlayer : PlayerBase
    {
        private const string APPEAR_ANIMPARAM = "Appear";
        private const string DISAPPEAR_ANIMPARAM = "Disappear";

        public override event Action OnAppeared;
        public override event Action OnDisappeared;
        public override event Action<IVulnerable> OnBeingHitted;

        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private PlayerMovementController _movementController;
        [SerializeField] private ParticipantAnimatorListener _animatorListener;
        [SerializeField] private Animator _animator;

        private bool _isActive = false;

        public override float Radius { get; protected set; }

        private IUserInputReader _inputReader;

        public override void Setup()
        {
            float horizontalScale = Mathf.Max(_collider.transform.lossyScale.x, _collider.transform.lossyScale.z);
            Radius = _collider.radius * horizontalScale;
        }

        public override void ConnectInputReader(IUserInputReader inputReader)
        {
            _inputReader = inputReader;
            _movementController.Setup(_inputReader);
        }

        public override void Launch()
        {
            _movementController.IsMovementEnabled = true;
            _isActive = true;
        }

        public override void Stop()
        {
            _movementController.IsMovementEnabled = false;
            _isActive = false;
        }

        public override void TakeHit(GameParticipant fromParticipant)
        {
            if (_isActive)
                OnBeingHitted?.Invoke(this);
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
            _animatorListener.OnAnimationCompleted += OnDisappearingCompleted;
            _animator.SetTrigger(DISAPPEAR_ANIMPARAM);
        }

        private void OnDisappearingCompleted()
        {
            _animatorListener.OnAnimationCompleted -= OnDisappearingCompleted;
            OnDisappeared?.Invoke();
        }

        private void OnDestroy()
        {
            _animatorListener.OnAnimationCompleted -= OnDisappearingCompleted;
            _animatorListener.OnAnimationCompleted -= OnAppearingCompleted;
        }
    }
}