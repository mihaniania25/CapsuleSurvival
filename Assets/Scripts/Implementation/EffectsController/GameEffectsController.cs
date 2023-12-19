using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class GameEffectsController : MonoBehaviour, IGameEffectsController
    {
        private const string EARTHQUAKE_ANIMPARAM = "Shaking";

        [SerializeField] private Animator _cameraAnimator;

        public void EnableGameOverEffect()
        {
            _cameraAnimator.SetBool(EARTHQUAKE_ANIMPARAM, true);
        }

        public void DisableGameOverEffect()
        {
            _cameraAnimator.SetBool(EARTHQUAKE_ANIMPARAM, false);
        }
    }
}