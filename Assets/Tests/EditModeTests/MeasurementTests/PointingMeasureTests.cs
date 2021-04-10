using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using Measurement;
using Storage;
using Helper;
using Games;

namespace EditModeTests
{
    namespace MeasurementTests
    {
        public class PointingMeasureTests
        {
            BalloonsRound balloonRound = new BalloonsRound();
            BalloonsRound balloonRound_shortestPossibleDestClickTime = new BalloonsRound();
            BalloonsRound balloonRound_longDestClickTime = new BalloonsRound();
            BalloonsRound balloonRound_longDistance = new BalloonsRound();
            BalloonsRound balloonRound_hasMultipleClicks = new BalloonsRound();

            [SetUp]
            public void SetUp()
            {
                // Create example clicks:
                List<TimeAndPosition> exampleClicks = new List<TimeAndPosition>();
                TimeAndPosition timeAndPosition_normal = new TimeAndPosition(0.8, new Position2D(0, 0));
                exampleClicks.Add(timeAndPosition_normal);

                // Create example balloon rounds:

                // balloonRound is an exmaple of round
                balloonRound.BalloonSize = 60;
                balloonRound.DestinationPoint = new Position2D(100, 200);
                balloonRound.DestinationClickTime = 0.8;
                balloonRound.Clicks = new List<TimeAndPosition>();
                balloonRound.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_shortestPossibleDestClickTime is an exmaple of round with extreme short DestinationClickTime
                // which is smaller than 0.15s(the fastest human reaction time)
                // other fields are the same with balloonRound
                balloonRound_shortestPossibleDestClickTime.BalloonSize = 60;
                balloonRound_shortestPossibleDestClickTime.DestinationPoint = new Position2D(100, 200);
                balloonRound_shortestPossibleDestClickTime.DestinationClickTime = 0.1; // time smaller than 0.15 which is the fastest human reaction time
                balloonRound_shortestPossibleDestClickTime.Clicks = new List<TimeAndPosition>();
                balloonRound_shortestPossibleDestClickTime.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_longDestClickTime is an exmaple of round with long DestinationClickTime
                // other fields are the same with balloonRound
                balloonRound_longDestClickTime.BalloonSize = 60;
                balloonRound_longDestClickTime.DestinationPoint = new Position2D(100, 200);
                balloonRound_longDestClickTime.DestinationClickTime = 1.5; // long destination click time
                balloonRound_longDestClickTime.Clicks = new List<TimeAndPosition>();
                balloonRound_longDestClickTime.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_longDistance is an exmaple of round with long distance between SuccessClickPoint and DestinationPoint
                // other fields are the same with balloonRound
                balloonRound_longDistance.BalloonSize = 60;
                balloonRound_longDistance.DestinationPoint = new Position2D(100, 200);
                balloonRound_longDistance.DestinationClickTime = 0.8;
                balloonRound_longDistance.Clicks = new List<TimeAndPosition>();
                balloonRound_longDistance.SuccessClickPoint = new Position2D(89, 210); // further distance

                // balloonRound_hasMultipleClicks is an exmaple of round with one unsuccessful click
                // other fields are the same with balloonRound
                balloonRound_hasMultipleClicks.BalloonSize = 60;
                balloonRound_hasMultipleClicks.DestinationPoint = new Position2D(100, 200);
                balloonRound_hasMultipleClicks.DestinationClickTime = 0.8;
                balloonRound_hasMultipleClicks.Clicks = exampleClicks; // one extra click
                balloonRound_hasMultipleClicks.SuccessClickPoint = new Position2D(102, 197);
            }

            // Clear PointingMeasure object
            private void ClearPointingMeasure()
            {
                // Initialize PointingMeasure public fields
                PointingMeasure.subScoreBalloons = new SubscoreStorage();
                PointingMeasure.balloonsData = new BalloonsStorage();
                PointingMeasure.balloonsData.Rounds = new List<BalloonsRound>();
            }

            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public functions

