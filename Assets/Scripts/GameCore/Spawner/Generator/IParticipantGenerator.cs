namespace CapsuleSurvival.Core
{
    public interface IParticipantGenerator
    {
        void Setup(ParticipantGeneratorParams generatorParams);

        void Launch();
        void Stop();

        void Dispose();
    }
}