using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using Storage;
using System;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class UIInteractionStorageTests
        {
            // Tolerance for comparing decimals
            private double tolerance = 0.001;
            // Decleare UIInteractionStorage object
            private UIInteractionStorage testUIInteractionStorage;

            [SetUp]
            public void CreateTimerObject()
            {
                // Reset singlton object fields to initialized values
                UIInteractionStorageSingleton.TimeStayOnStartScene = -1;
                UIInteractionStorageSingleton.TimeStayOnSquaresInstructionsScene = -1;
                UIInteractionStorageSingleton.TimeStayOnQuestionnaireScene = new List<double>();
                UIInteractionStorageSingleton.TimeStayOnMenuScene = new List<double>();
                UIInteractionStorageSingleton.TimeStayOnInfoScene = -1;
                UIInteractionStorageSingleton.TimeStayOnImageHitInstructionsScene = -1;
                UIInteractionStorageSingleton.TimeStayOnCTFInstructionsScene = -1;
                UIInteractionStorageSingleton.TimeStayOnBalloonsInstructionsScene = -1;
                UIInteractionStorageSingleton.TimeStayOnCatchTheBallInstructionsScene = -1;
                UIInteractionStorageSingleton.TimeStayOnSaveOneBallInstructionsScene = -1;
                UIInteractionStorageSingleton.TimeStayOnJudgeTheBallInstructionsScene = -1;
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
            public IEnumerator WHEN_NewInstanceCreated_THEN_ValuesAreInitialized()
            {
                testUIInteractionStorage = new UIInteractionStorage();
                double expectedInitializedTimeStayScene = -1;
                double expectedInitializedTimeSeqLength = 0;

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnStartScene - expectedInitializedTimeStayScene) < tolerance);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnSquaresInstructionsScene - expectedInitializedTimeStayScene) < tolerance);
                Assert.AreEqual(expectedInitializedTimeSeqLength, testUIInteractionStorage.TimeStayOnQuestionnaireScene.Count);
                Assert.AreEqual(expectedInitializedTimeSeqLength, testUIInteractionStorage.TimeStayOnMenuScene.Count);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnInfoScene - expectedInitializedTimeStayScene) < tolerance);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnImageHitInstructionsScene - expectedInitializedTimeStayScene) < tolerance);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnCTFInstructionsScene - expectedInitializedTimeStayScene) < tolerance);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnBalloonsInstructionsScene - expectedInitializedTimeStayScene) < tolerance);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnCatchTheBallInstructionsScene - expectedInitializedTimeStayScene) < tolerance);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnSaveOneBallInstructionsScene - expectedInitializedTimeStayScene) < tolerance);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnJudgeTheBallInstructionsScene - expectedInitializedTimeStayScene) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnStartScene field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnStartSceneInSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnStartScene = 1.5;
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnStartScene - UIInteractionStorageSingleton.TimeStayOnStartScene) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnSquaresInstructionsScene field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnSquaresInstructionsSceneInSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnSquaresInstructionsScene = 0.32;
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnSquaresInstructionsScene - UIInteractionStorageSingleton.TimeStayOnSquaresInstructionsScene) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnQuestionnaireScene field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnQuestionnaireSceneInSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnQuestionnaireScene = new List<double> { 0.1, 1.5 };
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.AreEqual(2, testUIInteractionStorage.TimeStayOnQuestionnaireScene.Count);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnQuestionnaireScene[0] - 0.1) < tolerance);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnQuestionnaireScene[1] - 1.5) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnMenuScene field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnMenuSceneInSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnMenuScene = new List<double> { 5.2 };
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.AreEqual(1, testUIInteractionStorage.TimeStayOnMenuScene.Count);
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnMenuScene[0] - 5.2) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnInfoScene field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnInfoSceneInSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnInfoScene = 7.65;
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnInfoScene - UIInteractionStorageSingleton.TimeStayOnInfoScene) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnImageHitInstructionsScene field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnImageHitInstructionsSceneInSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnImageHitInstructionsScene = 8.3;
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnImageHitInstructionsScene - UIInteractionStorageSingleton.TimeStayOnImageHitInstructionsScene) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnCTFInstructionsScene field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnCTFInstructionsSceneInSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnCTFInstructionsScene = 2.7;
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnCTFInstructionsScene - UIInteractionStorageSingleton.TimeStayOnCTFInstructionsScene) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnBalloonsInstructionsScene field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnBalloonsInstructionsSceneInSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnBalloonsInstructionsScene = 0.6;
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnBalloonsInstructionsScene - UIInteractionStorageSingleton.TimeStayOnBalloonsInstructionsScene) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnBallInstructionsScene1 field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnCatchTheBallInstructionsScene_InSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnCatchTheBallInstructionsScene = 8.61;
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnCatchTheBallInstructionsScene - UIInteractionStorageSingleton.TimeStayOnCatchTheBallInstructionsScene) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnBallInstructionsScene2 field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnSaveOneBallInstructionsScene_InSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnSaveOneBallInstructionsScene = 7.9;
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnSaveOneBallInstructionsScene - UIInteractionStorageSingleton.TimeStayOnSaveOneBallInstructionsScene) < tolerance);
            }

            [Description(@"
            <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing TimeStayOnBallInstructionsScene3 field.
            </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TimeStayOnJudgeTheBallInstructionsScene_InSingletonChanged_THEN_ItAlsoChanged()
            {
                UIInteractionStorageSingleton.TimeStayOnJudgeTheBallInstructionsScene = 6.0;
                testUIInteractionStorage = new UIInteractionStorage();

                yield return null;
                Assert.IsTrue(Math.Abs(testUIInteractionStorage.TimeStayOnJudgeTheBallInstructionsScene - UIInteractionStorageSingleton.TimeStayOnJudgeTheBallInstructionsScene) < tolerance);
            }
        }
    }
}