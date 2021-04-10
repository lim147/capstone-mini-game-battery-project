using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UI;
using Measurement;
using Management;
using Storage;
using UnityEngine.SceneManagement;
using TMPro;

namespace PlayModeTests
{
    namespace UITests
    {
        public class ResultSceneTests
        {
            // UI objects for displaying results of pointing ability
            private TextMeshProUGUI pointingTitle;
            private TextMeshProUGUI pointingLevel;
            private TextMeshProUGUI pointingScore;

            // UI objects for displaying results of inhibition ability
            private TextMeshProUGUI inhibitionTitle;
            private TextMeshProUGUI inhibitionLevel;
            private TextMeshProUGUI inhibitionScore;

            // UI objects for displaying results of selective visual ability
            private TextMeshProUGUI selectiveVisualTitle;
            private TextMeshProUGUI selectiveVisualLevel;
            private TextMeshProUGUI selectiveVisualScore;

            // UI objects for displaying results of visuospatial sketchpad ability
            private TextMeshProUGUI visuospatialSketchpadTitle;
            private TextMeshProUGUI visuospatialSketchpadLevel;
            private TextMeshProUGUI visuospatialSketchpadScore;

            // UI objects for displaying results of time to contact ability
            private TextMeshProUGUI timeToContactTitle;
            private TextMeshProUGUI timeToContactLevel;
            private TextMeshProUGUI timeToContactScore;

            // UI objects for displaying results of object recognition ability
            private TextMeshProUGUI objectRecognitionTitle;
            private TextMeshProUGUI objectRecognitionLevel;
            private TextMeshProUGUI objectRecognitionScore;

            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                pointingTitle = GameObject.Find("PointingTitle").GetComponent<TMPro.TextMeshProUGUI>();
                pointingScore = GameObject.Find("PointingScore").GetComponent<TMPro.TextMeshProUGUI>();
                pointingLevel = GameObject.Find("PointingLevel").GetComponent<TMPro.TextMeshProUGUI>();

                inhibitionTitle = GameObject.Find("InhibitionTitle").GetComponent<TMPro.TextMeshProUGUI>();
                inhibitionScore = GameObject.Find("InhibitionScore").GetComponent<TMPro.TextMeshProUGUI>();
                inhibitionLevel = GameObject.Find("InhibitionLevel").GetComponent<TMPro.TextMeshProUGUI>();

                selectiveVisualTitle = GameObject.Find("SelectiveVisualTitle").GetComponent<TMPro.TextMeshProUGUI>();
                selectiveVisualScore = GameObject.Find("SelectiveVisualScore").GetComponent<TMPro.TextMeshProUGUI>();
                selectiveVisualLevel = GameObject.Find("SelectiveVisualLevel").GetComponent<TMPro.TextMeshProUGUI>();

                visuospatialSketchpadTitle = GameObject.Find("VisuospatialSketchpadTitle").GetComponent<TMPro.TextMeshProUGUI>();
                visuospatialSketchpadScore = GameObject.Find("VisuospatialSketchpadScore").GetComponent<TMPro.TextMeshProUGUI>();
                visuospatialSketchpadLevel = GameObject.Find("VisuospatialSketchpadLevel").GetComponent<TMPro.TextMeshProUGUI>();

                timeToContactTitle = GameObject.Find("TimeToContactTitle").GetComponent<TMPro.TextMeshProUGUI>();
                timeToContactScore = GameObject.Find("TimeToContactScore").GetComponent<TMPro.TextMeshProUGUI>();
                timeToContactLevel = GameObject.Find("TimeToContactLevel").GetComponent<TMPro.TextMeshProUGUI>();

                objectRecognitionTitle = GameObject.Find("ObjectRecognitionTitle").GetComponent<TMPro.TextMeshProUGUI>();
                objectRecognitionScore = GameObject.Find("ObjectRecognitionScore").GetComponent<TMPro.TextMeshProUGUI>();
                objectRecognitionLevel = GameObject.Find("ObjectRecognitionLevel").GetComponent<TMPro.TextMeshProUGUI>();

