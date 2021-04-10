using System.Collections;
using UnityEngine.TestTools;
using Unity.PerformanceTesting;
using UnityEngine.SceneManagement;
using UI;
using UnityEngine;
using NUnit.Framework;

namespace PerformanceTests
{
    namespace PerformanceTests
    {
        public class LoadingTimeTests
        {

            // Test the loading time and frame updating time for all scenes

            [UnityTest, Performance]
            public IEnumerator TestMenuScene_Loading_AND_FrameUpdating_Time()
            {
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.MENU_SCENE);
                })
                    .MeasurementCount(10) // how many times the method will be run
                    .IterationsPerMeasurement(5) // how many times of measure will be chosen to find an average
                    .SampleGroup("Load.Menu_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }


            [UnityTest, Performance]
            public IEnumerator TestInfoScene_Loading_AND_FrameUpdating_Time()
            {
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.INFO_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.Info_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }



            [UnityTest, Performance]
            public IEnumerator TestQuestionnaireScene_Loading_AND_FrameUpdating_Time()
            {
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.QUESTIONNAIRE_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.Questionnaire_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the frame updating time in in the process of loading Balloons Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestBalloonsInstructionsScene_Loading_AND_FrameUpdating_Time()
            {
                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.BALLOONS_INSTRUCTIONS_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.BalloonsInstructions_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the frame updating time in in the process of loading Squares Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestSquaresInstructionsScene_Loading_AND_FrameUpdating_Time()
            {
                LogAssert.ignoreFailingMessages = true;

                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.SQUARES_INSTRUCTIONS_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.SquaresInstructions_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the frame updating time in in the process of loading Catch The Thief Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestCTFInstructionsScene_Loading_AND_FrameUpdating_Time()
            {
                LogAssert.ignoreFailingMessages = true;

                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.CatchTheThiefInstructions_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the frame updating time in in the process of loading Image Hit Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestImageHitInstructionsScene_Loading_AND_FrameUpdating_Time()
            {
                LogAssert.ignoreFailingMessages = true;

                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.IMAGEHIT_INSTRUCTIONS_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.ImageHitInstructions_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the frame updating time in in the process of loading Catch The Ball Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest, Performance]
            public IEnumerator ExpectPossibleFailOnPipeline_TestBallInstructionsScene_Loading_AND_FrameUpdating_Time()
            {
                LogAssert.ignoreFailingMessages = true;

                yield return new WaitForSeconds(0.1f);
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.CATCH_THE_BALL_INSTRUCTIONS_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.BallInstructions_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }




            //--------------------- Acceptance Tests For Performance Requirement start -------------------
            //--------------------------------------------------------------------------------------------


            /// <summary>
            /// Load game scene multiple times and calculate an average loading time and
            /// frame updating time.
            /// Test the performance requirement [PER-1](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-1)
            ///  explicitly for game Balloons.
            /// </summary>
            /// <returns></returns>
            [UnityTest, Performance]
            public IEnumerator TestBalloonsScene_Loading_AND_FrameUpdating_Time()
            {
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.BALLOONS_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.Balloon_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            /// <summary>
            /// Load game scene multiple times and calculate an average loading time and
            /// frame updating time.
            /// Test the performance requirement [PER-1](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-1)
            ///  explicitly for game Squares.
            /// </summary>
            /// <returns></returns>
            [UnityTest, Performance]
            public IEnumerator TestSquaresScene_Loading_AND_FrameUpdating_Time()
            {
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.SQUARES_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.Squares_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            /// <summary>
            /// Load game scene multiple times and calculate an average loading time and
            /// frame updating time.
            /// Test the performance requirement [PER-1](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-1)
            ///  explicitly for game Catch The Thief.
            /// </summary>
            /// <returns></returns>
            [UnityTest, Performance]
            public IEnumerator TestCTFScene_Loading_AND_FrameUpdating_Time()
            {
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.CATCHTHETHIEF_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.CatchTheThief_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            /// <summary>
            /// Load game scene multiple times and calculate an average loading time and
            /// frame updating time.
            /// Test the performance requirement [PER-1](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-1)
            ///  explicitly for game ImageHit.
            /// </summary>
            /// <returns></returns>
            [UnityTest, Performance]
            public IEnumerator TestImageHitScene_Loading_AND_FrameUpdating_Time()
            {
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.IMAGEHIT_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.ImageHit_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            /// <summary>
            /// Load game scene multiple times and calculate an average loading time and
            /// frame updating time.
            /// Test the performance requirement [PER-1](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-1)
            ///  explicitly for game Ball.
            /// </summary>
            /// <returns></returns>
            [UnityTest, Performance]
            public IEnumerator TestCatchTheBallScene_Loading_AND_FrameUpdating_Time()
            {
                Measure.Method(() =>
                {
                    SceneManager.LoadScene(SceneName.CATCH_THE_BALL_SCENE);
                })
                    .MeasurementCount(10)
                    .IterationsPerMeasurement(5)
                    .SampleGroup("Load.Catch_The_Ball_Scene")
                    .Run();

                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

        }

    }
}
