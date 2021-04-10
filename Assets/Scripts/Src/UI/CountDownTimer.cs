using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This module implements [CountDownTimer Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#countdowntimer-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// 
    /// A helper object that is for time count down purposes.
    /// All time values are in seconds.
    /// </summary>
    public class CountDownTimer : MonoBehaviour
    {
        /// <summary>
        /// Time value that is updated and decreased
        /// </summary>
        public float currentTime = 0f;

        /// <summary>
        /// constant time for the countdown, which is the duration for a mini-game
        /// </summary>
        public float startingTime = 40f;

        /// <summary>
        /// Text object that displays the time value
        /// </summary>
        public Text timerText;

        /// <summary>
        /// Constant for the number of seconds remaining before displaying text
        /// with the highlighted colour
        /// </summary>
        private const float HIGHLIGHTED_TIME = 5;

        /// <summary>
        /// Color of the highlighted timer
        /// </summary>
        private Color HIGHLIGHTED_COLOUR = Color.red;

        
        /// <summary>
        /// Start is a a predefined function and is called when the attached scene is loaded.
        /// Sets the timer value to be the starting time, and should be called first for
        /// initialization.
        /// </summary>
        public void Start()
        {
            currentTime = startingTime;
        }

        /// <summary>
        /// Update is a predefined function in unity and is called at every frame.
        /// The time value decreases as appropriate at every frame
        /// </summary>
        public void Update()
        {
            // deltaTime is to make current time decrease by each second instead of each frame
            currentTime = currentTime -= Time.deltaTime;
            // Convert the float time value into a string to display on the screen
            // timerText.text displays the time with 1 decimal place due to the "1f" argument to ToString()
            timerText.text = currentTime.ToString("f1");

            // Make the text red if there are less than 5 seconds left
            if (currentTime <= HIGHLIGHTED_TIME)
            {
                timerText.color = HIGHLIGHTED_COLOUR;
            }

            // If the time reaches 0, move to the next scene.
            // Since it is possible that the time calculated may not ever reach the exact value of 0,
            // Check for when the currentTime value is below 0.1
            if (currentTime < 0.1)
            {
                currentTime = 0;

                // Function to move to the menu scene
                SceneSwitcher.MoveToScene(SceneName.MENU_SCENE);
            }
        }

    }

}
