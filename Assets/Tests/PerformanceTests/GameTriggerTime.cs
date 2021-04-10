using System.Collections;
using NUnit.Framework;
using UnityEngine;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UI;
using UnityEngine.UI;

namespace PerformanceTests
{
    namespace PerformanceTests
    {
        public class GameTriggerTime
        {
            // Game buttons:
            private Button CTFButton;
            private Button BalloonsButton;
            private Button SquaresButton;
            private Button ImageHitButton;
            private Button BallButton;

            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.MENU_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                CTFButton = GameObject.Find("CatchTheThief").GetComponent<Button>();
                BalloonsButton = GameObject.Find("Balloons").GetComponent<Button>();
                SquaresButton = GameObject.Find("Squares").GetComponent<Button>();
                ImageHitButton = GameObject.Find("ImageHit").GetComponent<Button>();
                BallButton = GameObject.Find("CatchTheBall").GetComponent<Button>();

                // Remove event so this method is not re-evaluated
                // if a non-Questionnaire scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            //--------------------- Acceptance Tests For Performance Requirement start -------------------
            //--------------------------------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Performance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-2'>PER-2</a></li>
            <li><b>Test description:</b> Calculate the average elapsed time between when clicking on the Balloons game button on the menu page
                   and when the corresponding game instruction scene is finished loading.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestMenuScene_TriggerBalloonsGame()
            {
                LogAssert.ignoreFailingMessages = true;

                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                // Simulate a click on the button
                BalloonsButton.onClick.Invoke();
                    new WaitUntil(() => SceneName.BALLOONS_INSTRUCTIONS_SCENE == SceneManager.GetActiveScene().name);
                })
                    .MeasurementCount(10) // how many times the method will be run
                    .IterationsPerMeasurement(5) // how many times of measure will be chosen to find an average
                    .SampleGroup("Trigger.Balloons_Game")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Performance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-2'>PER-2</a></li>
            <li><b>Test description:</b> Calculate the average elapsed time between when clicking on the Squares game button on the menu page
                   and when the corresponding game instruction scene is finished loading.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestMenuScene_TriggerSquaresGame()
            {
                LogAssert.ignoreFailingMessages = true;

                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                    // Simulate a click on the button
                    SquaresButton.onClick.Invoke();
                    new WaitUntil(() => SceneName.SQUARES_INSTRUCTIONS_SCENE == SceneManager.GetActiveScene().name);
                })
                    .MeasurementCount(10) // how many times the method will be run
                    .IterationsPerMeasurement(5) // how many times of measure will be chosen to find an average
                    .SampleGroup("Trigger.Squares_Game")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Performance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-2'>PER-2</a></li>
            <li><b>Test description:</b> Calculate the average elapsed time between when clicking on the Catch The Thief game button on the menu page
                   and when the corresponding game instruction scene is finished loading.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestMenuScene_TriggerCTFGame()
            {
                LogAssert.ignoreFailingMessages = true;

                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                    // Simulate a click on the button
                    CTFButton.onClick.Invoke();
                    new WaitUntil(() => SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE == SceneManager.GetActiveScene().name);
                })
                    .MeasurementCount(10) // how many times the method will be run
                    .IterationsPerMeasurement(5) // how many times of measure will be chosen to find an average
                    .SampleGroup("Trigger.CatchTheThief_Game")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Performance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-2'>PER-2</a></li>
            <li><b>Test description:</b> Calculate the average elapsed time between when clicking on the Image Hit game button on the menu page
                   and when the corresponding game instruction scene is finished loading.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestMenuScene_TriggerImageHitGame()
            {
                LogAssert.ignoreFailingMessages = true;

                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                // Simulate a click on the button
                ImageHitButton.onClick.Invoke();
                    new WaitUntil(() => SceneName.IMAGEHIT_INSTRUCTIONS_SCENE == SceneManager.GetActiveScene().name);
                })
                    .MeasurementCount(10) // how many times the method will be run
                    .IterationsPerMeasurement(5) // how many times of measure will be chosen to find an average
                    .SampleGroup("Trigger.ImageHit_Game")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Performance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-2'>PER-2</a></li>
            <li><b>Test description:</b> Calculate the average elapsed time between when clicking on the Catch The Ball game button on the menu page
                   and when the corresponding game instruction scene is finished loading.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestMenuScene_TriggerBallGame()
            {
                LogAssert.ignoreFailingMessages = true;

                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                // Simulate a click on the button
                BallButton.onClick.Invoke();
                    new WaitUntil(() => SceneName.CATCH_THE_BALL_INSTRUCTIONS_SCENE == SceneManager.GetActiveScene().name);
                })
                    .MeasurementCount(10) // how many times the method will be run
                    .IterationsPerMeasurement(5) // how many times of measure will be chosen to find an average
                    .SampleGroup("Trigger.Ball_Game")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }
        }
    }
}