using NUnit.Framework;
using Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Helper;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class CatchTheThiefStorageTests
        {
            private CatchTheThiefStorage ctf;
            private CatchTheThiefRound round;

            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public fields

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that identified KeyCode and KeyName are correctly initialized
                    when a new CatchTheThiefStorage object is created.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_CatchTheThiefStorageCreated_THEN_IdentifiedKeySetCorrectly()
            {
                ctf = new CatchTheThiefStorage();

                KeyCode expectedKeyCode = KeyCode.Space;
                string expectedKeyName = KeyCode.Space.ToString();

                yield return null;
                Assert.IsTrue(ctf.IdentifiedKeyCode == expectedKeyCode);
                Assert.IsTrue(ctf.IdentifiedKeyName == expectedKeyName);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that KeysPressed list is empty when created.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestCatchTheThiefKeysPressedSetter()
            {
                round = new CatchTheThiefRound();
                round.UnidentifiedKeysPressed = new List<TimeAndKey>();
                int expectedListSize = 0;

                yield return null;
                Assert.IsTrue(round.UnidentifiedKeysPressed.Count == expectedListSize);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that KeysPressed list has one item when one item is added to the list.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestCatchTheThiefKeysPressedAdd1ItemThenListHas1Item()
            {
                round = new CatchTheThiefRound();
                round.UnidentifiedKeysPressed = new List<TimeAndKey>();
                round.UnidentifiedKeysPressed.Add(new TimeAndKey(1f, KeyCode.Alpha0));
                int expectedListSize = 1;

                yield return null;
                Assert.IsTrue(round.UnidentifiedKeysPressed.Count == expectedListSize);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that when you add one item to the KeysPressed list, the item that was added is correctly returned by the getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestCatchTheThiefKeysPressedAdd1ItemThenGetterReturnsCorrectValue()
            {
                round = new CatchTheThiefRound();
                round.UnidentifiedKeysPressed = new List<TimeAndKey>();
                TimeAndKey expectedTimeAndKey = new TimeAndKey(1f, KeyCode.Alpha0);
                round.UnidentifiedKeysPressed.Add(expectedTimeAndKey);
                TimeAndKey actualTimeAndKey = round.UnidentifiedKeysPressed[0];
                
                yield return null;
                Assert.IsTrue(expectedTimeAndKey == actualTimeAndKey);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that Catch The Thief gets the correct key pressed by the player.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestCatchTheThiefRoundGetterIsKeyPressedValue()
            {
                round = new CatchTheThiefRound();
                round.IsIdentifiedKeyPressed = false;

                bool ispressed = false;

                yield return null;
                Assert.IsTrue(ispressed == round.IsIdentifiedKeyPressed, "CatchTheThiefIsKeyPressed getter returning incorrect value: " + ispressed + " , " + round.IsIdentifiedKeyPressed);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test KeyPressTime public field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_KeyPressTimeSet_THEN_CatchTheThiefGetsKeyPressTimeValue()
            {
                round = new CatchTheThiefRound();
                round.identifiedKeyPressTime = 7f; // float value

                float expectedHighlightInterval = 7f;

                yield return null;
                Assert.IsTrue(expectedHighlightInterval == round.identifiedKeyPressTime);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test ThiefAppearInRound public field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ThiefAppearInRoundSet_THEN_CatchTheThiefGetsThiefAppearInRoundValue()
            {
                round = new CatchTheThiefRound();
                round.ThiefAppearInRound = true; // float value

                bool thiefAppearInRound = true;

                yield return null;
                Assert.IsTrue(thiefAppearInRound == round.ThiefAppearInRound);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test PersonAppearInRound public field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_PersonAppearInRoundSet_THEN_CatchTheThiefGetsPersonAppearInRoundValue()
            {
                round = new CatchTheThiefRound();
                round.PersonAppearInRound = true; // float value

                bool PersonAppearInRound = true;

                yield return null;
                Assert.IsTrue(PersonAppearInRound == round.PersonAppearInRound);
            }
        }
    }
}