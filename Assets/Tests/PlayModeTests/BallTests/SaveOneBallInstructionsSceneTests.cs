using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UI;
using UnityEngine.UI;
using TMPro;

namespace PlayModeTests
{
    namespace BallTests
    {
        public class SaveOneBallInstructionsSceneTests
        {
            Button startButton;
            TextMeshProUGUI instructions;

            /// <summary>
            /// Load the scene for the playmode tests.
            /// </summary>
            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.SAVE_ONE_BALL_INSTRUCTIONS_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once the scene is loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                startButton = GameObject.Find("StartButton").GetComponent<Button>();
                Assert.IsNotNull(startButton, "Missing button start button");
                instructions = GameObject.Find("InstructionsText").GetComponent<TextMeshProUGUI>();
                Assert.IsNotNull(instructions, "Missing instruction text");

                // Remove event so this method is not re-evaluated
                // if a non-BallInstruction scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            //----------------- Tests driven by Functional Requirements -------------
            //-----------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-2'>FG-2</a></li>
            <li><b>Test description:</b> Tests that once the game start button
                has been clicked, the scene containing the Save One Ball game is loaded.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SaveOneBallInstructionSceneStartButtonPressed_THEN_MoveToSaveOneBallScene()
            {
                startButton.onClick.Invoke();
                yield return null;
                Assert.AreEqual(SceneName.SAVE_ONE_BALL_SCENE, SceneManager.GetActiveScene().name);
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4'>FG-4</a></li>
            <li><b>Test description:</b> Tests that instructions for the Save One Ball
                game are provided in this scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SaveOneTheBallInstructionSceneLoad_THEN_InstructionsDisplay()
            {
                yield return null;
                Assert.IsNotNull(instructions);
                Assert.IsNotEmpty(instructions.text);
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
