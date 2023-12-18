using System;
using System.Collections.Generic;
using UnityEngine;

namespace CapsuleSurvival.Core
{
    public class ParticipantsCleaner
    {
        public event Action OnCleaningCompleted;

        private Dictionary<GameParticipant, ParticipantDisapearingHelper> _disappearingParticipants =
            new Dictionary<GameParticipant, ParticipantDisapearingHelper>();

        private IGameContext _gameContext;
        private ParticipantsRegister _participantsRegister => _gameContext.ParticipantsRegister;

        public void Setup(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void CleanUp()
        {
            if (_participantsRegister.AllParticipants.Count == 0)
            {
                OnCleaningCompleted?.Invoke();
                return;
            }

            List<GameParticipant> participantsToClear = new List<GameParticipant>(_participantsRegister.AllParticipants);
            foreach (GameParticipant participant in participantsToClear)
            {
                _participantsRegister.UnregisterParticipant(participant);

                ParticipantDisapearingHelper dissapearingHelper = new ParticipantDisapearingHelper();
                dissapearingHelper.Setup(participant);
                _disappearingParticipants[participant] = dissapearingHelper;

                dissapearingHelper.OnDisappearingCompleted += OnDissappearingCompleted;
            }

            List<ParticipantDisapearingHelper> disapearingHelpers = new List<ParticipantDisapearingHelper>(_disappearingParticipants.Values);
            foreach (ParticipantDisapearingHelper disapearingHelper in disapearingHelpers)
                disapearingHelper.MakeParticipantDisappear();
        }

        private void OnDissappearingCompleted(GameParticipant participant)
        {
            ParticipantDisapearingHelper disapearingHelper = _disappearingParticipants[participant];
            disapearingHelper.OnDisappearingCompleted -= OnDissappearingCompleted;
            disapearingHelper.Dispose();

            _disappearingParticipants.Remove(participant);

            GameObject.Destroy(participant.gameObject);

            if (_disappearingParticipants.Count == 0)
                OnCleaningCompleted?.Invoke();
        }

        public void Dispose()
        {
            _gameContext = null;

            foreach (GameParticipant participant in _disappearingParticipants.Keys)
            {
                ParticipantDisapearingHelper disapearingHelper = _disappearingParticipants[participant];
                disapearingHelper.OnDisappearingCompleted -= OnDissappearingCompleted;
                disapearingHelper.Dispose();
            }
            _disappearingParticipants.Clear();
        }
    }
}