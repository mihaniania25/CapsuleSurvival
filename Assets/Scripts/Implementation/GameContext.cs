using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class GameContext : IGameContext
    {
        public IArena Arena { get; private set; }
        public PlayerBase Player { get; private set; }
        public ParticipantsRegister ParticipantsRegister { get; private set; }
        public GameSessionModel SessionModel { get; private set; }
        public IGameEffectsController EffectsController { get; private set; }

        public GameContext()
        {
            SessionModel = new GameSessionModel();
        }

        public void Reset()
        {
            ParticipantsRegister = new ParticipantsRegister();
            SessionModel.Reset();

            UnregisterPlayer();
        }

        public void RegisterArena(IArena arena)
        {
            if (Arena == null)
                Arena = arena;
            else
                GameLog.Error("[GameContext] trying to register ARENA multiple times!");
        }

        public void RegisterPlayer(PlayerBase player)
        {
            if (Player == null)
                Player = player;
            else
                GameLog.Error("[GameContext] trying to register PLAYER multiple times!");
        }

        public void RegisterEffectsController(IGameEffectsController controller)
        {
            if (EffectsController == null)
                EffectsController = controller;
            else
                GameLog.Error("[GameContext] trying to register EFFECTS CONTROLLER multiple times!");
        }

        public void UnregisterArena()
        {
            Arena = null;
        }

        public void UnregisterPlayer()
        {
            Player = null;
        }

        public void UnregisterEffectsController()
        {
            EffectsController = null;
        }
    }
}