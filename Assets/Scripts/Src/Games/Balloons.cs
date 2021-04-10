using System.Collections.Generic;
using Storage;
using UnityEngine;
using Helper;
using UnityEngine.UI;

namespace Games
{
    /// <summary>
    /// This module implements [Balloons Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#balloons-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// 
    /// This module is described in the [SRS](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#4-pointing-balloons).
    ///
    /// <div class="WARNING">
    ///     <h5>ASSUMPTION</h5>
    ///     <p>
    ///     <ul>
    ///         <li>This script is attached to the `Balloon` object in
    ///           `BalloonsScene`
    ///         </li>
    ///         <li>`balloonFrame1`, `balloonFrame2`, and `balloonFrame3` are
    ///           populated with assets in the Unity Editor in the form
    ///           described by their documentation.
    ///         </li>
    ///     </ul>
    ///     </p>
    /// </div>
    /// </summary>
    public class Balloons : MonoBehaviour
    {
        /// <summary>
        /// Button where the balloon object is attached
        /// </summary>
        public Button balloon;

        // Images to show the frames of balloon popping animation
        /// <summary>
        /// <img src="../images/balloon_pieces1.png" width="100" />
        /// Frame 1 of the balloon popping animation.
        /// </summary>
        public Image balloonFrame1;

        /// <summary>
        /// <img src="../images/balloon_pieces2.png" width="100" />
        /// Frame 2 of the balloon popping animation.
        /// </summary>
        public Image balloonFrame2;

        /// <summary>
        /// <img src="../images/balloon_pieces3.png" width="100" />
        /// Frame 3 of the balloon popping animation.
        /// </summary>
        public Image balloonFrame3;

        /// <summary>
        /// Canvas attached to the scene that this script is attached to.
        /// </summary>
        public Canvas canvas;

        /// <summary>
        /// Text to indicate to the user how many rounds they have done.
        /// </summary>
        public Text RoundText;

        // Local state variables:

        // Variable for which round number the game is currently on
        private int level;
        // Variable for the size of the balloon on the screen
        private double balloonSize;
        // Position2D variable for x and y values of the random destination
        private Position2D destinationPoint;
        // Amount of time for the player to successfully click on the random balloon
        private double destinationClickTime;
        // List of Time and position values of unsuccessful clicks in a round
        private List<TimeAndPosition> clicks;
        // Boolean value for cueing function execution upon recognizing click actions by the player 
        private bool isBeingHeld;
        // Boolean value for cueing function execution upon recognizing a successful balloon click action
        private bool isBalloonInCenter;
        // Time value between each unsuccessful click
        private double initClickTime;
        // Size of the balloon after decrement 
        private double balloonSizeAfterDecrement;
        // Position2D representation of the world position
        private Position2D pos;

        // Constant State Variables:

        // The center of the screen is at (0,0)
        // The following constants are the minimum and maximum
        // x and y values that a balloon may spawn at
        private const float MINX = -350f;
        private const float MAXX = 350f;
        private const float MINY = -200f;
        private const float MAXY = 140f;
        // Constant Vector 3 value for the x and y values of the center balloon
        private Vector3 centerPoint = new Vector3(0, 0, 0); // (local position);
        // Duration of the mini-game, value is in seconds
        private const double GAME_DURATION = 40;
        // Initial balloon size of the balloons that appear on the screen
        private const double MAX_BALLOON_SIZE = 200;
        // Minimum balloon size of the balloons that appear on the screen
        private const double MIN_BALLOON_SIZE = 30;
        // Decrement value when balloon size decreases
        private const float BALLOON_SIZE_DECREMENT = 10f;

        // Variables for storage purposes:
        // Datatype to hold all game data in the balloons mini-game
        private static BalloonsStorage balloonsData;
        // Datatype to hold game data for every round in balloons mini-game
        private BalloonsRound round;

        // Variable for display the balloon pieces images
        // The time duration for displaying balloon pieces images
        private double balloonAnimationDisplayTime;
        // The position of the balloon pieces image that is displayed; it should be the previous balloon's position
        private Vector3 previousBalloonPos;
        // The size of the balloon pieces image that is displayed; it should be the previous balloon's size
        private Vector2 previousBalloonSize;

        /// <summary>
        /// Start is a a predefined function and is called when the attached scene is loaded
        /// </summary>
        public void Start()
        {
            Balloon_init();
        }

