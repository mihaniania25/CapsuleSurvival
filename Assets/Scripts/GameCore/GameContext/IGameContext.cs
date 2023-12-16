namespace CapsuleSurvival.Core
{
    public interface IGameContext
    {
        void Reset();

        ParticipantsRegister ParticipantsRegister { get; }
        GameSessionModel SessionModel { get; }

        PlayerBase Player { get; }
        void RegisterPlayer(PlayerBase player);
        void UnregisterPlayer();

        IArena Arena { get; }
        void RegisterArena(IArena arena);
        void UnregisterArena();

        IGameEffectsController EffectsController { get; }
        void RegisterEffectsController(IGameEffectsController controller);
        void UnregisterEffectsController();
    }
}