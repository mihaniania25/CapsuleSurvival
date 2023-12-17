using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class GameContext : IGameContext
    {
        public GameManager GameManager { get; private set; }

        public IArena Arena { get; private set; }
        public PropagationField<PlayerBase> Player { get; private set; }
        public ParticipantsRegister ParticipantsRegister { get; private set; }
        public GameSessionModel SessionModel { get; private set; }
        public IGameEffectsController EffectsController { get; private set; }
        public IUserInputReader UserInputReader { get; private set; }

        public GameContext()
        {
            SessionModel = new GameSessionModel();
            GameManager = new GameManager();
            Player = new PropagationField<PlayerBase>();
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
                Player.Value = player;
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

        public void RegisterUserInputReader(IUserInputReader userInputReader)
        {
            if (UserInputReader == null)
                UserInputReader = userInputReader;
            else
                GameLog.Error("[GameContext] trying to register INPUT READER multiple times!");
        }

        public void UnregisterArena()
        {
            Arena = null;
        }

        public void UnregisterPlayer()
        {
            Player.Value = null;
        }

        public void UnregisterEffectsController()
        {
            EffectsController = null;
        }

        public void UnregisterUserInputReader()
        {
            UserInputReader = null;
        }
    }
}