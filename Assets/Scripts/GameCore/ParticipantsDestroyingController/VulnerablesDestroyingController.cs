using System.Collections.Generic;
using UnityEngine;

namespace CapsuleSurvival.Core
{
    public class VulnerablesDestroyingController
    {
        private List<IVulnerable> _vulnerablesSubscribed = new List<IVulnerable>();
        private Dictionary<GameParticipant, ParticipantDisapearingHelper> _disappearingParticipants =
            new Dictionary<GameParticipant, ParticipantDisapearingHelper>();

        private IGameContext _gameContext;
        private ParticipantsRegister _participantsRegister => _gameContext.ParticipantsRegister;

        public void Setup(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Launch()
        {
            _participantsRegister.OnVulnerableRegistered += OnVulnerableRegistered;
        }

        private void OnVulnerableRegistered(IVulnerable vulnerable)
        {
            _vulnerablesSubscribed.Add(vulnerable);

            vulnerable.OnBeingHitted += OnVulnerableHitted;
        }

        private void OnVulnerableHitted(IVulnerable vulnerable)
        {
            vulnerable.OnBeingHitted -= OnVulnerableHitted;
            _vulnerablesSubscribed.Remove(vulnerable);

            GameParticipant participant = _participantsRegister.GetVulnerableFullParticipant(vulnerable);
            _participantsRegister.UnregisterParticipant(participant);

            ParticipantDisapearingHelper dissapearingHelper = new ParticipantDisapearingHelper();
            dissapearingHelper.Setup(participant);
            _disappearingParticipants[participant] = dissapearingHelper;

            dissapearingHelper.OnDisappearingCompleted += OnDissappearingCompleted;
            dissapearingHelper.MakeParticipantDisappear();
        }

        private void OnDissappearingCompleted(GameParticipant participant)
        {
            ParticipantDisapearingHelper disapearingHelper = _disappearingParticipants[participant];
            disapearingHelper.OnDisappearingCompleted -= OnDissappearingCompleted;
            disapearingHelper.Dispose();

            _disappearingParticipants.Remove(participant);

            GameObject.Destroy(participant.gameObject);
        }

        public void Stop()
        {
            _participantsRegister.OnVulnerableRegistered -= OnVulnerableRegistered;

            _vulnerablesSubscribed.ForEach(v => v.OnBeingHitted -= OnVulnerableHitted);
            _vulnerablesSubscribed.Clear();
        }

        public void Dispose()
        {
            Stop();

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