        /// <summary>
        /// Update is a predefined function in unity and is called at every frame.
        /// </summary>
        public void Update()
        {
            // Update the time related variables
            destinationClickTime += 1 * Time.deltaTime;
            initClickTime += 1 * Time.deltaTime;
            balloonAnimationDisplayTime += Time.deltaTime;

            RoundText.text = level.ToString();

            // Control the display of the balloon popping animation
            DisplayBalloonAnimation();

            // If the click action was on the balloon
            if (isBeingHeld)
            {
                // If the balloon should appear in the center next
                if (isBalloonInCenter)
                {
                    ClickOnCenterBalloon();
                }

                // If the balloon should appear in a random position next. This begins a new round.
                else
                {
                    PrepareDestinationBalloon();
                    StoreDestinationBalloonInfo();
                }

                // The boolean is set to false until the next valid click action
                // has been detected.         
                isBeingHeld = false;
                
            }

            // If the click action on the random balloon is unsuccessful
            else if (isBalloonInCenter)
            {
                PressNotOnObject();
            }
        }

        /// <summary>
        /// ClickOnBalloon is called when the player presses and releases on the balloon button object.
        /// </summary>
        public void ClickOnBalloon()
        {
            //The click button has been triggered
            isBeingHeld = true;

            // Reset the balloon display timer
            balloonAnimationDisplayTime = 0;
        }

        /// <summary>
        /// Getter function for the Balloons gameplay data
        /// </summary>
        /// <returns>The gameplay data of the Balloons mini-game.
        /// </returns> 
        public static BalloonsStorage GetGameplayData()
        {
            return balloonsData;
        }

        //Helper functions start:
        //-----------------------------------------------------------------

        /// <summary>
        /// Initializes all state variables necessary for the Balloons mini-game.
        /// </summary>
        private void Balloon_init()
        {
            // Initialize the storage variables
            balloonsData = new BalloonsStorage();
            balloonsData.Rounds = new List<BalloonsRound>();

            level = 0;

            // The balloon size starts at  x, y =  MAX_BALLOON_SIZE
            // The x and y values decrement by BALLOON_SIZE_DECREMENT every level
            // until it reaches a minimum value of MIN_BALLOON_SIZE
            balloonSize = MAX_BALLOON_SIZE;

            // Initialize the variables for the positions on the screen.
            destinationPoint = new Position2D(0, 0); // (world position) 

            // Initialize datatype to hold the time and positions of clicks every round
            clicks = new List<TimeAndPosition>();

            // Initialize function execution booleans
            isBeingHeld = false;
            isBalloonInCenter = false;

            // Set the balloon display timer to be a big enough number so that
            // the balloon pieces images won't be shown as the game starts
            balloonAnimationDisplayTime = 100;
        }

        /// <summary>
        /// ClickOnCenterBalloon is called when the player is going to
        /// click on the balloon object that would appears on the center.
        /// It will set up the game state for the next round and store the gameplay data for the finished round.
        /// </summary>
        private void ClickOnCenterBalloon()
        {
            // Save the previous balloon info before it's been updated
            previousBalloonPos = balloon.transform.localPosition;
            previousBalloonSize = balloon.image.rectTransform.sizeDelta;

            // Balloon size after the decrement
            balloonSizeAfterDecrement = balloon.image.rectTransform.sizeDelta.x - BALLOON_SIZE_DECREMENT;

            // Decrease the balloon size if balloon size after decreasement is no less than the minimum allowed size
            if (balloonSizeAfterDecrement >= MIN_BALLOON_SIZE)
            {
                balloon.image.rectTransform.sizeDelta -= new Vector2(BALLOON_SIZE_DECREMENT, BALLOON_SIZE_DECREMENT);
            }

            // Set the balloon position to be the center position
            balloon.transform.localPosition = centerPoint;

            // Toggle the isBalloonInCenter bool for the next balloon
            // The next balloon should appear in a random position
            isBalloonInCenter = false;

            // Increment the round number
            level += 1;

            // Update the finished round's game play data
            round.DestinationClickTime = destinationClickTime;

            // Get and set the position of the finished round's successful click
            round.SuccessClickPoint = convertScreenToLocalPosition2D();

            // Insert the round data into the Balloon s mini-game storage object
            balloonsData.Rounds.Add(round);
        }

        /// <summary>
        /// PrepareDestinationBalloons is called when the player is going to click on the balloon
        /// that would appears on a random position.
        /// It will set up the game state for the current round.
        /// </summary>
        private void PrepareDestinationBalloon()
        {
            // Save the previous balloon info before it's been updated
            previousBalloonPos = balloon.transform.localPosition;
            previousBalloonSize = balloon.image.rectTransform.sizeDelta;

            // Generate random x and y values for the random balloon position (local position)
            // Make sure the random position is different from center point (0,0)
            float localX = RamGenerator.GenerateARamNum(MINX, MAXX);
            float localY = RamGenerator.GenerateARamNum(MINY, MAXY);
            while (localX == 0 && localY == 0)
            {
                localX = RamGenerator.GenerateARamNum(MINX, MAXX);
                localY = RamGenerator.GenerateARamNum(MINY, MAXY);
            }


            // Display balloon at the random position (local position)
            balloon.transform.localPosition = new Vector3(localX, localY, 0);

            // Reset destinationClickTime when the balloon appears in ramdom position
            destinationClickTime = 0;

            // Reset the click time timer for the time between clicks
            initClickTime = 0;

            // Toggle the isBalloonInCenter value because The next balloon should appear at the center
            isBalloonInCenter = true;

            // Set the local state variables:
            balloonSize = balloon.image.rectTransform.sizeDelta.x;
            destinationPoint = new Position2D(localX, localY);
        }

