using UnityEngine;
using TMPro;

namespace CapsuleSurvival.Impl
{
    public class ElapsedTimeViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        private GameContext _gameContext => AppContext.GameContext;
        private GameSessionModel _gameSessionModel => _gameContext.SessionModel;

        private void OnEnable()
        {
            _gameSessionModel.PlayingTimeElapsed.Subscribe(OnElapsedTimeUpd);
        }

        private void OnElapsedTimeUpd(float elapsedSeconds)
        {
            _label.text = string.Format("{0:0.00}", elapsedSeconds);
        }

        private void OnDisable()
        {
            _gameSessionModel.PlayingTimeElapsed.Unsubscribe(OnElapsedTimeUpd);
        }

        private void OnDestroy()
        {
            _gameSessionModel.PlayingTimeElapsed.Unsubscribe(OnElapsedTimeUpd);
        }
    }
}