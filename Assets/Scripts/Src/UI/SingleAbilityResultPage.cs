using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Storage;
using Management;
using Measurement;
using UnityEngine.SceneManagement;

namespace UI
{
    /// <summary>
    /// The module is for displaying the score and level of one cognitive and 
    /// or ability on screen.
    /// This module implements [SingleAbilityResultPage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#singleabilityresultpage-module)
    /// found in the Architecture and Module Design Document.
    /// </summary>
    public class SingleAbilityResultPage : MonoBehaviour
    {
        /// <summary>
        /// Ability score text.
        /// </summary>
        public TextMeshProUGUI scoreValue;

        /// <summary>
        /// Ability level text.
        /// </summary>
        public TextMeshProUGUI levelValue;

        /// <summary>
        /// Name of current active scene.
        /// </summary>
        private string currentSceneName;

        /// <summary>
        /// Datatype to hold overall scores for each cognitive and motor ability tested.
        /// </summary>
        private List<OverallScoreStorage> overallScoreSeq;

        // Start is called before the first frame update
        void Start()
        {
            // Get the name of the current active scene
            currentSceneName = SceneManager.GetActiveScene().name;

            // Call the function to calculate the overall cognitive or motor level
            overallScoreSeq = AbilityManagement.GetOverallScoreSeq();

            // Link TextMeshProUGUI field to UI Component
            scoreValue = GameObject.Find("ScoreValue").GetComponent<TMPro.TextMeshProUGUI>();
            levelValue = GameObject.Find("LevelValue").GetComponent<TMPro.TextMeshProUGUI>();

            // Fill in score and level in UI
            UpdateScoreAndLevelText();
        }

        /// <summary>
        /// Update the score and level text on scene.
        /// </summary>
        private void UpdateScoreAndLevelText()
        {
            OverallScoreStorage scoreAndLevel = new OverallScoreStorage();

            // Get the overallScoreStorage based on the curently active ability result scene 
            switch (currentSceneName)
            {
                case SceneName.INHIBITION_RESULT_SCENE:
                    scoreAndLevel = GetOverallScoreStorageForOneAbility(AbilityName.INHIBITION);
                    break;
                case SceneName.OBJECT_RECOGNITION_RESULT_SCENE:
                    scoreAndLevel = GetOverallScoreStorageForOneAbility(AbilityName.OBJECT_RECOGNITION);
                    break;
                case SceneName.POINTING_RESULT_SCENE:
                    scoreAndLevel = GetOverallScoreStorageForOneAbility(AbilityName.POINTING);
                    break;
                case SceneName.SELECTIVE_VISUAL_RESULT_SCENE:
                    scoreAndLevel = GetOverallScoreStorageForOneAbility(AbilityName.SELECTIVE_VISUAL);
                    break;
                case SceneName.TIME_TO_CONTACT_RESULT_SCENE:
                    scoreAndLevel = GetOverallScoreStorageForOneAbility(AbilityName.TIME_TO_CONTACT);
                    break;
                case SceneName.VISUOSPATIAL_SKETCHPAD_RESULT_SCENE:
                    scoreAndLevel = GetOverallScoreStorageForOneAbility(AbilityName.VISUOSPATIAL_SKETCHPAD);
                    break;
            }
            // Set Text
            scoreValue.text = scoreAndLevel.Score.ToString();
            levelValue.text = scoreAndLevel.Level.ToString();

            RadialScoreRing radialScoreRing = GameObject.Find("RadialScoreRing").GetComponent<RadialScoreRing>();
            radialScoreRing.GetComponent<RadialScoreRing>().Score = scoreAndLevel.Score;
            radialScoreRing.BeginAnimation();
        }

        /// <summary>
        /// Get the overallScoreStorage object for the specified input ability from the overallScoreSeq.
        /// </summary>
        /// <param name="abilityName"></param>
        /// <returns></returns>
        private OverallScoreStorage GetOverallScoreStorageForOneAbility(AbilityName abilityName)
        {
            OverallScoreStorage result = new OverallScoreStorage();
            foreach (OverallScoreStorage overallScore in overallScoreSeq)
            {
                if (overallScore.AbilityName == abilityName)
                {
                    result = overallScore;
                    break;
                }
            }
            return result;
        }
    }
}
