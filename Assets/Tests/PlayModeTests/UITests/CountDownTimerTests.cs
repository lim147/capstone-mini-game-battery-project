using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PlayModeTests
{
    namespace UITests
    {
        public class CountDownTimerTests
        {
            // count down timer object
            private CountDownTimer timerObject;

            // tolerance for comparing decimals
            private float tolerance = 0.01f;

            [SetUp]
            public void CreateTimerObject()
            {
                timerObject = new CountDownTimer();

            }

            //----------------- Tests driven by Functional Requirements -------------
            //-----------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-6'>FG-6</a></li>
            <li><b>Test description:</b> Test Timer behaves properly in Balloons game.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestTimerInBalloonsGame()
            {
                SceneManager.LoadScene(SceneName.BALLOONS_SCENE);
                yield return null;

                // Test Balloons game has timer text
                Text timerInBalloons = GameObject.Find("TimerValue").GetComponent<Text>();
                Assert.IsNotNull(timerInBalloons, "Balloons game missing gameobject: " + "timer");

                // Test that Balloons timer starts from 40
                float expectedStartingTime = 40f;
                float actualStaringTime = float.Parse(timerInBalloons.text);
                Assert.IsTrue(Math.Abs(expectedStartingTime - actualStaringTime) <= tolerance);

                // Test the timer text color changes to red when it's less than 5s
                yield return new WaitForSeconds(35);
                Assert.AreEqual(Color.red, timerInBalloons.color);

                // Test when time is up, move to menu page
                yield return new WaitForSeconds(float.Parse(timerInBalloons.text));
                Assert.AreEqual(SceneName.MENU_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-6'>FG-6</a></li>
            <li><b>Test description:</b> Test Timer behaves properly in squares game.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestTimerInSquaresGame()
            {
                SceneManager.LoadScene(SceneName.SQUARES_SCENE);
                yield return null;

                // Test Squares game has timer text
                Text timerInSquares = GameObject.Find("TimerValue").GetComponent<Text>();
                Assert.IsNotNull(timerInSquares, "Squares game missing gameobject: " + "timer");

                // Test squares timer starts from 55
                float expectedStartingTime = 55f;
                float actualStaringTime = float.Parse(timerInSquares.text);
                Assert.IsTrue(Math.Abs(expectedStartingTime - actualStaringTime) <= tolerance);

                // Test the timer text color changes to red when it's less than 5s
                yield return new WaitForSeconds(50);
                Assert.AreEqual(Color.red, timerInSquares.color);

                // Test when time is up, move to menu page
                yield return new WaitForSeconds(float.Parse(timerInSquares.text));
                Assert.AreEqual(SceneName.MENU_SCENE, SceneManager.GetActiveScene().name);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-6'>FG-6</a></li>
            <li><b>Test description:</b> Test Timer behaves properly in catch the thief game.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestTimerInCatchTheThiefGame()
            {
                SceneManager.LoadScene(SceneName.CATCHTHETHIEF_SCENE);
                yield return null;

                // Test Squares game has timer text
                Text timerInCTF = GameObject.Find("TimerValue").GetComponent<Text>();
                Assert.IsNotNull(timerInCTF, "Catch The Thief game missing gameobject: " + "timer");

                // Test catch the thief timer starts from 50
                float expectedStartingTime = 50f;
                float actualStaringTime = float.Parse(timerInCTF.text);
                Assert.IsTrue(Math.Abs(expectedStartingTime - actualStaringTime) <= tolerance);

                // Test the timer text color changes to red when it's less than 5s
                yield return new WaitForSeconds(45);
                Assert.AreEqual(Color.red, timerInCTF.color);

                // Test when time is up, move to menu page
                yield return new WaitForSeconds(float.Parse(timerInCTF.text));
                Assert.AreEqual(SceneName.MENU_SCENE, SceneManager.GetActiveScene().name);
            }

            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public functions

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test when timer object is created, it's filed values are initialized properly.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TimerObjectCreated_THEN_FieldValueInitialized()
            {
                float expectedCurrentTime = 0;
                float expectedStartingTime = 40;

                yield return null;
                Assert.IsTrue(Math.Abs(expectedCurrentTime - timerObject.currentTime) <= tolerance);
                Assert.IsTrue(Math.Abs(expectedStartingTime - timerObject.startingTime) <= tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test when Start function is called, the field value of currentTime is reset.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_StartFunctionCalled_THEN_CurrentTimeReset()
            {
                timerObject.Start();
                float expectedCurrentTime = 40;

                yield return null;
                Assert.IsTrue(Math.Abs(expectedCurrentTime - timerObject.currentTime) <= tolerance);
            }
        }
    }
}