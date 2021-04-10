using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Storage;
using UnityEngine.EventSystems;

namespace PlayModeTests
{
    namespace UITests
    {
        public class UIInteractionRecordingTests
        {
            // Button object
            private Button button;
            // Toggle object
            private Toggle toggle;

            // Tolerance for comparing two elapsed time. If it's within 0.2s, it's acceptable.
            private double tolerance = 0.2;

            //------------------------ Syatem Tests begin ------------------------------
            //--------------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on StartScene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_StartScene()
            {
                // Load Start Scene
                SceneManager.LoadScene(SceneName.START_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("StartButton").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 1.2f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                double timeRecorded = UIInteractionStorageSingleton.TimeStayOnStartScene;

                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on InfoScene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_InfoScene()
            {
                // Load Info Scene
                SceneManager.LoadScene(SceneName.INFO_SCENE);
                // Wait for scene loading finished
                yield return null;

                UIInteractionRecording uIInteractionRecording = new UIInteractionRecording();

                // Stay on scene for 1 second
                yield return new WaitForSeconds(1.0f);

                uIInteractionRecording.RecordElapsedTime();

                // Get the actual time that is recorded
                double timeRecorded = UIInteractionStorageSingleton.TimeStayOnInfoScene;
                // time recorded is expected to a number smaller than (1.0f + tolerance)
                Assert.IsTrue(timeRecorded <= 1.0f + tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on MenuScene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_MenuScene_WHEN_BalloonsButtonIsClicked()
            {
                // Load Menu Scene
                SceneManager.LoadScene(SceneName.MENU_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("Balloons").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 1.8f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                List<double> timeRecorded = UIInteractionStorageSingleton.TimeStayOnMenuScene;

                int lastIndex = timeRecorded.Count - 1;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded[lastIndex]) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on MenuScene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_MenuScene_WHEN_BallButtonIsClicked()
            {
                // Load Menu Scene
                SceneManager.LoadScene(SceneName.MENU_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("CatchTheBall").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 1.0f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                List<double> timeRecorded = UIInteractionStorageSingleton.TimeStayOnMenuScene;

                int lastIndex = timeRecorded.Count - 1;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded[lastIndex]) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on MenuScene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_MenuScene_WHEN_CTFButtonIsClicked()
            {
                // Load Menu Scene
                SceneManager.LoadScene(SceneName.MENU_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("CatchTheThief").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 1.3f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                List<double> timeRecorded = UIInteractionStorageSingleton.TimeStayOnMenuScene;

                int lastIndex = timeRecorded.Count - 1;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded[lastIndex]) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on MenuScene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_MenuScene_WHEN_SquaresButtonIsClicked()
            {
                // Load Menu Scene
                SceneManager.LoadScene(SceneName.MENU_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("Squares").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 1.3f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                List<double> timeRecorded = UIInteractionStorageSingleton.TimeStayOnMenuScene;

                int lastIndex = timeRecorded.Count - 1;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded[lastIndex]) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on MenuScene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_MenuScene_WHEN_ImageHitButtonIsClicked()
            {
                // Load Menu Scene
                SceneManager.LoadScene(SceneName.MENU_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("ImageHit").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 1.3f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                List<double> timeRecorded = UIInteractionStorageSingleton.TimeStayOnMenuScene;

                int lastIndex = timeRecorded.Count - 1;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded[lastIndex]) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on MenuScene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_MenuScene_WHEN_FinishButtonClicked()
            {
                // Load Menu Scene
                SceneManager.LoadScene(SceneName.MENU_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Gray out one game button to make Finish button visiable
                Globals.isCTFButtonOn = false;

                // Reload the menu page, the finish button is active now
                SceneManager.LoadScene(SceneName.MENU_SCENE);
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("FinishButton").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 1.0f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                List<double> timeRecorded = UIInteractionStorageSingleton.TimeStayOnMenuScene;

                int lastIndex = timeRecorded.Count - 1;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded[lastIndex]) < tolerance);

                // Go back to Menu Page
                SceneManager.LoadScene(SceneName.MENU_SCENE);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on QuestionnaireScene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_QuestionnaireScene()
            {
                // Load Questionnaire Scene
                SceneManager.LoadScene(SceneName.QUESTIONNAIRE_SCENE);
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("Button").GetComponent<Button>();

                // Stay on scene for expectedTime1 in seconds
                float expectedTime1 = 0.6f;
                yield return new WaitForSeconds(expectedTime1);
                // Press button by simulating a pointer event.
                // Since Age field is empty, it won't go to next scene when press the button; but the time is recorded.
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerDownHandler);
                // Get the actual time that is recorded
                List<double> timeRecorded1 = UIInteractionStorageSingleton.TimeStayOnQuestionnaireScene;
                int lastIndex1 = timeRecorded1.Count - 1;
                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime1 - timeRecorded1[lastIndex1]) < tolerance);

                // Fill in Age field
                TMP_InputField ageInputField = GameObject.Find("AgeInputField").GetComponent<TMP_InputField>();
                ageInputField.text = "21";
                // Stay on scene for (expectedTime2-expectedTime1) in seconds
                float expectedTime2 = 1.2f;
                yield return new WaitForSeconds(expectedTime2-expectedTime1);
                // Press button by simulating a pointer event.
                // Since Age field is filled, it should go to next scene when press the button.
                ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerDownHandler);
                // Get the actual time that is recorded
                List<double> timeRecorded2 = UIInteractionStorageSingleton.TimeStayOnQuestionnaireScene;
                int lastIndex2 = timeRecorded2.Count - 1;
                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime2 - timeRecorded2[lastIndex2]) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on Ball Instructions Scene 1.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_CatchTheBallInstructionsScene()
            {
                LogAssert.ignoreFailingMessages = true;

                // Load Menu Scene
                SceneManager.LoadScene(SceneName.CATCH_THE_BALL_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("StartButton").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 0.5f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                double timeRecorded = UIInteractionStorageSingleton.TimeStayOnCatchTheBallInstructionsScene;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on Ball Instructions Scene Round 2.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_SaveOneBallInstructionsScene()
            {
                LogAssert.ignoreFailingMessages = true;

                // Load Menu Scene
                SceneManager.LoadScene(SceneName.SAVE_ONE_BALL_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("StartButton").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 0.4f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                double timeRecorded = UIInteractionStorageSingleton.TimeStayOnSaveOneBallInstructionsScene;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on Ball Instructions Scene Round 3.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_JudgeTheBallInstructionsScene()
            {
                LogAssert.ignoreFailingMessages = true;

                // Load Menu Scene
                SceneManager.LoadScene(SceneName.JUDGE_THE_BALL_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("StartButton").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 0.3f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                double timeRecorded = UIInteractionStorageSingleton.TimeStayOnJudgeTheBallInstructionsScene;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on Balloon Instructions Scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_BalloonInstructionsScene()
            {
                LogAssert.ignoreFailingMessages = true;

                // Load Menu Scene
                SceneManager.LoadScene(SceneName.BALLOONS_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("BalloonsStartButton").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 0.3f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                double timeRecorded = UIInteractionStorageSingleton.TimeStayOnBalloonsInstructionsScene;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on Catch The Thief Instructions Scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_CTFInstructionsScene()
            {
                LogAssert.ignoreFailingMessages = true;

                // Load Menu Scene
                SceneManager.LoadScene(SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("Button").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 0.4f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                double timeRecorded = UIInteractionStorageSingleton.TimeStayOnCTFInstructionsScene;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on ImageHit Instructions Scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_ImageHitInstructionsScene()
            {
                LogAssert.ignoreFailingMessages = true;

                // Load Menu Scene
                SceneManager.LoadScene(SceneName.IMAGEHIT_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("Button").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 0.8f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                double timeRecorded = UIInteractionStorageSingleton.TimeStayOnImageHitInstructionsScene;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test correct interaction time is recorded and stored on Squares Instructions Scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Test_CorrectTimeRecorded_ON_SquaresInstructionsScene()
            {
                LogAssert.ignoreFailingMessages = true;

                // Load Menu Scene
                SceneManager.LoadScene(SceneName.SQUARES_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return null;

                // Link button object to UI component
                button = GameObject.Find("SquaresStartButton").GetComponent<Button>();

                // Stay on scene for expectedTime in seconds
                float expectedTime = 0.8f;
                yield return new WaitForSeconds(expectedTime);

                // Press button to leave the scene
                button.onClick.Invoke();

                // Get the actual time that is recorded
                double timeRecorded = UIInteractionStorageSingleton.TimeStayOnSquaresInstructionsScene;

                // Check if the correct time is appended
                Assert.IsTrue(Math.Abs(expectedTime - timeRecorded) < tolerance);
            }
        }
    }
}