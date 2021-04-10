using System.Collections;
using Games;
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
        public class DisableGameButtonsTests
        {
            // Button object
            private Button button;

            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public functions

            // Test Disable functions for games:
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test DisableCTFFunction function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_DisableCTFFunctionCalled()
            {
                // Load Start Scene
                SceneManager.LoadScene(SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("Button").GetComponent<Button>();
                // click the button to trigger the tested function
                button.onClick.Invoke();

                yield return null;
                // Game Button should be not selectable
                Assert.IsFalse(Globals.isCTFButtonOn);
                // GameName should be appended to game order list
                Assert.AreEqual(GameName.CATCH_THE_THIEF, Globals.gameOrder[Globals.gameOrder.Count - 1]);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests DisableBalloons function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_DisableBalloonsFunctionCalled()
            {
                // Load Start Scene
                SceneManager.LoadScene(SceneName.BALLOONS_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("BalloonsStartButton").GetComponent<Button>();
                // click the button to trigger the tested function
                button.onClick.Invoke();

                yield return null;
                // Game Button should be not selectable
                Assert.IsFalse(Globals.isBalloonsButtonOn);
                // GameName should be appended to game order list
                Assert.AreEqual(GameName.BALLOONS, Globals.gameOrder[Globals.gameOrder.Count - 1]);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests DisableSquares function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_DisableSquaresFunctionCalled()
            {
                // Load Start Scene
                SceneManager.LoadScene(SceneName.SQUARES_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("SquaresStartButton").GetComponent<Button>();
                // click the button to trigger the tested function
                button.onClick.Invoke();

                yield return null;
                // Game Button should be not selectable
                Assert.IsFalse(Globals.isSquaresButtonOn);
                // GameName should be appended to game order list
                Assert.AreEqual(GameName.SQUARES, Globals.gameOrder[Globals.gameOrder.Count - 1]);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests DisableImageHit function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_DisableImageHitFunctionCalled()
            {
                // Load Start Scene
                SceneManager.LoadScene(SceneName.IMAGEHIT_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("Button").GetComponent<Button>();
                // click the button to trigger the tested function
                button.onClick.Invoke();

                yield return null;
                // Game Button should be not selectable
                Assert.IsFalse(Globals.isImageHitButtonOn);
                // GameName should be appended to game order list
                Assert.AreEqual(GameName.IMAGE_HIT, Globals.gameOrder[Globals.gameOrder.Count - 1]);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests DisableBall function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_DisableBallFunctionCalled()
            {
                // Load Start Scene
                SceneManager.LoadScene(SceneName.CATCH_THE_BALL_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("StartButton").GetComponent<Button>();
                // click the button to trigger the tested function
                button.onClick.Invoke();

                yield return null;
                // Game Button should be not selectable
                Assert.IsFalse(Globals.isCatchTheBallButtonOn);
                // GameName should be appended to game order list
                Assert.AreEqual(GameName.CATCH_THE_BALL, Globals.gameOrder[Globals.gameOrder.Count - 1]);
            }
        }
    }
}