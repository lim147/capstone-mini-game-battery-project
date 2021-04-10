using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace PlayModeTests
{
    namespace BalloonsTests
    {
        public class BalloonsInstructionsSceneTests
        {
            private GameObject startButton;

            /// <summary>
            /// Load the scene for the playmode tests.
            /// </summary>
            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.BALLOONS_INSTRUCTIONS_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                startButton = GameObject.Find("BalloonsStartButton");
                Assert.IsNotNull(startButton, "Missing button: " + "BalloonsStartButton");

                // Remove event so this method is not re-evaluated
                // if a non-BalloonsInstruction scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            /// <summary>
            /// Simulate a button press event
            /// </summary>
            public void ClickButton()
            {
                // Set button selected for the Event System
                EventSystem.current.SetSelectedGameObject(startButton);

                // Invoke click
                startButton.GetComponent<Button>().onClick.Invoke();

            }


            //----------------- Tests driven by Functional Requirements -------------
            //-----------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-2'>FG-2</a></li>
            <li><b>Test description:</b> Tests that once the game start button
                has been clicked, the scene containing the Balloons game is loaded.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_BalloonsInstructionSceneStartButtonPressed_THEN_MoveToBalloonsScene()
            {
                ClickButton();
                yield return null;
                Assert.AreEqual(SceneName.BALLOONS_SCENE, SceneManager.GetActiveScene().name);
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4'>FG-4</a></li>
            <li><b>Test description:</b> Tests that instructions for the Balloons
                game are provided in this scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_BalloonsInstructionSceneLoad_THEN_InstructionsDisplay()
            {
                TextMeshProUGUI instructionsText = GameObject.Find("InstructionsText").GetComponent<TMPro.TextMeshProUGUI>();

                yield return null;
                Assert.IsNotNull(instructionsText);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the back button goes back to the menu scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator BackButtonMovesToMenuScene()
            {
                GameObject.Find("BackButton").GetComponent<Button>().onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.MENU_SCENE,
                    SceneManager.GetActiveScene().name);
            }

        }
    }
}