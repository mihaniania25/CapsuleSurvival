using System;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    [Serializable]
    public class PCInputBindingData
    {
        public KeyCode KeyCode;

        [Range(0f, 1f)]
        public float HorizontalValue;

        [Range(0f, 1f)]
        public float VerticalValue;
    }
}