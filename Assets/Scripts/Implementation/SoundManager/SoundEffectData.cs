using System;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    [Serializable]
    public class SoundEffectData
    {
        public AudioClip AudioClip;

        [Range(0f, 1f)]
        public float Volume = 1f;
    }
}