using System.Collections.Generic;
using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class PCInputReader : MonoBehaviour, IUserInputReader
    {
        [SerializeField] private List<PCInputBindingData> _inputBindings;

        public bool IsActive { get; private set; }

        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }

        public void Launch()
        {
            IsActive = true;
        }

        public void PerformReadingInputs()
        {
            HorizontalInput = 0f;
            VerticalInput = 0f;

            if (IsActive == false)
                return;

            foreach (PCInputBindingData bindingData in _inputBindings)
            {
                if (Input.GetKey(bindingData.KeyCode) == false)
                    continue;

                HorizontalInput += bindingData.HorizontalValue;
                VerticalInput += bindingData.VerticalValue;
            }
        }

        public void Stop()
        {
            IsActive = false;
        }
    }
}