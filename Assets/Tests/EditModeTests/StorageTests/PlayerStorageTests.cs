using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Storage;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class PlayerStorageTests
        {
            private PlayerStorage testPlayerStorage;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_NameSet_THEN_NameGetFunctionWorks()
            {
                testPlayerStorage = new PlayerStorage();
                testPlayerStorage.Name = "Jack";
                string expectedName = "Jack";

                yield return null;

                Assert.AreEqual(expectedName, testPlayerStorage.Name);
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
            public IEnumerator WHEN_UserIdSet_THEN_UserIdGetFunctionWorks()
            {
                testPlayerStorage = new PlayerStorage();
                Guid expectedGuid = Guid.NewGuid();
                testPlayerStorage.UserId = expectedGuid;

                yield return null;

                Assert.AreEqual(expectedGuid, testPlayerStorage.UserId);
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
            public IEnumerator WHEN_AgeSet_THEN_AgeGetFunctionWorks()
            {
                testPlayerStorage = new PlayerStorage();
                int expectedAge = 18;
                testPlayerStorage.Age = 18;

                yield return null;

                Assert.AreEqual(expectedAge, testPlayerStorage.Age);
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
            public IEnumerator WHEN_KeyboardSet_THEN_KeyboardGetFunctionWorks()
            {
                testPlayerStorage = new PlayerStorage();
                bool expectedKeyboard = false;
                testPlayerStorage.KeyBoard = false;

                yield return null;

                Assert.AreEqual(expectedKeyboard, testPlayerStorage.KeyBoard);
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
            public IEnumerator WHEN_MouseSet_THEN_MouseGetFunctionWorks()
            {
                testPlayerStorage = new PlayerStorage();
                bool expectedMouse = true;
                testPlayerStorage.Mouse = true;

                yield return null;

                Assert.AreEqual(expectedMouse, testPlayerStorage.Mouse);
            }
        }
    }
}