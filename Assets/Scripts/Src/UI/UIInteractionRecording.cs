using UnityEngine;
using Storage;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// This module implements [UIInteractionRecording Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#uiinteractionrecording-module)
    /// found in the Architecture and Module Design Document. This module is used to record the time the player stays on non-game scenes.
    /// It's used for external testing purpose.
    /// </summary>
    public class UIInteractionRecording : MonoBehaviour
    {
        // To store elapsedTime in seconds
        private double elapsedTime;
        // To store the name of current active scene
        private string currentSceneName;

        private void Start()
        {
            // Reset elapsedTime to 0 when the attached scene is loaded.
            elapsedTime = 0;
            // Get the name of the current active scene
            currentSceneName = SceneManager.GetActiveScene().name;
        }

        private void Update()
        {
            // Update the elapsedTime
            elapsedTime += 1 * Time.deltaTime;
        }

        /// <summary>
        /// RecordElapsedTime stores the elapsedTime to UIInteractionStorageSingleton
        /// based on which scene is active currently.
        /// </summary>
        public void RecordElapsedTime()
        {
            switch (currentSceneName)
            {
                // Store the elapsedTime to UIInteractionStorageSingleton based on which scene is active currently
                case SceneName.START_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnStartScene = elapsedTime;
                    break;
                case SceneName.INFO_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnInfoScene = elapsedTime;
                    break;
                case SceneName.QUESTIONNAIRE_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnQuestionnaireScene.Add(elapsedTime);
                    break;
                case SceneName.MENU_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnMenuScene.Add(elapsedTime);
                    break;
                case SceneName.BALLOONS_INSTRUCTIONS_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnBalloonsInstructionsScene = elapsedTime;
                    break;
                case SceneName.CATCH_THE_BALL_INSTRUCTIONS_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnCatchTheBallInstructionsScene = elapsedTime;
                    break;
                case SceneName.SAVE_ONE_BALL_INSTRUCTIONS_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnSaveOneBallInstructionsScene = elapsedTime;
                    break;
                case SceneName.JUDGE_THE_BALL_INSTRUCTIONS_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnJudgeTheBallInstructionsScene = elapsedTime;
                    break;
                case SceneName.SQUARES_INSTRUCTIONS_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnSquaresInstructionsScene = elapsedTime;
                    break;
                case SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnCTFInstructionsScene = elapsedTime;
                    break;
                case SceneName.IMAGEHIT_INSTRUCTIONS_SCENE:
                    UIInteractionStorageSingleton.TimeStayOnImageHitInstructionsScene = elapsedTime;
                    break;
                default:
                    Debug.Log("Interaction time for the current active scene is not recorded.");
                    break;
            }
        }
    }
}