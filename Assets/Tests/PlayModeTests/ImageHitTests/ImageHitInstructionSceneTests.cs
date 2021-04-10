using NUnit.Framework;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace PlayModeTests
{
    namespace ImageHitTests
    {
        public class ImageHitInstructionSceneTests
        {

            private GameObject btn;

            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.IMAGEHIT_INSTRUCTIONS_SCENE);
            }

            /// <summary>
            /// Find button object and then invoke the click.
            /// </summary>
            public void ClickButton()
            {
                // Find button object, if not, throw out exceptions
                var startButton = GameObject.Find("Button");
                Assert.IsNotNull(startButton, "Missing button: " + "ImageHitButton");

                // Set button selected for the Event System
                EventSystem.current.SetSelectedGameObject(startButton);

                // Invoke click
                startButton.GetComponent<Button>().onClick.Invoke();

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Once the game start button has been
                clicked, the scene containing the ImageHit game should
                load.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ImageHitInstructionSceneStartButtonPressed_THEN_MoveToCirclesScene()
            {
                ClickButton();
                yield return null;
                Assert.AreEqual(SceneName.IMAGEHIT_SCENE, SceneManager.GetActiveScene().name);
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4'>FG-4</a></li>
            <li><b>Test description:</b> Tests that instructions for the ImageHit
                game are provided in this scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ImageHitInstructionSceneLoad_THEN_InstructionsDisplay()
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


