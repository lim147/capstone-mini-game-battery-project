using System.Collections;
using System.Collections.Generic;
using Storage;
using UnityEngine;
using Helper;
using UnityEngine.UI;

namespace Games
{
    /// <summary>
    /// This module implements [Squares Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#squares-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// 
    /// This module is described in the [SRS](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#5-visuospatial-sketchpad-squares).
    ///
    /// <div class="WARNING">
    ///     <h5>ASSUMPTION</h5>
    ///     <p>
    ///     <ul>
    ///         <li>This script is attached to the `SquareGridArea` object in
    ///           `SquaresScene`
    ///         </li>
    ///         <li>`displayedSquares` is populated in the Unity Editor with
    ///           the individual square objects in `SquareGridArea`
    ///         </li>
    ///     </ul>
    ///     </p>
    /// </div>
    /// </summary>
    public class Squares : MonoBehaviour
    {
        // Public state variables

        /// <summary>
        /// List of all square game objects shown on screen that may be used in the game
        /// </summary>
        public List<GameObject> displayedSquares;

        /// <summary>
        /// Button for when the user is finished recalling the square sequence
        /// </summary>
        public Button doneButton;

        // Local state variables:
        // Time duration between each square highlight
        private float squareHighlightInterval;
        // Time duration that square is highlighted for
        private float squareHighlightDuration;
        // Number of highlighted squares in a round
        private int numberOfHighlightedSquares;
        // List of square objects in the game (unused)
        private List<GameObject> squaresToHighlight;
        // List of squares in the order that the game highlighted them
        private List<IndexAndPosition> highlightedSquares;
        // List of squares in the order inputted by the player
        private List<IndexAndPosition> recalledSquares;
        // Duration of time spent recalling squares
        private float recallTime;
        // Boolean for done button 
        private bool doneButtonPressed;
        // Boolean for if the player can click on the squares
        private bool canClick;
        // Boolean for if the player clicked on a square
        private bool isClicked;
        // Id of the square that was clicked
        private int clickedSquareId;
        // Square object to hold the script attached to the square
        private Square squareScript;
        // Id of the square game object
        private int squareGameObjectId;
        // Position of the square game object
        private Position2D squarePosition;
        // Index and position of a square game object
        private IndexAndPosition squareIndexAndPosition;
        // Round number of the square mini-game
        private int roundNumber;
        // Mouse position
        private Vector3 worldMousePos;
        // 2D collider variable
        private Collider2D col;
        // Variable to hold game object
        private GameObject squareObjectToHighlight;
        // Number of clicks in a round
        private int totalSquareClicks;

        // Constant State Variables:

        // Color32 stores the  RGBA color value in four values of 8 bits (32-bit total). This is the format used in html colors. 0 means nothing of that color channel (r,g,b,a). and 255 means 100% of that color channel. 
        // Highlighted square colour
        private Color HIGHLIGHTED_COLOR = new Color32(0xFF, 0xFF, 0, 0xFF);
        // Regular square colour
        private Color UNHIGHLIGHTED_COLOR = new Color32(0xAA, 0xAA, 0xAA, 0xFF);

        // Width of the individual square
        private const float SQUARE_WIDTH = 1F;
        // Width of the square grid
        private const float SQUARE_CANVAS_WIDTH = 10F;
        // Height of the square grid
        private const float SQUARE_CANVAS_HEIGHT = 10F;
        // Starting number of highlighted squares
        private const int STARTING_NUMBER_OF_HIGHLIGHTED_SQUARES = 3;
        // Duration of highlight for a square that was clicked by the player in seconds
        private const float CLICK_SQUARE_HIGHLIGHT_DURATION = 0.25F;
        // Maximum number of highlighted squares
        private const int MAX_HIGHLIGHTED_SQUARES = 10;
        // Primary button value
        private const int PRIMARY_BUTTON = 0;
        // Maximum number of square clicks that are recorded. At the fastest possible game speed, a player would only ever have less than 15 squares in the highlight phase.
        private int MAXIMUM_RECORDED_CLICKS = 20;

        // Variables for storage purposes:
        // Datatype to hold all game data in the circles mini-game
        private static SquaresStorage squaresData;
        // Datatype to hold game data for every round in circles mini-game
        private SquaresRound round;


        /// <summary>
        /// Start is a a predefined function and is called when the attached scene is loaded. Prepares the game variables for the Squares mini-game.
        /// </summary>
        void Start()
        {
            // Initialize game variables
            Squares_init();

            // Begin the first round of the Squares mini-game
            StartCoroutine(ExecuteRound());
        }

        /// <summary>
        /// Update is called once per frame. It updates time related variables,
        /// recognizes click events, and executes the stages in the Squares mini-game
        /// </summary>
        void Update()
        {
            // Update the timer values
            recallTime += 1 * Time.deltaTime;

            // If the player clicks on the square and it is the recall square sequence stage
            if (canClick && isClicked)
            {
                // After clicking on a square, it must highlight and information must be stored
                // Before you click on another square. To ensure this, set canClick = false, so
                // that you cannot pass the condition on the previous line.
                canClick = false;
                StartCoroutine(ClickOnSquare());
            }

            // If they are on the recall squares stage and press the "done" button
            if (doneButtonPressed && canClick)
            {
                // Execute the next round of the Squares mini-game
                StartCoroutine(ExecuteRound());
            }
        }

