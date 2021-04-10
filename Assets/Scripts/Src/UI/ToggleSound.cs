using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This module implements [ToggleSound Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#togglesound-module)
    /// found in the Architecture and Module Design Document. A simple script to toggle all sound. This script should be attached to a Toggle.
    /// </summary>
    public class ToggleSound : MonoBehaviour
    {
        /// <summary>
        /// The icon to show that sound is on.
        /// </summary>
        public Sprite SoundOn;

        /// <summary>
        /// The icon to show that sound is off.
        /// </summary>
        public Sprite SoundOff;

        /// <summary>
        /// Sound toggle.
        /// </summary>
        private Toggle soundToggle;

        void Start()
        {
            soundToggle = gameObject.GetComponent<Toggle>();
            SetIcon();

            soundToggle.onValueChanged.AddListener((isSoundOn) =>
            {
                AudioListener.volume = isSoundOn ? 1 : 0;
                SetIcon();
            });
        }

        /// <summary>
        /// Sets the icon shown according to the current setting, and changes the setting from off to on, or from on to off.
        /// </summary>
        private void SetIcon()
        {
            soundToggle.isOn = AudioListener.volume == 1;
            soundToggle.image.sprite = soundToggle.isOn ? SoundOn : SoundOff;
        }
    }
}