            //--------- For Balloons related methods ------------------------
            //--------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateBalloonsScore function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_EvaluateBalloonsScoreFunctionCalled_BalloonsSubScoreDerived()
            {
                ClearPointingMeasure();

                // Call tested function
                PointingMeasure.EvaluateBalloonsScore();

                yield return null;
                Assert.AreEqual(AbilityName.POINTING, PointingMeasure.subScoreBalloons.AbilityName);
                Assert.AreEqual(GameName.BALLOONS, PointingMeasure.subScoreBalloons.GameName);
                Assert.AreEqual(0, PointingMeasure.subScoreBalloons.Score);
                Assert.AreEqual(2, PointingMeasure.subScoreBalloons.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetSubScoreForBalloons function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetSubScoreForBalloonsFunctionCalled_BalloonsSubScoreReturned()
            {
                ClearPointingMeasure();

                // Set values for PointingMeasure.subScoreBalloons
                PointingMeasure.subScoreBalloons.AbilityName = AbilityName.POINTING;
                PointingMeasure.subScoreBalloons.GameName = GameName.BALLOONS;
                PointingMeasure.subScoreBalloons.Score = 65;
                PointingMeasure.subScoreBalloons.Weight = 2;

                // Call tested function
                SubscoreStorage returnedSubscoreBalloons = PointingMeasure.GetSubScoreForBalloons();
                SubscoreStorage expectedSubscoreBalloons = PointingMeasure.subScoreBalloons;

                yield return null;
                // Test PointingMeasure.subScoreBalloons is correctly returned
                Assert.AreEqual(expectedSubscoreBalloons.AbilityName, returnedSubscoreBalloons.AbilityName);
                Assert.AreEqual(expectedSubscoreBalloons.GameName, returnedSubscoreBalloons.GameName);
                Assert.AreEqual(expectedSubscoreBalloons.Score, returnedSubscoreBalloons.Score);
                Assert.AreEqual(expectedSubscoreBalloons.Weight, returnedSubscoreBalloons.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that destination click time would affect the score.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_DestinationClickTime()
            {
                ClearPointingMeasure();

                // Add balloonRound to balloonsData
                PointingMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_normal = PointingMeasure.subScoreBalloons.Score;

                ClearPointingMeasure();

                // Add balloonRound_longDestClickTime to balloonsData
                PointingMeasure.balloonsData.Rounds.Add(balloonRound_longDestClickTime);
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_longDesClickTime = PointingMeasure.subScoreBalloons.Score;

                yield return null;
                // Test that long destination click time should result a lower score
                Assert.IsTrue(score_normal > score_longDesClickTime);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None<li>
            <li><b>Test description:</b> Test that shortest possible destination click time would result in a higher score.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_ShortestPossibleDestinationClickTime()
            {
                ClearPointingMeasure();

                // Add balloonRound to balloonsData
                PointingMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_normal = PointingMeasure.subScoreBalloons.Score;

                ClearPointingMeasure();

                // Add balloonRound_shortestPossibleDestClickTime to balloonsData
                PointingMeasure.balloonsData.Rounds.Add(balloonRound_shortestPossibleDestClickTime);
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_shortestDesClickTime = PointingMeasure.subScoreBalloons.Score;

                yield return null;
                // Test that shortest possible destination click time should result a higher score
                Assert.IsTrue(score_normal < score_shortestDesClickTime);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that distance between destination
                   balloon and success clicking point would affect the score.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_Distance()
            {
                ClearPointingMeasure();

                // Add balloonRound to balloonsData
                PointingMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_normal = PointingMeasure.subScoreBalloons.Score;

                ClearPointingMeasure();

                // Add balloonRound_longDistance to balloonsData
                PointingMeasure.balloonsData.Rounds.Add(balloonRound_longDistance);
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_longDistance = PointingMeasure.subScoreBalloons.Score;

                yield return null;
                // Test that long distance should result a lower score
                Assert.IsTrue(score_normal > score_longDistance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that number of clicks would affect the score.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_MultipleClicks()
            {
                ClearPointingMeasure();

                // Add balloonRound to balloonsData
                PointingMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_normal = PointingMeasure.subScoreBalloons.Score;

                ClearPointingMeasure();

                // Add balloonRound_hasMultipleClicks to balloonsData
                PointingMeasure.balloonsData.Rounds.Add(balloonRound_hasMultipleClicks);
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_multipleClicks = PointingMeasure.subScoreBalloons.Score;

                yield return null;
                // Test that more clicks should result a lower score
                Assert.IsTrue(score_normal > score_multipleClicks);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that number of rounds played would affect the score.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_RoundsPlayed()
            {
                ClearPointingMeasure();

                // Add 10 balloonRound to balloonsData
                for (int i = 0; i < 10; i++)
                {
                    PointingMeasure.balloonsData.Rounds.Add(balloonRound);
                }
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_10Rounds = PointingMeasure.subScoreBalloons.Score;

                ClearPointingMeasure();

                // Add 20 balloonRound to balloonsData
                for (int i = 0; i < 20; i++)
                {
                    PointingMeasure.balloonsData.Rounds.Add(balloonRound);
                }
                // Call Evaluate function
                PointingMeasure.EvaluateBalloonsScore();
                int score_20Rounds = PointingMeasure.subScoreBalloons.Score;

                yield return null;
                // Test that fewer rounds played should result a lower score
                Assert.IsTrue(score_10Rounds < score_20Rounds);
            }
        }
    }
}