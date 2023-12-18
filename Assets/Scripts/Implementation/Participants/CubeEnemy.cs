using System;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class CubeEnemy : GameParticipant
    {
        public override event Action OnAppeared;
        public override event Action OnDisappeared;

#warning TODO Radius in 'CubeEnemy'
        public override float Radius => 0.05f;

        public override void Appear()
        {
            GameLog.Error("[CubeEnemy] 'Appear' not implemented");
            OnAppeared?.Invoke();
        }

        public override void Dissapear()
        {
            GameLog.Error("[CubeEnemy] 'Dissapear' not implemented");
            OnDisappeared?.Invoke();
        }

        public override void Launch()
        {
            GameLog.Error("[CubeEnemy] 'Launch' not implemented");
        }

        public override void Stop()
        {
            GameLog.Error("[CubeEnemy] 'Stop' not implemented");
        }
    }
}