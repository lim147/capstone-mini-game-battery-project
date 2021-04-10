using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UI;
using Games;
using Helper;
using TMPro;
using System;
using Storage;

namespace PlayModeTests
{
    namespace SquaresTests
    {
        public class SquaresSceneTests
        {
            // Done button for recallPhase
            private Button doneButton;

            // Text game objects shown on screen
            private Text timerValue;
            private TextMeshProUGUI timerText;
            private TextMeshProUGUI doneText;

            // Game objects in the scene hierarchy
            private List<GameObject> displayedSquares;
            private GameObject squareGridArea;
            private GameObject square;
            private CountDownTimer canvas;

            // Values in CountDownTimer attached to SquareCanvas
            private float currentTimeText;
            private float startingTimeText;
            private float gameDuration = 55f;
            // Time duration between each square highlight
            private float squareHighlightInterval = 0.7f;
            // Time duration that square is highlighted for
            private float squareHighlightDuration = 0.7f;

            // Scripts
            // Square.cs script attached to a square gameobject
            private Square squareScript;
            // Squares.cs script attached to SquareGridArea
            private Squares squaresScript;

            // Constants
            private Color UNHIGHLIGHTED_COLOR = new Color32(0xAA, 0xAA, 0xAA, 0xFF);
            private Color HIGHLIGHTED_COLOR = new Color32(0xFF, 0xFF, 0, 0xFF);
            private Color TEXT_COLOR = new Color32(0x0, 0x0, 0x0, 0xFF);
            private Color BUTTON_COLOR = new Color32(0xA0, 0xBB, 0xFF, 0xFF);

            private int totalSquaresHighlighted = 0;

            // Tolerance for comparing decimal values
            private float tolerance = 0.01f;

            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.SQUARES_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                doneButton = GameObject.Find("DoneButton").GetComponent<Button>();

                currentTimeText = ((GameObject.Find("SquareCanvas").GetComponent<Canvas>()).GetComponent<CountDownTimer>()).currentTime;
                currentTimeText = ((GameObject.Find("SquareCanvas").GetComponent<Canvas>()).GetComponent<CountDownTimer>()).startingTime;
                timerValue = GameObject.Find("TimerValue").GetComponent<Text>();
                timerText = GameObject.Find("TimerLabel").GetComponent<TMPro.TextMeshProUGUI>();
                doneText = GameObject.Find("DoneText").GetComponent<TMPro.TextMeshProUGUI>();

                squareScript = GameObject.Find("SquareGridArea").GetComponent<Square>();
                // Gets the instance of the Squares.cs script attached to SquareGridArea
                squaresScript = GameObject.Find("SquareGridArea").GetComponent<Squares>();

                // Gets the list of displayed squares shown on the screen
                displayedSquares = squaresScript.displayedSquares;

                // Gets a single square out of the displayed squares list
                square = displayedSquares[0];

