using System;
using System.Collections;
using System.Collections.Generic;
using Games;
using NUnit.Framework;
using Storage;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeTests
{
    namespace SystemTests
    {
        namespace BackendConnectionTests
        {
            public class ElasticsearchStorageManagerTests : MonoBehaviour
            {
                // Sample battery session storage used for the test.
                static BatterySessionStorage batterySessionToStore = new BatterySessionStorage
                {
                    BatterySessionId = new Guid(),
                    Player = new PlayerStorage
                    {
                        Name = "Alan Turing",
                        Age = 108,
                        KeyBoard = true,
                        Mouse = false,
                        UserId = new Guid()
                    },
                    SubScoreSeq = new List<SubscoreStorage>
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
            },
                    OverallScoreSeq = new List<OverallScoreStorage>
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
            },
                    MiniGameOrder = new List<GameName>
            {
                GameName.CATCH_THE_BALL,
                GameName.BALLOONS,
                GameName.IMAGE_HIT,
                GameName.SQUARES,
                GameName.CATCH_THE_THIEF
            }
                };
                static BatterySessionStorage retrievedBatterySession;


                [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> This test checks a number of things:
                <ol>
                    <li>It checks that a battery session storage can be successfully saved to
                    the remote Elasticsearch.</li>
                    <li>It checks that a battery session storage can be successfully retrieved from
                        the remote Elasticsearch.</li>
                    <li>It checks that there is no information lost (during JSON conversion)
                        in the battery session when it is stored in Elasticsearch
                        by seeing if the saved BatterySessionStorage object is
                        equal to that which is retrieved after the former is saved.</li>
                </ol>
            </li>
        </ul>
    ")]
                [UnityTest]
                public IEnumerator WHEN_BatterySessionStoredToElasticsearchAndThenRetrieved_THEN_BatterySessionsAreEqual()
                {
                    // When yielded back, the retrieved battery session will be stored in
                    // retrievedBatterySession.
                    yield return new MonoBehaviourTest<ElasticsearchStorageThenRetrieval>();
                    Assert.AreEqual(batterySessionToStore, retrievedBatterySession);
                }

                /// <summary>
                /// Since UnityWebRequest relies on MonoBehaviour, we need this throwaway
                /// class for the test. See: https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/reference-tests-monobehaviour.html
                /// </summary>
                class ElasticsearchStorageThenRetrieval : MonoBehaviour, IMonoBehaviourTest
                {
                    public bool IsTestFinished { set; get; } = false;

                    /// <summary>
                    /// Stores the batterySessionToStore to Elasticsearch, and upon successful confirmation
                    /// that it has been saved, retrieves it back from Elasticsearch by
                    /// looking it up using its BatterySessionId.
                    /// </summary>
                    void Start()
                    {
                        IStorage storage = gameObject.AddComponent<ElasticsearchStorageManager>();
                        storage.Store(batterySessionToStore, () =>
                            storage.Retrieve(batterySessionToStore.BatterySessionId, retrieved =>
                            {
                                retrievedBatterySession = retrieved;
                                IsTestFinished = true;
                            })
                        );
                    }
                }
            }
        }
    }
}