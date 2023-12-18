using CapsuleSurvival.Core;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class PlayerMovementController : MonoBehaviour
    {
        private IUserInputReader _userInputReader;

        public bool IsMovementEnabled { get; set; }

        private UnityConfigsProvider _configsProvider => AppContext.ConfigsProvider;
        private ArenaParticipantsSettings _arenaParticipantsSettings => _configsProvider.ArenaParticipantsSettings;
        private float _baseSpeed => _arenaParticipantsSettings.PlayerBaseSpeed;

        public void Setup(IUserInputReader userInputReader)
        {
            _userInputReader = userInputReader;
        }

        public void Update()
        {
            if (IsMovementEnabled)
            {
                _userInputReader.PerformReadingInputs();
                Vector3 input = new Vector3(_userInputReader.HorizontalInput, 0f, _userInputReader.VerticalInput);

                if (input.magnitude > 0f)
                    transform.position += input.normalized * _baseSpeed * Time.deltaTime;
            }
        }
    }
}