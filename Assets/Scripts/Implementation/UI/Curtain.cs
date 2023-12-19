using System;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class Curtain : MonoBehaviour
    {
        private const string OPEN_ANIM_PARAM = "Open";

        public event Action OnOpeningCompleted;

        [SerializeField] private Animator _animator;

        public void Open()
        {
            _animator.SetTrigger(OPEN_ANIM_PARAM);
        }

        public void OnOpenAnimationCompleted()
        {
            OnOpeningCompleted?.Invoke();
        }
    }
}