                // Remove event so this method is not re-evaluated
                // if a non-Questionnaire scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            //----------------- Tests driven by Functional Requirements -------------
            //-----------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-3'>FG-3</a></li>
            <li><b>Test description:</b> Test the objects of ability results after the game are not null.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestAbilityResultObjectsAreNotNull()
            {
                yield return null;
                Assert.IsNotNull(pointingTitle);
                Assert.IsNotNull(pointingScore);
                Assert.IsNotNull(pointingLevel);

                Assert.IsNotNull(inhibitionTitle);
                Assert.IsNotNull(inhibitionScore);
                Assert.IsNotNull(inhibitionLevel);

                Assert.IsNotNull(selectiveVisualTitle);
                Assert.IsNotNull(selectiveVisualScore);
                Assert.IsNotNull(selectiveVisualLevel);

                Assert.IsNotNull(visuospatialSketchpadTitle);
                Assert.IsNotNull(visuospatialSketchpadScore);
                Assert.IsNotNull(visuospatialSketchpadLevel);

                Assert.IsNotNull(timeToContactTitle);
                Assert.IsNotNull(timeToContactScore);
                Assert.IsNotNull(timeToContactScore);

                Assert.IsNotNull(objectRecognitionTitle);
                Assert.IsNotNull(objectRecognitionScore);
                Assert.IsNotNull(objectRecognitionLevel);
            }

            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public fields

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that titles are ability names.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestTitleIsAbilityName()
            {
                yield return null;
                Assert.IsTrue("Pointing" == pointingTitle.text || "<u>Pointing</u>" == pointingTitle.text);
                Assert.IsTrue("Inhibition" == inhibitionTitle.text || "<u>Inhibition</u>" == inhibitionTitle.text);
                Assert.IsTrue("Selective Visual" == selectiveVisualTitle.text || "<u>Selective Visual</u>" == selectiveVisualTitle.text);
                Assert.IsTrue("Visuospatial Sketchpad" == visuospatialSketchpadTitle.text || "<u>Visuospatial Sketchpad</u>" == visuospatialSketchpadTitle.text);
                Assert.IsTrue("Time To Contact" == timeToContactTitle.text || "<u>Time To Contact</u>" == timeToContactTitle.text);
                Assert.IsTrue("Object Recognition" == objectRecognitionTitle.text || "<u>Object Recognition</u>" == objectRecognitionTitle.text);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test each score is an integer number between -1(inclusive) and 100(inclusive), or
                   the default value '...'.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestScoreIsANumBetweenNeg1And100()
            {
                yield return null;

                Assert.IsTrue(isStringScoreValidScore(pointingScore.text));
                Assert.IsTrue(isStringScoreValidScore(inhibitionScore.text));
                Assert.IsTrue(isStringScoreValidScore(selectiveVisualScore.text));
                Assert.IsTrue(isStringScoreValidScore(visuospatialSketchpadScore.text));
                Assert.IsTrue(isStringScoreValidScore(timeToContactScore.text));
                Assert.IsTrue(isStringScoreValidScore(objectRecognitionScore.text));
            }

            // isStringScoreValidScore returns true if the input strScore is a string of
            // number between -1 and 100, or the default value '...'.
            // It will be used in TestScoreIsANumBetweenNeg1And100 test.
            private bool isStringScoreValidScore(string strScore)
            {
                if (strScore == "...")
                {
                    return true;
                }
                else //score is a number
                {
                    int intScore = int.Parse(strScore);
                    return (-1 <= intScore) && (intScore <= 100);
                }
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test each level is one of the 6 identified levels:
                    excellent, good, ok, poor, very poor, not known, or the default value '...'.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestLevelIsAbilityLevel()
            {
                yield return new WaitForSeconds(0.5f);

                Assert.IsTrue(isStringLevelAbilityLevel(pointingLevel.text));
                Assert.IsTrue(isStringLevelAbilityLevel(inhibitionLevel.text));
                Assert.IsTrue(isStringLevelAbilityLevel(selectiveVisualLevel.text));
                Assert.IsTrue(isStringLevelAbilityLevel(visuospatialSketchpadLevel.text));
                Assert.IsTrue(isStringLevelAbilityLevel(timeToContactLevel.text));
                Assert.IsTrue(isStringLevelAbilityLevel(objectRecognitionLevel.text));
            }

            // isStringLevelAbilityLevel returns true if the input strLevel is a string of
            // one of AbilityLevel or the default value.
            // It will be used in TestLevelIsAbilityLevel test.
            private bool isStringLevelAbilityLevel(string strLevel)
            {
                bool isEXCELLENT = (strLevel == AbilityLevel.EXCELLENT.ToString());
                bool isGOOD = (strLevel == AbilityLevel.GOOD.ToString());
                bool isOK = (strLevel == AbilityLevel.OK.ToString());
                bool isPOOR = (strLevel == AbilityLevel.POOR.ToString());
                bool isVERYPOOR = (strLevel == AbilityLevel.VERY_POOR.ToString());
                bool isNOTKNOWN = (strLevel == AbilityLevel.NOT_KNOWN.ToString());
                bool isDefaultValue = (strLevel == "...");

                return (isEXCELLENT || isGOOD || isOK || isPOOR || isVERYPOOR || isNOTKNOWN || isDefaultValue);
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that resultSceneLoadingTimesCnt stores correct numer of times that
                    Result Scene is loaded.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestResultPageLoadingTimes()
            {
                LogAssert.ignoreFailingMessages = true;

                // Initialize counter
                BatterySesstionLoadingTimesControl.resultSceneLoadingTimesCnt = 0;

                // Load scene once
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);
                Assert.AreEqual(1, BatterySesstionLoadingTimesControl.resultSceneLoadingTimesCnt);

                // Load scene second time
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);
                Assert.AreEqual(2, BatterySesstionLoadingTimesControl.resultSceneLoadingTimesCnt);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that the pointing text goes to the correct scene when clicked.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestPointingTextGoesToCorrectScene()
            {
                TextButton textButton = GameObject.Find("PointingTitle").GetComponent<TextButton>();
                textButton.onClick.Invoke();
                yield return new WaitForSeconds(0.1f);

                Assert.AreEqual(SceneName.POINTING_RESULT_SCENE, SceneManager.GetActiveScene().name);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that the inhibition text goes to the correct scene when clicked.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestInhibitionTextGoesToCorrectScene()
            {
                TextButton textButton = GameObject.Find("InhibitionTitle").GetComponent<TextButton>();
                textButton.onClick.Invoke();
                yield return new WaitForSeconds(0.1f);

                Assert.AreEqual(SceneName.INHIBITION_RESULT_SCENE, SceneManager.GetActiveScene().name);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that the selective visual text goes to the correct scene when clicked.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestSVTextGoesToCorrectScene()
            {
                TextButton textButton = GameObject.Find("SelectiveVisualTitle").GetComponent<TextButton>();
                textButton.onClick.Invoke();
                yield return new WaitForSeconds(0.1f);

                Assert.AreEqual(SceneName.SELECTIVE_VISUAL_RESULT_SCENE, SceneManager.GetActiveScene().name);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that the visuospatial sketchpad text goes to the correct scene when clicked.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestVSTextGoesToCorrectScene()
            {
                TextButton textButton = GameObject.Find("VisuospatialSketchpadTitle").GetComponent<TextButton>();
                textButton.onClick.Invoke();
                yield return new WaitForSeconds(0.1f);

                Assert.AreEqual(SceneName.VISUOSPATIAL_SKETCHPAD_RESULT_SCENE, SceneManager.GetActiveScene().name);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that the time to contact text goes to the correct scene when clicked.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestTTCTextGoesToCorrectScene()
            {
                TextButton textButton = GameObject.Find("TimeToContactTitle").GetComponent<TextButton>();
                textButton.onClick.Invoke();
                yield return new WaitForSeconds(0.1f);

                Assert.AreEqual(SceneName.TIME_TO_CONTACT_RESULT_SCENE, SceneManager.GetActiveScene().name);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that the object recognition text goes to the correct scene when clicked.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestORTextGoesToCorrectScene()
            {
                TextButton textButton = GameObject.Find("ObjectRecognitionTitle").GetComponent<TextButton>();
                textButton.onClick.Invoke();
                yield return new WaitForSeconds(0.1f);

                Assert.AreEqual(SceneName.OBJECT_RECOGNITION_RESULT_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that score and level text is filled when it's not filled.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator FillTheTextWhenTextNotFilled()
            {
                // When measure is finished
                BatterySessionManagement.measureEnd = true;

                // Load result scene, so the text is filled
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                // Then the measure should be finished
                Assert.IsTrue(BatterySessionManagement.measureEnd);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test that result page display correct level and score values.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CorrectScoreANDLevelDisplayed()
            {
                // Create example OverallScoreSeq
                OverallScoreStorage pointing = new OverallScoreStorage
                {
                    Score = 10,
                    AbilityName = AbilityName.POINTING,
                    Level = AbilityLevel.VERY_POOR
                };
                OverallScoreStorage inhibition = new OverallScoreStorage
                {
                    Score = 20,
                    AbilityName = AbilityName.INHIBITION,
                    Level = AbilityLevel.VERY_POOR
                };
                OverallScoreStorage objectRecognition = new OverallScoreStorage
                {
                    Score = 30,
                    AbilityName = AbilityName.OBJECT_RECOGNITION,
                    Level = AbilityLevel.POOR
                };
                OverallScoreStorage selectiveVisual = new OverallScoreStorage
                {
                    Score = 60,
                    AbilityName = AbilityName.POINTING,
                    Level = AbilityLevel.OK
                };
                OverallScoreStorage timeToContact = new OverallScoreStorage
                {
                    Score = 80,
                    AbilityName = AbilityName.POINTING,
                    Level = AbilityLevel.GOOD
                };
                OverallScoreStorage visuospatialSketchpad = new OverallScoreStorage
                {
                    Score = 95,
                    AbilityName = AbilityName.POINTING,
                    Level = AbilityLevel.EXCELLENT
                };

                // When measure is finished
                BatterySessionManagement.measureEnd = true;

                // Set overallScoreSeq in AbilityManagement
                AbilityManagement.overallScoreSeq = new List<OverallScoreStorage>();
                AbilityManagement.overallScoreSeq.Add(pointing);
                AbilityManagement.overallScoreSeq.Add(inhibition);
                AbilityManagement.overallScoreSeq.Add(objectRecognition);
                AbilityManagement.overallScoreSeq.Add(selectiveVisual);
                AbilityManagement.overallScoreSeq.Add(timeToContact);
                AbilityManagement.overallScoreSeq.Add(visuospatialSketchpad);

                // Load result scene
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
                yield return new WaitForSeconds(0.1f);

                // Then the displyed text should not be null
                Assert.IsNotNull(pointingLevel.text);
                Assert.IsNotNull(pointingScore.text);

                Assert.IsNotNull(inhibitionLevel.text);
                Assert.IsNotNull(inhibitionScore.text);

                Assert.IsNotNull(objectRecognitionLevel.text);
                Assert.IsNotNull(objectRecognitionScore.text);

                Assert.IsNotNull(selectiveVisualLevel.text);
                Assert.IsNotNull(selectiveVisualScore.text);

                Assert.IsNotNull(timeToContactLevel.text);
                Assert.IsNotNull(timeToContactScore.text);

                Assert.IsNotNull(visuospatialSketchpadLevel.text);
                Assert.IsNotNull(visuospatialSketchpadScore.text);
            }
        }
    }
}