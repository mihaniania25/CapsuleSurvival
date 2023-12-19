using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class PlayerSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private SoundEffectData _appearSFX;
        [SerializeField] private SoundEffectData _disappearSFX;
        [SerializeField] private SoundEffectData _explodeSFX;

        private SoundManager _soundManager => AppContext.SoundManager;

        public void PlayAppearSoundEffect()
        {
            _soundManager.PlaySoundEffect(_appearSFX, _audioSource);
        }

        public void PlayDisappearSoundEffect()
        {
            _soundManager.PlaySoundEffect(_disappearSFX, _audioSource);
        }

        public void PlayExplodeSoundEffect()
        {
            _soundManager.PlaySoundEffect(_explodeSFX, _audioSource);
        }
    }
}