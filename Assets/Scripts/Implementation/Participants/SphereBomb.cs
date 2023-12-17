using System;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class SphereBomb : GameParticipant
    {
#warning TODO Radius in 'SphereBomb'
        public override float Radius => throw new NotImplementedException();

        public override event Action OnAppeared;
        public override event Action OnDisappeared;

        public override void Appear()
        {
            GameLog.Error("[SphereBomb] 'Appear' not implemented");
            OnAppeared?.Invoke();
        }

        public override void Dissapear()
        {
            GameLog.Error("[SphereBomb] 'Dissapear' not implemented");
            OnDisappeared?.Invoke();
        }

        public override void Launch()
        {
            GameLog.Error("[SphereBomb] 'Launch' not implemented");
        }

        public override void Stop()
        {
            GameLog.Error("[SphereBomb] 'Stop' not implemented");
        }
    }
}