                // Remove event so this method is not re-evaluated
                // if a non-Questionnaire scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> The game timer should start at 55 seconds for this game, which is set when the scene is loaded.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresGameLoads_THEN_TimerStartsAt55()
            {
                float expectedStartTime = 55;

                yield return null;

                Assert.IsTrue(expectedStartTime == currentTimeText, "Current time: " + currentTimeText);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> The squares should show on the screen when the game loads. When the game starts, it immediately goes into the highlight phase, which requires the squares to be active.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresGameLoads_THEN_AllSquaresActive()
            {

                yield return null;

                for (int i = 0; i < displayedSquares.Count; i++)
                {
                    square = displayedSquares[i];
                    Assert.AreEqual(true, square.activeSelf);
                }

            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> The player should know the amount of time left in the game.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresGameLoads_THEN_TimerValueActive()
            {
                yield return null;

                Assert.AreEqual(true, timerValue.gameObject.activeSelf);

            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> It should be clear to the player that the timer value is the amount of time left.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresGameLoads_THEN_TimerTextActive()
            {
                yield return null;

                Assert.AreEqual(true, timerText.gameObject.activeSelf);

            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> The timer value should be clearly visible in front of the background.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresGameLoads_THEN_TimerValueColourBlack()
            {
                yield return null;

                Assert.AreEqual(TEXT_COLOR, timerValue.color);

            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> The timer text should be clearly visible in front of the background.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresGameLoads_THEN_TimerTextColourBlack()
            {
                yield return null;

                Assert.AreEqual(TEXT_COLOR, timerText.color);

            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> When the scene loads, the highlight phase immediately starts, and there should only be one square that is highlighted.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresGameLoads_THEN_SquaresShouldBeTheCorrectColour()
            {

                Color ExpectedColor = UNHIGHLIGHTED_COLOR;
                Color ActualColor;
                totalSquaresHighlighted = 0;
                int expectedSquaresHighlighted = 1;

                for (int i = 0; i < displayedSquares.Count; i++)
                {
                    ActualColor = displayedSquares[i].GetComponent<Image>().color;


                    if (ActualColor != ExpectedColor)
                    {
                        totalSquaresHighlighted += 1;
                        Assert.IsTrue(ActualColor == HIGHLIGHTED_COLOR, "Highlighted Square is the wrong colour");
                    }
                }
                Assert.IsTrue(expectedSquaresHighlighted == 1, "More than one square is highlighted at the same time");
                yield return null;
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> When the scene loads, it is the highlight phase, and the done button should not be clickable.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresGameLoads_THEN_DoneButtonNotActive()
            {
                yield return null;

                Assert.AreEqual(false, doneButton.gameObject.activeSelf);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> During the recall phase, the player should be able to click on the done button to go to the next round, and to indicate that they are done giving input.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_RecallPhase_THEN_DoneButtonActive()
            {
                yield return new WaitForSeconds(3.4f);

                Assert.AreEqual(true, doneButton.gameObject.activeSelf);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> The timer value should be clearly visible in front of the background.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator DoneButtonTextColourBlack()
            {
                yield return null;

                Assert.AreEqual(TEXT_COLOR, doneText.color);

            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> The done button should be the predefined colour so that the done button text is easily visible.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator DoneButtonIsThePredefinedColour()
            {
                // Expected Colour
                ColorBlock expectedColorBlock = new ColorBlock();
                expectedColorBlock.normalColor = BUTTON_COLOR;

                // Actual Colour
                ColorBlock buttonColour = doneButton.colors;

                yield return null;

                // ColorUtility.ToHtmlStringRGBA turns the format of the colour type from RGBA to RGB
                Assert.IsTrue(ColorUtility.ToHtmlStringRGBA(BUTTON_COLOR) == ColorUtility.ToHtmlStringRGBA(buttonColour.normalColor), "Colours do not match. Expected: " + BUTTON_COLOR + ColorUtility.ToHtmlStringRGBA(BUTTON_COLOR) + " Actual: " + ColorUtility.ToHtmlStringRGBA(buttonColour.normalColor));
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> When the amount of time alotted to the game is up, then the player should not be allowed to keep playing.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GameDurationPassed_THEN_MoveToMenuScene()
            {
                yield return new WaitForSeconds(gameDuration);
                Assert.AreEqual(SceneName.MENU_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> The timer value should be within the expected range.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator TimerIsInRange()
            {
                float timeLeft = float.Parse(timerValue.text);
                yield return null;
                Assert.IsTrue(timeLeft >= 0 && timeLeft <= gameDuration);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> When the player clicks on a square, only one square should highlight.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_RecallPhaseAndPlayerClicksOnce_THEN_OneSquareHighlighted()
            {
                // Wait for highlight sequence stage
                yield return new WaitForSeconds(squareHighlightDuration * 3 + squareHighlightInterval * 2);
                // Simulate a click
                displayedSquares[0].GetComponent<Button>().onClick.Invoke();
                yield return null;
                Assert.IsTrue(CheckOneSquareIsHighlighted() == true);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> During the highlight phase, the squares should highlight one by one, to properly show the highlight sequence. This test lasts for 10 rounds.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_HighlightPhase_THEN_SquaresHighlightOneByOne()
            {
                int roundNum = 0;

                for (int j = 0; j < 7; j++)
                {
                    for (int i = 0; i < 2 + roundNum; i++)
                    {
                        Assert.IsTrue(CheckOneSquareIsHighlighted() == true);
                        yield return new WaitForSeconds(squareHighlightDuration);
                    }
                    yield return new WaitForSeconds(squareHighlightDuration);
                    doneButton.onClick.Invoke();
                    roundNum += 1;

                    // Time between the done button is clicked and the next round starts
                    yield return new WaitForSeconds(0.2f);
                }
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Highlighted squares start at 3 squares and increase by 1 square each round until a maximum of 10 squares.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator HighlightedSquaresStartFrom3AndIncreaseBy1Till10()
            {
                int roundNum = 0;
                // List to keep track of the number of highlighted squares in a round
                List<int> highlightedSquares;

                for (int j = 0; j < 8; j++)
                {
                    // Reset the highlighted number of squares list at the beginning of the round
                    highlightedSquares = new List<int>();

                    for (int i = 0; i < (3 + roundNum); i++)
                    {
                        highlightedSquares.Add(GetIndexOfHighlightedSquare());
                        yield return new WaitForSeconds(squareHighlightDuration);
                    }

                    Assert.IsTrue(highlightedSquares.Count == (3 + roundNum), " Expected Num: " + (3 + roundNum) + " Actual Num: " + highlightedSquares.Count);

                    // If more than the expected number of squares are lit up, the done button cannot be clicked
                    doneButton.onClick.Invoke();
                    roundNum += 1;
                    // Time between the done button is clicked and the next round starts
                    yield return new WaitForSeconds(0.1f);
                }
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> In every highlight phase of the game, the same square is not light consecutively.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_HighlightPhase_THEN_NoConsecutiveSquaresInARow()
            {
                int roundNum = 0;
                int indexOfPreviousSquare;
                int indexOfNextSquare;

                for (int j = 0; j < 8; j++)
                {
                    // Record the first square's index
                    indexOfPreviousSquare = GetIndexOfHighlightedSquare();
                    yield return new WaitForSeconds(squareHighlightDuration);

                    for (int i = 0; i < 2 + roundNum; i++)
                    {
                        indexOfNextSquare = GetIndexOfHighlightedSquare();
                        Assert.IsTrue(indexOfPreviousSquare != indexOfNextSquare);

                        indexOfPreviousSquare = indexOfNextSquare;
                        yield return new WaitForSeconds(squareHighlightDuration);
                    }

                    // If more than the expected number of squares are lit up, the done button cannot be clicked
                    doneButton.onClick.Invoke();
                    roundNum += 1;
                    // Time between the done button is clicked and the next round starts
                    yield return new WaitForSeconds(0.1f);
                }
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Ensure that the GetGameplayData function returns data that was collected during the game. This test case tests the situation where the function is called before data is stored.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetGameplayDataFunctionCalledAtBeginning_THEN_EmptyGamePlayDataRetured()
            {
                // Test the storage right at the beginning of the game
                SquaresStorage actualSquaresStorage = Squares.GetGameplayData();
                yield return null;
                List<SquaresRound> expectedSquaresStorage = new List<SquaresRound>();
                Assert.AreEqual(expectedSquaresStorage, actualSquaresStorage.Rounds);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Ensure that the GetGameplayData function returns data that was collected during the game. This test case tests the situation where the function is called after a round has completed
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetGameplayDataFunctionCalled_THEN_GamePlayDataRetured()
            {
                // Wait for highlight sequence stage
                yield return new WaitForSeconds(squareHighlightDuration * 3);

                yield return new WaitForSeconds(0.02f);
                // Click on a square
                displayedSquares[1].GetComponent<Button>().onClick.Invoke();
                yield return new WaitForSeconds(squareHighlightDuration);

                // End the round (And store information)
                doneButton.onClick.Invoke();
                yield return new WaitForSeconds(0.01f);

                // Test the storage after round information is 
                SquaresStorage actualSquaresStorage = Squares.GetGameplayData();

                List<IndexAndPosition> expectedRecalledSquares = new List<IndexAndPosition>();

                // The game object is displayedSquares
                Square squareScript = displayedSquares[1].GetComponent<Square>();
                Position2D expectedPosition2D = squareScript.Position;

                IndexAndPosition expectedIndexAndPosition = new IndexAndPosition(1, expectedPosition2D);
                expectedRecalledSquares.Add(expectedIndexAndPosition);
                double expectedRecallTime = squareHighlightDuration+0.02;

                // Check that the information in the storage is correct
                Assert.AreEqual(3, actualSquaresStorage.Rounds[0].HighlightedSquares.Count);
                Assert.AreEqual(expectedRecalledSquares[0].Index, actualSquaresStorage.Rounds[0].RecalledSquares[0].Index);
                Assert.IsTrue(Math.Abs((expectedRecalledSquares[0].Position.X) - (actualSquaresStorage.Rounds[0].RecalledSquares[0].Position.X)) <= tolerance);
                Assert.IsTrue(Math.Abs((expectedRecalledSquares[0].Position.Y) - (actualSquaresStorage.Rounds[0].RecalledSquares[0].Position.Y)) <= tolerance);
                Assert.IsTrue(Math.Abs(actualSquaresStorage.Rounds[0].RecallTime - expectedRecallTime) <= tolerance);
                Assert.IsTrue(Math.Abs(squareHighlightInterval - actualSquaresStorage.Rounds[0].SquareHighlightInterval) <= tolerance);
                Assert.IsTrue(Math.Abs(squareHighlightDuration - actualSquaresStorage.Rounds[0].SquareHighlightDuration) <= tolerance);
            }

            //----------------- Functional requirement tests begin -------------
            //-----------------------------------------------------------------------

            [Description(@"
            <ul>
                <li><b>Test type:</b> Functional requirement test</li>
                <li><b>Associated SRS requirements:</b> FHS-1 (https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fhs-1), FG-8 (https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-8)</li>
                <li><b>Test description:</b> During the recall phase of the game, the square that was clicked (input) should be highlighted according to the following functional requirements:
                    <ol>
                        <li>The player should be able to select squares and be notified that their input was received. </li>
                        <li>The game should have a function to get key input from the user. When the correct square highlights, it means that the input was received. </li>
                    </ol>
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_RecallPhaseAndPlayerClicksOnSquare_THEN_CorrectSquareHighlights()
            {
                // Wait for highlight sequence stage
                yield return new WaitForSeconds(squareHighlightDuration * 3 + squareHighlightInterval * 2);

                // Check that all squares highlight

                for (int i = 0; i < displayedSquares.Count; i++)
                {
                    // Click on square i
                    displayedSquares[i].GetComponent<Button>().onClick.Invoke();
                    yield return new WaitForSeconds(0.1f);
                    // Ensure that square i is highlighted
                    Assert.IsTrue(displayedSquares[i].GetComponent<Image>().color == HIGHLIGHTED_COLOR);
                    yield return new WaitForSeconds(squareHighlightDuration);
                }
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Functional requirement test</li>
                <li><b>Associated SRS requirements:</b> FHS-1 (https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fhs-1)</li>
                <li><b>Test description:</b> During the recall phase of the game, squares that are clicked should be highlighted. This test ensures that the square highlights for a duration that is visible by the player.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_HighlightPhase_THEN_SquareHighlightsForSquareHighlightDuration()
            {
                // The game starts in the highlight phase, so check immediately that there is a square highlighted
                int indexOfHighlightedSquare1 = GetIndexOfHighlightedSquare();
                // Wait the highlight duration
                yield return new WaitForSeconds(squareHighlightDuration);
                int indexOfHighlightedSquare2 = GetIndexOfHighlightedSquare();
                // After the squareHighlightDuration has passed, then there should not be any square highlighted
                Assert.IsTrue(indexOfHighlightedSquare1 != indexOfHighlightedSquare2);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Functional requirement test</li>
                <li><b>Associated SRS requirements:</b> FHS-2 (https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fhs-2)</li>
                <li><b>Test description:</b> The square sequence in each round must be random. This test tests the first round of the squares mini-game, where there are 3 squares highlighted. The assumption of this mini-game is such that the player is not expected to play the mini-game more than once. However, this test proves that the highlighted square sequence is not the same every round. There is a small chance that this test may fail, since this is based on random elements. However, statistically this test should not fail every time.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator SquaresSequenceIsRandomMax16Squares()
            {
                List<int> previousRound = new List<int>();
                List<int> nextRound = new List<int>();
                int roundNum = 0;

                // Set the values for the previousRound (The first round in the game)
                for (int k = 0; k < 3; k++)
                {
                    previousRound.Add(GetIndexOfHighlightedSquare());
                    yield return new WaitForSeconds(squareHighlightDuration);
                }

                doneButton.onClick.Invoke();
                yield return new WaitForSeconds(0.1f);

                // Check and set the values for all subsequent rounds
                for (int j = 0; j < 9; j++)
                {
                    nextRound = new List<int>();

                    for (int i = 0; i < 4 + roundNum; i++)
                    {
                        nextRound.Add(GetIndexOfHighlightedSquare());
                        yield return new WaitForSeconds(squareHighlightDuration);
                    }

                    // Check that the items in the nextRound are not identical to items in the previous Round

                    // Get the first previousRound.Count items from nextRound
                    List<int> nextRoundSliced = nextRound.GetRange(0, previousRound.Count);


                    int index = 0;
                    while (index < previousRound.Count + 1)
                    {
                        if (previousRound[index] != nextRoundSliced[index])
                        {
                            break;
                        }
                        index += 1;
                    }

                    Assert.IsTrue(index != previousRound.Count, "The two rounds are identical");

                    // Assign the new previousRound
                    previousRound = nextRound.GetRange(0, nextRound.Count);

                    doneButton.onClick.Invoke();
                    roundNum += 1;

                    // Time between the done button is clicked and the next round starts
                    yield return new WaitForSeconds(0.1f);
                }
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Functional requirement test</li>
                <li><b>Associated SRS requirements:</b> FHS-3 (https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fhs-3)</li>
                <li><b>Test description:</b> From the very start of the game, the square objects should exist on the screen and be seen. These are the square objects that will be used to get input from the user.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresGameLoads_THEN_ThereAreSquaresObjects()
            {
                yield return null;

                Assert.IsTrue(displayedSquares.Count > 0, "No squares on screen ");
            }

            //------------------------ System tests begin ----------------------------
            //------------------------------------------------------------------------

            [Description(@"
            <ul>
                <li><b>Test type:</b> System test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> This test will simulate a normal player behaviour, and ensures that under regular conditions, the game works properly.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator Simulate_NormalPlayerBehaviour()
            {
                int roundNum = 0;

                // List to keep track of the indices of highlighted squares in a round
                List<int> highlightedSquares;

                for (int j = 0; j < 6; j++)
                {
                    // Reset the highlighted number of squares list at the beginning of the round
                    highlightedSquares = new List<int>();

                    // Highlight phase
                    for (int i = 0; i < (3 + roundNum); i++)
                    {
                        highlightedSquares.Add(GetIndexOfHighlightedSquare());
                        yield return new WaitForSeconds(squareHighlightDuration);
                    }

                    // Ensure that when the recall phase starts, that there is no highlighted square.
                    Assert.IsTrue(GetIndexOfHighlightedSquare() == -1);

                    // Recall phase
                    for (int i = 0; i < (3 + roundNum); i++)
                    {
                        displayedSquares[highlightedSquares[i]].GetComponent<Button>().onClick.Invoke();
                        yield return new WaitForSeconds(squareHighlightDuration);
                        yield return new WaitForSeconds(0.1f);
                    }

                    // Ensure that when the player is done clicking, there is no highlighted square
                    Assert.IsTrue(GetIndexOfHighlightedSquare() == -1);

                    doneButton.onClick.Invoke();
                    roundNum += 1;

                    // Time between the done button is clicked and the next round starts
                    yield return new WaitForSeconds(0.1f);
                }
            }

            //------------------------ Stress tests begin ----------------------------
            //------------------------------------------------------------------------

            [Description(@"
            <ul>
                <li><b>Test type:</b> Stress test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> This test will simulate a player giving a large number of clicks during the recall and highlight phase, and make sure that the state of the game is such that when the recall phase ends, and the highlight phase ends, there are no unusually highlighted squares.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator Simulate_ExtremePlayerBehaviour()
            {
                int roundNum = 0;

                // Simulate each round
                for (int j = 0; j < 6; j++)
                {
                    // Highlight phase
                    for (int i = 0; i < (3 + roundNum); i++)
                    {
                        // Player clicks on each square 5 times
                        for (int k = 0; k < 5; k++)
                        {
                            for (int l = 0; l < displayedSquares.Count; l++)
                            {
                                displayedSquares[l].GetComponent<Button>().onClick.Invoke();
                            }
                        }
                        yield return new WaitForSeconds(squareHighlightDuration);
                    }

                    // Ensure that when the recall phase starts, that there is no highlighted squares from the recall phase.
                    yield return new WaitForSeconds(1f);
                    Assert.IsTrue(GetIndexOfHighlightedSquare() == -1);


                    // Recall phase
                    for (int i = 0; i < (3 + roundNum); i++)
                    {
                        // Player clicks on each square 5 times
                        for (int k = 0; k < 5; k++)
                        {
                            for (int l = 0; l < displayedSquares.Count; l++)
                            {
                                displayedSquares[l].GetComponent<Button>().onClick.Invoke();
                            }
                        }
                    }

                    // Ensure that when the player is done clicking, there is no squares that remain highlighted
                    yield return new WaitForSeconds(1f);
                    Assert.IsTrue(GetIndexOfHighlightedSquare() == -1);

                    doneButton.onClick.Invoke();
                    roundNum += 1;

                    // Time between the done button is clicked and the next round starts
                    yield return new WaitForSeconds(0.1f);
                }
            }

            //------------------------ Helper functions begin ----------------------------
            //------------------------------------------------------------------------

            /// <summary>
            /// Helper function for testing, returns a boolean indicating if only one square is highlighted.
            /// </summary>
            public bool CheckOneSquareIsHighlighted()
            {
                // Initialized square highlight count
                int totalSquaresHighlighted = 0;
                Color ExpectedColor = UNHIGHLIGHTED_COLOR;
                Color ActualColor;

                for (int i = 0; i < displayedSquares.Count; i++)
                {
                    ActualColor = displayedSquares[i].GetComponent<Image>().color;

                    if (ActualColor != ExpectedColor)
                    {
                        totalSquaresHighlighted += 1;
                    }
                }
                return (totalSquaresHighlighted == 1);
            }

            /// <summary>
            /// Helper function for testing, returns the index number of the square that is highlighted. If there is no square highlighted, then -1 is return.
            /// </summary>
            public int GetIndexOfHighlightedSquare()
            {
                Color ExpectedColor = UNHIGHLIGHTED_COLOR;
                Color ActualColor;

                for (int i = 0; i < displayedSquares.Count; i++)
                {
                    ActualColor = displayedSquares[i].GetComponent<Image>().color;

                    if (ActualColor != ExpectedColor)
                    {
                        return i;
                    }
                }
                return -1;
            }
        }
    }
}
