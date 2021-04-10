using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;
using UI;
using UnityEngine.UI;

namespace PerformanceTests
{
    namespace PerformanceTests
    {

        public class NewRoundStartingTime
        {
            //--------------------- Acceptance Tests For Performance Requirement start -------------------
            //--------------------------------------------------------------------------------------------


            /// <summary>
            /// Calculate the average time cost to start a new round.
            /// Test the performance requirement [PER-3](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-3)
            ///  explicitly for game Balloons.
            /// </summary>
            /// <returns></returns>
            [UnityTest, Performance]
            public IEnumerator TestStartANewRound_BalloonsGame()
            {
                // Load game scene
                SceneManager.LoadScene(SceneName.BALLOONS_SCENE);
                // Wait for 1 frame to finsh the loading
                yield return null;
                // Find game object
                Button balloon = GameObject.Find("Balloon").GetComponent<Button>();

                // Simulate a click on the balloon. One more click and it goes to next round
                balloon.onClick.Invoke();

                // The execution of functions(s) within Measure.Method will be timed.
                Measure.Method(() =>
                {
                    // Simulate a click on the balloon. Now the current round ends and a new round starts.
                    balloon.onClick.Invoke();
                    new WaitForEndOfFrame();
                })
                    .MeasurementCount(10) // how many times the method will be run
                    .IterationsPerMeasurement(5) // how many times of measure will be chosen to find an average
                    .SampleGroup("Trigger.Balloons_Game")
                    .Run();

                // Call Measure.Frames() to get the average frame updating time of the execution of above function(s).
                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

            /// <summary>
            /// Calculate the average time cost to start a new round.
            /// Test the performance requirement [PER-3](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#per-3)
            ///  explicitly for game Squares.
            /// </summary>
            /// <returns></returns>
            [UnityTest, Performance]
            public IEnumerator TestStartANewRound_SquaresGame()
            {
                // Load game scene
                SceneManager.LoadScene(SceneName.SQUARES_SCENE);

                // Wait for recall phase to begin
                yield return new WaitForSeconds(0.7f * 3 + 0.1f);

                // Find done button object
                Button doneButton = GameObject.Find("DoneButton").GetComponent<Button>();

                // The execution of functions(s) within Measure.Method will be timed.
                Measure.Method(() =>
                {
                    // Simulate a click on the done button to end current round and start next round
                    doneButton.onClick.Invoke();
                    new WaitForEndOfFrame();
                })
                    .MeasurementCount(10) // how many times the method will be run
                    .IterationsPerMeasurement(5) // how many times of measure will be chosen to find an average
                    .SampleGroup("Trigger.Squares_Game")
                    .Run();

                // Call Measure.Frames() to get the average frame updating time of the execution of above function(s).
                yield return Measure.Frames()
                    .SampleGroup("Update.Frame")
                    .Run();
            }

        }
    }
}
