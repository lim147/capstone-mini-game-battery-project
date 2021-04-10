using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Management;
using Measurement;
using Storage;
using System;

namespace EditModeTests
{
    namespace ManagementTests
    {
        public class AbilityManagementTests
        {
            // Create some example SubScoreStorage objects for testing:
            private SubscoreStorage inhibition_ctf = new SubscoreStorage
            {
                AbilityName = AbilityName.INHIBITION,
                GameName = Games.GameName.CATCH_THE_THIEF,
                Score = 89,
                Weight = 2
            };

            private SubscoreStorage inhibition_balloons = new SubscoreStorage
            {
                AbilityName = AbilityName.INHIBITION,
                GameName = Games.GameName.BALLOONS,
                Score = 65,
                Weight = 1
            };

            private SubscoreStorage pointing_balloons = new SubscoreStorage
            {
                AbilityName = AbilityName.POINTING,
                GameName = Games.GameName.BALLOONS,
                Score = 71,
                Weight = 2
            };

            // Tolerance for comparing decimal values
            private float tolerance = 0.001f;

            [SetUp]
            public void InitializeObjects()
            {
                // Clear AbilityManagement:
                AbilityManagement.overallScoreSeq = new List<OverallScoreStorage>();
                AbilityManagement.subScoreSeq = new List<SubscoreStorage>();
            }

            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public functions

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test when measure function is called,
                   both subscore sequence and overall score sequence should be updated.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_MeasureFunctionCalled_THEN_ScoreSeqUpdated()
            {
                // Make all games labelled as played
                AbilityManagement.notYetCatchTheBall = false;
                AbilityManagement.notYetJudgeTheBall = false;
                AbilityManagement.notYetSaveOneBall = false;
                AbilityManagement.notYetPlayBalloons = false;
                AbilityManagement.notYetPlayCTF = false;
                AbilityManagement.notYetPlayImageHit = false;
                AbilityManagement.notYetPlaySquares = false;

                // Call tested function
                AbilityManagement.Measure();

                yield return null;

                // subScoreSeq contains 0 to 13 records
                Assert.IsTrue(0 <= AbilityManagement.subScoreSeq.Count
                    && AbilityManagement.subScoreSeq.Count <= 13);
                // overallScoreSeq contains all 6 ability records
                Assert.AreEqual(6, AbilityManagement.overallScoreSeq.Count);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetSubScoreSeq function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetSubScoreSeqFunctionCalled_THEN_SubScoreSeqReturned()
            {
                // Add example SubScoreStorage objects to subScoreSeq
                AbilityManagement.subScoreSeq.Add(inhibition_ctf);

                // Call tested function
                List<SubscoreStorage> gottenSubScoreSeq = AbilityManagement.GetSubScoreSeq();

                yield return null;
                Assert.AreEqual(AbilityManagement.subScoreSeq, gottenSubScoreSeq);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetOverallScoreSeq function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetOverallScoreSeqFunctionCalled_THEN_OverallScoreSeqReturned()
            {
                // Create example OverallScoreStorage object:
                OverallScoreStorage overall_inhibition = new OverallScoreStorage
                {
                    AbilityName = AbilityName.INHIBITION,
                    Score = 100,
                    Level = AbilityLevel.EXCELLENT
                };
                AbilityManagement.overallScoreSeq.Add(overall_inhibition);

                // Call tested function
                List<OverallScoreStorage> gottenOverallScoreSeq = AbilityManagement.GetOverallScoreSeq();

                yield return null;
                Assert.AreEqual(AbilityManagement.overallScoreSeq, gottenOverallScoreSeq);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test CalculateOverallScoreSeq function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_CalculateOverallScoreSeqFunctionCalled_THEN_OverallScoreSeqUpdated()
            {
                // Add example SubScoreStorage objects to subScoreSeq
                AbilityManagement.subScoreSeq.Add(inhibition_ctf);
                AbilityManagement.subScoreSeq.Add(inhibition_balloons);
                AbilityManagement.subScoreSeq.Add(pointing_balloons);

                // Call tested function
                AbilityManagement.CalculateOverallScoreSeq();

                // Expected overall score and level
                int expectedOverallScore_Inhition = (int)Math.Ceiling((double)(89 * 2 + 65 * 1) / (2 + 1));
                int expectedOverallScore_Pointing = (int)Math.Ceiling((double)(71 * 2) / (2));
                AbilityLevel expectedOverallLevel_Inhition = AbilityManagement.EvaluateLevel(expectedOverallScore_Inhition);
                AbilityLevel expectedOverallLevel_Pointing = AbilityManagement.EvaluateLevel(expectedOverallScore_Pointing);

                List<OverallScoreStorage> actualOverallScoreSeq = AbilityManagement.overallScoreSeq;

                yield return null;
                // Test the actualOverallScoreSeq is as expected
                foreach (OverallScoreStorage overallScore in actualOverallScoreSeq)
                {
                    if (overallScore.AbilityName == AbilityName.INHIBITION)
                    {
                        Assert.AreEqual(expectedOverallScore_Inhition, overallScore.Score);
                        Assert.AreEqual(expectedOverallLevel_Inhition, overallScore.Level);
                    }
                    else if (overallScore.AbilityName == AbilityName.POINTING)
                    {
                        Assert.AreEqual(expectedOverallScore_Pointing, overallScore.Score);
                        Assert.AreEqual(expectedOverallLevel_Pointing, overallScore.Level);
                    }
                    else
                    {
                        Assert.AreEqual(-1, overallScore.Score);
                        Assert.AreEqual(AbilityLevel.NOT_KNOWN, overallScore.Level);
                    }
                }
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test CalculateOverallScoreForOneAbility function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_CalculateOverallScoreForOneAbilityFunctionCalled_THEN_AnOverallScoreDerived()
            {
                // Add example SubScoreStorage objects to subScoreSeq
                AbilityManagement.subScoreSeq.Add(inhibition_ctf);
                AbilityManagement.subScoreSeq.Add(inhibition_balloons);
                AbilityManagement.subScoreSeq.Add(pointing_balloons);

                int expectedOverallScore_Inhition = (int)Math.Ceiling((double)(89 * 2 + 65 * 1) / (2 + 1));
                int expectedOverallScore_Pointing = (int)Math.Ceiling((double)(71 * 2) / (2));

                int actualOverallScore_Inhition = AbilityManagement.CalculateOverallScoreForOneAbility(AbilityName.INHIBITION);
                int actualOverallScore_Pointing = AbilityManagement.CalculateOverallScoreForOneAbility(AbilityName.POINTING);

                yield return null;
                Assert.IsTrue(Mathf.Abs(expectedOverallScore_Inhition - actualOverallScore_Inhition) <= tolerance);
                Assert.IsTrue(Mathf.Abs(expectedOverallScore_Pointing - actualOverallScore_Pointing) <= tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateLevel function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_EvaluateLevelFunctionCalled_THEN_LevelDerived()
            {
                Assert.AreEqual(typeof(AbilityLevel), AbilityManagement.EvaluateLevel(10).GetType());
                yield return null;
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluationLevel function such that when input is
                   an integer that is over 90, the output level should be excellent.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestEvaluateLevelFunction_InputIsOver90_OutputEXCELLENT()
            {
                yield return null;
                // Test the input has to be strict greater than 90, to get EXCELLENT
                Assert.AreEqual(AbilityLevel.EXCELLENT, AbilityManagement.EvaluateLevel(100));
                Assert.AreEqual(AbilityLevel.EXCELLENT, AbilityManagement.EvaluateLevel(95));

                // Edge case tests for 90: pick 89, 90, 91 as input
                Assert.AreEqual(AbilityLevel.EXCELLENT, AbilityManagement.EvaluateLevel(91));
                Assert.AreNotEqual(AbilityLevel.EXCELLENT, AbilityManagement.EvaluateLevel(90));
                Assert.AreNotEqual(AbilityLevel.EXCELLENT, AbilityManagement.EvaluateLevel(89));
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test description:</b> Test EvaluationLevel function such that when input is
                   an integer that is between 75 and 90, the output level should be good.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestEvaluateLevel_InputIsBetween75And90_OutputGOOD()
            {
                yield return null;
                // Test the input has to be strict greater than 75 and less than or equal to 90, to get GOOD
                // Edge case tests for 90: pick 89, 90, 91 as input
                Assert.AreNotEqual(AbilityLevel.GOOD, AbilityManagement.EvaluateLevel(91));
                Assert.AreEqual(AbilityLevel.GOOD, AbilityManagement.EvaluateLevel(90));
                Assert.AreEqual(AbilityLevel.GOOD, AbilityManagement.EvaluateLevel(89));

                Assert.AreEqual(AbilityLevel.GOOD, AbilityManagement.EvaluateLevel(80));

                // Edge case tests for 75: pick 76, 75, 74 as input
                Assert.AreEqual(AbilityLevel.GOOD, AbilityManagement.EvaluateLevel(76));
                Assert.AreNotEqual(AbilityLevel.GOOD, AbilityManagement.EvaluateLevel(75));
                Assert.AreNotEqual(AbilityLevel.GOOD, AbilityManagement.EvaluateLevel(74));
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test description:</b> Test EvaluationLevel function such that when input is
                   an integer that is between 50 and 75, the output level should be ok.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestEvaluateLevel_InputIsBetween50And75_OutputOK()
            {
                yield return null;
                // Test the input has to be strict greater than 50 and less than or equal to 75, to get OK
                // Edge case tests for 75: pick 76, 75, 74 as input
                Assert.AreNotEqual(AbilityLevel.OK, AbilityManagement.EvaluateLevel(76));
                Assert.AreEqual(AbilityLevel.OK, AbilityManagement.EvaluateLevel(75));
                Assert.AreEqual(AbilityLevel.OK, AbilityManagement.EvaluateLevel(74));

                Assert.AreEqual(AbilityLevel.OK, AbilityManagement.EvaluateLevel(55));

                // Edge case tests for 50: pick 51, 50, 49 as input
                Assert.AreEqual(AbilityLevel.OK, AbilityManagement.EvaluateLevel(51));
                Assert.AreNotEqual(AbilityLevel.OK, AbilityManagement.EvaluateLevel(50));
                Assert.AreNotEqual(AbilityLevel.OK, AbilityManagement.EvaluateLevel(49));
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test description:</b> Test EvaluationLevel function such that when input is
                   an integer that is between 25 and 50, the output level should be poor.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestEvaluateLevel_InputIsBetween25And50_OutputPOOR()
            {
                yield return null;
                // Test the input has to be strict greater than 25 and less than or equal to 50, to get POOR
                // Edge case tests for 50: pick 51, 50, 49 as input
                Assert.AreNotEqual(AbilityLevel.POOR, AbilityManagement.EvaluateLevel(51));
                Assert.AreEqual(AbilityLevel.POOR, AbilityManagement.EvaluateLevel(50));
                Assert.AreEqual(AbilityLevel.POOR, AbilityManagement.EvaluateLevel(49));

                Assert.AreEqual(AbilityLevel.POOR, AbilityManagement.EvaluateLevel(42));

                // Edge case tests for 25: pick 26, 25, 24 as input
                Assert.AreEqual(AbilityLevel.POOR, AbilityManagement.EvaluateLevel(26));
                Assert.AreNotEqual(AbilityLevel.POOR, AbilityManagement.EvaluateLevel(25));
                Assert.AreNotEqual(AbilityLevel.POOR, AbilityManagement.EvaluateLevel(24));
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test description:</b> Test EvaluationLevel function such that when input is
                   an integer that is between 0 and 25, the output level should be very poor.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestEvaluateLevel_InputIsBetween0And25_OutputVERYPOOR()
            {
                yield return null;
                // Test the input has to be greater than or equal to 0 and less than or equal to 25, to get VERY POOR
                // Edge case tests for 25: pick 26, 25, 24 as input
                Assert.AreNotEqual(AbilityLevel.VERY_POOR, AbilityManagement.EvaluateLevel(26));
                Assert.AreEqual(AbilityLevel.VERY_POOR, AbilityManagement.EvaluateLevel(25));
                Assert.AreEqual(AbilityLevel.VERY_POOR, AbilityManagement.EvaluateLevel(24));

                Assert.AreEqual(AbilityLevel.VERY_POOR, AbilityManagement.EvaluateLevel(12));

                // Edge case tests for 0: 1, 0, -1 as input
                Assert.AreEqual(AbilityLevel.VERY_POOR, AbilityManagement.EvaluateLevel(1));
                Assert.AreEqual(AbilityLevel.VERY_POOR, AbilityManagement.EvaluateLevel(0));
                Assert.AreNotEqual(AbilityLevel.VERY_POOR, AbilityManagement.EvaluateLevel(-1));
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test description:</b> Test EvaluationLevel function such that when input is
                   an integer that is -1, the output level should be not known.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestEvaluateLevel_InputIsNeg1_OutputNOTKNOWN()
            {
                yield return null;
                Assert.AreNotEqual(AbilityLevel.NOT_KNOWN, AbilityManagement.EvaluateLevel(0));
                Assert.AreEqual(AbilityLevel.NOT_KNOWN, AbilityManagement.EvaluateLevel(-1));
            }
        }
    }
}