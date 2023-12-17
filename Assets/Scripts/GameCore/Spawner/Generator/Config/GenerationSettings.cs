using System;
using UnityEngine;

namespace CapsuleSurvival.Core
{
    [Serializable]
    public class GenerationSettings
    {
        public GenerationType GenType;
        public GameParticipant ParticipantPrefab;

        [Min(0f)]
        public float StartDelay;

        [Min(0.000001f)]
        public float GenInterval;

        [Min(1)]
        public int MaxGeneratedSimultaneously = 9999;
    }
}