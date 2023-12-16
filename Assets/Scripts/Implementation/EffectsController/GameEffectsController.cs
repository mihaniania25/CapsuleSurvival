using UnityEngine;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class GameEffectsController : MonoBehaviour, IGameEffectsController
    {
        public void EnableGameOverEffect()
        {
            GameLog.Error("[GameEffectsController] 'EnableGameOverEffect' not implemented");
        }

        public void DisableGameOverEffect()
        {
            GameLog.Error("[GameEffectsController] 'DisableGameOverEffect' not implemented");
        }
    }
}