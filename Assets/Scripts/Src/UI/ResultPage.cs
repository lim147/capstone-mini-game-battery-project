using System.Collections.Generic;
using UnityEngine;
using Storage;
using Management;
using Measurement;
using TMPro;

namespace UI
{
    /// <summary>
    /// This module is a static class(singleton) which is used to make
    /// sure that BatterySesstion methods are called only once when
    /// result scene is first time loaded, to avoid the redundant executions.
    /// </summary>
    public static class BatterySesstionLoadingTimesControl
    {
        public static int resultSceneLoadingTimesCnt = 0;

    }


    /// <summary>
    /// This module implements [Result Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#result-module)
    /// found in the Architecture and Module Design Document. 
    /// The Result module is for displaying the cognitive and 
    /// motor ability levels and scores on the screen of the application.
    /// </summary>
    public class ResultPage : MonoBehaviour

    {
        /// <summary>
        /// Datatype to hold subscore values for cognitive and motor abilities
        /// </summary>
        private List<SubscoreStorage> subScoreSeq;

        /// <summary>
        /// Datatype to hold overall scores for each cognitive and motor ability tested
        /// </summary>
        private List<OverallScoreStorage> overallScoreSeq;

        /// <summary>
        /// Unity Text object for the pointing score
        /// </summary>
        public TextMeshProUGUI pointingScore;
        /// <summary>
        /// Unity Text object for the pointing level
        /// </summary>
        public TextMeshProUGUI pointingLevel;
        /// <summary>
        /// Ring indicating pointing score
        /// </summary>
        public RadialScoreRing pointingRadialScoreRing;

        /// <summary>
        /// Unity Text object for the inhibition score
        /// </summary>
        public TextMeshProUGUI inhibitionScore;
        /// <summary>
        /// Unity Text object for the inhibition level
        /// </summary>
        public TextMeshProUGUI inhibitionLevel;
        /// <summary>
        /// Ring indicating inhibition score
        /// </summary>
        public RadialScoreRing inhibitionRadialScoreRing;

        /// <summary>
        /// Unity Text object for the selective visual score
        /// </summary>
        public TextMeshProUGUI selectiveVisualScore;
        /// <summary>
        /// Unity Text object for the selective visual level
        /// </summary>
        public TextMeshProUGUI selectiveVisualLevel;
        /// <summary>
        /// Ring indicating selective visual score
        /// </summary>
        public RadialScoreRing selectiveVisualRadialScoreRing;

        /// <summary>
        /// Unity Text object for the visuospatial skepchpad score
        /// </summary>
        public TextMeshProUGUI visuospatialSketchpadScore;
        /// <summary>
        /// Unity Text object for the visuospatial skepchpad level
        /// </summary>
        public TextMeshProUGUI visuospatialSketchpadLevel;
        /// <summary>
        /// Ring indicating visuospatial sketchpad score
        /// </summary>
        public RadialScoreRing visuospatialSketchpadRadialScoreRing;

        /// <summary>
        /// Unity Text object for the time to contact score
        /// </summary>
        public TextMeshProUGUI timeToContactScore;
        /// <summary>
        /// Unity Text object for the time to contact level
        /// </summary>
        public TextMeshProUGUI timeToContactLevel;
        /// <summary>
        /// Ring indicating time to contact score
        /// </summary>
        public RadialScoreRing timeToContactRadialScoreRing;

        /// <summary>
        /// Unity Text object for the object recognition score
        /// </summary>
        public TextMeshProUGUI objectRecognitionScore;
        /// <summary>
        /// Unity Text object for the object recognition level
        /// </summary>
        public TextMeshProUGUI objectRecognitionLevel;
        /// <summary>
        /// Ring indicating object recognition score
        /// </summary>
        public RadialScoreRing objectRecognitionRadialScoreRing;



        // Boolean for keeping track if above Text has been filled
        // It's to mare sure that the Text is only filled once
        private bool isFilled = false;

        /// <summary>
        /// The Update function
        /// calls the functions to get the calculated scores to be displayed on
        /// the screen.
        /// </summary>
        private void Update()
        {
            // If the Ability.Measure() function is called in BatterySessionManagement
            // and the Text is not filled yet
            if (!isFilled && BatterySessionManagement.IfMeasureCalled())
            {
                // Call the function to calculate and update subscore values
                subScoreSeq = AbilityManagement.GetSubScoreSeq();
                // Call the function to calculate the overall cognitive or motor level
                overallScoreSeq = AbilityManagement.GetOverallScoreSeq();

                // Update the text shown on the screen to show the cognitive and motor levels
                GetScoreAndLevel();

                // Toggle the boolean to indicate that the text values for the levels have been filled
                isFilled = true;
            }
        }

        /// <summary>
        /// Modifies the Unity text objects to show the score values as text on the screen
        /// </summary>
        private void GetScoreAndLevel()
        {
            foreach (OverallScoreStorage overallScore in overallScoreSeq)
            {
                if (overallScore.AbilityName == AbilityName.POINTING)
                {
                    pointingScore.text = overallScore.Score.ToString();
                    pointingLevel.text = overallScore.Level.ToString();
                    pointingRadialScoreRing.Score = overallScore.Score;
                    pointingRadialScoreRing.BeginAnimation();
                }
                if (overallScore.AbilityName == AbilityName.INHIBITION)
                {
                    inhibitionScore.text = overallScore.Score.ToString();
                    inhibitionLevel.text = overallScore.Level.ToString();
                    inhibitionRadialScoreRing.Score = overallScore.Score;
                    inhibitionRadialScoreRing.BeginAnimation();
                }
                if (overallScore.AbilityName == AbilityName.SELECTIVE_VISUAL)
                {
                    selectiveVisualScore.text = overallScore.Score.ToString();
                    selectiveVisualLevel.text = overallScore.Level.ToString();
                    selectiveVisualRadialScoreRing.Score = overallScore.Score;
                    selectiveVisualRadialScoreRing.BeginAnimation();
                }
                if (overallScore.AbilityName == AbilityName.VISUOSPATIAL_SKETCHPAD)
                {
                    visuospatialSketchpadScore.text = overallScore.Score.ToString();
                    visuospatialSketchpadLevel.text = overallScore.Level.ToString();
                    visuospatialSketchpadRadialScoreRing.Score = overallScore.Score;
                    visuospatialSketchpadRadialScoreRing.BeginAnimation();
                }
                if (overallScore.AbilityName == AbilityName.TIME_TO_CONTACT)
                {
                    timeToContactScore.text = overallScore.Score.ToString();
                    timeToContactLevel.text = overallScore.Level.ToString();
                    timeToContactRadialScoreRing.Score = overallScore.Score;
                    timeToContactRadialScoreRing.BeginAnimation();
                }
                if(overallScore.AbilityName == AbilityName.OBJECT_RECOGNITION)
                {
                    objectRecognitionScore.text = overallScore.Score.ToString();
                    objectRecognitionLevel.text = overallScore.Level.ToString();
                    objectRecognitionRadialScoreRing.Score = overallScore.Score;
                    objectRecognitionRadialScoreRing.BeginAnimation();
                }

            }
        }
    }
}
