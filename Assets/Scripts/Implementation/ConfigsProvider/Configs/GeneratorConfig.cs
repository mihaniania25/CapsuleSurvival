using System.Collections.Generic;
using UnityEngine;
using CapsuleSurvival.Core;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    [CreateAssetMenu(fileName = "InGameGeneratorConfig", menuName = "Config/InGameGenerator")]
    public class GeneratorConfig : ScriptableObject, IGeneratorConfig
    {
        [SerializeField] private List<GenerationSettings> _generationSettings = new List<GenerationSettings>();

        public GenerationSettings GetSettings(GenerationType generationType)
        {
            GenerationSettings settings = _generationSettings.Find(gs => gs.GenType == generationType);

            if (settings == null)
                GameLog.Error($"[GeneratorConfig] failed to find GenerationSettings with gen type '{generationType}'");

            return settings;
        }
    }
}