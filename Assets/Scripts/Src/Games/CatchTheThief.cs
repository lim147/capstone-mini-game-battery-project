using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Storage;
using Helper;
using UnityEngine.UI;

namespace Games
{
    /// <summary>
    /// This module implements [CatchTheThief Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#catchthethief-module)
    /// found in the Architecture and Module Design Document.
    /// 
    /// This module is described in the [SRS](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#game-catchthethief-measuring-selective-visual-perception).
    ///
    /// <div class="WARNING">
    ///     <h5>ASSUMPTION</h5>
    ///     <p>
    ///     <ul>
    ///         <li>This script is attached to the `SquareGridArea` object in
    ///           `CatchTheThiefScene`
    ///         </li>
    ///         <li>`displayedSquares` is populated in the Unity Editor with
    ///           the individual square objects in `SquareGridArea`
    ///         </li>
    ///     </ul>
    ///     </p>
    /// </div>
    /// <div class="NOTE">
    ///     <h5>NOTATION</h5>
    ///     <p>
    ///     <ul>
    ///         <li><img src="../images/thief.png"></img>
    ///         <b>Thief</b>: the character
    ///         who the player wants to identify by activating the input trigger
    ///         </li>
    ///         <li><img src="../images/person.png"></img>
    ///         <b>Person</b>: the character
    ///         for whom the player <i>does not</i> want to activate the input
    ///         trigger
    ///         </li>
    ///     </ul>
    ///     </p>
    /// </div>
    /// </summary>
    public class CatchTheThief : MonoBehaviour
    {
        // Images type for the different type of images that may appear in the game
        private enum Images
        {
            THIEF,
            PERSON,
            BLANK
        }


        // Public state variables

        /// <summary>
        /// List of square game objects on which a thief or person image may appear
        /// </summary>
        public List<GameObject> displayedSquares;

        /// <summary>
        /// Text to indicate to the user how many thieves they have caught so far.
        /// </summary>
        public Text CaughtValueText;

        /// <summary>
        /// The sound to play when the correct key is pressed.
        /// </summary>
        public AudioSource Beep;

        public int numberOfCaughtThieves = 0;

        // Local state variables:
        // Amount of time spent in one round of the mini-game, in seconds
        private float roundDuration;
        // Number of squares in the mini-game
        private int numberOfSquaresInGrid;
        // Boolean to determine if the thief appears in this round
        public bool thiefAppearInRound;
        // Boolean to determine if the person appears in this round
        public bool personAppearInRound;
        // Duration of time between when images appear and player presses the ThiefIdentified key
        public float identifiedKeyPressTime;
        // Boolean to keep track if the player pressed the ThiefIdentified key in this round
        public bool isIdentifiedKeyPressed;
        // Round number of the "Catch The Thief" mini-game
        private int roundNumber;
        // Variable to hold the person image game object
        private GameObject personImage;
        // Variable to hold the thief image game object
        private GameObject thiefImage;
        // Boolean to keep track of if the round is over
        private bool isRoundOver;
        // Number of regular people that will appear in this round
        private int numberOfPeople;
        // List of thief, person, and blank images that will appear in a round, where the index of the images in this list indicates the index of the square the image will appear on
        private List<Images> imageOrder;
        // Variable to keep track of the index of the square that the thief appeared on if the thief appeared in the previous round.
        // Since there is no indication between each round in the game, it is possible that the thief may appear on the same square twice in two consecutive rounds.
        // The player is only told to press the ThiefIndicated key once if the thief appears by itself, so the player may accidentally lose marks from their score in this scenario.
        // If the thief did not appear in the previous round, or there was no previous round, this value is 0.
        private int indexThiefAppearedLastRound;

        // Constant State Variables:
        // Minimum number of regular people when regular person appears in a round
        private const int MIN_PEOPLE = 1;
        // Maximum number of regular people when regular people appear in a round
        private const int MAX_PEOPLE = 3;
        // Minimum amount of round time
        private const float MIN_ROUND_TIME = 0.5f;
        // Maximum amount of round time
        private const float MAX_ROUND_TIME = 1.5f;
        // Amount that the round duration decrements by in the next round
        private const float ROUND_TIME_DECREMENT = 0.05f;

        // Variables for storage purposes:
        // Datatype to hold all game data in the "Catch The Thief" mini-game
        public static CatchTheThiefStorage ctfData;
        // Datatype to hold game data for every round in "Catch The Thief" mini-game
        public CatchTheThiefRound round;

        // To show the number of thieves, set for testing
        private int showThiefCount = 0;
        // To show the number of persons, set for testing
        private int showPersonCount = 0;

        /// <summary>
        /// Start is a a predefined function and is called when the attached scene is loaded.
        /// It prepares the game variables for the "Catch The Thief" mini-game.
        /// </summary>
        void Start()
        {
            // Initialize game variables
            CTF_init();

            // Begin the first round of the Catch The Thief mini-game
            StartCoroutine(ExecuteRound());
        }

