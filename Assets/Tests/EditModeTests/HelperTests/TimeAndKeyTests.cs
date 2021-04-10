using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Helper;

namespace EditModeTests
{
    namespace HelperTests
    {
        public class TimeAndKeyTests
        {
            private TimeAndKey tempTimeAndKey;
            private double tempTime;
            private KeyCode tempKeyCode;
            private string tempKeyName;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing time getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TimeAndKeyCreated_THEN_TimeGetFunctionWorks()
            {
                tempTimeAndKey = new TimeAndKey(3.1, KeyCode.Space);
                tempTime = tempTimeAndKey.Time;

                yield return null;

                Assert.AreEqual(3.1, tempTime);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing KeyCode getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TimeAndKeyCreated_THEN_KeyCodeGetFunctionWorks()
            {
                tempTimeAndKey = new TimeAndKey(3.1, KeyCode.Space);
                tempKeyCode = tempTimeAndKey.KeyCode;

                yield return null;

                Assert.AreEqual(KeyCode.Space,tempKeyCode);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing KeyName getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_TimeAndKeyCreated_THEN_KeyNameGetFunctionWorks()
            {
                tempTimeAndKey = new TimeAndKey(3.1, KeyCode.Space);
                tempKeyName = tempTimeAndKey.KeyName;

                yield return null;

                Assert.AreEqual("Space", tempKeyName);
            }
        }
    }
}