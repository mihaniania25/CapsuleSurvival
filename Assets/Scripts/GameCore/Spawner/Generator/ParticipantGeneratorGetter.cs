using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Core
{
    public class ParticipantGeneratorGetter
    {
        public IParticipantGenerator GetParticipantGenerator(GenerationType generationType)
        {
            switch (generationType)
            {
                case GenerationType.Enemy:
                case GenerationType.Bomb:
                    return new ObstacleGenerator();

                default:
                    GameLog.Error($"[ParticipantGeneratorGetter] failed to find generator for gen type '{generationType}'");
                    return null;
            }
        }
    }
}