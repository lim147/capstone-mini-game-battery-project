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
        public class SelectiveVisualMeasureTests
        {
            BalloonsRound balloonRound = new BalloonsRound();
            BalloonsRound balloonRound_shortestPossibleDestClickTime = new BalloonsRound();
            BalloonsRound balloonRound_longDestinationClickTime = new BalloonsRound();
            BalloonsRound balloonRound_longTimeInClicks = new BalloonsRound();

            // Variables to hold predetermined round information for rounds in the Squares mini-game
            SquaresRound squareRound = new SquaresRound();
            SquaresRound squareRound_sameOrder = new SquaresRound();
            SquaresRound squareRound_differentOrder = new SquaresRound();
            SquaresRound squareRound_gap = new SquaresRound();
            SquaresRound squareRound_mismatch = new SquaresRound();

            CatchTheThiefRound catchRound = new CatchTheThiefRound();
            CatchTheThiefRound catchRound_longTimeInClicks = new CatchTheThiefRound();
            CatchTheThiefRound catchRound_showTimeInClicks = new CatchTheThiefRound();

            List<ImageHitRound> imaghtHitRound = new List<ImageHitRound>();
            List<ImageHitRound> imahtHitRound_longTimeInClicks = new List<ImageHitRound>();
            List<ImageHitRound> imageHit_showTimeInClicks = new List<ImageHitRound>();

            [SetUp]
            public void SetUp()
            {
                // Create example clicks:
                List<TimeAndPosition> exampleClicks = new List<TimeAndPosition>();
                TimeAndPosition timeAndPosition_longTime = new TimeAndPosition(1.5, new Position2D(0, 0));
                exampleClicks.Add(timeAndPosition_longTime);

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

                // balloonRound_longMoveTimeInDestinationClickTime is an exmaple of round with long DestinationClickTime
                // other fields are the same with balloonRound
                balloonRound_longDestinationClickTime.BalloonSize = 60;
                balloonRound_longDestinationClickTime.DestinationPoint = new Position2D(100, 200);
                balloonRound_longDestinationClickTime.DestinationClickTime = 1.5; // long destination click time
                balloonRound_longDestinationClickTime.Clicks = new List<TimeAndPosition>();
                balloonRound_longDestinationClickTime.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_longMoveTimeInClicks is an exmaple of round with long time in clicks items
                // other fields are the same with balloonRound
                balloonRound_longTimeInClicks.BalloonSize = 60;
                balloonRound_longTimeInClicks.DestinationPoint = new Position2D(100, 200);
                balloonRound_longTimeInClicks.DestinationClickTime = 0.8;
                balloonRound_longTimeInClicks.Clicks = exampleClicks; // long time in clicks item
                balloonRound_longTimeInClicks.SuccessClickPoint = new Position2D(102, 197);

                // Example squares
                IndexAndPosition IndexAndPositionIndex0 = new IndexAndPosition(0, new Position2D(0, 0));
                IndexAndPosition IndexAndPositionIndex1 = new IndexAndPosition(1, new Position2D(1, 1));

                // Set data for round where Highlighted and Recalled are identical in Squares mini-game
                squareRound_sameOrder.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_sameOrder.HighlightedSquares.Add(IndexAndPositionIndex0);
                squareRound_sameOrder.HighlightedSquares.Add(IndexAndPositionIndex1);

                squareRound_sameOrder.RecalledSquares = new List<IndexAndPosition>();
                squareRound_sameOrder.RecalledSquares.Add(IndexAndPositionIndex0);
                squareRound_sameOrder.RecalledSquares.Add(IndexAndPositionIndex1);

                squareRound_sameOrder.RecallTime = 4f;
                squareRound_sameOrder.SquareHighlightInterval = 0.7f;
                squareRound_sameOrder.SquareHighlightInterval = 0.7f;

                // Set data for round where Highlighted and Recalled squares are in a different order in Squares mini-game
                squareRound_differentOrder.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_differentOrder.HighlightedSquares.Add(IndexAndPositionIndex0);
                squareRound_differentOrder.HighlightedSquares.Add(IndexAndPositionIndex1);

                squareRound_differentOrder.RecalledSquares = new List<IndexAndPosition>();
                squareRound_differentOrder.RecalledSquares.Add(IndexAndPositionIndex1);
                squareRound_differentOrder.RecalledSquares.Add(IndexAndPositionIndex0);

                squareRound_differentOrder.RecallTime = 4f;
                squareRound_differentOrder.SquareHighlightInterval = 0.7f;
                squareRound_differentOrder.SquareHighlightInterval = 0.7f;

                // Set data for round where Recalled squares has a gap value in Squares mini-game
                squareRound_gap.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_gap.HighlightedSquares.Add(IndexAndPositionIndex0);
                squareRound_gap.HighlightedSquares.Add(IndexAndPositionIndex1);

                squareRound_gap.RecalledSquares = new List<IndexAndPosition>();
                squareRound_gap.RecalledSquares.Add(IndexAndPositionIndex0);

                squareRound_gap.RecallTime = 4f;
                squareRound_gap.SquareHighlightInterval = 0.7f;
                squareRound_gap.SquareHighlightInterval = 0.7f;

                // Set data for round where Recalled squares has a mismatch value in Squares mini-game
                squareRound_mismatch.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_mismatch.HighlightedSquares.Add(IndexAndPositionIndex0);
                squareRound_mismatch.HighlightedSquares.Add(IndexAndPositionIndex1);

                squareRound_mismatch.RecalledSquares = new List<IndexAndPosition>();
                squareRound_mismatch.RecalledSquares.Add(IndexAndPositionIndex0);

                squareRound_mismatch.RecallTime = 4f;
                squareRound_mismatch.SquareHighlightInterval = 0.7f;
                squareRound_mismatch.SquareHighlightInterval = 0.7f;

                catchRound.IsIdentifiedKeyPressed = true;
                catchRound.identifiedKeyPressTime = 0.15f;
                catchRound.ThiefAppearInRound = false;
                catchRound.PersonAppearInRound = true;

                catchRound_longTimeInClicks.IsIdentifiedKeyPressed = true;
                catchRound_longTimeInClicks.identifiedKeyPressTime = 0.5f;
                catchRound_longTimeInClicks.ThiefAppearInRound = true;
                catchRound_longTimeInClicks.PersonAppearInRound = false;

                catchRound_showTimeInClicks.IsIdentifiedKeyPressed = true;
                catchRound_showTimeInClicks.identifiedKeyPressTime = 0.8f;
                catchRound_showTimeInClicks.ThiefAppearInRound = true;
                catchRound_showTimeInClicks.PersonAppearInRound = false;

                catchRound_showTimeInClicks.IsIdentifiedKeyPressed = true;
                catchRound_showTimeInClicks.identifiedKeyPressTime = 0.8f;
                catchRound_showTimeInClicks.ThiefAppearInRound = true;
                catchRound_showTimeInClicks.PersonAppearInRound = false;

                imaghtHitRound.Add(new ImageHitRound());
                imahtHitRound_longTimeInClicks.Add(new ImageHitRound());
                imageHit_showTimeInClicks.Add(new ImageHitRound());

                imaghtHitRound[0] = new ImageHitRound();
                imaghtHitRound[0].imageName = "";
                imaghtHitRound[0].imageTheme = "";
                imaghtHitRound[0].isCorrectlyIdentified = false;
                imaghtHitRound[0].isKeyPressed = false;
                imaghtHitRound[0].isSpaceKey = false;
                imaghtHitRound[0].keyPressTime = 0.5f;
                imaghtHitRound[0].testTheme = "";

                imahtHitRound_longTimeInClicks[0] = new ImageHitRound();
                imahtHitRound_longTimeInClicks[0].imageName = "";
                imahtHitRound_longTimeInClicks[0].imageTheme = "";
                imahtHitRound_longTimeInClicks[0].isCorrectlyIdentified = false;
                imahtHitRound_longTimeInClicks[0].isKeyPressed = false;
                imahtHitRound_longTimeInClicks[0].isSpaceKey = false;
                imahtHitRound_longTimeInClicks[0].keyPressTime = 0.2f;
                imahtHitRound_longTimeInClicks[0].testTheme = "";

                imageHit_showTimeInClicks[0] = new ImageHitRound();
                imageHit_showTimeInClicks[0].imageName = "";
                imageHit_showTimeInClicks[0].imageTheme = "";
                imageHit_showTimeInClicks[0].isCorrectlyIdentified = false;
                imageHit_showTimeInClicks[0].isKeyPressed = false;
                imageHit_showTimeInClicks[0].isSpaceKey = false;
                imageHit_showTimeInClicks[0].keyPressTime = 0.8f;
                imageHit_showTimeInClicks[0].testTheme = "";
            }

            // Clear SelectiveVisualMeasure object
            private void ClearSelectiveVisualMeasure()
            {
                // Initialize SelectiveVisualMeasure public fields
                SelectiveVisualMeasure.subScoreBalloons = new SubscoreStorage();
                SelectiveVisualMeasure.balloonsData = new BalloonsStorage();
                SelectiveVisualMeasure.balloonsData.Rounds = new List<BalloonsRound>();

                SelectiveVisualMeasure.subScoreCTF = new SubscoreStorage();
                SelectiveVisualMeasure.ctfData = new CatchTheThiefStorage();
                SelectiveVisualMeasure.ctfData.Rounds = new List<CatchTheThiefRound>();

                SelectiveVisualMeasure.subScoreImageHit = new SubscoreStorage();
                SelectiveVisualMeasure.imagehitData = new ImageHitStorage();
                SelectiveVisualMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                SelectiveVisualMeasure.subScoreSquares = new SubscoreStorage();
                SelectiveVisualMeasure.squaresData = new SquaresStorage();
                SelectiveVisualMeasure.squaresData.Rounds = new List<SquaresRound>();
            }

            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public functions

            //--------- For Image Hit related methods ------------------------
            //--------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if correct score is calculated for game image hit.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckImageHitSource()
            {
                ClearSelectiveVisualMeasure();

                // Call tested function

                yield return null;
                SelectiveVisualMeasure.imagehitData.Rounds.Add(new List<ImageHitRound>());
                SelectiveVisualMeasure.imagehitData.Rounds.Add(new List<ImageHitRound>());
                float sorce = SelectiveVisualMeasure.GetScore();
                Assert.True(sorce == 0, "message no Source");

                SelectiveVisualMeasure.imagehitData = null;
                float sorce1 = SelectiveVisualMeasure.GetScore();
                Assert.True(sorce1 == 0, "message no Source");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test MatchKeyPressTimeToScore function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckMatchKeyPressTimeToScore()
            {
                ClearSelectiveVisualMeasure();

                yield return null;

                float sorce = SelectiveVisualMeasure.MatchKeyPressTimeToScore(0.1);
                Assert.True(sorce == 100, "message no Source");

                float sorce1 = SelectiveVisualMeasure.MatchKeyPressTimeToScore(4);
                Assert.True(sorce1 <= 0, "message no Source");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetScoreFromOneImage function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetScoreFromOneImage()
            {
                ClearSelectiveVisualMeasure();
                yield return null;

                ImageHitRound imageHitRound = new ImageHitRound();
                imageHitRound.isKeyPressed = true;
                imageHitRound.isCorrectlyIdentified = true;
                float source = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound, 30f);
                Assert.AreEqual(30, source, "message no Source");

                ImageHitRound imageHitRound2 = new ImageHitRound();
                imageHitRound2.isKeyPressed = true;
                imageHitRound2.isCorrectlyIdentified = false;
                imageHitRound2.keyPressTime = 0.6f;
                float source2 = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound2, 30f);

                Assert.AreEqual(18, source2, "message no Source");

                ImageHitRound imageHitRound3 = null;
                //imageHitRound.isKeyPressed = true;
                //imageHitRound.isCorrectlyIdentified = false;
                float source3 = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound3, 30f);
                Assert.AreEqual(0, source3, "message no Source");

                ImageHitRound imageHitRound4 = new ImageHitRound();
                imageHitRound4.isKeyPressed = false;
                imageHitRound4.isCorrectlyIdentified = true;
                imageHitRound4.keyPressTime = 1.2f;
                float source4 = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound4, 30f);
                Assert.AreEqual(0, source4, "message no Source");

                ImageHitRound imageHitRound5 = new ImageHitRound();
                imageHitRound5.isKeyPressed = false;
                imageHitRound5.isCorrectlyIdentified = true;
                imageHitRound5.keyPressTime = 1.6f;
                float source5 = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound5, 30f);
                Assert.AreEqual(0, source5, "message no Source");

                ImageHitRound imageHitRound6 = new ImageHitRound();
                imageHitRound6.isKeyPressed = false;
                imageHitRound6.isCorrectlyIdentified = true;
                imageHitRound6.keyPressTime = 2.1f;
                float source6 = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound6, 30f);
                Assert.AreEqual(0, source6, "message no Source");

                ImageHitRound imageHitRound7 = new ImageHitRound();
                imageHitRound7.isKeyPressed = false;
                imageHitRound7.isCorrectlyIdentified = true;
                imageHitRound7.keyPressTime = 2.6f;
                float source7 = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound7, 30f);
                Assert.AreEqual(0, source7, "message no Source");

                ImageHitRound imageHitRound8 = new ImageHitRound();
                imageHitRound8.isKeyPressed = false;
                imageHitRound8.isCorrectlyIdentified = false;
                float source8 = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound8, 30f);
                Assert.AreEqual(0, source8, "message no Source");

                ImageHitRound imageHitRound9 = new ImageHitRound();
                imageHitRound9.isKeyPressed = false;
                imageHitRound9.isCorrectlyIdentified = false;
                imageHitRound9.keyPressTime = 0.9f;
                float source9 = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound9, 30f);
                Assert.AreEqual(0, source9, "message no Source");

                ImageHitRound imageHitRound10 = new ImageHitRound();
                imageHitRound10.isKeyPressed = false;
                imageHitRound10.isCorrectlyIdentified = false;
                imageHitRound10.keyPressTime = 1.1f;
                float source10= SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound10, 30f);
                Assert.AreEqual(0, source10, "message no Source");

                ImageHitRound imageHitRound11 = new ImageHitRound();
                imageHitRound11.isKeyPressed = false;
                imageHitRound11.isCorrectlyIdentified = false;
                imageHitRound11.keyPressTime = 1.6f;
                float source11 = SelectiveVisualMeasure.GetScoreFromOneImage(imageHitRound11, 30f);
                Assert.AreEqual(0, source11, "message no Source");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetKeyNum function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetKeyNum()
            {
                ClearSelectiveVisualMeasure();
                yield return null;

                SelectiveVisualMeasure.imagehitData = null;
                float source = SelectiveVisualMeasure.GetKeyNum(0);
                Assert.True(source == 0, "message no Source");

                SelectiveVisualMeasure.imagehitData = new ImageHitStorage();
                SelectiveVisualMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                for (int i = 0; i <= 10; i++)
                {
                    List<ImageHitRound> testRound = new List<ImageHitRound>();

                    for (int j = 0; j <= 10; j++)
                    {
                        ImageHitRound testro = new ImageHitRound();
                        testro.imageName = "111";
                        testRound.Add(testro);
                    }

                    SelectiveVisualMeasure.imagehitData.Rounds.Add(testRound);
                }

                float source1 = SelectiveVisualMeasure.GetKeyNum(0);

                Assert.True(source1 == 0, "message no Source");

                SelectiveVisualMeasure.imagehitData = null;

                SelectiveVisualMeasure.imagehitData = new ImageHitStorage();
                SelectiveVisualMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                for (int i = 0; i <= 10; i++)
                {
                    List<ImageHitRound> testRound = new List<ImageHitRound>();

                    for (int j = 0; j <= 10; j++)
                    {
                        ImageHitRound testro = new ImageHitRound();
                        testro.imageName = "111";
                        testro.isKeyPressed = j % 2 != 0;
                        testRound.Add(testro);
                    }

                    SelectiveVisualMeasure.imagehitData.Rounds.Add(testRound);
                }

                float source2 = SelectiveVisualMeasure.GetKeyNum(0);
                Assert.True(source2 == 20, "message no Source");
            }

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
                ClearSelectiveVisualMeasure();

                // Call tested function
                SelectiveVisualMeasure.EvaluateBalloonsScore();

                yield return null;
                Assert.AreEqual(AbilityName.SELECTIVE_VISUAL, SelectiveVisualMeasure.subScoreBalloons.AbilityName);
                Assert.AreEqual(GameName.BALLOONS, SelectiveVisualMeasure.subScoreBalloons.GameName);
                Assert.AreEqual(0, SelectiveVisualMeasure.subScoreBalloons.Score);
                Assert.AreEqual(2, SelectiveVisualMeasure.subScoreBalloons.Weight);
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
            public IEnumerator WHEN_GetSubScoreForBalloonesFunctionCalled_BalloonsSubScoreReturned()
            {
                ClearSelectiveVisualMeasure();

                // Set values for SelectiveVisualMeasure.subScoreBalloons
                SelectiveVisualMeasure.subScoreBalloons.AbilityName = AbilityName.SELECTIVE_VISUAL;
                SelectiveVisualMeasure.subScoreBalloons.GameName = GameName.BALLOONS;
                SelectiveVisualMeasure.subScoreBalloons.Score = 49;
                SelectiveVisualMeasure.subScoreBalloons.Weight = 2;

                // Call tested function
                SubscoreStorage returnedSubscoreBalloons = SelectiveVisualMeasure.GetSubScoreForBalloons();
                SubscoreStorage expectedSubscoreBalloons = SelectiveVisualMeasure.subScoreBalloons;

                yield return null;
                // Test SelectiveVisualMeasure.subScoreBalloons is correctly returned
                Assert.AreEqual(expectedSubscoreBalloons.AbilityName, returnedSubscoreBalloons.AbilityName);
                Assert.AreEqual(expectedSubscoreBalloons.GameName, returnedSubscoreBalloons.GameName);
                Assert.AreEqual(expectedSubscoreBalloons.Score, returnedSubscoreBalloons.Score);
                Assert.AreEqual(expectedSubscoreBalloons.Weight, returnedSubscoreBalloons.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that shortest possible destination click time would result in a higher score.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_ShortestPossibleDestinationClickTime()
            {
                ClearSelectiveVisualMeasure();

                // Add balloonRound to balloonsData
                SelectiveVisualMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                SelectiveVisualMeasure.EvaluateBalloonsScore();
                int score_normal = SelectiveVisualMeasure.subScoreBalloons.Score;

                ClearSelectiveVisualMeasure();

                // Add balloonRound_shortestPossibleDestClickTime to balloonsData
                SelectiveVisualMeasure.balloonsData.Rounds.Add(balloonRound_shortestPossibleDestClickTime);
                // Call Evaluate function
                SelectiveVisualMeasure.EvaluateBalloonsScore();
                int score_shortestDesClickTime = SelectiveVisualMeasure.subScoreBalloons.Score;

                yield return null;
                // Test that shortest possible destination click time should result a higher score
                Assert.IsTrue(score_normal < score_shortestDesClickTime);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that move time would affect the score in
                   the situation that move time is DestinationClickTime.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_MoveTimeIsDestinationClickTime()
            {
                ClearSelectiveVisualMeasure();

                // Add balloonRound to balloonsData
                SelectiveVisualMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                SelectiveVisualMeasure.EvaluateBalloonsScore();
                int score_normal = SelectiveVisualMeasure.subScoreBalloons.Score;

                ClearSelectiveVisualMeasure();

                // Add balloonRound_longDestClickTime to balloonsData
                SelectiveVisualMeasure.balloonsData.Rounds.Add(balloonRound_longDestinationClickTime);
                // Call Evaluate function
                SelectiveVisualMeasure.EvaluateBalloonsScore();
                int score_longDesClickTime = SelectiveVisualMeasure.subScoreBalloons.Score;

                yield return null;
                // Test that long destination click time should result a lower score
                Assert.IsTrue(score_normal > score_longDesClickTime);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that move time would affect the score in the situation
                   that move time is time of last item in clicks list.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_MoveTimeIsTimeInClicks()
            {
                ClearSelectiveVisualMeasure();

                // Add balloonRound to balloonsData
                SelectiveVisualMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                SelectiveVisualMeasure.EvaluateBalloonsScore();
                int score_normal = SelectiveVisualMeasure.subScoreBalloons.Score;

                ClearSelectiveVisualMeasure();

                // Add balloonRound_longTimeInClicks to balloonsData
                SelectiveVisualMeasure.balloonsData.Rounds.Add(balloonRound_longTimeInClicks);
                // Call Evaluate function
                SelectiveVisualMeasure.EvaluateBalloonsScore();
                int score_longTimeInClicks = SelectiveVisualMeasure.subScoreBalloons.Score;

                yield return null;
                // Test that long destination click time should result a lower score
                Assert.IsTrue(score_normal > score_longTimeInClicks);
            }

            //--------- For catch the thief related methods ------------------------
            //----------------------------------------------------------------------
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateCTFScore function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_EvaluateCTFScore()
            {
                ClearSelectiveVisualMeasure();
                yield return null;

                SelectiveVisualMeasure.ctfData.Rounds = new List<CatchTheThiefRound>();
                CatchTheThiefRound catchTheThiefRound = new CatchTheThiefRound();
                catchTheThiefRound.ThiefAppearInRound = true;
                catchTheThiefRound.IsIdentifiedKeyPressed = false;
                catchTheThiefRound.PersonAppearInRound = false;
                SelectiveVisualMeasure.ctfData.Rounds.Add(catchTheThiefRound);

                SelectiveVisualMeasure.EvaluateCTFScore();
                Assert.AreEqual(AbilityName.SELECTIVE_VISUAL, SelectiveVisualMeasure.subScoreCTF.AbilityName);
                Assert.AreEqual(GameName.CATCH_THE_THIEF, SelectiveVisualMeasure.subScoreCTF.GameName);
                Assert.AreEqual(0, SelectiveVisualMeasure.subScoreCTF.Score);
                Assert.AreEqual(2, SelectiveVisualMeasure.subScoreCTF.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test WHEN_GetSubScoreForCTF function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetSubScoreForCTF()
            {
                ClearSelectiveVisualMeasure();

                // Set values for SelectiveVisualMeasure.subScoreBalloons
                SelectiveVisualMeasure.subScoreCTF.AbilityName = AbilityName.SELECTIVE_VISUAL;
                SelectiveVisualMeasure.subScoreCTF.GameName = GameName.BALLOONS;
                SelectiveVisualMeasure.subScoreCTF.Score = 49;
                SelectiveVisualMeasure.subScoreCTF.Weight = 2;

                // Call tested function
                SubscoreStorage returnedSubscoreBalloons = SelectiveVisualMeasure.GetSubScoreForCTF();
                SubscoreStorage expectedSubscoreBalloons = SelectiveVisualMeasure.subScoreCTF;

                yield return null;
                // Test SelectiveVisualMeasure.subScoreBalloons is correctly returned
                Assert.AreEqual(expectedSubscoreBalloons.AbilityName, returnedSubscoreBalloons.AbilityName);
                Assert.AreEqual(expectedSubscoreBalloons.GameName, returnedSubscoreBalloons.GameName);
                Assert.AreEqual(expectedSubscoreBalloons.Score, returnedSubscoreBalloons.Score);
                Assert.AreEqual(expectedSubscoreBalloons.Weight, returnedSubscoreBalloons.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description: Test that unsuccessful identifications result in a lower score.</b> 
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestCatchRound_showTimeInClicks()
            {
                ClearSelectiveVisualMeasure();

                // Get score for having 14 unsuccessful click
                SelectiveVisualMeasure.ctfData.Rounds.Add(catchRound);
                SelectiveVisualMeasure.EvaluateCTFScore();
                int score_has14UnsuccessfulClick = SelectiveVisualMeasure.subScoreCTF.Score;
                ClearSelectiveVisualMeasure();

                // Get score for having 20 unsuccessful click
                SelectiveVisualMeasure.ctfData.Rounds.Add(catchRound_showTimeInClicks);
                SelectiveVisualMeasure.EvaluateCTFScore();
                int score_has20UnsuccessfulClick = SelectiveVisualMeasure.subScoreCTF.Score;

                yield return null;
                // Test that more unsuccessful clicks should result a lower score
                Assert.IsTrue(score_has14UnsuccessfulClick < score_has20UnsuccessfulClick);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description: Test that unsuccessful identifications result in a lower score.</b> 
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestCatchRoundThiefAppearInRound()
            {
                ClearSelectiveVisualMeasure();

                // Get score for having 14 unsuccessful click
                SelectiveVisualMeasure.ctfData.Rounds.Add(catchRound);
                SelectiveVisualMeasure.EvaluateCTFScore();
                int score_has14UnsuccessfulClick = SelectiveVisualMeasure.subScoreCTF.Score;
                ClearSelectiveVisualMeasure();

                // Get score for having 20 unsuccessful click
                SelectiveVisualMeasure.ctfData.Rounds.Add(catchRound_longTimeInClicks);
                SelectiveVisualMeasure.EvaluateCTFScore();
                int score_has20UnsuccessfulClick = SelectiveVisualMeasure.subScoreCTF.Score;

                yield return null;
                // Test that more unsuccessful clicks should result a lower score
                Assert.IsTrue(score_has14UnsuccessfulClick < score_has20UnsuccessfulClick);
            }

            //--------- For Image hit related methods ------------------------------
            //----------------------------------------------------------------------
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateImageHitScore function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_EvaluateImageHitScore()
            {
                ClearSelectiveVisualMeasure();

                // Call tested function
                SelectiveVisualMeasure.EvaluateImageHitScore();

                yield return null;
                Assert.AreEqual(AbilityName.SELECTIVE_VISUAL, SelectiveVisualMeasure.subScoreImageHit.AbilityName);
                Assert.AreEqual(GameName.IMAGE_HIT, SelectiveVisualMeasure.subScoreImageHit.GameName);
                Assert.AreEqual(0, SelectiveVisualMeasure.subScoreImageHit.Score);
                Assert.AreEqual(1, SelectiveVisualMeasure.subScoreImageHit.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetSubScoreForImageHit function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetSubScoreForImageHit()
            {
                ClearSelectiveVisualMeasure();

                // Set values for InhibitionMeasure.subScoreImageHit
                SelectiveVisualMeasure.subScoreImageHit.AbilityName = AbilityName.SELECTIVE_VISUAL;
                SelectiveVisualMeasure.subScoreImageHit.GameName = GameName.IMAGE_HIT;
                SelectiveVisualMeasure.subScoreImageHit.Score = 89;
                SelectiveVisualMeasure.subScoreImageHit.Weight = 2;

                // Call tested function
                SubscoreStorage returnedSubscoreImageHit = SelectiveVisualMeasure.GetSubScoreForImageHit();
                SubscoreStorage expectedSubscoreImageHit = SelectiveVisualMeasure.subScoreImageHit;

                yield return null;
                // Test InhibitionMeasure.subScoreImageHit is correctly returned
                Assert.AreEqual(expectedSubscoreImageHit.AbilityName, returnedSubscoreImageHit.AbilityName);
                Assert.AreEqual(expectedSubscoreImageHit.GameName, returnedSubscoreImageHit.GameName);
                Assert.AreEqual(expectedSubscoreImageHit.Score, returnedSubscoreImageHit.Score);
                Assert.AreEqual(expectedSubscoreImageHit.Weight, returnedSubscoreImageHit.Weight);

                yield return null;
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that unsuccessful identifications result in a lower score.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestImageHitRoundThiefAppearInRound()
            {
                ClearSelectiveVisualMeasure();

                // Get score for having 14 unsuccessful click
                SelectiveVisualMeasure.imagehitData.Rounds.Add(imaghtHitRound);
                SelectiveVisualMeasure.EvaluateCTFScore();
                int score_has14UnsuccessfulClick = SelectiveVisualMeasure.subScoreCTF.Score;
                ClearSelectiveVisualMeasure();

                // Get score for having 20 unsuccessful click
                SelectiveVisualMeasure.imagehitData.Rounds.Add(imageHit_showTimeInClicks);
                SelectiveVisualMeasure.EvaluateCTFScore();
                int score_has20UnsuccessfulClick = SelectiveVisualMeasure.subScoreCTF.Score;

                yield return null;
                // Test that more unsuccessful clicks should result a lower score
                Assert.IsTrue(score_has14UnsuccessfulClick == score_has20UnsuccessfulClick);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that unsuccessful identifications result in a lower score.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestImageHitRoundLongTimeCheck()
            {
                ClearSelectiveVisualMeasure();

                // Get score for having 14 unsuccessful click
                SelectiveVisualMeasure.imagehitData.Rounds.Add(imaghtHitRound);
                SelectiveVisualMeasure.EvaluateCTFScore();
                int score_has14UnsuccessfulClick = SelectiveVisualMeasure.subScoreCTF.Score;
                ClearSelectiveVisualMeasure();

                // Get score for having 20 unsuccessful click
                SelectiveVisualMeasure.imagehitData.Rounds.Add(imahtHitRound_longTimeInClicks);
                SelectiveVisualMeasure.EvaluateCTFScore();
                int score_has20UnsuccessfulClick = SelectiveVisualMeasure.subScoreCTF.Score;

                yield return null;
                // Test that more unsuccessful clicks should result a lower score
                Assert.IsTrue(score_has14UnsuccessfulClick == score_has20UnsuccessfulClick);
            }

            //--------- For Squares related methods ------------------------------
            //----------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateSquaresScore function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_EvaluateSquaresScoreFunctionCalled_SquaresSubScoreDerived()
            {
                ClearSelectiveVisualMeasure();

                // Call tested function
                SelectiveVisualMeasure.EvaluateSquaresScore();

                yield return null;
                Assert.AreEqual(AbilityName.SELECTIVE_VISUAL, SelectiveVisualMeasure.subScoreSquares.AbilityName);
                Assert.AreEqual(GameName.SQUARES, SelectiveVisualMeasure.subScoreSquares.GameName);
                Assert.AreEqual(0, SelectiveVisualMeasure.subScoreSquares.Score);
                Assert.AreEqual(1, SelectiveVisualMeasure.subScoreSquares.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetSubScoreForSquares function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetSubScoreForSquaresFunctionCalled_SquaresSubScoreReturned()
            {
                ClearSelectiveVisualMeasure();

                // Set values for SelectiveVisualMeasure.subScoreSquares
                SelectiveVisualMeasure.subScoreSquares.AbilityName = AbilityName.SELECTIVE_VISUAL;
                SelectiveVisualMeasure.subScoreSquares.GameName = GameName.SQUARES;
                SelectiveVisualMeasure.subScoreSquares.Score = 49;
                SelectiveVisualMeasure.subScoreSquares.Weight = 2;

                // Call tested function
                SubscoreStorage returnedSubscoreSquares = SelectiveVisualMeasure.GetSubScoreForSquares();
                SubscoreStorage expectedSubscoreSquares = SelectiveVisualMeasure.subScoreSquares;

                yield return null;
                // Test SelectiveVisualMeasure.subScoreSquares is correctly returned
                Assert.AreEqual(expectedSubscoreSquares.AbilityName, returnedSubscoreSquares.AbilityName);
                Assert.AreEqual(expectedSubscoreSquares.GameName, returnedSubscoreSquares.GameName);
                Assert.AreEqual(expectedSubscoreSquares.Score, returnedSubscoreSquares.Score);
                Assert.AreEqual(expectedSubscoreSquares.Weight, returnedSubscoreSquares.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that mismatch results in a score reduction. 
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TestSquaresScore_Mismatch()
            {
                int scoreWithMisMatch;
                int scoreWithoutMisMatch;

                // Score for round without mismatch
                ClearSelectiveVisualMeasure();
                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_sameOrder);
                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_sameOrder);
                SelectiveVisualMeasure.EvaluateSquaresScore();
                scoreWithoutMisMatch = SelectiveVisualMeasure.subScoreSquares.Score;

                // Score for round with mismatch
                ClearSelectiveVisualMeasure();
                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_sameOrder);
                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_mismatch);
                SelectiveVisualMeasure.EvaluateSquaresScore();
                scoreWithMisMatch = SelectiveVisualMeasure.subScoreSquares.Score;

                yield return null;
                Assert.IsTrue(scoreWithMisMatch < scoreWithoutMisMatch, "scoreWithMismatch: " + scoreWithMisMatch + " ScoreWithoutMisMatch: " + scoreWithMisMatch);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that gap results in a score reduction.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TestSquaresScore_Gap()
            {
                int scoreWithGap;
                int scoreWithoutGap;

                // Score for round without mismatch
                ClearSelectiveVisualMeasure();

                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_sameOrder);
                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_sameOrder);
                SelectiveVisualMeasure.EvaluateSquaresScore();
                scoreWithoutGap = SelectiveVisualMeasure.subScoreSquares.Score;

                // Score for round with mismatch
                ClearSelectiveVisualMeasure();
                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_sameOrder);
                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_gap);
                SelectiveVisualMeasure.EvaluateSquaresScore();
                scoreWithGap = SelectiveVisualMeasure.subScoreSquares.Score;

                yield return null;
                Assert.IsTrue(scoreWithGap < scoreWithoutGap, "scoreWithGap: " + scoreWithGap + " ScoreWithoutGap: " + scoreWithoutGap);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that if the recalled squares are in a different order, the same score is received.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TestSquaresScore_DifferentOrderAndSameOrderHaveSameScore()
            {
                int scoreWithSameOrder;
                int scoreWithDifferentOrder;

                // Score for round without mismatch
                ClearSelectiveVisualMeasure();
                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_sameOrder);
                SelectiveVisualMeasure.EvaluateSquaresScore();
                scoreWithSameOrder = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Score for round with mismatch
                ClearSelectiveVisualMeasure();
                SelectiveVisualMeasure.squaresData.Rounds.Add(squareRound_differentOrder);
                SelectiveVisualMeasure.EvaluateSquaresScore();
                scoreWithDifferentOrder = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                yield return null;
                Assert.IsTrue(scoreWithDifferentOrder == scoreWithSameOrder);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that if there are no rounds stored, the score returned is 0.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TestSquaresScore_NoRoundsReturn0()
            {
                int expectedScore = 0;
                int actualScore;

                // Score for round without mismatch
                ClearSelectiveVisualMeasure();
                SelectiveVisualMeasure.EvaluateSquaresScore();
                actualScore = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                yield return null;
                Assert.IsTrue(expectedScore == actualScore);
            }
        }
    }
}