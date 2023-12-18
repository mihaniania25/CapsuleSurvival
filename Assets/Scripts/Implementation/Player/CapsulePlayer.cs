using System;
using UnityEngine;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class CapsulePlayer : PlayerBase
    {
        public override event Action OnAppeared;
        public override event Action OnDisappeared;
        public override event Action<IVulnerable> OnBeingHitted;

        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private PlayerMovementController _movementController;

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
        }

        public override void Stop()
        {
            _movementController.IsMovementEnabled = false;
        }

        public override void TakeHit(GameParticipant fromParticipant)
        {
            GameLog.Error("[CapsulePlayer] 'TakeHit' not implemented");
            OnBeingHitted?.Invoke(this);
        }

        public override void Appear()
        {
            GameLog.Error("[CapsulePlayer] 'Appear' not implemented");
            OnAppeared?.Invoke();
        }

        public override void Dissapear()
        {
            GameLog.Error("[CapsulePlayer] 'Dissapear' not implemented");
            OnDisappeared?.Invoke();
        }
    }
}