using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class EnemySoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource; 

        [SerializeField] private SoundEffectData _appearSFX;
        [SerializeField] private SoundEffectData _disappearSFX;

        private SoundManager _soundManager => AppContext.SoundManager;

        public void PlayAppearSound()
        {
            _soundManager.PlaySoundEffect(_appearSFX, _audioSource);
        }

        public void PlayDisappearSound()
        {
            _soundManager.PlaySoundEffect(_disappearSFX, _audioSource);
        }
    }
}