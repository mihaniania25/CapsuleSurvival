using UnityEngine;
using UnityEngine.UI;

namespace CapsuleSurvival.Impl
{
    public class SoundSwitcher : MonoBehaviour
    {
        [SerializeField] private Button _switchButton;
        [SerializeField] private GameObject _enabledView;
        [SerializeField] private GameObject _disabledView;

        private SoundManager _soundManager => AppContext.SoundManager;

        private void Awake()
        {
            _switchButton.onClick.AddListener(OnSwitchButtonClick);
            _soundManager.SoundEffectsEnabled.Subscribe(OnSoundEffectsEnabledUpd);
        }

        private void OnSwitchButtonClick()
        {
            _soundManager.SoundEffectsEnabled.Value = !_soundManager.SoundEffectsEnabled.Value;
        }

        private void OnSoundEffectsEnabledUpd(bool areSoundEffectsEnabled)
        {
            _enabledView.SetActive(areSoundEffectsEnabled);
            _disabledView.SetActive(areSoundEffectsEnabled == false);
        }

        private void OnDestroy()
        {
            _switchButton.onClick.RemoveListener(OnSwitchButtonClick);
            _soundManager.SoundEffectsEnabled.Unsubscribe(OnSoundEffectsEnabledUpd);
        }
    }
}