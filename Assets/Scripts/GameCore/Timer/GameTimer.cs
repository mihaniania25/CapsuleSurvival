using System.Collections;
using UnityEngine;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Core
{
    public class GameTimer
    {
        private float _startTrackTime;
        private CoroutineTask _trackingTimeTask;

        private IGameContext _gameContext;
        private GameSessionModel _sessionModel => _gameContext.SessionModel;

        public void Setup(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Launch()
        {
            _startTrackTime = Time.time;

            _trackingTimeTask?.Stop();
            _trackingTimeTask = new CoroutineTask(TrackingTimeCoro());
        }

        private IEnumerator TrackingTimeCoro()
        {
            while (true)
            {
                _sessionModel.PlayingTimeElapsed.Value = Time.time - _startTrackTime;
                yield return null;
            }
        }

        public void Stop()
        {
            _trackingTimeTask?.Stop();
            _trackingTimeTask = null;
        }

        public void Dispose()
        {
            Stop();
            _gameContext = null;
        }
    }
}