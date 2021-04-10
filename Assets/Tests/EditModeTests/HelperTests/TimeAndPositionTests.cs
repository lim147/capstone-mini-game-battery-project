using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Helper;

namespace EditModeTests
{
    namespace HelperTests
    {
        public class TimeAndPositionTests
        {
            private TimeAndPosition tempTimeAndPosition;
            private Position2D tempPosition2D;
            private double tempTime;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TimeAndPositionCreated_THEN_TimeGetFunctionWorks()
            {
                tempTimeAndPosition = new TimeAndPosition(3, new Position2D(3, 5));
                tempTime = tempTimeAndPosition.Time;

                yield return null;

                Assert.AreEqual(3, tempTime);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TimeAndPositionCreated_THEN_PositionGetFunctionWorks()
            {
                tempTimeAndPosition = new TimeAndPosition(3, new Position2D(3, 5));
                tempPosition2D = tempTimeAndPosition.Position;

                yield return null;

                Assert.AreEqual(3, tempPosition2D.X);
                Assert.AreEqual(5, tempPosition2D.Y);
            }
        }
    }
}
