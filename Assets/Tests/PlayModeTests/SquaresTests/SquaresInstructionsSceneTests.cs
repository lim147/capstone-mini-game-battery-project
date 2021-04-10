using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UI;
using TMPro;

namespace PlayModeTests
{
    namespace SquaresTests
    {
        public class SquaresInstructionsSceneTests
        {
            private Button startButton;
            private TextMeshProUGUI instructionTitle;
            private TextMeshProUGUI instructionText;
            private Color TEXT_COLOR = new Color32(0x0, 0x0, 0x0, 0xFF);
            private Color BACKGROUND_COLOR = new Color32(0xFF, 0xFF, 0xFF, 0x00);
            private Camera camera;


            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.SQUARES_INSTRUCTIONS_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                startButton = GameObject.Find("SquaresStartButton").GetComponent<Button>();
                camera = GameObject.Find("Main Camera").GetComponent<Camera>();
                instructionTitle = GameObject.Find("InstructionsTitle").GetComponent<TMPro.TextMeshProUGUI>();
                instructionText = GameObject.Find("InstructionsText").GetComponent<TMPro.TextMeshProUGUI>();

                // Remove event so this method is not re-evaluated
                // if a non-Questionnaire scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the background is the expected colour.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator BackgroundWhite()
            {
                yield return null;
                Assert.AreEqual(BACKGROUND_COLOR,
                    camera.backgroundColor);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that when the start button is pressed, the scene moves to the squares scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresInstructionSceneStartButtonPressed_THEN_MoveToSquaresScene()
            {
                startButton.onClick.Invoke();

                yield return null;
                Assert.AreEqual(SceneName.SQUARES_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the instructions title is black.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator InstructionTitleColourBlack()
            {
                yield return null;

                Assert.AreEqual(TEXT_COLOR, instructionTitle.color);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the instructions text is black.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator InstructionTextColourBlack()
            {
                yield return null;

                Assert.AreEqual(TEXT_COLOR, instructionText.color);

            }
         
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that instructions are provided in this scene. This tests the general functional requirements [FG-4](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4) explicitly for game Squares.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresInstructionSceneLoad_THEN_InstructionsDisplay()
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