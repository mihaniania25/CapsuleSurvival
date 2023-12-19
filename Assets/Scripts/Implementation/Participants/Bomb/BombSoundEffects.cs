using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CapsuleSurvival.Impl
{
    public class BombSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private SoundEffectData _appearSFX;
        [SerializeField] private SoundEffectData _disappearSFX;
        [SerializeField] private SoundEffectData _explodeSFX;

        private SoundManager _soundManager => AppContext.SoundManager;

        public void PlayAppearSound()
        {
            _soundManager.PlaySoundEffect(_appearSFX, _audioSource);
        }

        public void PlayDisappearSound()
        {
            _soundManager.PlaySoundEffect(_disappearSFX, _audioSource);
        }

        public void PlayExplodeSound()
        {
            _soundManager.PlaySoundEffect(_explodeSFX, _audioSource);
        }
    }
}
