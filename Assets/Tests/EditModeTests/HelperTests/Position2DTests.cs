using System;
using NUnit.Framework;
using UnityEngine;
using Helper;

namespace EditModeTests
{
    namespace HelperTests
    {
        public class Position2DTests : MonoBehaviour
        {
            private Position2D testPosition2D1;
            private Position2D testPosition2D2;
            double expectedX;
            double expectedY;
            double distance2DMarginOfError = 0.01;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_NewPosition2DCreated_THEN_XGetterGivesCorrectValue()
            {
                expectedX = 1;
                testPosition2D1 = new Position2D(1, 4);

                Assert.AreEqual(expectedX, testPosition2D1.X);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_NewPosition2DCreated_THEN_YGetterGivesCorrectValue()
            {
                expectedY = 4;
                testPosition2D1 = new Position2D(1, 4);

                Assert.AreEqual(expectedY, testPosition2D1.Y);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-7'>FG-7</a></li>
            <li><b>Test description:</b> Testing distance calculator on two points that are identical.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_UsingDistance2DOnTheSamePoints_THEN_Distanceis0()
            {
                testPosition2D1 = new Position2D(0, 0);
                testPosition2D2 = new Position2D(0, 0);

                double expectedDistance = Position2D.Distance2D(testPosition2D1, testPosition2D2);
                double actualDistance = 0;

                Assert.IsTrue(Math.Abs(expectedDistance - actualDistance) <= distance2DMarginOfError);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing for one point in quadrant 1.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_UsingDistance2DOrigintoQ1_THEN_DistanceIsWithinMargin()
            {
                testPosition2D1 = new Position2D(0, 0);
                testPosition2D2 = new Position2D(1, 1);

                double expectedDistance = Position2D.Distance2D(testPosition2D1, testPosition2D2);
                double actualDistance = 1.414214;
                Assert.IsTrue(Math.Abs(actualDistance - expectedDistance) < distance2DMarginOfError, "Exceeded margin of error");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing for one point in quadrant 3.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_UsingDistance2DOrigintoQ3_THEN_DistanceIsWithinMargin()
            {
                testPosition2D1 = new Position2D(0, 0);
                testPosition2D2 = new Position2D(-1, -1);

                double expectedDistance = Position2D.Distance2D(testPosition2D1, testPosition2D2);
                double actualDistance = 1.414214;
                Assert.IsTrue(Math.Abs(actualDistance - expectedDistance) < distance2DMarginOfError, "Exceeded margin of error");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing for one point in quadrant 2.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_UsingDistance2DOrigintoQ2_THEN_DistanceIsWithinMargin()
            {
                testPosition2D1 = new Position2D(0, 0);
                testPosition2D2 = new Position2D(-1, 1);

                double expectedDistance = Position2D.Distance2D(testPosition2D1, testPosition2D2);
                double actualDistance = 1.414214;
                Assert.IsTrue(Math.Abs(actualDistance - expectedDistance) < distance2DMarginOfError, "Exceeded margin of error");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing for one point in quadrant 4.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_UsingDistance2DOrigintoQ4_THEN_DistanceIsWithinMargin()
            {
                testPosition2D1 = new Position2D(0, 0);
                testPosition2D2 = new Position2D(1, -1);

                double expectedDistance = Position2D.Distance2D(testPosition2D1, testPosition2D2);
                double actualDistance = 1.414214;
                Assert.IsTrue(Math.Abs(actualDistance - expectedDistance) < distance2DMarginOfError, "Exceeded margin of error");
            }
        }
    }
}