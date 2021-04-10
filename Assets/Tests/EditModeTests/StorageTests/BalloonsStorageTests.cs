using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using Storage;
using Helper;
using System;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class BalloonsStorageTests
        {
            private BalloonsStorage balloonsData;
            private BalloonsRound round;

            // Tolerance for comparing decimal values
            private float tolerance = 0.001f;

            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public fields

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test BalloonSize public field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_BalloonSizeSet_THEN_BalloonsRoundGetsCorrectValue()
            {
                round = new BalloonsRound();
                round.BalloonSize = 200;

                double expectedBalloonSize = 200;

                yield return null;
                Assert.IsTrue(Math.Abs(expectedBalloonSize - round.BalloonSize) <= tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test DestinationPoint public field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_DestinationPointSet_THEN_BalloonsRoundGetsCorrectValue()
            {
                round = new BalloonsRound();
                round.DestinationPoint = new Position2D(100, 200);

                Position2D expectedDestinationPoint = new Position2D(100, 200);

                yield return null;
                Assert.IsTrue(Math.Abs(expectedDestinationPoint.X - round.DestinationPoint.X) <= tolerance
                    && Math.Abs(expectedDestinationPoint.Y - round.DestinationPoint.Y) <= tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test DestinationClickTime public field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_DestinationClickTimeSet_THEN_BalloonsRoundGetsCorrectValue()
            {
                round = new BalloonsRound();
                round.DestinationClickTime = 1.2;

                double expectedDestinationClickTime = 1.2;

                yield return null;
                Assert.IsTrue(Math.Abs(expectedDestinationClickTime - round.DestinationClickTime) <= tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test Clicks public field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClicksCreated_THEN_ClicksIsEmptyByDefault()
            {
                round = new BalloonsRound();
                round.Clicks = new List<TimeAndPosition>();

                int expectedSizeOfList = 0;

                yield return null;
                Assert.IsTrue(expectedSizeOfList == round.Clicks.Count);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if Add functions works proprly for Clicks sequence.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClicksAdd1Item_THEN_ClicksHas1Item()
            {
                TimeAndPosition testTimeAndPosition = new TimeAndPosition(0.5, new Position2D(35, 45));
                round = new BalloonsRound();
                round.Clicks = new List<TimeAndPosition>();
                round.Clicks.Add(testTimeAndPosition);
                int expectedSizeOfList = 1;

                yield return null;
                // Clciks has only 1 items:
                Assert.IsTrue(expectedSizeOfList == round.Clicks.Count);
                // The only 1 item should be testTimeAndPosition:
                Assert.IsTrue(Math.Abs(round.Clicks[0].Time - testTimeAndPosition.Time) <= tolerance);
                Assert.IsTrue(Math.Abs(round.Clicks[0].Position.X - testTimeAndPosition.Position.X) <= tolerance
                    && Math.Abs(round.Clicks[0].Position.Y - testTimeAndPosition.Position.Y) <= tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if balloon round gets the correct value when
                         SuccessClickPoint is set.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SuccessClickPointSet_THEN_BalloonsRoundGetsCorrectValue()
            {
                round = new BalloonsRound();
                round.SuccessClickPoint = new Position2D(101.1, 90.5);

                Position2D expectedSuccessClickPoint = new Position2D(101.1, 90.5);

                yield return null;
                Assert.IsTrue(Math.Abs(expectedSuccessClickPoint.X - round.SuccessClickPoint.X) <= tolerance
                    && Math.Abs(expectedSuccessClickPoint.Y - round.SuccessClickPoint.Y) <= tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Tests Rounds public field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_BalloonRoundCreated_THEN_RoundIsEmpty()
            {
                balloonsData = new BalloonsStorage();
                balloonsData.Rounds = new List<BalloonsRound>();
                int expectedSizeOfList = 0;

                yield return null;
                Assert.IsTrue(expectedSizeOfList == balloonsData.Rounds.Count);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if balloon round size is one when a new round is added to
                   an initialized balloon round sequence.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_BalloonRoundAdded1Round_THEN_BalloonRoundSizeIs1()
            {
                balloonsData = new BalloonsStorage();
                balloonsData.Rounds = new List<BalloonsRound>();

                round = new BalloonsRound();
                balloonsData.Rounds.Add(round);

                int expectedSizeOfList = 1;

                yield return null;
                Assert.IsTrue(expectedSizeOfList == balloonsData.Rounds.Count);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test BalloonRoundGetter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_BalloonRoundAdded1Round_THEN_BalloonRoundGetterReturnsCorrectValues()
            {
                balloonsData = new BalloonsStorage();
                balloonsData.Rounds = new List<BalloonsRound>();

                round = new BalloonsRound();

                round.BalloonSize = 180;
                double expectedBalloonSize = 180;

                round.DestinationClickTime = 1.5;
                double expectedDestinationClickTime = 1.5;

                round.DestinationPoint = new Position2D(0, 0);
                Position2D expectedDestinationPoint = new Position2D(0, 0);

                round.SuccessClickPoint = new Position2D(-200, 67);
                Position2D expectedSuccessClickPoint = new Position2D(-200, 67);

                TimeAndPosition testItem1 = new TimeAndPosition(0.5, new Position2D(10, 20));
                TimeAndPosition testItem2 = new TimeAndPosition(1.5, new Position2D(0.1, -40));
                round.Clicks = new List<TimeAndPosition>();
                round.Clicks.Add(testItem1);
                round.Clicks.Add(testItem2);

                balloonsData.Rounds.Add(round);
                BalloonsRound actualCirclesRound = balloonsData.Rounds[0];

                yield return null;
                Assert.IsTrue(expectedBalloonSize == actualCirclesRound.BalloonSize);
                Assert.IsTrue(expectedDestinationClickTime == actualCirclesRound.DestinationClickTime);
                Assert.IsTrue(Math.Abs(expectedDestinationPoint.X - actualCirclesRound.DestinationPoint.X) <= tolerance
                    && Math.Abs(expectedDestinationPoint.Y - actualCirclesRound.DestinationPoint.Y) <= tolerance);
                Assert.IsTrue(Math.Abs(expectedSuccessClickPoint.X - actualCirclesRound.SuccessClickPoint.X) <= tolerance
                    && Math.Abs(expectedSuccessClickPoint.Y - actualCirclesRound.SuccessClickPoint.Y) <= tolerance);
                Assert.IsTrue(2 == actualCirclesRound.Clicks.Count);
                Assert.IsTrue(Math.Abs(testItem1.Time - actualCirclesRound.Clicks[0].Time) <= tolerance
                    && Math.Abs(testItem1.Position.X - actualCirclesRound.Clicks[0].Position.X) <= tolerance
                    && Math.Abs(testItem1.Position.Y - actualCirclesRound.Clicks[0].Position.Y) <= tolerance);
                Assert.IsTrue(Math.Abs(testItem2.Time - actualCirclesRound.Clicks[1].Time) <= tolerance
                    && Math.Abs(testItem2.Position.X - actualCirclesRound.Clicks[1].Position.X) <= tolerance
                    && Math.Abs(testItem2.Position.Y - actualCirclesRound.Clicks[1].Position.Y) <= tolerance);
            }
        }
    }
}