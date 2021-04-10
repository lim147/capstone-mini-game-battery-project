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
    namespace CatchTheThiefTests
    {
        public class CatchTheThiefInstructionsSceneTests
        {
            /// <summary>
            /// Load the scene for the playmode tests.
            /// </summary>
            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE);
            }

            /// <summary>
            /// Find button object and then invoke the click.
            /// </summary>
            public void ClickButton()
            {
                // Find button object, if not, throw out exceptions
                var startButton = GameObject.Find("Button");
                Assert.IsNotNull(startButton, "Missing button: " + "CatchTheThiefStartButton");

                // Set button selected for the Event System
                EventSystem.current.SetSelectedGameObject(startButton);

                // Invoke click
                startButton.GetComponent<Button>().onClick.Invoke();

            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-2'>FG-2</a></li>
            <li><b>Test description:</b> Once the game start button has been
                clicked, the scene containing the Catch The Thief game should
                load.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_CatchTheThiefInstructionSceneStartButtonPressed_THEN_MoveToCTFScene()
            {
                ClickButton();
                yield return null;
                Assert.AreEqual(SceneName.CATCHTHETHIEF_SCENE, SceneManager.GetActiveScene().name);
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4'>FG-4</a></li>
            <li><b>Test description:</b> Tests that instructions for the Catch The Thief
                game are provided in this scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_CTFInstructionSceneLoad_THEN_InstructionsDisplay()
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

