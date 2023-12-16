using System;

namespace CapsuleSurvival.Core
{
    public interface IVulnerable
    {
        event Action<IVulnerable> OnBeingHitted;
        void TakeHit(GameParticipant fromParticipant);
    }
}