        /// <summary>
        /// Getter function for the Squares gameplay data
        /// </summary>
        /// <returns>The gameplay data of the Squares mini-game.
        /// </returns> 
        /// <remark>This method is expected to be called only after the game has completed</remark>
        public static SquaresStorage GetGameplayData()
        {
            return squaresData;
        }

        //Helper functions start:
        //-----------------------------------------------------------------

        /// <summary>
        /// Initializes all state variables and objects
        /// necessary for the Squares mini-game.
        /// </summary>
        private void Squares_init()
        {
            // Initialize the storage variables
            squaresData = new SquaresStorage();
            squaresData.Rounds = new List<SquaresRound>();

            // Initialize game variables
            numberOfHighlightedSquares = STARTING_NUMBER_OF_HIGHLIGHTED_SQUARES;
            // The time between each highlight is calculated using the formula: squareHighlightInterval - squareHighlightDuration
            squareHighlightInterval = 0.7f;
            squareHighlightDuration = 0.7f;
            recallTime = 0;
            roundNumber = 0;
            doneButton.gameObject.SetActive(false);
            totalSquareClicks = 0;

            // Set the Id value for each of the square game objects
            AssignSquareIdValue();
        }

        /// <summary>
        /// Initializes the id value of each square in the game object list.
        /// Regardless of the number of square game objects, this ensures that
        /// each square has a unique id
        /// </summary>
        private void AssignSquareIdValue()
        {
            // Initialize square game object values for each item in the game object list
            for (int i = 0; i < displayedSquares.Count; i++)
            {
                // Get the script of the square game object
                squareScript = displayedSquares[i].GetComponent<Square>();
                // Set the id value of the square game object
                squareScript.Id = i;
            }
        }

        /// <summary>
        /// Changes the colour of the square game object given to the colour given.
        /// </summary>
        /// <param name="squareObjectToChangeColour">The square object whose colour will change.</param>
        /// <param name="colourToChangeTo">The colour to change to</param>
        private void FlipSquareColour(GameObject squareObjectToChangeColour, Color colourToChangeTo)
        {
            // Get the script of the square game object
            squareScript = squareObjectToChangeColour.GetComponent<Square>();
            // Call the function in the script to change the square colour 
            squareScript.ChangeColor(colourToChangeTo);
        }

        /// <summary>
        /// Highlight an individual square for a predetermined number of seconds.
        /// This function is type IEnumerator because it is called as a coroutine and
        /// ensures that all actions within this function execute with the correct wait duration in between.
        /// The square is highlighted for a specified amount of time, and during that time,
        /// it must stay that color.
        /// </summary>
        /// <param name="indexOfSquareToHighlight">The index of the square in the displayedSquares game object list</param>
        /// <param name="highlightDuration">Duration of time to highlight the square</param>
        private IEnumerator HighlightSquare(int indexOfSquareToHighlight, float highlightDuration)
        {
            // Get the correct square game object
            squareObjectToHighlight = displayedSquares[indexOfSquareToHighlight];
            // Call the function in the script to change the square colour to be the highlight color
            FlipSquareColour(squareObjectToHighlight, HIGHLIGHTED_COLOR);

            // Wait for the squareHighlightDuration
            yield return new WaitForSeconds(highlightDuration);

            // Change the colour in the sprite renderer to be the unhighlight color
            FlipSquareColour(squareObjectToHighlight, UNHIGHLIGHTED_COLOR);
        }

        /// <summary>
        /// Function for displaying the highlight sequence
        /// </summary>
        private IEnumerator HighlightSequenceStage()
        {
            roundNumber += 1;

            // Initialize round gameplay data variables
            round = new SquaresRound();
            round.HighlightedSquares = new List<IndexAndPosition>();
            round.RecalledSquares = new List<IndexAndPosition>();
            round.SquareHighlightInterval = squareHighlightInterval; // float value
            round.SquareHighlightDuration = squareHighlightDuration; // float value

            // Randomly select a new square highlight sequence for the new round,
            // by using the existing list of square game objects
            squaresToHighlight = RamGenerator.PickNRandomElems(displayedSquares, numberOfHighlightedSquares);

            // Display the square highlight sequence
            yield return StartCoroutine(HighlightSquareSequence(squaresToHighlight, squareHighlightInterval));

            // Clear the list of squares recalled by the player
            recalledSquares = new List<IndexAndPosition>();

        }

