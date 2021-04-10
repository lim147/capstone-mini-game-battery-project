using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using Management;
using Storage;
using System;
using UI;
using Games;

namespace EditModeTests
{
    namespace ManagementTests
    {
        public class BatterySessionManagementTests
        {
            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------
            // Tests driven by public functions

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test IfMeasureCalled function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_IfMeasureCalledFunctionCalled_THEN_ValueOfMeasureEndReturned()
            {
                bool actualValue = BatterySessionManagement.IfMeasureCalled();

                yield return null;
                Assert.AreEqual(BatterySessionManagement.measureEnd, actualValue);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test FillPlayer function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_FillPlayerFunctionCalled_THEN_PlayerIsFilled()
            {
                BatterySessionManagement batterySessionManagement = new BatterySessionManagement();
                // Call tested function
                batterySessionManagement.FillPlayer();
                PlayerStorage actualPlayer = batterySessionManagement.player;

                yield return null;
                Assert.IsNotNull(actualPlayer);
                Assert.AreEqual(typeof(Guid), actualPlayer.UserId.GetType());
                Assert.AreEqual(typeof(int), actualPlayer.Age.GetType());
                Assert.AreEqual(typeof(bool), actualPlayer.KeyBoard.GetType());
                Assert.AreEqual(typeof(bool), actualPlayer.Mouse.GetType());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test FillSessionId function to make sure that the session ID value type is as expected.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_FillSessionIdFunctionCalled_THEN_SessionIdIsFilled()
            {
                BatterySessionManagement batterySessionManagement = new BatterySessionManagement();
                // Call tested function
                batterySessionManagement.FillSessionId();

                yield return null;
                Assert.AreEqual(typeof(Guid), batterySessionManagement.sessionId.GetType());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test FillSessionId function to make sure that the guid value is random.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_FillSessionIdFunctionCalled_THEN_RandomGuidIsGenerated()
            {
                BatterySessionManagement batterySessionManagement1 = new BatterySessionManagement();
                batterySessionManagement1.FillSessionId();
                Guid guid1 = batterySessionManagement1.sessionId;

                BatterySessionManagement batterySessionManagement2 = new BatterySessionManagement();
                batterySessionManagement2.FillSessionId();
                Guid guid2 = batterySessionManagement2.sessionId;

                yield return null;
                Assert.AreNotEqual(guid1, guid2);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test FillGameOrder function to make sure that the gameorder is the same as the global value.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_FillGameOrderFunctionCalled_THEN_GameOrderIsFilled()
            {
                BatterySessionManagement batterySessionManagement = new BatterySessionManagement();
                batterySessionManagement.FillGameOrder();

                yield return null;
                // The gameOrder should be the same as Globals.gameOrder
                Assert.AreEqual(Globals.gameOrder, batterySessionManagement.gameOrder);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test FillAllGameData function to make sure that global values are set.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_FillAllGameDataFunctionCalled_THEN_GameDataIsFilled()
            {
                // Make all games labelled as played
                Globals.isBalloonsButtonOn = false;
                Globals.isCTFButtonOn = false;
                Globals.isImageHitButtonOn = false;
                Globals.isSquaresButtonOn = false;
                Globals.isCatchTheBallButtonOn = false;
                Globals.isJudgeTheBallButtonOn = false;
                Globals.isSaveOneBallButtonOn = false;
                
                BatterySessionManagement batterySessionManagement = new BatterySessionManagement();

                batterySessionManagement.FillAllGameData();

                yield return null;
                // Game data should be filled with actual gameplay data from each game
                Assert.AreEqual(Balloons.GetGameplayData(), batterySessionManagement.balloonsData);
                Assert.AreEqual(CatchTheThief.GetGameplayData(), batterySessionManagement.ctfData);
                Assert.AreEqual(Squares.GetGameplayData(), batterySessionManagement.squaresData);
                Assert.AreEqual(ImageHit.GetGameplayData(), batterySessionManagement.imageHitData);
                Assert.AreEqual(CatchTheBall.GetGameplayData(), batterySessionManagement.catchTheBallData);
                Assert.AreEqual(JudgeTheBall.GetGameplayData(), batterySessionManagement.judgeTheBallData);
                Assert.AreEqual(SaveOneBall.GetGameplayData(), batterySessionManagement.saveOneBallData);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test FillSubScoreSeq function to make that the number of records is within expected amount.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_FillSubScoreSeqFunctionCalled_THEN_SubScoreSeqIsFilled()
            {
                // Make all games labelled as played
                Globals.isBalloonsButtonOn = false;
                Globals.isCTFButtonOn = false;
                Globals.isImageHitButtonOn = false;
                Globals.isSquaresButtonOn = false;
                Globals.isCatchTheBallButtonOn = false;
                Globals.isJudgeTheBallButtonOn = false;
                Globals.isSaveOneBallButtonOn = false;

                BatterySessionManagement batterySessionManagement = new BatterySessionManagement();
                batterySessionManagement.FillSubScoreSeq();

                yield return null;
                // subScoreSeq should contain 0 to 13 records
                Assert.IsTrue(0 <= batterySessionManagement.subScoreSeq.Count
                    && batterySessionManagement.subScoreSeq.Count <= 13);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test FillOverallScoreSeq function, to make sure that the overallScoreSeq value is empty by default.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_FillOverallScoreSeqFunctionCalled_THEN_OverallScoreSeqIsFilled()
            {
                // Make all games labelled as played
                Globals.isBalloonsButtonOn = false;
                Globals.isCTFButtonOn = false;
                Globals.isImageHitButtonOn = false;
                Globals.isSquaresButtonOn = false;
                Globals.isCatchTheBallButtonOn = false;
                Globals.isJudgeTheBallButtonOn = false;
                Globals.isSaveOneBallButtonOn = false;

                // Clear AbilityManagement
                AbilityManagement.overallScoreSeq = new List<OverallScoreStorage>();

                BatterySessionManagement batterySessionManagement = new BatterySessionManagement();
                batterySessionManagement.FillOverallScoreSeq();

                yield return null;
                // overallScoreSeq should be empty list by default
                Assert.AreEqual(0, batterySessionManagement.overallScoreSeq.Count);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test FillBatterySessionStorage function, to make sure values are not null after the function is called.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_FillBatterySessionStorageFunctionCalled_THEN_BatterySessionStorageIsFilled()
            {
                BatterySessionManagement batterySessionManagement = new BatterySessionManagement();
                batterySessionManagement.FillBatterySessionStorage();
                BatterySessionStorage actualBatterySessionStorage = batterySessionManagement.batterySessionStorage;
                PlayerStorage actualPlayer = actualBatterySessionStorage.Player;

                yield return null;
                Assert.IsNotNull(actualBatterySessionStorage.BatterySessionId);

                Assert.IsNotNull(actualPlayer);
                Assert.AreEqual(typeof(Guid), actualPlayer.UserId.GetType());
                Assert.AreEqual(typeof(int), actualPlayer.Age.GetType());
                Assert.AreEqual(typeof(bool), actualPlayer.KeyBoard.GetType());
                Assert.AreEqual(typeof(bool), actualPlayer.Mouse.GetType());

                Assert.IsNull(actualBatterySessionStorage.SubScoreSeq);
                Assert.IsNull(actualBatterySessionStorage.OverallScoreSeq);
                Assert.IsNull(actualBatterySessionStorage.MiniGameOrder);

                Assert.IsNotNull(actualBatterySessionStorage.BalloonsData);
                Assert.IsNotNull(actualBatterySessionStorage.CatchTheThiefData);
                Assert.IsNotNull(actualBatterySessionStorage.SquaresData);
                Assert.IsNotNull(actualBatterySessionStorage.ImageHitData);
                Assert.IsNotNull(actualBatterySessionStorage.CatchTheBallData);
                Assert.IsNotNull(actualBatterySessionStorage.SaveOneBallData);
                Assert.IsNotNull(actualBatterySessionStorage.JudgeTheBallData);

                Assert.IsNotNull(actualBatterySessionStorage.UIInteractionData);
            }
        }
    }
}