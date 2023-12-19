using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class TouchesInputReader : MonoBehaviour, IUserInputReader
    {
        [SerializeField] private Joystick _joystick;

        public bool IsActive { get; private set; }

        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }

        public void Launch()
        {
            IsActive = true;
            _joystick.gameObject.SetActive(true);
        }

        public void PerformReadingInputs()
        {
            HorizontalInput = 0f;
            VerticalInput = 0f;

            if (IsActive)
            {
                HorizontalInput = _joystick.Horizontal;
                VerticalInput = _joystick.Vertical;
            }
        }

        public void Stop()
        {
            IsActive = false;
            _joystick.gameObject.SetActive(false);
        }
    }
}