        /// <summary>
        /// Highlight a sequence of squares one by one with each lit up for a predetermined number of seconds.
        /// This function is type IEnumerator because it is called as a coroutine
        /// and ensures that all actions within this function execute
        /// with the correct wait duration in between.
        /// </summary>
        /// <param name="listOfSquares">The list of square game objects to highlight</param>
        /// <param name="durationToWaitBetweenHighlight">The duration of time to wait between highlights of squares</param>
        private IEnumerator HighlightSquareSequence(List<GameObject> listOfSquares, float durationToWaitBetweenHighlight)
        {
            // Go through each square that was selected for highlighting and
            // call HighlightSquare() with the index of square
            foreach (GameObject squareGameObject in listOfSquares)
            {
                // Convert the game object to a IndexAndPosition object to store in gameplay data
                squareIndexAndPosition = GameObjectToIndexAndPosition(squareGameObject);
                // Add this IndexAndPosition object to the round gamedata object 
                round.HighlightedSquares.Add(squareIndexAndPosition);


                // Highlight the square
                StartCoroutine(HighlightSquare(squareIndexAndPosition.Index, squareHighlightDuration));
                /// WaitForSecondsRealtime suspends the coroutine execution for the given amount of seconds using scaled time.
                yield return new WaitForSeconds(durationToWaitBetweenHighlight);
            }
        }

        /// <summary>
        /// Function for getting the recall sequence from player
        /// </summary>
        private IEnumerator RecallSequenceStage()
        {
            // Reset the time variable
            recallTime = 0;

            // Reset the doneButtonpressed value
            doneButtonPressed = false;
            // Make the "done" visible
            doneButton.gameObject.SetActive(true);
            // Script will recognize click inputs
            canClick = true;
            isClicked = false;
            totalSquareClicks = 0;

            // Set the highlight sequence variables for the next round
            PrepareRound();

            yield return null;
        }

        /// <summary>
        /// Executes after the player presses the "done" button. Stores all the information of the round
        /// </summary>
        public IEnumerator EndRound()
        {
            // Reset the "done" button press value
            doneButtonPressed = false;
            // Disable the "done" button
            doneButton.gameObject.SetActive(false);
            // Script will stop recognizing click inputs
            canClick = false;

            yield return null;
        }

        /// <summary>
        /// Function recognizing that the player is done recalling the square sequence
        /// This function is attached to the "done" button
        /// </summary>
        public void DoneButtonPressAction()
        {
            // Record the recall time
            round.RecallTime = recallTime;

            // Toggle the doneButtonPressed boolean so that the game recognizes that the recall sequence stage is complete
            doneButtonPressed = true;

            // Insert the round data into the squares mini-game storage object
            squaresData.Rounds.Add(round);
        }

        /// <summary>
        /// Given a game object, get an index and position object.
        /// </summary>
        /// <param name="targetSquareObject">The game object to be converted to an IndexAndPosition type</param>
        /// <returns>Position2D object with the game object's index and position</returns>
        public IndexAndPosition GameObjectToIndexAndPosition(GameObject targetSquareObject)
        {
            // Get the square script of the square game object
            squareScript = targetSquareObject.GetComponent<Square>();
            // Get the position of the square object
            squarePosition = squareScript.Position;
            // Return a new IndexAndPosition object to hold the position and id of the square game object
            return new IndexAndPosition(squareScript.Id, squarePosition);
        }

        /// <summary>
        /// Executes after the player clicks on a square in the recall sequence stage.
        /// The square clicked is added to the gameplay data variable.
        /// </summary>
        public IEnumerator ClickOnSquare()
        {
            // Highlight the square to indicate the successful click
            StartCoroutine(HighlightSquare(clickedSquareId, CLICK_SQUARE_HIGHLIGHT_DURATION));

            // Convert the game object to a IndexAndPosition object
            squareIndexAndPosition = GameObjectToIndexAndPosition(displayedSquares[clickedSquareId].gameObject);

            totalSquareClicks += 1;
            // Limit the number of clicks recorded to avoid "Out of memory" error
            if (totalSquareClicks < MAXIMUM_RECORDED_CLICKS)
            {
                // Add this IndexAndPosition object to the round gamedata object for recalled squares
                round.RecalledSquares.Add(squareIndexAndPosition);
            }

            isClicked = false;
            canClick = true;

            yield return null;
        }

        /// <summary>
        /// Execute one round of the Squares mini-game. StarCoroutine ensures that the function
        /// inside returns before continuing to the next function. First, the "done" button and clicks must be disabled
        /// since the highlight square sequence stage should not allow submitting a recall sequence. Next, the recall 
        /// stage enables the "done" button and allows the player to click on squares to indicate their answer.
        /// </summary>
        private IEnumerator ExecuteRound()
        {
            // Disable the "done" button and disallow clicks
            yield return StartCoroutine(EndRound());
            // Execute the highlight square sequence stage
            yield return StartCoroutine(HighlightSequenceStage());
            // Enable the "done" button and allow clicks
            yield return StartCoroutine(RecallSequenceStage());
        }

        /// <summary>
        /// Set the level variables for the next round. The number of squares that are highlighted increase
        /// until a maximum of MAX_HIGHLIGHTED_SQUARES at a time
        /// </summary>
        private void PrepareRound()
        {
            if (numberOfHighlightedSquares < MAX_HIGHLIGHTED_SQUARES)
            {
                numberOfHighlightedSquares += 1;
            }
        }

        public void SquarePressed(string squareId)
        {
            if (!isClicked)
            {
                // Save the id of the square that was clicked, passed by the function
                clickedSquareId = int.Parse(squareId);
                isClicked = true;
            }
        }
    }
}
