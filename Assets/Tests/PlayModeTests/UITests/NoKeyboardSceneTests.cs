using System.Collections;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace PlayModeTests
{
    namespace UITests
    {
        public class NoKeyboardSceneTests
        {
            private Color BACKGROUND_COLOR = new Color32(0xFF, 0xFF, 0xFF, 0x00);
            private GameObject keyboardButton;

            /// <summary>
            /// Load the scene for the playmode tests.
            /// </summary>
            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.NO_KEYBOARD_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                keyboardButton = GameObject.Find("Button");
                Assert.IsNotNull(keyboardButton, "Missing button");

                // Remove event so this method is not re-evaluated
                // if a non-BalloonsInstruction scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the button moves the game to the Questionnaire scene.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_StartButtonPressed_THEN_GoesToInfoScene()
            {
                // Press the button
                keyboardButton.GetComponent<Button>().onClick.Invoke();
                yield return null;

                Assert.AreEqual(SceneName.QUESTIONNAIRE_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the background is white.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator BackgroundWhite()
            {
                yield return null;

                Color backgroundColor = GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor;
                Assert.AreEqual(BACKGROUND_COLOR, backgroundColor);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the keyboard image is loaded.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator ImageLoaded()
            {
                GameObject keyboardImage = GameObject.Find("KeyboardImage");

                yield return null;
                Assert.IsNotNull(keyboardImage, "Missing image");
            }
        }
    }
}