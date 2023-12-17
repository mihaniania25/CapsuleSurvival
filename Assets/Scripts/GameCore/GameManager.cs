using System;

namespace CapsuleSurvival.Core
{
    public class GameManager
    {
        public event Action OnGameOver;

        private GameFlowController _flowController = new GameFlowController();
        private IGameContext _gameContext;

        public void Setup(GameManagerParams gameManagerParams) 
        {
            _gameContext = gameManagerParams.GameContext;

            _flowController.Setup(gameManagerParams);
            _flowController.OnGameOver += OnGameOver;
        }

        public void Launch()
        {
            _gameContext.Reset();
            _flowController.Launch();
        }

        public void Dispose()
        {
            _flowController.Dispose();
            _flowController = null;

            _gameContext = null;
        }
    }
}