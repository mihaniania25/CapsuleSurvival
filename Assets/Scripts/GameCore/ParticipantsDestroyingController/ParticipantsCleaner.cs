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
            foreach (GameParticipant participant in _participantsRegister.AllParticipants)
            {
                _participantsRegister.UnregisterParticipant(participant);

                ParticipantDisapearingHelper dissapearingHelper = new ParticipantDisapearingHelper();
                dissapearingHelper.Setup(participant);
                _disappearingParticipants[participant] = dissapearingHelper;

                dissapearingHelper.OnDisappearingCompleted += OnDissappearingCompleted;
                dissapearingHelper.MakeParticipantDisappear();
            }
        }

        private void OnDissappearingCompleted(GameParticipant participant)
        {
            ParticipantDisapearingHelper disapearingHelper = _disappearingParticipants[participant];
            disapearingHelper.OnDisappearingCompleted -= OnDissappearingCompleted;
            disapearingHelper.Dispose();

            _disappearingParticipants.Remove(participant);

            GameObject.Destroy(participant);

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