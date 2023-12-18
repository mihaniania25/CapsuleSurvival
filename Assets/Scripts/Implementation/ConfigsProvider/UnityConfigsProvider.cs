using UnityEngine;
using CapsuleSurvival.Core;

namespace CapsuleSurvival.Impl
{
    public class UnityConfigsProvider : IConfigsProvider
    {
        private PlayerConfig _playerConfig;
        public IPlayerConfig PlayerConfig => _playerConfig ??= Resources.Load<PlayerConfig>(ResourcesPath.PLAYER_CONFIG);

        private GeneratorConfig _generatorConfig;
        public IGeneratorConfig GeneratorConfig => _generatorConfig ??= Resources.Load<GeneratorConfig>(ResourcesPath.GENERATOR_CONFIG);

        private ArenaParticipantsSettings _arenaParticipantsSettings;
        public ArenaParticipantsSettings ArenaParticipantsSettings
        {
            get
            {
                return _arenaParticipantsSettings ??= 
                    Resources.Load<ArenaParticipantsSettings>(ResourcesPath.PARTICIPANTS_SETTINGS_CONFIG);
            }
        }
    }
}