        /// <summary>
        /// StoreDestinationBalloonInfo is called after PrepareDestinationBalloon is called.
        /// It will store the position and size of the destination balloon in the current round to
        /// a round object.
        /// </summary>
        private void StoreDestinationBalloonInfo()
        {
            // Initialize and set the values for the new round object 
            round = new BalloonsRound();
            round.DestinationPoint = destinationPoint;
            round.BalloonSize = balloonSize;
            round.Clicks = new List<TimeAndPosition>();
        }

        /// <summary>
        /// convertScreenToLocalPosition2D gets the screen position of current cursor and
        /// converts it into the local position
        /// </summary>
        private Position2D convertScreenToLocalPosition2D()
        {
            // Get the position of the click on the screen
            Vector3 screenMousePos = Input.mousePosition;

            // Declare a vector to store canvas mouse position
            Vector2 canvasMousePos;

            // Convert the screen position to UI canvas rectangle local position
            // Canvas position will be saved into the last parameter
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenMousePos, canvas.worldCamera, out canvasMousePos);

            // Create a Position2D object for conversion
            pos = new Position2D(canvasMousePos.x, canvasMousePos.y);
            return pos;
        }

        /// <summary>
        /// PressNotOnObject is the function called when there is a
        /// click action, and this click attempt on the balloon is
        /// unsuccessful.
        /// </summary>
        private void PressNotOnObject()
        {
            // If the click attempt was with the left mouse button
            if (Input.GetMouseButtonUp(0))
            {
                // Get the local position as a Position2D
                pos = convertScreenToLocalPosition2D();
                // Create a timeAndPosition object for the unsuccessful click
                TimeAndPosition timeAndPosition = new TimeAndPosition(initClickTime, pos);
                // Add the unsuccessful click object to the list of unsuccessful click objects
                round.Clicks.Add(timeAndPosition);
                // Reset the timer for time between unsuccessful clicks
                initClickTime = 0;
            }
        }

        /// <summary>
        /// DisplayBalloonAnimation controls the display of game objects balloonFrame1, balloonFrame2, balloonFrame3.
        /// It will generate the explosion animation of a balloon when the balloon is clicked.
        /// </summary>
        private void DisplayBalloonAnimation()
        {
            // Explosion stage 1 will last from 0s - 0.05s
            if (balloonAnimationDisplayTime <= 0.05)
            {
                // In this stage, balloonFrame1 will display
                balloonFrame1.gameObject.SetActive(true);
                balloonFrame1.transform.localPosition = previousBalloonPos;
                balloonFrame1.rectTransform.sizeDelta = previousBalloonSize * 2;

                balloonFrame2.gameObject.SetActive(false);
                balloonFrame3.gameObject.SetActive(false);
            }
            // Explosion stage 2 will last from 0.05s - 0.08s
            else if (balloonAnimationDisplayTime <= 0.08)
            {
                balloonFrame1.gameObject.SetActive(false);

                // In this stage, balloonFrame2 will display
                balloonFrame2.gameObject.SetActive(true);
                balloonFrame2.transform.localPosition = previousBalloonPos;
                balloonFrame2.rectTransform.sizeDelta = previousBalloonSize * 2;

                balloonFrame3.gameObject.SetActive(false);
            }
            // Explosion stage 2 will last from 0.08s - 0.01s
            else if (balloonAnimationDisplayTime <= 0.1)
            {
                balloonFrame1.gameObject.SetActive(false);
                balloonFrame2.gameObject.SetActive(false);

                // In this stage, balloonFrame3 will display
                balloonFrame3.gameObject.SetActive(true);
                balloonFrame3.transform.localPosition = previousBalloonPos;
                balloonFrame3.rectTransform.sizeDelta = previousBalloonSize * 2;
            }
            // Explosion should be done, no balloon pieces images should display
            else
            {
                balloonFrame1.gameObject.SetActive(false);
                balloonFrame2.gameObject.SetActive(false);
                balloonFrame3.gameObject.SetActive(false);
            }
        }
    }
}