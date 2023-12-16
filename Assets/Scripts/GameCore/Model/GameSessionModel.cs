using CapsuleSurvival.Utility;

namespace CapsuleSurvival
{
    public class GameSessionModel
    {
        public PropagationField<float> PlayingTimeElapsed { get; private set; } = new PropagationField<float>();

        public void Reset()
        {
            PlayingTimeElapsed.Value = 0f;
        }
    }
}