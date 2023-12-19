using CapsuleSurvival.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CapsuleSurvival.Core
{
    public class ParticipantsCleaner
    {
        private const float DISAPPEAR_INVOKE_INTERVAL = 0.2f;

        public event Action OnCleaningCompleted;

        private CoroutineTask _invokeDisappearingTask;

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

            PrepareDisappearingParticipants();
            LaunchGradualDisappearing();
        }

        private void PrepareDisappearingParticipants()
        {
            List<GameParticipant> participantsToClear = new List<GameParticipant>(_participantsRegister.AllParticipants);
            foreach (GameParticipant participant in participantsToClear)
            {
                _participantsRegister.UnregisterParticipant(participant);

                ParticipantDisapearingHelper dissapearingHelper = new ParticipantDisapearingHelper();
                dissapearingHelper.Setup(participant);
                _disappearingParticipants[participant] = dissapearingHelper;

                dissapearingHelper.OnDisappearingCompleted += OnDissappearingCompleted;
            }
        }

        private void LaunchGradualDisappearing()
        {
            _invokeDisappearingTask?.Stop();
            _invokeDisappearingTask = new CoroutineTask(InvokeDisappearingCoro());
        }

        private IEnumerator InvokeDisappearingCoro()
        {
            List<ParticipantDisapearingHelper> disapearingHelpers = new List<ParticipantDisapearingHelper>(_disappearingParticipants.Values);

            foreach (ParticipantDisapearingHelper disapearingHelper in disapearingHelpers)
            {
                disapearingHelper.MakeParticipantDisappear();
                yield return new WaitForSeconds(DISAPPEAR_INVOKE_INTERVAL);
            }
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
            _invokeDisappearingTask?.Stop();
            _invokeDisappearingTask = null;

            foreach (GameParticipant participant in _disappearingParticipants.Keys)
            {
                ParticipantDisapearingHelper disapearingHelper = _disappearingParticipants[participant];
                disapearingHelper.OnDisappearingCompleted -= OnDissappearingCompleted;
                disapearingHelper.Dispose();
            }
            _disappearingParticipants.Clear();

            _gameContext = null;
        }
    }
}