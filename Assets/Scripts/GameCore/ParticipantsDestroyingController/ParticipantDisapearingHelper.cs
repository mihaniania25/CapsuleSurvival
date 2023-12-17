using System;

namespace CapsuleSurvival.Core
{
    public class ParticipantDisapearingHelper
    {
        public event Action<GameParticipant> OnDisappearingCompleted;

        private GameParticipant _participant;

        public void Setup(GameParticipant participant)
        {
            _participant = participant;
        }

        public void MakeParticipantDisappear()
        {
            _participant.OnDisappeared += OnParticipantDisappeared;
            _participant.Appear();
        }

        private void OnParticipantDisappeared()
        {
            _participant.OnDisappeared -= OnParticipantDisappeared;
            OnDisappearingCompleted?.Invoke(_participant);
        }

        public void Dispose()
        {
            if (_participant != null)
            {
                _participant.OnDisappeared -= OnParticipantDisappeared;
                _participant = null;
            }
        }
    }
}