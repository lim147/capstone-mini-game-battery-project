using UnityEngine;

namespace Helper
{
    /// <summary>
    /// A helper object that associates a keyboard key press with the time
    /// that the key was pressed;
    /// This module implements [TimeAndKey Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#timeandkey-module)
    /// found in the Architecture and Module Design Document.
    /// </summary>
    public class TimeAndKey
    {
        /// <summary>
        /// Variable to hold a time value.
        /// </summary>
        public double Time { get; }

        /// <summary>
        /// Variable to hold the keycode of the key pressed.
        /// </summary>
        public KeyCode KeyCode { get; }


        /// <summary>
        /// Variable to hold the name of the key pressed.
        /// </summary>
        public string KeyName { get; }

        /// <summary>
        /// Creates a new TimeAndKey object.
        /// </summary>
        /// <param name="time">The time</param>
        /// <param name="key">The key</param>
        public TimeAndKey(double time, KeyCode key)
        {
            Time = time;
            KeyCode = key;
            KeyName = key.ToString();
        }
    }
}

