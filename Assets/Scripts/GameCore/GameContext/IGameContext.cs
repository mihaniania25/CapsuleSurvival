using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Core
{
    public interface IGameContext
    {
        void Reset();

        ParticipantsRegister ParticipantsRegister { get; }
        GameSessionModel SessionModel { get; }

        PropagationField<PlayerBase> Player { get; }
        void RegisterPlayer(PlayerBase player);
        void UnregisterPlayer();

        IArena Arena { get; }
        void RegisterArena(IArena arena);
        void UnregisterArena();

        IGameEffectsController EffectsController { get; }
        void RegisterEffectsController(IGameEffectsController controller);
        void UnregisterEffectsController();

        IUserInputReader UserInputReader { get; }
        void RegisterUserInputReader(IUserInputReader userInputReader);
        void UnregisterUserInputReader();
    }
}