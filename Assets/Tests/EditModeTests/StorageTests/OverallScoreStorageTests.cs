using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Measurement;
using Storage;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class OverallScoreStorageTests
        {
            private OverallScoreStorage testOverallScoreStorage;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_AbilityNameSet_THEN_AbilityNameGetFunctionWorks()
            {
                testOverallScoreStorage = new OverallScoreStorage();
                testOverallScoreStorage.AbilityName = AbilityName.VISUOSPATIAL_SKETCHPAD;
                AbilityName expectedAbilityName = AbilityName.VISUOSPATIAL_SKETCHPAD;

                yield return null;

                Assert.AreEqual(expectedAbilityName, testOverallScoreStorage.AbilityName);
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
                testOverallScoreStorage = new OverallScoreStorage();
                testOverallScoreStorage.Score = 5;
                int expectedScore = 5;

                yield return null;

                Assert.AreEqual(expectedScore, testOverallScoreStorage.Score);
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
            public IEnumerator WHEN_LevelSet_THEN_LevelGetFunctionWorks()
            {
                testOverallScoreStorage = new OverallScoreStorage();
                testOverallScoreStorage.Level = AbilityLevel.OK;
                AbilityLevel expectedAbilityLevel = AbilityLevel.OK;

                yield return null;

                Assert.AreEqual(expectedAbilityLevel, testOverallScoreStorage.Level);
            }
        }
    }
}