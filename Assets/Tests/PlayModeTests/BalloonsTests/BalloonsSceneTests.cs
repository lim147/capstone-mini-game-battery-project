using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System;
using UI;
using Games;
using Storage;
using Helper;

namespace PlayModeTests
{
    namespace BalloonsTests
    {

        public class BalloonsSceneTests
        {
            // Balloon game object
            private GameObject balloon;

            // Balloon Frame objects:
            private GameObject balloonFrame1;
            private GameObject balloonFrame2;
            private GameObject balloonFrame3;

            // Timer text
            private Text timer;

            // Transparent test button
            private GameObject testButton;

            // Balloons.cs script attached to canvas
            Balloons balloonScript;

            // Tolerance for comparing decimal valus
            private float tolerance = 0.01f;

            // Range for the balloon positions
            private float MINX = -350f;
            private float MAXX = 350f;
            private float MINY = -200f;
            private float MAXY = 140f;

            // Range for the balloon size
            private double MAX_SIZE = 200;
            private double MIN_SIZE = 30;
            // Gap in decrement
            private const float BALLOON_SIZE_DECREMENT = 10f;

            // Time duration for the game
            private float timeDuration = 40f;


            /// <summary>
            /// Load the scene for playmode unit tests.
            /// </summary>
            /// The methods with SetUp attribute will be run once before every unit test.
            [SetUp]
            public void LoadScene()
            {
                // Load game scene
                SceneManager.LoadScene(SceneName.BALLOONS_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                // Link game object to local variable; throw out exception if there is no such game object
                balloon = GameObject.Find("Balloon");
                Assert.IsNotNull(balloon, "Missing gameobject: " + "balloon");

                timer = GameObject.Find("TimerValue").GetComponent<Text>();
                Assert.IsNotNull(timer, "Missing gameobject: " + "timer");

                testButton = GameObject.Find("TransparentTestButton");
                Assert.IsNotNull(testButton, "Missing gameobject: " + "transparent test button");

                balloonScript = GameObject.Find("Canvas").GetComponent<Balloons>();
                Assert.IsNotNull(balloonScript, "Balloon.cs script is not attached to canvas");

                // Remove event so this method is not re-evaluated
                // if a non-BalloonsInstruction scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }





            //----------------- Tests driven by Functional Requirements -------------
            //-----------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-8'>FG-8</a></li>
            <li><b>Test description:</b> Tests that the ClickOnBalloon function
                detects the player click input event, and the UI reacts through
                a change of position of the balloon.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickOnBalloonFunctionCalled_THEN_BalloonPositionChanged()
            {
                // Balloon position before function call
                Vector3 oldP = balloon.transform.localPosition;
                // Get rid of the z value, as it's in 2D space
                Vector2 oldPos2D = new Vector2(oldP.x, oldP.y);

                // Call ClickOnBalloon function
                balloonScript.ClickOnBalloon();

                yield return new WaitForSeconds(0.01f);

                // balloon position after function call
                Vector3 newP = balloon.transform.localPosition;
                Vector2 newPos2D = new Vector2(newP.x, newP.y);

                Assert.AreNotEqual(oldPos2D, newPos2D);
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#cm-1'>CM-1</a></li>
            <li><b>Test description:</b> Tests that when the balloon is clicked, 
                the balloon position will be changed.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickOnBalloon_THEN_BalloonPositionChanged()
            {
                // Position before click
                Vector3 oldP = balloon.transform.localPosition;
                // Get rid of the z value, as it's in 2D space
                Vector2 oldPos2D = new Vector2(oldP.x, oldP.y);

                // Silulate a click automatically:
                balloon.GetComponent<Button>().onClick.Invoke();

                yield return new WaitForSeconds(0.01f);

                // Position after click
                Vector3 newP = balloon.transform.localPosition;
                Vector2 newPos2D = new Vector2(newP.x, newP.y);

                Assert.AreNotEqual(oldPos2D, newPos2D);

            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#cm-2'>CM-2</a></li>
            <li><b>Test description:</b> Tests that when the balloon is clicked, 
                an explosion animation is displayed that lasts for one second.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickOnBalloon_THEN_ExplosionAnimationDisplay()
            {
                // Silulate a click automatically:
                balloon.GetComponent<Button>().onClick.Invoke();

                // Give 0.01s for the system to react
                yield return new WaitForSeconds(0.01f);

                // Animaiton starts
                balloonFrame1 = GameObject.Find("BalloonFrame1");
                balloonFrame2 = GameObject.Find("BalloonFrame2");
                balloonFrame3 = GameObject.Find("BalloonFrame3");
                Assert.IsTrue(balloonFrame1 != null || balloonFrame2 != null || balloonFrame3 != null);

                // After 0.1s
                yield return new WaitForSeconds(0.1f);

                // Animation ends
                balloonFrame1 = GameObject.Find("BalloonFrame1");
                balloonFrame2 = GameObject.Find("BalloonFrame2");
                balloonFrame3 = GameObject.Find("BalloonFrame3");
                Assert.IsTrue(balloonFrame1 == null && balloonFrame2 == null && balloonFrame3 == null);

            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#cm-3'>CM-3</a></li>
            <li><b>Test description:</b> Tests that the balloon is NOT clicked, 
                the balloon position is unchanged.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickNotOnBalloon_THEN_BalloonPositionNotChanged()
            {
                // Balloon position before click
                Vector3 oldP = balloon.transform.localPosition;
                // Get rid of the z value, as it's in 2D space
                Vector2 oldPos2D = new Vector2(oldP.x, oldP.y);

                // Simulate a click not on balloon:
                // Since the balloon size is weight = height <= 200, so if we set the testButton position = balloon position + (300, 300)
                // there won't be overlap between balloon and testButton.
                // Thus, simulating a click on textButton is the same as simulating a click NOT on balloon.
                testButton.transform.localPosition = new Vector3(oldP.x + 300, oldP.y + 300, 0);
                testButton.GetComponent<Button>().onClick.Invoke();

                yield return new WaitForSeconds(0.01f);

                // Balloon position after click
                Vector3 newP = balloon.transform.localPosition;
                Vector2 newPos2D = new Vector2(newP.x, newP.y);

                Assert.AreEqual(oldPos2D, newPos2D);
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#cm-4'>CM-4</a></li>
            <li><b>Test description:</b> Tests that the balloon appears in a 
                random position in each round.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_NewRoundStart_THEN_BalloonAppearsOnRandomPosition()
            {
                // Give 0.1s for system to finish set up
                yield return new WaitForSeconds(0.1f);

                balloon.GetComponent<Button>().onClick.Invoke();
                yield return new WaitForSeconds(0.01f);

                // Position for round 1:
                Vector3 round1P = balloon.transform.localPosition;

                balloon.GetComponent<Button>().onClick.Invoke();
                yield return new WaitForSeconds(0.01f);
                balloon.GetComponent<Button>().onClick.Invoke();
                yield return new WaitForSeconds(0.01f);

                // Position for round 2:
                Vector3 round2P = balloon.transform.localPosition;

                Assert.AreNotEqual(round1P, round2P);

            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#cm-5'>CM-5</a></li>
            <li><b>Test description:</b> Tests that as Balloons game ends, 
                it will jump back to the menu page.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TimeUp_THEN_MoveToMenuScene()
            {
                float timeLeft = float.Parse(timer.text);
                yield return new WaitForSeconds(timeLeft);
                Assert.AreEqual(SceneName.MENU_SCENE, SceneManager.GetActiveScene().name);
            }




            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public functions

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetGameplayData function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetGameplayDataFunctionCalled_THEN_GamePlayDataRetured()
            {
                BalloonsStorage balloonsData1 = Balloons.GetGameplayData();
                List<BalloonsRound> expectedRoundsData1 = new List<BalloonsRound>();
                Assert.AreEqual(expectedRoundsData1, balloonsData1.Rounds);

                balloonScript.ClickOnBalloon();
                yield return new WaitForSeconds(0.01f);


                // After the click on balloon that is located on center point:
                BalloonsStorage balloonsData2 = Balloons.GetGameplayData();
                List<BalloonsRound> expectedRoundsData2 = new List<BalloonsRound>();
                Assert.AreEqual(expectedRoundsData2, balloonsData2.Rounds);

                // Take 0.01s to click on the balloon that is in random position
                balloonScript.ClickOnBalloon();
                yield return new WaitForSeconds(0.01f);

                // After the click on balloon that is located on random position:
                BalloonsStorage balloonsData3 = Balloons.GetGameplayData();

                double expectedBalloonSize = 200;
                List<TimeAndPosition> expectedClicks = new List<TimeAndPosition>();
                Assert.AreEqual(1, balloonsData3.Rounds.Count);
                Assert.IsTrue(Math.Abs(expectedBalloonSize - balloonsData3.Rounds[0].BalloonSize) <= tolerance);
                Assert.AreEqual(expectedClicks, balloonsData3.Rounds[0].Clicks);

            }







            //------------------------ Integration Tests begin -----------------------
            //------------------------------------------------------------------------
            [Description(@"
        <ul>
            <li><b>Test type:</b> Integration Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test balloon position is in center when game starts.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator BalloonPosStartFromCenter()
            {
                float posX = balloon.transform.localPosition.x;
                float posY = balloon.transform.localPosition.y;

                yield return null;
                Assert.IsTrue(Math.Abs(posX) <= tolerance);
                Assert.IsTrue(Math.Abs(posY) <= tolerance);
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Integration Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Tests if the balloon object position
                is always in the acceptable range.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator BalloonPositionIsInRange()
            {
                balloon = GameObject.Find("Balloon");
                float posX = balloon.transform.localPosition.x;
                float posY = balloon.transform.localPosition.y;

                bool xIsInRange = (posX <= MAXX) && (posX >= MINX);
                bool yIsInRange = (posY <= MAXY) && (posX >= MINY);

                yield return null;
                Assert.IsTrue(xIsInRange && yIsInRange);



            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Integration Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Tests if the balloon object size
                is always in the acceptable range.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator BalloonSizeIsInRange()
            {

                float width = ((RectTransform)balloon.transform).rect.width;
                float height = ((RectTransform)balloon.transform).rect.height;


                bool widthIsInRange = (width <= MAX_SIZE) && (width >= MIN_SIZE);
                bool heightIsInRange = (height <= MAX_SIZE) && (height >= MIN_SIZE);

                yield return null;
                Assert.IsTrue(widthIsInRange && heightIsInRange);

            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Integration Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test Timer value is in range.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TimerIsInRange()
            {
                float timeLeft = float.Parse(timer.text);
                yield return null;
                Assert.IsTrue(timeLeft >= 0 && timeLeft <= timeDuration);

            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Integration Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test when game starts, there balloon frame objects are disabled.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GameStart_THEN_BalloonFrameObjectsAreNotActive()
            {

                yield return null;

                balloonFrame1 = GameObject.Find("BalloonFrame1");
                balloonFrame2 = GameObject.Find("BalloonFrame2");
                balloonFrame3 = GameObject.Find("BalloonFrame3");

                Assert.IsNull(balloonFrame1);
                Assert.IsNull(balloonFrame2);
                Assert.IsNull(balloonFrame3);

            }







            //------------------------ System Tests begin ----------------------------
            //------------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> This test will simulate a normal player
                behaviour, which provides 35 clicks on the balloon object. Then,
                it checks if the resulting game states are as intended.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Simulate_NormalPlayerBehaviour()
            {
                // The number of valid clicks that will be simulated
                int validClicks = 35; //should be odd number

                // For each click, check the game state
                while (validClicks > 0)
                {
                    // Before click:
                    Vector3 oldP = balloon.transform.localPosition;
                    Vector2 oldPos2D = new Vector2(oldP.x, oldP.y);

                    float oldWidth = ((RectTransform)balloon.transform).rect.width;
                    float oldHeight = ((RectTransform)balloon.transform).rect.height;

                    // Silulate a click automatically:
                    balloon.GetComponent<Button>().onClick.Invoke();

                    yield return new WaitForSeconds(0.01f);

                    // After click
                    Vector3 newP = balloon.transform.localPosition;
                    Vector2 newPos2D = new Vector2(newP.x, newP.y);

                    float newWidth = ((RectTransform)balloon.transform).rect.width;
                    float newHeight = ((RectTransform)balloon.transform).rect.height;

                    // Tests:

                    // Position is changed:
                    Assert.AreNotEqual(oldPos2D, newPos2D);

                    // After Odd click, the balloon position should not be on center (0, 0)
                    if (validClicks % 2 == 1)
                    {
                        Assert.IsTrue(Math.Abs(newPos2D.x) > tolerance && Math.Abs(newPos2D.x) > tolerance);
                    }
                    else // After Even click, the balloon position should not on center
                    {
                        Assert.IsTrue(Math.Abs(newPos2D.x) <= tolerance && Math.Abs(newPos2D.x) <= tolerance);
                    }


                    // Size is no greater than before:
                    Assert.IsTrue(newWidth <= oldWidth && newHeight <= oldHeight);
                    // Size is decreased by BALLOON_SIZE_DECREMENT:
                    Assert.IsTrue(
                        (Math.Abs(oldWidth - BALLOON_SIZE_DECREMENT - newWidth) <= tolerance && Math.Abs(oldHeight - BALLOON_SIZE_DECREMENT - newHeight) <= tolerance) // the difference between old and new should be no more than tolerance + BALLOON_SIZE_DECREMENT
                     || ((oldWidth - newWidth) <= tolerance && Math.Abs(oldHeight - newHeight) <= tolerance)
                                 );

                    // wait for 1s before go to next round.
                    yield return new WaitForSeconds(1f);

                    validClicks -= 1;

                }

            }




            //------------------------ Stress Tests begin ----------------------------
            //------------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Stress Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> This test will simulate 
                behaviour under extreme simutations, which provides 555 clicks
                on the balloon object. Then, it checks if the resulting game
                states are as intended.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Simulate_ExtremeBehaviour()
            {
                // The number of valid clicks that will be simulated
                int validClicks = 555; //should be odd number

                // For each click, check the game state
                while (validClicks > 0)
                {
                    // Before click:
                    Vector3 oldP = balloon.transform.localPosition;
                    Vector2 oldPos2D = new Vector2(oldP.x, oldP.y);

                    float oldWidth = ((RectTransform)balloon.transform).rect.width;
                    float oldHeight = ((RectTransform)balloon.transform).rect.height;

                    // Silulate a click automatically:
                    balloon.GetComponent<Button>().onClick.Invoke();

                    yield return new WaitForSeconds(0.01f);

                    // After click
                    Vector3 newP = balloon.transform.localPosition;
                    Vector2 newPos2D = new Vector2(newP.x, newP.y);

                    float newWidth = ((RectTransform)balloon.transform).rect.width;
                    float newHeight = ((RectTransform)balloon.transform).rect.height;

                    // Tests:

                    // Position is changed:
                    Assert.AreNotEqual(oldPos2D, newPos2D);

                    // After Odd click, the balloon position should not be on center (0, 0)
                    if (validClicks % 2 == 1)
                    {
                        Assert.IsTrue(Math.Abs(newPos2D.x) > tolerance && Math.Abs(newPos2D.x) > tolerance);
                    }
                    else // After Even click, the balloon position should not on center
                    {
                        Assert.IsTrue(Math.Abs(newPos2D.x) <= tolerance && Math.Abs(newPos2D.x) <= tolerance);
                    }


                    // Size is no greater than before:
                    Assert.IsTrue(newWidth <= oldWidth && newHeight <= oldHeight);
                    // Size is decreased by BALLOON_SIZE_DECREMENT:
                    Assert.IsTrue(
                        (Math.Abs(oldWidth - BALLOON_SIZE_DECREMENT - newWidth) <= tolerance && Math.Abs(oldHeight - BALLOON_SIZE_DECREMENT - newHeight) <= tolerance) // the difference between old and new should be no more than tolerance + BALLOON_SIZE_DECREMENT
                     || ((oldWidth - newWidth) <= tolerance && Math.Abs(oldHeight - newHeight) <= tolerance)
                                 );

                    // wait for 0.01s before go to next round.
                    yield return new WaitForSeconds(0.01f);

                    validClicks -= 1;
                }
            }


        }
    }

}


