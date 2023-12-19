using UnityEngine;
using CapsuleSurvival.Utility;

namespace CapsuleSurvival.Impl
{
    public class SoundManager
    {
        public PropagationField<bool> SoundEffectsEnabled { get; private set; }

        public SoundManager()
        {
            SoundEffectsEnabled = new PropagationField<bool>(true);
        }

        public void PlaySoundEffect(SoundEffectData soundEffectData, AudioSource source)
        {
            if (SoundEffectsEnabled.Value == true)
            {
                source.volume = soundEffectData.Volume;
                source.clip = soundEffectData.AudioClip;
                source.Play();
            }
        }
    }
}