using System.Collections.Generic;
using Measurement;
using Storage;
using UI;
using UnityEngine;
using Newtonsoft.Json;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace EditModeTests
{
    namespace MeasurementTests
    {
        public class TimeToContactMeasureTests
        {
            private const int DUMMY_TARGET_RADIUS = 5;

            public void Setup()
            {
                Globals.isCatchTheBallButtonOn = false;
                Globals.isSaveOneBallButtonOn = false;
                Globals.isJudgeTheBallButtonOn = false;

                TimeToContact.catchTheBallData = new CatchTheBallStorage();
                TimeToContact.saveOneBallData = new SaveOneBallStorage();
                TimeToContact.judgeTheBallData = new JudgeTheBallStorage();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateCatchTheBallScore function when no data is given.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_EvaluateCatchTheBallScoreCalledWithNoData_THEN_ScoreOfNegative1IsObtained()
            {
                Setup();

                TimeToContact.EvaluateCatchTheBallScore();
                SubscoreStorage subscore = TimeToContact.GetSubScoreForCatchTheBall();
                Assert.AreEqual(-1, subscore.Score);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateSaveOneBallScore function when no data is given.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_EvaluateSaveOneBallScoreCalledWithNoData_THEN_ScoreOfNegative1IsObtained()
            {
                Setup();

                TimeToContact.EvaluateSaveOneBallScore();
                SubscoreStorage subscore = TimeToContact.GetSubScoreForSaveOneBall();
                Assert.AreEqual(-1, subscore.Score);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateJudgeTheBallScore function when no data is given.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_EvaluateJudgeTheBallScoreCalledWithNoData_THEN_ScoreOfNegative1IsObtained()
            {
                Setup();

                TimeToContact.EvaluateJudgeTheBallScore();
                SubscoreStorage subscore = TimeToContact.GetSubScoreForJudgeTheBall();
                Assert.AreEqual(-1, subscore.Score);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if Catch The Ball game is played perfectly in all rounds, a
                   perfect score should be given.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_CatchTheBallPlayedPerfectlyInAllRounds_THEN_SubscoreOf100IsObtained()
            {
                Setup();

                TimeToContact.catchTheBallData = new CatchTheBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<CatchTheBallStorage.BallRound>
                    {
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 3.5F,
                            PredictedTimeToContact = 3.5F
                        },
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 5.6F,
                            PredictedTimeToContact = 5.6F
                        },
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 8.3F,
                            PredictedTimeToContact = 8.3F
                        },
                    }
                };

                TimeToContact.EvaluateCatchTheBallScore();

                Assert.AreEqual(100, TimeToContact.GetSubScoreForCatchTheBall().Score);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if Save One Ball game is played perfectly in all rounds, a
                   perfect score should be given.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_SaveOneBallPlayedPerfectlyInAllRounds_THEN_SubscoreOf100IsObtained()
            {
                Setup();

                TimeToContact.saveOneBallData = new SaveOneBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<SaveOneBallStorage.BallRound>
                    {
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 3.5F,
                            RightActualTimeToContact = 3.5F,
                            PredictedTimeToContact = 3.5F,
                            DidPredictFirstArrivingBall = true
                        },
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 5.6F,
                            RightActualTimeToContact = 5.6F,
                            PredictedTimeToContact = 5.6F,
                            DidPredictFirstArrivingBall = true
                        },
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 8.3F,
                            RightActualTimeToContact = 8.3F,
                            PredictedTimeToContact = 8.3F,
                            DidPredictFirstArrivingBall = true
                        },
                    }
                };

                TimeToContact.EvaluateSaveOneBallScore();

                Assert.AreEqual(100, TimeToContact.GetSubScoreForSaveOneBall().Score);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if Judge The Ball game is played perfectly in all rounds, a
                   perfect score should be given.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_JudgeOneBallPlayedPerfectlyInAllRounds_THEN_SubscoreOf100IsObtained()
            {
                Setup();

                TimeToContact.judgeTheBallData = new JudgeTheBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<JudgeTheBallStorage.BallRound>
                    {
                        new JudgeTheBallStorage.BallRound
                        {
                            PlayerBallActualTimeToContact = 3.5F,
                            PredictedTimeToContact = 3.5F,
                        },
                        new JudgeTheBallStorage.BallRound
                        {
                            PlayerBallActualTimeToContact = 5.6F,
                            PredictedTimeToContact = 5.6F,
                        },
                        new JudgeTheBallStorage.BallRound
                        {
                            PlayerBallActualTimeToContact = 8.3F,
                            PredictedTimeToContact = 8.3F
                        },
                    }
                };

                TimeToContact.EvaluateJudgeTheBallScore();

                Assert.AreEqual(100, TimeToContact.GetSubScoreForJudgeTheBall().Score);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if subscore is correctly calculated for Catch The Ball game.
            </li>
        </ul>
        ")]
            [Test]
            public void CatchTheBallSubScoreCalculatedAsAccuracyAverageBetweenRounds()
            {
                Setup();

                TimeToContact.catchTheBallData = new CatchTheBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<CatchTheBallStorage.BallRound>
                    {
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 3.5F,
                            PredictedTimeToContact = 5F
                        },
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 5.6F,
                            PredictedTimeToContact = 5
                        },
                        new CatchTheBallStorage.BallRound
                        {
                            ActualTimeToContact = 9F,
                            PredictedTimeToContact = 8.3F
                        },
                    }
                };

                TimeToContact.EvaluateCatchTheBallScore();

                // FLOOR(1/3 * ((100 - |1 - 5/3.5|*100) + (100 - |1 - 5/5.6|*100) + (100 - |1 - 8.3/9|*100))) = 79

                Assert.AreEqual(79, TimeToContact.GetSubScoreForCatchTheBall().Score);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if subscore is correctly calculated for Save One Ball game.
            </li>
        </ul>
        ")]
            [Test]
            public void SaveOneBallSubScoreCalculatedAsAccuracyAverageBetweenRounds()
            {
                Setup();

                TimeToContact.saveOneBallData = new SaveOneBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<SaveOneBallStorage.BallRound>
                    {
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 3.5F,
                            RightActualTimeToContact = 3.5F,
                            PredictedTimeToContact = 5F,
                            DidPredictFirstArrivingBall = true,
                        },
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 5.6F,
                            RightActualTimeToContact = 5.6F,
                            PredictedTimeToContact = 5,
                            DidPredictFirstArrivingBall = false
                        },
                        new SaveOneBallStorage.BallRound
                        {
                            LeftActualTimeToContact = 9F,
                            RightActualTimeToContact = 9F,
                            PredictedTimeToContact = 8.3F,
                            DidPredictFirstArrivingBall = true
                        },
                    }
                };

                TimeToContact.EvaluateSaveOneBallScore();

                // FLOOR(1/3 * ((100 - |1 - 5/3.5|*100) + 0 + (100 - |1 - 8.3/9|*100))) = 49

                Assert.AreEqual(49, TimeToContact.GetSubScoreForSaveOneBall().Score);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if subscore is correctly calculated for Judge The Ball game.
            </li>
        </ul>
        ")]
            [Test]
            public void JudgeTheBallSubScoreCalculatedAsAccuracyAverageBetweenRounds()
            {
                Setup();

                TimeToContact.judgeTheBallData = new JudgeTheBallStorage
                {
                    TargetRadius = DUMMY_TARGET_RADIUS,
                    Rounds = new List<JudgeTheBallStorage.BallRound>
                    {
                        new JudgeTheBallStorage.BallRound
                        {
                            PlayerBallActualTimeToContact = 3.5F,
                            PredictedTimeToContact = 5F
                        },
                        new JudgeTheBallStorage.BallRound
                        {
                            PlayerBallActualTimeToContact = 5.6F,
                            PredictedTimeToContact = 5
                        },
                        new JudgeTheBallStorage.BallRound
                        {
                            PlayerBallActualTimeToContact = 9F,
                            PredictedTimeToContact = 8.3F
                        },
                    }
                };

                TimeToContact.EvaluateJudgeTheBallScore();

                // FLOOR(1/3 * ((100 - |1 - 5/3.5|*100) + (100 - |1 - 5/5.6|*100) + (100 - |1 - 8.3/9|*100))) = 79

                Assert.AreEqual(79, TimeToContact.GetSubScoreForJudgeTheBall().Score);
            }
        }
    }
}
