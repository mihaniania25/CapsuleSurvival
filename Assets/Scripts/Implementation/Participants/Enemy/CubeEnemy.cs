using System;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class CubeEnemy : GameParticipant, IVulnerable
    {
        public override event Action OnAppeared;
        public override event Action OnDisappeared;
        public event Action<IVulnerable> OnBeingHitted;

        [SerializeField] private BoxCollider _collider;
        [SerializeField] private EnemyMovementController _movementController;
        [SerializeField] private EnemyRotationController _rotationController;

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
    }
}