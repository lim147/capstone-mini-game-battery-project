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
        public class InhibitionMeasureTests
        {
            BalloonsRound balloonRound = new BalloonsRound();
            BalloonsRound balloonRound_shortestPossibleDestClickTime = new BalloonsRound();
            BalloonsRound balloonRound_longDestinationClickTime = new BalloonsRound();
            BalloonsRound balloonRound_longTimeInClicks = new BalloonsRound();
            BalloonsRound balloonRound_hasOneUnsuccessfulClick = new BalloonsRound();
            BalloonsRound balloonRound_has4UnsuccessfulClick = new BalloonsRound();
            BalloonsRound balloonRound_has9UnsuccessfulClick = new BalloonsRound();
            BalloonsRound balloonRound_has14UnsuccessfulClick = new BalloonsRound();
            BalloonsRound balloonRound_has20UnsuccessfulClick = new BalloonsRound();

            CatchTheThiefRound catchRound = new CatchTheThiefRound();

            CatchTheThiefRound catchRound_longTimeInClicks = new CatchTheThiefRound();

            CatchTheThiefRound catchRound_showTimeInClicks = new CatchTheThiefRound();

            List<ImageHitRound> imaghtHitRound = new List<ImageHitRound>();
            List<ImageHitRound> imahtHitRound_longTimeInClicks = new List<ImageHitRound>();
            List<ImageHitRound> imageHit_showTimeInClicks = new List<ImageHitRound>();

            [SetUp]
            public void CreateExampleObjects()
            {
                // Create example clicks:
                List<TimeAndPosition> exampleClicks_normal = new List<TimeAndPosition>();
                TimeAndPosition timeAndPosition_normal = new TimeAndPosition(0.8, new Position2D(0, 0));
                exampleClicks_normal.Add(timeAndPosition_normal);

                List<TimeAndPosition> exampleClicks_longTime = new List<TimeAndPosition>();
                TimeAndPosition timeAndPosition_longTime = new TimeAndPosition(1.5, new Position2D(0, 0));
                exampleClicks_longTime.Add(timeAndPosition_normal);

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

                // balloonRound_longDestinationClickTime is an exmaple of round with long DestinationClickTime
                // other fields are the same with balloonRound
                balloonRound_longDestinationClickTime.BalloonSize = 60;
                balloonRound_longDestinationClickTime.DestinationPoint = new Position2D(100, 200);
                balloonRound_longDestinationClickTime.DestinationClickTime = 1.5; // long destination click time
                balloonRound_longDestinationClickTime.Clicks = new List<TimeAndPosition>();
                balloonRound_longDestinationClickTime.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_longTimeInClicks is an exmaple of round with long time in clicks items
                // other fields are the same with balloonRound
                balloonRound_longTimeInClicks.BalloonSize = 60;
                balloonRound_longTimeInClicks.DestinationPoint = new Position2D(100, 200);
                balloonRound_longTimeInClicks.DestinationClickTime = 0.8;
                balloonRound_longTimeInClicks.Clicks = exampleClicks_longTime; // long time in clicks item
                balloonRound_longTimeInClicks.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_hasUnsuccessfulClicks is an exmaple of round with one unsuccessful click
                // other fields are the same with balloonound
                balloonRound_hasOneUnsuccessfulClick.BalloonSize = 60;
                balloonRound_hasOneUnsuccessfulClick.DestinationPoint = new Position2D(100, 200);
                balloonRound_hasOneUnsuccessfulClick.DestinationClickTime = 0.8;
                balloonRound_hasOneUnsuccessfulClick.Clicks = exampleClicks_normal; // one unsuccessful clicks
                balloonRound_hasOneUnsuccessfulClick.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_has4UnsuccessfulClick has 4 same click items as in balloonRound_hasUnsuccessfulClicks
                // other fields are the same with balloonRound_hasUnsuccessfulClicks
                balloonRound_has4UnsuccessfulClick.BalloonSize = 60;
                balloonRound_has4UnsuccessfulClick.DestinationPoint = new Position2D(100, 200);
                balloonRound_has4UnsuccessfulClick.Clicks = AddClickItemsNTimes(timeAndPosition_normal, 4);
                balloonRound_has4UnsuccessfulClick.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_has9UnsuccessfulClick has 9 same click items as in balloonRound_hasUnsuccessfulClicks
                // other fields are the same with balloonRound_hasUnsuccessfulClicks
                balloonRound_has9UnsuccessfulClick.BalloonSize = 60;
                balloonRound_has9UnsuccessfulClick.DestinationPoint = new Position2D(100, 200);
                balloonRound_has9UnsuccessfulClick.Clicks = AddClickItemsNTimes(timeAndPosition_normal, 9);
                balloonRound_has9UnsuccessfulClick.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_has14UnsuccessfulClick has 14 same click items as in balloonRound_hasUnsuccessfulClicks
                // other fields are the same with balloonRound_hasUnsuccessfulClicks
                balloonRound_has14UnsuccessfulClick.BalloonSize = 60;
                balloonRound_has14UnsuccessfulClick.DestinationPoint = new Position2D(100, 200);
                balloonRound_has14UnsuccessfulClick.Clicks = AddClickItemsNTimes(timeAndPosition_normal, 14);
                balloonRound_has14UnsuccessfulClick.SuccessClickPoint = new Position2D(102, 197);

                // balloonRound_has20UnsuccessfulClick has 20 same click items as in balloonRound_hasUnsuccessfulClicks
                // other fields are the same with balloonRound_hasUnsuccessfulClicks
                balloonRound_has20UnsuccessfulClick.BalloonSize = 60;
                balloonRound_has20UnsuccessfulClick.DestinationPoint = new Position2D(100, 200);
                balloonRound_has20UnsuccessfulClick.Clicks = AddClickItemsNTimes(timeAndPosition_normal, 20);
                balloonRound_has20UnsuccessfulClick.SuccessClickPoint = new Position2D(102, 197);

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

                imaghtHitRound.Add(new ImageHitRound());
                imahtHitRound_longTimeInClicks.Add(new ImageHitRound());
                imageHit_showTimeInClicks.Add(new ImageHitRound());

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

            // Clear InhibitionMeasure object
            private void ClearInhibitionMeasure()
            {
                // Initialize InhitionMeasure public fields
                InhibitionMeasure.subScorBalloons = new SubscoreStorage();
                InhibitionMeasure.balloonsData = new BalloonsStorage();
                InhibitionMeasure.balloonsData.Rounds = new List<BalloonsRound>();

                InhibitionMeasure.subScoreCTF = new SubscoreStorage();
                InhibitionMeasure.ctfData = new CatchTheThiefStorage();
                InhibitionMeasure.ctfData.Rounds = new List<CatchTheThiefRound>();

                InhibitionMeasure.subScoreImageHit = new SubscoreStorage();
                InhibitionMeasure.imagehitData = new ImageHitStorage();
                InhibitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();
            }

            // Add clickTime N times to the Clicks List
            private List<TimeAndPosition> AddClickItemsNTimes(TimeAndPosition clickItem, int N)
            {
                List<TimeAndPosition> resultList = new List<TimeAndPosition>();
                for (int i = 0; i < N; i++)
                {
                    resultList.Add(clickItem);
                }
                return resultList;
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
            <li><b>Test description:</b> Test if inhibition score is correctly calculated for both rounds.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetInhibitionScoreFromRound12()
            {
                ClearInhibitionMeasure();

                // Call tested function
                yield return null;
                List<ImageHitRound> ihr = new List<ImageHitRound>();
                ihr = null;
                InhibitionMeasure.GetInhibitionScoreFromRound12(ihr);

                List<ImageHitRound> ihr1 = new List<ImageHitRound>();
                ImageHitRound ih = new ImageHitRound();

                ih.isCorrectlyIdentified = true;
                ihr1.Add(ih);

                InhibitionMeasure.GetInhibitionScoreFromRound12(ihr1);

                ihr1 = new List<ImageHitRound>();
                ih.isCorrectlyIdentified = false;
                ih.isSpaceKey = false;
                ih.isKeyPressed = true;
                ihr1.Add(ih);
                InhibitionMeasure.GetInhibitionScoreFromRound12(ihr1);

                ihr1 = new List<ImageHitRound>();
                ImageHitRound ih2 = new ImageHitRound();

                ih.isCorrectlyIdentified = true;
                ih.isSpaceKey = false;
                ih.isKeyPressed = true;

                ih2.isCorrectlyIdentified = false;
                ih2.isSpaceKey = false;
                ih2.isKeyPressed = true;

                ihr1.Add(ih);
                ihr1.Add(ih2);
                InhibitionMeasure.GetInhibitionScoreFromRound12(ihr1);

                ihr1 = new List<ImageHitRound>();
                ih2 = new ImageHitRound();

                ih.isCorrectlyIdentified = true;
                ih.isSpaceKey = false;
                ih.isKeyPressed = true;

                ih2.isCorrectlyIdentified = true;
                ih2.isSpaceKey = false;
                ih2.isKeyPressed = true;

                ihr1.Add(ih);
                ihr1.Add(ih2);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if correct image information is derived for both rounds.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetImageFromRound12()
            {
                ClearInhibitionMeasure();

                // Call tested function
                yield return null;

                ImageHitRound r1 = new ImageHitRound();
                List<ImageHitRound> r2 = new List<ImageHitRound>();

                r1 = null;
                InhibitionMeasure.GetImageFromRound12(r1, r2);

                r1 = new ImageHitRound();
                r1.imageName = "1";
                r2.Add(r1);
                InhibitionMeasure.GetImageFromRound12(r1, r2);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if a overall inhibition score is correctly calculated.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetInhibitionScore()
            {
                ClearInhibitionMeasure();
                yield return null;

                InhibitionMeasure.imagehitData = new ImageHitStorage();
                InhibitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                for (int i = 0; i <= 10; i++)
                {
                    List<ImageHitRound> testRound = new List<ImageHitRound>();

                    for (int j = 0; j <= 10; j++)
                    {
                        ImageHitRound testro = new ImageHitRound();
                        testro.imageName = "111";
                        testRound.Add(testro);
                    }

                    InhibitionMeasure.imagehitData.Rounds.Add(testRound);
                }

                float d = InhibitionMeasure.GetInhibitionScore();

                Assert.IsTrue(d >= 0 && d <= 100);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if the side case of inhibition correct.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetInhibitionScoreForSpecialCase()
            {
                ClearInhibitionMeasure();
                yield return null;

                InhibitionMeasure.imagehitData = new ImageHitStorage();
                InhibitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                for (int i = 0; i <= 10; i++)
                {
                    List<ImageHitRound> testRound = new List<ImageHitRound>();

                    for (int j = 0; j <= 10; j++)
                    {
                        ImageHitRound testro = new ImageHitRound();
                        testro.imageName = "111";
                        testro.isKeyPressed = true;
                        testRound.Add(testro);
                    }

                    InhibitionMeasure.imagehitData.Rounds.Add(testRound);
                }

                float d = InhibitionMeasure.GetInhibitionScore();

                Assert.IsTrue(d == 0);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateImageHitScores function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_EvaluateImageHitScore_Method()
            {
                ClearInhibitionMeasure();

                // Call tested function
                InhibitionMeasure.EvaluateImageHitScore();

                yield return null;
                Assert.AreEqual(AbilityName.INHIBITION, InhibitionMeasure.subScoreImageHit.AbilityName);
                Assert.AreEqual(GameName.IMAGE_HIT, InhibitionMeasure.subScoreImageHit.GameName);
                Assert.AreEqual(0, InhibitionMeasure.subScoreImageHit.Score);
                Assert.AreEqual(2, InhibitionMeasure.subScoreImageHit.Weight);
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
            public IEnumerator TEST_GetSubScoreForImageHit_Method()
            {
                ClearInhibitionMeasure();

                // Set values for InhibitionMeasure.subScoreForImageHit
                InhibitionMeasure.subScoreImageHit.AbilityName = AbilityName.INHIBITION;
                InhibitionMeasure.subScoreImageHit.GameName = GameName.IMAGE_HIT;
                InhibitionMeasure.subScoreImageHit.Score = 89;
                InhibitionMeasure.subScoreImageHit.Weight = 2;

                // Call tested function
                SubscoreStorage returnedSubscoreImageHit = InhibitionMeasure.GetSubScoreForImageHit();
                SubscoreStorage expectedSubscoreImageHit = InhibitionMeasure.subScoreImageHit;

                yield return null;
                // Test InhibitionMeasure.subScoreImageHit is correctly returned
                Assert.AreEqual(expectedSubscoreImageHit.AbilityName, returnedSubscoreImageHit.AbilityName);
                Assert.AreEqual(expectedSubscoreImageHit.GameName, returnedSubscoreImageHit.GameName);
                Assert.AreEqual(expectedSubscoreImageHit.Score, returnedSubscoreImageHit.Score);
                Assert.AreEqual(expectedSubscoreImageHit.Weight, returnedSubscoreImageHit.Weight);

                yield return null;
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
                ClearInhibitionMeasure();

                // Call tested function
                InhibitionMeasure.EvaluateBalloonsScore();

                yield return null;
                Assert.AreEqual(AbilityName.INHIBITION, InhibitionMeasure.subScorBalloons.AbilityName);
                Assert.AreEqual(GameName.BALLOONS, InhibitionMeasure.subScorBalloons.GameName);
                Assert.AreEqual(0, InhibitionMeasure.subScorBalloons.Score);
                Assert.AreEqual(2, InhibitionMeasure.subScorBalloons.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetSubScoreForBalloons finction.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetSubScoreForBalloonsFunctionCalled_BalloonsSubScoreReturned()
            {
                ClearInhibitionMeasure();

                // Set values for InhibitionMeasure.subScoreBalloons
                InhibitionMeasure.subScorBalloons.AbilityName = AbilityName.INHIBITION;
                InhibitionMeasure.subScorBalloons.GameName = GameName.BALLOONS;
                InhibitionMeasure.subScorBalloons.Score = 89;
                InhibitionMeasure.subScorBalloons.Weight = 2;

                // Call tested function
                SubscoreStorage returnedSubscoreBalloons = InhibitionMeasure.GetSubScoreForBalloons();
                SubscoreStorage expectedSubscoreBalloons = InhibitionMeasure.subScorBalloons;

                yield return null;
                // Test InhibitionMeasure.subScoreBalloons is correctly returned
                Assert.AreEqual(expectedSubscoreBalloons.AbilityName, returnedSubscoreBalloons.AbilityName);
                Assert.AreEqual(expectedSubscoreBalloons.GameName, returnedSubscoreBalloons.GameName);
                Assert.AreEqual(expectedSubscoreBalloons.Score, returnedSubscoreBalloons.Score);
                Assert.AreEqual(expectedSubscoreBalloons.Weight, returnedSubscoreBalloons.Weight);

                yield return null;
            }

            // Test that shortest possible Destination Click Time would result in a higher score
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b>  Test that shortest possible destination click time would result in a higher score.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_ShortestPossibleDestinationClickTime()
            {
                ClearInhibitionMeasure();

                // Add balloonRound to balloonsData
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_normal = InhibitionMeasure.subScorBalloons.Score;

                ClearInhibitionMeasure();

                // Add balloonRound_shortestPossibleDestClickTime to balloonsData
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_shortestPossibleDestClickTime);
                // Call Evaluate function
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_shortestDesClickTime = InhibitionMeasure.subScorBalloons.Score;

                yield return null;
                // Test that shortest possible destination click time should result a higher score
                Assert.IsTrue(score_normal < score_shortestDesClickTime);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that move time would affect the score in the
                   situation that move time is DestinationClickTime.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_MoveTimeIsDestinationClickTime()
            {
                ClearInhibitionMeasure();

                // Add balloonRound to balloonsData
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_normal = InhibitionMeasure.subScorBalloons.Score;

                ClearInhibitionMeasure();

                // Add balloonRound_longMoveTime to balloonsData
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_longDestinationClickTime);
                // Call Evaluate function
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_longMoveTime = InhibitionMeasure.subScorBalloons.Score;

                yield return null;
                // Test that long move time should result a lower score
                Assert.IsTrue(score_normal > score_longMoveTime);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that move time would affect the score in the
                   situation that move time is time of last item in clicks list.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_MoveTimeIsTimeInClicks()
            {
                ClearInhibitionMeasure();

                // Add balloonRound to balloonsData
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_normal = InhibitionMeasure.subScorBalloons.Score;

                ClearInhibitionMeasure();

                // Add balloonRound_longTimeInClicks to balloonsData
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_longTimeInClicks);
                // Call Evaluate function
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_longTimeInClicks = InhibitionMeasure.subScorBalloons.Score;

                yield return null;
                // Test that long destination click time should result a lower score
                Assert.IsTrue(score_normal > score_longTimeInClicks);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that number of unsuccessful clicks would affect the score when the number
                   is below 3.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_NumberOfUnsuccessClick_IsBelow3()
            {
                ClearInhibitionMeasure();

                // Add balloonRound to balloonsData
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound);
                // Call Evaluate function
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_normal = InhibitionMeasure.subScorBalloons.Score;

                ClearInhibitionMeasure();

                // Add balloonRound_hasOneUnsuccessfulClick to balloonsData
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_hasOneUnsuccessfulClick);
                // Call Evaluate function
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_hasOneUnsuccessfulClick = InhibitionMeasure.subScorBalloons.Score;

                yield return null;
                // Test that unsuccessful clicks should result a lower score
                Assert.IsTrue(score_normal > score_hasOneUnsuccessfulClick);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that number Of unsuccessful clicks would affect the score
                   when the number is between 3 and 5.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_NumberOfUnsuccessClick_Is3to5()
            {
                ClearInhibitionMeasure();

                // Get score for having one unsuccessful click
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_hasOneUnsuccessfulClick);
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_hasOneUnsuccessfulClick = InhibitionMeasure.subScorBalloons.Score;

                ClearInhibitionMeasure();

                // Get score for having 4 unsuccessful click
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_has4UnsuccessfulClick);
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_has4UnsuccessfulClick = InhibitionMeasure.subScorBalloons.Score;

                yield return null;
                // Test that more unsuccessful clicks should result a lower score
                Assert.IsTrue(score_hasOneUnsuccessfulClick > score_has4UnsuccessfulClick);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that number of unsuccessful clicks would affect the score
                   when the number is between 5 and 10.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_NumberOfUnsuccessClick_Is5to10()
            {
                ClearInhibitionMeasure();

                // Get score for having 4 unsuccessful click
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_has4UnsuccessfulClick);
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_has4UnsuccessfulClick = InhibitionMeasure.subScorBalloons.Score;

                ClearInhibitionMeasure();

                // Get score for having 9 unsuccessful click
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_has9UnsuccessfulClick);
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_has9UnsuccessfulClick = InhibitionMeasure.subScorBalloons.Score;

                yield return null;
                // Test that more unsuccessful clicks should result a lower score
                Assert.IsTrue(score_has4UnsuccessfulClick > score_has9UnsuccessfulClick);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that number of unsuccessful clicks would affect the score
                   when the number is between 10 and 15.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_NumberOfUnsuccessClick_Is10to15()
            {
                ClearInhibitionMeasure();

                // Get score for having 9 unsuccessful click
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_has9UnsuccessfulClick);
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_has9UnsuccessfulClick = InhibitionMeasure.subScorBalloons.Score;

                ClearInhibitionMeasure();

                // Get score for having 14 unsuccessful click
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_has14UnsuccessfulClick);
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_has14UnsuccessfulClick = InhibitionMeasure.subScorBalloons.Score;

                yield return null;
                // Test that more unsuccessful clicks should result a lower score
                Assert.IsTrue(score_has9UnsuccessfulClick > score_has14UnsuccessfulClick);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that number of unsuccessful clicks would affect the score
                   when the number is above 15.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsScore_NumberOfUnsuccessClick_IsAbove15()
            {
                ClearInhibitionMeasure();

                // Get score for having 14 unsuccessful click
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_has14UnsuccessfulClick);
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_has14UnsuccessfulClick = InhibitionMeasure.subScorBalloons.Score;

                ClearInhibitionMeasure();

                // Get score for having 20 unsuccessful click
                InhibitionMeasure.balloonsData.Rounds.Add(balloonRound_has20UnsuccessfulClick);
                InhibitionMeasure.EvaluateBalloonsScore();
                int score_has20UnsuccessfulClick = InhibitionMeasure.subScorBalloons.Score;

                yield return null;
                // Test that more unsuccessful clicks should result a lower score
                Assert.IsTrue(score_has14UnsuccessfulClick > score_has20UnsuccessfulClick);
            }

            //--------- For Catch The Thief related methods ----------------
            //--------------------------------------------------------------
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateCTFScore function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_EvaluateEvaluateCTFScore()
            {
                ClearInhibitionMeasure();

                // Call tested function
                InhibitionMeasure.EvaluateCTFScore();

                yield return null;
                Assert.AreEqual(AbilityName.INHIBITION, InhibitionMeasure.subScoreCTF.AbilityName);
                Assert.AreEqual(GameName.CATCH_THE_THIEF, InhibitionMeasure.subScoreCTF.GameName);
                Assert.AreEqual(0, InhibitionMeasure.subScoreCTF.Score);
                Assert.AreEqual(2, InhibitionMeasure.subScoreCTF.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetSubScoreForCTF function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetSubScoreForCTF()
            {
                ClearInhibitionMeasure();

                // Set values for InhibitionMeasure.subScoreForCTF
                InhibitionMeasure.subScoreCTF.AbilityName = AbilityName.INHIBITION;
                InhibitionMeasure.subScoreCTF.GameName = GameName.CATCH_THE_THIEF;
                InhibitionMeasure.subScoreCTF.Score = 89;
                InhibitionMeasure.subScoreCTF.Weight = 2;

                // Call tested function
                SubscoreStorage returnedSubscorCTF = InhibitionMeasure.GetSubScoreForCTF();
                SubscoreStorage expectedSubscoreCTF = InhibitionMeasure.subScoreCTF;

                yield return null;
                // Test InhibitionMeasure.subScoreCTF is correctly returned
                Assert.AreEqual(expectedSubscoreCTF.AbilityName, returnedSubscorCTF.AbilityName);
                Assert.AreEqual(expectedSubscoreCTF.GameName, returnedSubscorCTF.GameName);
                Assert.AreEqual(expectedSubscoreCTF.Score, returnedSubscorCTF.Score);
                Assert.AreEqual(expectedSubscoreCTF.Weight, returnedSubscorCTF.Weight);

                yield return null;
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description: Ensure that more unsuccessful catches result in a lower score.</b>
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestCatchRoundThiefAppearInRound()
            {
                ClearInhibitionMeasure();

                // Get score for having 14 unsuccessful catches
                InhibitionMeasure.ctfData.Rounds.Add(catchRound);
                InhibitionMeasure.EvaluateCTFScore();
                int score_has14UnsuccessfulClick = InhibitionMeasure.subScoreCTF.Score;
                ClearInhibitionMeasure();

                // Get score for having 20 unsuccessful catches
                InhibitionMeasure.ctfData.Rounds.Add(catchRound_longTimeInClicks);
                InhibitionMeasure.EvaluateCTFScore();
                int score_has20UnsuccessfulClick = InhibitionMeasure.subScoreCTF.Score;

                yield return null;
                // Test that more unsuccessful catches should result a lower score
                Assert.IsTrue(score_has14UnsuccessfulClick < score_has20UnsuccessfulClick);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description: Ensure that more unsuccessful catches result in a lower score.</b>
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestCatchRound_showTimeInClicks()
            {
                ClearInhibitionMeasure();

                // Get score for having 14 unsuccessful click
                InhibitionMeasure.ctfData.Rounds.Add(catchRound);
                InhibitionMeasure.EvaluateCTFScore();
                int score_has14UnsuccessfulClick = InhibitionMeasure.subScoreCTF.Score;
                ClearInhibitionMeasure();

                // Get score for having 20 unsuccessful click
                InhibitionMeasure.ctfData.Rounds.Add(catchRound_showTimeInClicks);
                InhibitionMeasure.EvaluateCTFScore();
                int score_has20UnsuccessfulClick = InhibitionMeasure.subScoreCTF.Score;

                yield return null;
                // Test that more unsuccessful clicks should result a lower score
                Assert.IsTrue(score_has14UnsuccessfulClick < score_has20UnsuccessfulClick);
            }

        }
    }
}