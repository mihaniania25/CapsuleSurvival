using System;

namespace CapsuleSurvival.Core
{
    public abstract class PlayerBase : GameParticipant, IVulnerable
    {
        public abstract event Action<IVulnerable> OnBeingHitted;

        public bool IsAlive { get; protected set; }

        public abstract void TakeHit(GameParticipant fromParticipant);
        public abstract void ConnectInputReader(IUserInputReader inputReader);
    }
}