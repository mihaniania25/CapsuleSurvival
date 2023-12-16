namespace CapsuleSurvival.Core
{
    public interface IUserInputReader
    {
        bool IsActive { get; }

        void Launch();
        void Stop();

        void PerformReadingInputs();

        float HorizontalInput { get; }
        float VerticalInput { get; }
    }
}