using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UI;
using Management;
using Measurement;
using TMPro;
using Storage;

namespace PlayModeTests
{
    namespace UITests
    {
        public class SingleAbilityResultSceneTests
        {
            string expectedPointingScore = "0";
            string expectedPointingLevel = "EXCELLENT";

            string expectedInhibitionScore = "0";
            string expectedInhibitionLevel = "EXCELLENT";

            string expectedSelectiveVisualScore = "0";
            string expectedSelectiveVisualLevel = "EXCELLENT";

            string expectedVisuospatialSketchpadScore = "0";
            string expectedVisuospatialSketchpadLevel = "EXCELLENT";

            string expectedTimeToContactScore = "0";
            string expectedTimeToContactLevel = "EXCELLENT";

            string expectedObjectRecognitionScore = "0";
            string expectedObjectRecognitionLevel = "EXCELLENT";

            List<OverallScoreStorage> overallScoreSeq;

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the scene has the correct score and level.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator VSResultSceneGetsCorrectScoreAndLevel()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                SceneManager.LoadScene(SceneName.VISUOSPATIAL_SKETCHPAD_RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                GetScoreAndLevel();

                // Get the score and level values
                TextMeshProUGUI visuospatialSketchpadScore = GameObject.Find("ScoreValue").GetComponent<TMPro.TextMeshProUGUI>();
                TextMeshProUGUI visuospatialSketchpadLevel = GameObject.Find("LevelValue").GetComponent<TMPro.TextMeshProUGUI>();

                yield return null;
                // Check that the calculated score is the same as the score showing on the screen
                Assert.IsTrue(isStringLevelAbilityLevel(visuospatialSketchpadLevel.text));
                Assert.IsTrue(isStringScoreValidScore(visuospatialSketchpadScore.text));
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the scene has the correct score and level.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator SVResultSceneGetsCorrectScoreAndLevel()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                SceneManager.LoadScene(SceneName.SELECTIVE_VISUAL_RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                GetScoreAndLevel();

                // Get the score and level values
                TextMeshProUGUI selectiveVisualScore = GameObject.Find("ScoreValue").GetComponent<TMPro.TextMeshProUGUI>();
                TextMeshProUGUI selectiveVisualLevel = GameObject.Find("LevelValue").GetComponent<TMPro.TextMeshProUGUI>();

                yield return null;
                // Check that the calculated score is the same as the score showing on the screen
                Assert.IsTrue(isStringLevelAbilityLevel(selectiveVisualLevel.text));
                Assert.IsTrue(isStringScoreValidScore(selectiveVisualScore.text));
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the scene has the correct score and level.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator PointingResultSceneGetsCorrectScoreAndLevel()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                SceneManager.LoadScene(SceneName.POINTING_RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                GetScoreAndLevel();

                // Get the score and level values
                TextMeshProUGUI pointingScore = GameObject.Find("ScoreValue").GetComponent<TMPro.TextMeshProUGUI>();
                TextMeshProUGUI pointingLevel = GameObject.Find("LevelValue").GetComponent<TMPro.TextMeshProUGUI>();

                yield return null;
                // Check that the calculated score is the same as the score showing on the screen
                Assert.IsTrue(isStringLevelAbilityLevel(pointingLevel.text));
                Assert.IsTrue(isStringScoreValidScore(pointingScore.text));
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the scene has the correct score and level.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator InhibitionResultSceneGetsCorrectScoreAndLevel()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                SceneManager.LoadScene(SceneName.INHIBITION_RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                GetScoreAndLevel();

                // Get the score and level values
                TextMeshProUGUI inhibitionScore = GameObject.Find("ScoreValue").GetComponent<TMPro.TextMeshProUGUI>();
                TextMeshProUGUI inhibitionLevel = GameObject.Find("LevelValue").GetComponent<TMPro.TextMeshProUGUI>();

                yield return null;
                // Check that the calculated score is the same as the score showing on the screen
                Assert.IsTrue(isStringLevelAbilityLevel(inhibitionLevel.text));
                Assert.IsTrue(isStringScoreValidScore(inhibitionScore.text));
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the scene has the correct score and level.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator ORResultSceneGetsCorrectScoreAndLevel()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                SceneManager.LoadScene(SceneName.OBJECT_RECOGNITION_RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                GetScoreAndLevel();

                // Get the score and level values
                TextMeshProUGUI objectRecognitionScore = GameObject.Find("ScoreValue").GetComponent<TMPro.TextMeshProUGUI>();
                TextMeshProUGUI objectRecognitionLevel = GameObject.Find("LevelValue").GetComponent<TMPro.TextMeshProUGUI>();

                yield return null;
                // Check that the calculated score is the same as the score showing on the screen
                Assert.IsTrue(isStringLevelAbilityLevel(objectRecognitionLevel.text));
                Assert.IsTrue(isStringScoreValidScore(objectRecognitionScore.text));
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the scene has the correct score and level.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator TTCResultSceneGetsCorrectScoreAndLevel()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                SceneManager.LoadScene(SceneName.TIME_TO_CONTACT_RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                GetScoreAndLevel();

                // Get the score and level values
                TextMeshProUGUI timeToContactScore = GameObject.Find("ScoreValue").GetComponent<TMPro.TextMeshProUGUI>();
                TextMeshProUGUI timeToContactLevel = GameObject.Find("LevelValue").GetComponent<TMPro.TextMeshProUGUI>();

                yield return null;
                // Check that the calculated score is the same as the score showing on the screen
                Assert.IsTrue(isStringLevelAbilityLevel(timeToContactLevel.text));
                Assert.IsTrue(isStringScoreValidScore(timeToContactScore.text));
            }

            // Helper Function

            /// <summary>
            /// Iterate through overallScoreSeq and fill the expected score variables
            /// with the correct score and level values.
            /// </summary>
            private void GetScoreAndLevel()
            {
                // Get the expected score values
                overallScoreSeq = AbilityManagement.GetOverallScoreSeq();

                foreach (OverallScoreStorage overallScore in overallScoreSeq)
                {
                    if (overallScore.AbilityName == AbilityName.POINTING)
                    {
                        expectedPointingScore = overallScore.Score.ToString();
                        expectedPointingLevel = overallScore.Level.ToString();
                    }
                    if (overallScore.AbilityName == AbilityName.INHIBITION)
                    {
                        expectedInhibitionScore = overallScore.Score.ToString();
                        expectedInhibitionLevel = overallScore.Level.ToString();
                    }
                    if (overallScore.AbilityName == AbilityName.SELECTIVE_VISUAL)
                    {
                        expectedSelectiveVisualScore = overallScore.Score.ToString();
                        expectedSelectiveVisualLevel = overallScore.Level.ToString();
                    }
                    if (overallScore.AbilityName == AbilityName.VISUOSPATIAL_SKETCHPAD)
                    {
                        expectedVisuospatialSketchpadScore = overallScore.Score.ToString();
                        expectedVisuospatialSketchpadLevel = overallScore.Level.ToString();
                    }
                    if (overallScore.AbilityName == AbilityName.TIME_TO_CONTACT)
                    {
                        expectedTimeToContactScore = overallScore.Score.ToString();
                        expectedTimeToContactLevel = overallScore.Level.ToString();
                    }
                    if (overallScore.AbilityName == AbilityName.OBJECT_RECOGNITION)
                    {
                        expectedObjectRecognitionScore = overallScore.Score.ToString();
                        expectedObjectRecognitionLevel = overallScore.Level.ToString();
                    }
                }
            }

            /// <summary>
            /// isStringLevelAbilityLevel returns true if the input strLevel is a string of
            /// </summary>
            /// <param name="strLevel"></param>
            /// <returns></returns>
            private bool isStringLevelAbilityLevel(string strLevel)
            {
                bool isEXCELLENT = (strLevel == AbilityLevel.EXCELLENT.ToString());
                bool isGOOD = (strLevel == AbilityLevel.GOOD.ToString());
                bool isOK = (strLevel == AbilityLevel.OK.ToString());
                bool isPOOR = (strLevel == AbilityLevel.POOR.ToString());
                bool isVERYPOOR = (strLevel == AbilityLevel.VERY_POOR.ToString());
                bool isNOTKNOWN = (strLevel == AbilityLevel.NOT_KNOWN.ToString());

                return (isEXCELLENT || isGOOD || isOK || isPOOR || isVERYPOOR || isNOTKNOWN);
            }

            /// <summary>
            /// isStringScoreValidScore returns true if the input strScore is a string of
            /// number between -1 and 100.
            /// </summary>
            /// <param name="strScore"></param>
            /// <returns></returns>
            private bool isStringScoreValidScore(string strScore)
            {
                int intScore = int.Parse(strScore);
                return (-1 <= intScore) && (intScore <= 100);
                
            }
        }
    }
}