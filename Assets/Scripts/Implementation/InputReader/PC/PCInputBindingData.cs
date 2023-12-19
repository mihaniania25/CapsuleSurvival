using System;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    [Serializable]
    public class PCInputBindingData
    {
        public KeyCode KeyCode;

        [Range(-1f, 1f)]
        public float HorizontalValue = 0f;

        [Range(-1f, 1f)]
        public float VerticalValue = 0f;
    }
}