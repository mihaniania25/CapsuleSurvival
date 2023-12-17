using System;

namespace CapsuleSurvival.Core
{
    public class ParticipantAppearingHelper
    {
        public event Action<GameParticipant> OnAppearingCompleted;

        private GameParticipant _participant;

        public void Setup(GameParticipant participant)
        {
            _participant = participant;
        }

        public void MakeParticipantAppear()
        {
            _participant.OnAppeared += OnParticipantAppeared;
            _participant.Appear();
        }

        private void OnParticipantAppeared()
        {
            _participant.OnAppeared -= OnParticipantAppeared;
            OnAppearingCompleted?.Invoke(_participant);
        }

        public void Dispose()
        {
            if (_participant != null)
            {
                _participant.OnAppeared -= OnParticipantAppeared;
                _participant = null;
            }
        }
    }
}