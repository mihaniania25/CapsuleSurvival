using System;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class CapsulePlayer : PlayerBase
    {
        public override event Action OnAppeared;
        public override event Action OnDisappeared;
        public override event Action<IVulnerable> OnBeingHitted;

#warning TODO implement PlayerBase Radius
        public override float Radius => 0f;

        private IUserInputReader _inputReader;

        public override void ConnectInputReader(IUserInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public override void Launch()
        {
            GameLog.Error("[CapsulePlayer] 'Launch' not implemented");
        }

        public override void Stop()
        {
            GameLog.Error("[CapsulePlayer] 'Stop' not implemented");
        }

        public override void TakeHit(GameParticipant fromParticipant)
        {
            GameLog.Error("[CapsulePlayer] 'TakeHit' not implemented");
            OnBeingHitted?.Invoke(this);
        }

        public override void Appear()
        {
            GameLog.Error("[CapsulePlayer] 'Appear' not implemented");
            OnAppeared?.Invoke();
        }

        public override void Dissapear()
        {
            GameLog.Error("[CapsulePlayer] 'Dissapear' not implemented");
            OnDisappeared?.Invoke();
        }
    }
}