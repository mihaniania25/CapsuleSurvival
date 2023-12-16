using System;
using System.Collections.Generic;

namespace CapsuleSurvival.Core
{
    public class ParticipantsRegister
    {
        public event Action<IVulnerable> OnVulnerableRegistered;

        public List<GameParticipant> AllParticipants { get; private set; }

        private Dictionary<IParticipantGenerator, ParticipantsGroup> _generatorsParticipants =
            new Dictionary<IParticipantGenerator, ParticipantsGroup>();

        private Dictionary<GameParticipant, IParticipantGenerator> _participantsGenerators = 
            new Dictionary<GameParticipant, IParticipantGenerator>();

        private Dictionary<IVulnerable, GameParticipant> _vulnerablesParticipants =
            new Dictionary<IVulnerable, GameParticipant>();

        public ParticipantsRegister()
        {
            AllParticipants = new List<GameParticipant>();
        }

        public GameParticipant GetVulnerableFullParticipant(IVulnerable vulnerable)
        {
            if (_vulnerablesParticipants.ContainsKey(vulnerable))
                return _vulnerablesParticipants[vulnerable];
            return null;
        }

        public int GetGeneratorAliveParticipants(IParticipantGenerator generator)
        {
            if (_generatorsParticipants.ContainsKey(generator))
                return _generatorsParticipants[generator].Count;
            return 0;
        }

        public void RegisterParticipant(IParticipantGenerator generator, GameParticipant participant)
        {
            if (_generatorsParticipants.ContainsKey(generator) == false)
                _generatorsParticipants[generator] = new ParticipantsGroup();

            if (_generatorsParticipants[generator].Contains(participant) == false)
                _generatorsParticipants[generator].Add(participant);

            if (AllParticipants.Contains(participant) == false)
                AllParticipants.Add(participant);

            _participantsGenerators[participant] = generator;

            TryRegisterVulnerableParticipant(participant);
        }

        private void TryRegisterVulnerableParticipant(GameParticipant participant)
        {
            if (participant is IVulnerable vulnerable && _vulnerablesParticipants.ContainsKey(vulnerable) == false)
            {
                _vulnerablesParticipants[vulnerable] = participant;
                OnVulnerableRegistered?.Invoke(vulnerable);
            }
        }

        public void UnregisterParticipant(GameParticipant participant)
        {
            if (_participantsGenerators.ContainsKey(participant))
            {
                IParticipantGenerator generator = _participantsGenerators[participant];

                if (_generatorsParticipants.ContainsKey(generator))
                    _generatorsParticipants[generator].Remove(participant);

                _participantsGenerators.Remove(participant);
                _vulnerablesParticipants.Remove(participant as IVulnerable);
                AllParticipants.Remove(participant);
            }
        }

        public void UnregisterParticipant(IVulnerable vulnerable)
        {
            UnregisterParticipant(GetVulnerableFullParticipant(vulnerable));
        }
    }
}