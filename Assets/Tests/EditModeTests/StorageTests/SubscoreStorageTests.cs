using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Storage;
using Games;
using Measurement;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class SubscoreStorageTests
        {
            private SubscoreStorage testSubscoreStorage;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_AbilityNameSet_THEN_NameGetFunctionWorks()
            {
                testSubscoreStorage = new SubscoreStorage();
                testSubscoreStorage.AbilityName = AbilityName.INHIBITION;
                AbilityName expectedAbilityName = AbilityName.INHIBITION;

                yield return null;

                Assert.AreEqual(expectedAbilityName, testSubscoreStorage.AbilityName);
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
            public IEnumerator WHEN_GameNameSet_THEN_GameNameGetFunctionWorks()
            {
                testSubscoreStorage = new SubscoreStorage();
                testSubscoreStorage.GameName = GameName.SQUARES;
                GameName expectedGameName = GameName.SQUARES;

                yield return null;

                Assert.AreEqual(expectedGameName, testSubscoreStorage.GameName);
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
            public IEnumerator WHEN_ScoreSet_THEN_ScoreGetFunctionWorks()
            {
                testSubscoreStorage = new SubscoreStorage();
                testSubscoreStorage.Score = 7;
                int expectedScore = 7;

                yield return null;

                Assert.AreEqual(expectedScore, testSubscoreStorage.Score);
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
            public IEnumerator WHEN_WeightSet_THEN_WeightGetFunctionWorks()
            {
                testSubscoreStorage = new SubscoreStorage();
                testSubscoreStorage.Weight = 7;
                int expectedWeight = 7;

                yield return null;

                Assert.AreEqual(expectedWeight, testSubscoreStorage.Weight);
            }
        }
    }
}