        /// <summary>
        /// Update is called once per frame. It updates time related variables,
        /// recognizes ThiefIdentified key presses, and executes the stages in the Catch The Thief mini-game
        /// </summary>
        void Update()
        {
            // Update the timer values
            identifiedKeyPressTime += 1 * Time.deltaTime;

            // If the player presses the input game key
            if (Input.GetButtonDown("ThiefIdentified") && !isIdentifiedKeyPressed)
            {
                Beep.Play();

                // The player may only press a key once in a round
                // If a key is pressed multiple times, the first time of pressing will only be recorded
                isIdentifiedKeyPressed = true;

                // Update the storage object
                round.identifiedKeyPressTime = identifiedKeyPressTime;

                UpdateCaughtThiefCount();
            }

            // If the round is over, start a new round
            if (isRoundOver)
            {
                StartCoroutine(ExecuteRound());
            }

            // For all other keys that are pressed, add the key to UnidentifiedKeys list
            foreach (KeyCode keyThatWasPressed in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyThatWasPressed) && !Input.GetButtonDown("ThiefIdentified"))
                {
                    round.UnidentifiedKeysPressed.Add(new TimeAndKey(identifiedKeyPressTime, keyThatWasPressed));
                }
            }
        }

        /// <summary>
        /// Updates and displays number of caught thieves.
        /// </summary>
        public void UpdateCaughtThiefCount()
        {
            if (thiefAppearInRound && !personAppearInRound)
            {
                numberOfCaughtThieves++;
                CaughtValueText.text = numberOfCaughtThieves.ToString();
            }
        }


        /// <summary>
        /// Getter function for the pressing time during the Catch the Thief game
        /// This function is set for testing
        /// </summary>
        public void GetPressTime()
        {
            if (!isIdentifiedKeyPressed)
            {
                isIdentifiedKeyPressed = true;

                // Update the storage object
                round.identifiedKeyPressTime = identifiedKeyPressTime;
            }
        }

        /// <summary>
        /// Getter function for getting the keyPresstime
        /// This function is set for testing
        /// </summary>
        /// <returns>KeyPressing of each round of the game</returns>
        public double GetPlayerCheckTime()
        {
            return round.identifiedKeyPressTime;
        }

        /// <summary>
        /// Getter function for getting the variable showPersonCount
        /// This function is set for testing
        /// </summary>
        /// <returns>The variable showPersonCount </returns>
        public int GetShowPersonCount()
        {
            return showPersonCount;
        }

        /// <summary>
        /// Getter function for getting the variable showThiefCount
        /// This function is set for testing
        /// </summary>
        /// <returns>The variable showThiefCount </returns>
        public int GetShowThiefCount()
        {
            return showThiefCount;
        }


        /// <summary>
        /// Getter function for the "Catch the thief" gameplay data
        /// </summary>
        /// <returns>The gameplay data of the "Catch the thief"  mini-game.
        /// </returns> 
        public static CatchTheThiefStorage GetGameplayData()
        {
            return ctfData;
        }



        //Helper functions start:
        //-----------------------------------------------------------------

        /// <summary>
        /// Initializes all state variables and objects
        /// necessary for the Catch The Thief mini-game.
        /// </summary>
        private void CTF_init()
        {
            // Initialize the storage variables
            ctfData = new CatchTheThiefStorage();

            ctfData.Rounds = new List<CatchTheThiefRound>();

            // Initialize game variables
            identifiedKeyPressTime = 0;
            roundNumber = 0;
            // The game starts with the maximum round duration
            roundDuration = MAX_ROUND_TIME;
            numberOfSquaresInGrid = displayedSquares.Count;
            indexThiefAppearedLastRound = 0;

            // Make sure that the game starts with all images disabled
            ClearSquares();
        }

        /// <summary>
        /// Initializes or resets the square game objects such that there are no person or thief images
        /// showing on the screen.
        /// </summary>
        private void ClearSquares()
        {
            // Ensure that the game starts with no images on the screen
            foreach (GameObject square in displayedSquares)
            {
                // Get the person image of the current square
                GameObject personImage = square.transform.Find("Person").gameObject;
                personImage.SetActive(false);
                // Get the thief image of the current square
                GameObject thiefImage = square.transform.Find("Thief").gameObject;
                thiefImage.SetActive(false);
            }
        }

        /// <summary>
        /// Calculates the number of thieves and people images that will appear in the round. The imageOrder
        /// list will be updated accordingly.
        /// </summary>
        private void FillImageList()
        {
            // Randomly determine whether thief or person images should appear
            // And make sure at least one of thief and person appears
            thiefAppearInRound = RamGenerator.GenerateARandomBool();
            if (thiefAppearInRound)
            {
                // personAppearInRound could be either true or false
                personAppearInRound = RamGenerator.GenerateARandomBool();
                // Add thief image type to the list of images that will appear
                imageOrder.Add(Images.THIEF);
            }
            else
            {
                // personAppearInRound should be true
                personAppearInRound = true;
            }

            if (personAppearInRound)
            {
                // Determine the number of people that will appear this round
                numberOfPeople = RamGenerator.GenerateARamInt(MIN_PEOPLE, MAX_PEOPLE);
                // Add one person image type to the list for each person that will appear this round
                for (int count = 0; count < numberOfPeople; count += 1)
                {
                    imageOrder.Add(Images.PERSON);
                }
            }

            // For all the remaining square game objects, they will be left blank
            while (imageOrder.Count < 9)
            {
                imageOrder.Add(Images.BLANK);
            }
        }

        /// <summary>
        /// Reset or Calculate the level variables for the next round.
        /// </summary>
        private IEnumerator PrepareRound()
        {
            // Initialize round gameplay data variables
            roundNumber += 1;
            identifiedKeyPressTime = 0;
            isRoundOver = false;
            isIdentifiedKeyPressed = false;
            // In each round there is a new image order
            imageOrder = new List<Images>();

            // Decrease round duration as the number of rounds increase, until it reaches the minimum
            // round duration time
            if (roundDuration > MIN_ROUND_TIME)
            {
                roundDuration -= ROUND_TIME_DECREMENT;
            }

            // Calculate whether thief or person images will appear in this round
            FillImageList();

            // Set the round variables in the game storage
            round = new CatchTheThiefRound();
            round.UnidentifiedKeysPressed = new List<TimeAndKey>();
            round.ThiefAppearInRound = thiefAppearInRound;
            round.PersonAppearInRound = personAppearInRound;

            // Randomize the list of images that will appear in this round
            imageOrder = RamGenerator.PickNRandomElems(imageOrder, numberOfSquaresInGrid);
            // If the thief has been randomized to appear on the same square as in the last round
            while (imageOrder[indexThiefAppearedLastRound] == Images.THIEF)
            {
                // Randomize the list of images again
                imageOrder = RamGenerator.PickNRandomElems(imageOrder, numberOfSquaresInGrid);
            }

            yield return null;
        }

        /// <summary>
        /// Executes after the round duration has completed, saves round variables, and sets variable to
        /// execute the next round.
        /// </summary>
        private IEnumerator EndRound()
        {
            // Set round variable
            round.IsIdentifiedKeyPressed = isIdentifiedKeyPressed;

            // If the player did not press a key in this round, set the KeyIsPressed time to -1
            if (!isIdentifiedKeyPressed)
            {
                round.identifiedKeyPressTime = -1;
            }
            // Clear the images on the screen
            ClearSquares();
            // Set the boolean to true to allow the update function to call the next round
            isRoundOver = true;

            // Insert the round data into the catch the thief mini-game storage object
            ctfData.Rounds.Add(round);

            yield return null;
        }

        /// <summary>
        /// Displays the person and thief images on the screen.
        /// </summary>
        /// <param name="imagesToDisplay">List of Image types,
        /// where the indice indicates the position of the image type that will appear.</param>
        private IEnumerator DisplayImages(List<Images> imagesToDisplay)
        {
            for (int i = 0; i < imagesToDisplay.Count; i++)
            {
                if (imagesToDisplay[i] == Images.THIEF)
                {
                    // Get the thief image for the corresponding square in the grid
                    thiefImage = displayedSquares[i].transform.Find("Thief").gameObject;
                    // Set the thief image as active to display
                    thiefImage.SetActive(true);
                    // Update the thief index variable
                    indexThiefAppearedLastRound = i;

                    if (i == 0)
                    {
                        showThiefCount++;

                    }

                }
                else if (imagesToDisplay[i] == Images.PERSON)
                {
                    // Get the thief image for the corresponding square in the grid
                    personImage = displayedSquares[i].transform.Find("Person").gameObject;
                    // Set the thief image as active to display
                    personImage.SetActive(true);
                    if (i == 0)
                    {
                        showPersonCount++;
                    }
                }
            }
            yield return null;
        }

        /// <summary>
        /// Displays the person and thief images on the screen.
        /// </summary>
        private IEnumerator WaitRoundDuration()
        {
            yield return new WaitForSeconds(roundDuration);
        }

        /// <summary>
        /// Execute one round of the CatchTheThief mini-game. StarCoroutine ensures that the function
        /// inside returns before continuing to the next function. First, the round variables are prepared.
        /// Next, the images are shown on the screen. Then the game starts for the game duration. Lastly,
        /// post-round variables are saved, and enables the next round to be executed.
        /// </summary>
        private IEnumerator ExecuteRound()
        {
            // Initialize and calculate variables for the round
            yield return StartCoroutine(PrepareRound());
            // Display the images for the round on the screen
            yield return StartCoroutine(DisplayImages(imageOrder));
            // Start the timer for the round duration and the timer to record when user input is received
            yield return StartCoroutine(WaitRoundDuration());
            // Save round variables, and set variable to execute next round
            yield return StartCoroutine(EndRound());
        }
    }
}
