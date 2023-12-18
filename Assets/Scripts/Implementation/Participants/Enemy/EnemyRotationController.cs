using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class EnemyRotationController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 60.0f;

        private bool _isActivated = false;
        private Vector3 _lastHandledPosition;

        public void Launch()
        {
            _isActivated = true;
            _lastHandledPosition = transform.position;
        }

        private void Update()
        {
            if (_isActivated)
            {
                Vector3 shift = _lastHandledPosition - transform.position;
                if (shift.magnitude > 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(shift, Vector3.up);

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

                    _lastHandledPosition = transform.position;
                }
            }
        }

        public void Stop()
        {
            _isActivated = false;
        }
    }
}