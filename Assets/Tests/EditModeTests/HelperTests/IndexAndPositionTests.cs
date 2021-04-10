using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Helper;

namespace EditModeTests
{
    namespace HelperTests
    {
        public class IndexAndPositionTests
        {
            // List of the square objects on screen
            private IndexAndPosition tempIndexAndPosition;
            private Position2D tempPosition2D;
            private int tempId;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_IndexAndPositionCreated_THEN_IndexGetFunctionWorks()
            {
                tempIndexAndPosition = new IndexAndPosition(3, new Position2D(3, 5));
                tempId = tempIndexAndPosition.Index;

                yield return null;

                Assert.AreEqual(3, tempId);
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
            public IEnumerator WHEN_IndexAndPositionCreated_THEN_PositionGetFunctionWorks()
            {
                tempIndexAndPosition = new IndexAndPosition(3, new Position2D(3, 5));
                tempPosition2D = tempIndexAndPosition.Position;

                yield return null;

                Assert.AreEqual(3, tempPosition2D.X);
                Assert.AreEqual(5, tempPosition2D.Y);
            }
        }
    }
}

