using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;

namespace PlayModeTests
{
    namespace UITests
    {
        public class SceneSwitcherTests
        {
            private Button buttonWithButtonPressNextSceneFunctionAttached;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the NextScene function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator NextSceneFunctionGoesToNextScene()
            {
                SceneManager.LoadScene(0);

                yield return null;

                SceneSwitcher.NextScene();
                int expectedSceneIndex = 1;

                yield return null;

                Assert.AreEqual(expectedSceneIndex,
                    SceneManager.GetActiveScene().buildIndex);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the NextScene function, when it is on the last scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator NextSceneFunctionStaysOnSameSceneIfOnLastScene()
            {

                SceneManager.LoadScene(SceneManager.sceneCount - 1);

                yield return null;

                SceneSwitcher.NextScene();
                int expectedSceneIndex = SceneManager.sceneCount - 1;

                yield return null;

                Assert.AreEqual(expectedSceneIndex,
                    SceneManager.GetActiveScene().buildIndex);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the RestartGame function, when it is on the last scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator RestartGameFunctionGoesToFirstScene()
            {

                SceneManager.LoadScene(SceneManager.sceneCount - 1);

                yield return null;

                SceneSwitcher.RestartGame();
                int expectedSceneIndex = 0;

                yield return null;

                Assert.AreEqual(expectedSceneIndex,
                    SceneManager.GetActiveScene().buildIndex);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the RestartGame function, when it is on the first scene.
        </ul>
        ")]
            [UnityTest]
            public IEnumerator RestartGameFunctionStaysOnFirstSceneIfAlreadyOnFirstScene()
            {

                SceneManager.LoadScene(0);

                yield return null;

                SceneSwitcher.RestartGame();
                int expectedSceneIndex = 0;

                yield return null;

                Assert.AreEqual(expectedSceneIndex,
                    SceneManager.GetActiveScene().buildIndex);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the MoveToScene function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToSceneGoesToCorrectScene()
            {

                SceneManager.LoadScene(0);

                yield return null;

                SceneSwitcher.MoveToScene(SceneName.QUESTIONNAIRE_SCENE);
                string expectedSceneName = SceneName.QUESTIONNAIRE_SCENE;

                yield return null;

                Assert.AreEqual(expectedSceneName,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the MoveToScene function when you are already on the scene that you are moving to.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToSceneGoesToCorrectSceneIfAlreadyOnScene()
            {

                SceneManager.LoadScene(SceneName.MENU_SCENE);

                yield return null;

                SceneSwitcher.MoveToScene(SceneName.MENU_SCENE);
                string expectedSceneName = SceneName.MENU_SCENE;

                yield return null;

                Assert.AreEqual(expectedSceneName,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the button press next scene button.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator ButtonWithButtonPressNextSceneAttachedGoesToNextScene()
            {
                SceneManager.LoadScene(SceneName.SQUARES_INSTRUCTIONS_SCENE);

                yield return null;

                buttonWithButtonPressNextSceneFunctionAttached = GameObject.Find("SquaresStartButton").GetComponent<Button>();
                buttonWithButtonPressNextSceneFunctionAttached.onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.SQUARES_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the MoveToResultScene function, by testing a button that has the function attached.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToResultSceneMovesToResultScene()
            {
                SceneManager.LoadScene(SceneName.OBJECT_RECOGNITION_RESULT_SCENE);

                yield return null;

                Button buttonWithMoveToResultSceneFunctionAttached = GameObject.Find("Button").GetComponent<Button>();
                buttonWithMoveToResultSceneFunctionAttached.onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.RESULT_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the MoveToPointingResultScene function, by testing a button that has the function attached.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToPointingResultSceneFunctionMoveToPointingResultScene()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);

                yield return null;

                GameObject.Find("PointingTitle").GetComponent<TextButton>().onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.POINTING_RESULT_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the MoveToInhibitionResultScene function, by testing a button that has the function attached.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToInhibitionResultSceneFunctionMovesToInhibitionResultScene()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);

                yield return null;

                GameObject.Find("InhibitionTitle").GetComponent<TextButton>().onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.INHIBITION_RESULT_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the MoveToORResultScene function, by testing a button that has the function attached.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToORResultSceneFunctionMovesToORResultScene()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);

                yield return null;

                GameObject.Find("ObjectRecognitionTitle").GetComponent<TextButton>().onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.OBJECT_RECOGNITION_RESULT_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the MoveToSVResultScene function, by testing a button that has the function attached.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToSVResultSceneFunctionMovesToSVResultScene()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);

                yield return null;

                GameObject.Find("SelectiveVisualTitle").GetComponent<TextButton>().onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.SELECTIVE_VISUAL_RESULT_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the MoveToTTCResultScene function, by testing a button that has the function attached.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToTTCResultSceneFunctionMovesToTTCResultScene()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);

                yield return null;

                GameObject.Find("TimeToContactTitle").GetComponent<TextButton>().onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.TIME_TO_CONTACT_RESULT_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the MoveToVSResultScene function, by testing a button that has the function attached.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToVSResultSceneFunctionMovesToVSResultScene()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);

                yield return null;

                GameObject.Find("VisuospatialSketchpadTitle").GetComponent<TextButton>().onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.VISUOSPATIAL_SKETCHPAD_RESULT_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the MoveToMenuScene function, by testing a button that has the function attached.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator MoveToMenuSceneFunctionMovesToMenuScene()
            {
                SceneManager.LoadScene(SceneName.BALLOONS_INSTRUCTIONS_SCENE);

                yield return null;

                GameObject.Find("BackButton").GetComponent<Button>().onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.MENU_SCENE,
                    SceneManager.GetActiveScene().name);
            }
        }
    }
}