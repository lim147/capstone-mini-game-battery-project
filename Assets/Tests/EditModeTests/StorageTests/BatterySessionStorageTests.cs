using System;
using System.Collections;
using System.Collections.Generic;
using Games;
using NUnit.Framework;
using Storage;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class BatterySessionStorageTests : MonoBehaviour
        {
            private BatterySessionStorage batterySessionStorage;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_BatterySessionIdSet_THEN_BatterySessionIdObtained()
            {
                batterySessionStorage = new BatterySessionStorage();
                Guid guid = new Guid();
                batterySessionStorage.BatterySessionId = guid;

                yield return null;

                Assert.AreEqual(guid, batterySessionStorage.BatterySessionId);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test Player field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_PlayerSet_THEN_PlayerObtained()
            {
                batterySessionStorage = new BatterySessionStorage();
                PlayerStorage player = new PlayerStorage
                {
                    Name = "Alan Turing",
                    Age = 108,
                    KeyBoard = true,
                    Mouse = false,
                    UserId = new Guid()
                };
                batterySessionStorage.Player = player;

                yield return null;

                Assert.AreEqual(player, batterySessionStorage.Player);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test SubScoreSeq field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SubScoreSeqSet_THEN_SubScoreSeqObtained()
            {
                batterySessionStorage = new BatterySessionStorage();
                List<SubscoreStorage> subscoreSeq = new List<SubscoreStorage>
                {
                    new SubscoreStorage
                    {
                        AbilityName = Measurement.AbilityName.TIME_TO_CONTACT,
                        GameName = Games.GameName.CATCH_THE_BALL,
                        Score = 50,
                        Weight = 1
                    },
                    new SubscoreStorage
                    {
                        AbilityName = Measurement.AbilityName.VISUOSPATIAL_SKETCHPAD,
                        GameName = Games.GameName.SQUARES,
                        Score = 80,
                        Weight = 1
                    }
                };
                batterySessionStorage.SubScoreSeq = subscoreSeq;

                yield return null;

                Assert.AreEqual(subscoreSeq, batterySessionStorage.SubScoreSeq);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test OverallScoreSeq field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_OverallScoreSeqSet_THEN_OverallScoreSeqObtained()
            {
                batterySessionStorage = new BatterySessionStorage();
                List<OverallScoreStorage> overallScoreSeq = new List<OverallScoreStorage>
                {
                    new OverallScoreStorage
                    {
                        AbilityName = Measurement.AbilityName.TIME_TO_CONTACT,
                        Score = 50,
                        Level = Measurement.AbilityLevel.POOR
                    },
                    new OverallScoreStorage
                    {
                        AbilityName = Measurement.AbilityName.VISUOSPATIAL_SKETCHPAD,
                        Score = 80,
                        Level = Measurement.AbilityLevel.GOOD
                    }
                };
                batterySessionStorage.OverallScoreSeq = overallScoreSeq;

                yield return null;

                Assert.AreEqual(overallScoreSeq, batterySessionStorage.OverallScoreSeq);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test MiniGameOrder field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_MiniGameOrderSet_THEN_MiniGameOrderObtained()
            {
                batterySessionStorage = new BatterySessionStorage();
                List<GameName> miniGameOrder = new List<GameName>
                {
                    GameName.CATCH_THE_BALL,
                    GameName.BALLOONS,
                    GameName.IMAGE_HIT,
                    GameName.SQUARES,
                    GameName.CATCH_THE_THIEF
                };
                batterySessionStorage.MiniGameOrder = miniGameOrder;

                yield return null;

                Assert.AreEqual(miniGameOrder, batterySessionStorage.MiniGameOrder);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Tests</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test UIInteractionData field.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_UIInteractionDataSet_THEN_UIInteractionDataObtained()
            {
                batterySessionStorage = new BatterySessionStorage();
                UIInteractionStorage uiInteractionStorage = new UIInteractionStorage();

                batterySessionStorage.UIInteractionData = uiInteractionStorage;

                yield return null;

                Assert.AreEqual(uiInteractionStorage, batterySessionStorage.UIInteractionData);
            }
        }
    }
}

