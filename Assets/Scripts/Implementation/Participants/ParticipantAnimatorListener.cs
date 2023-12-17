using System;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class ParticipantAnimatorListener : MonoBehaviour
    {
        public event Action OnAnimationCompleted;

        public void TriggerAnimCompleted()
        {
            OnAnimationCompleted?.Invoke();
        }
    }
}