using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Measurement;
using Storage;
using Games;
using UnityEngine.TestTools;

namespace EditModeTests
{
    namespace MeasurementTests
    {
        public class ObjectRecognitionMeasureTests
        {
            // Clear SelectiveVisualMeasure object
            private void ClearObjectRecognitionMeasure()
            {
                ObjectRecognitionMeasure.subScoreImageHit = new SubscoreStorage();
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if overall object recognition score is correctly calculated.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetObjectRecognitionScore()
            {
                yield return null;

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                for (int i = 0; i <= 10; i++)
                {
                    List<ImageHitRound> testRound = new List<ImageHitRound>();

                    for (int j = 0; j <= 10; j++)
                    {
                        ImageHitRound testro = new ImageHitRound();
                        testro.imageName = "111";
                        testro.isCorrectlyIdentified = true;
                        testro.keyPressTime = 0.2f;
                        testRound.Add(testro);
                    }

                    ObjectRecognitionMeasure.imagehitData.Rounds.Add(testRound);
                }

                float source1 = ObjectRecognitionMeasure.GetObjectRecognitionScore();
                Assert.True(source1 == 100, "message no Source");

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                for (int i = 0; i <= 10; i++)
                {
                    List<ImageHitRound> testRound = new List<ImageHitRound>();

                    for (int j = 0; j <= 10; j++)
                    {
                        ImageHitRound testro = new ImageHitRound();
                        testro.imageName = "111";
                        testro.isCorrectlyIdentified = true;
                        testro.keyPressTime = 0.7f;
                        testRound.Add(testro);
                    }

                    ObjectRecognitionMeasure.imagehitData.Rounds.Add(testRound);
                }

                float source2 = ObjectRecognitionMeasure.GetObjectRecognitionScore();
                Assert.True(source2 == 95, "message no Source");

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                for (int i = 0; i <= 10; i++)
                {
                    List<ImageHitRound> testRound = new List<ImageHitRound>();

                    for (int j = 0; j <= 10; j++)
                    {
                        ImageHitRound testro = new ImageHitRound();
                        testro.imageName = "111";
                        testro.isCorrectlyIdentified = true;
                        testro.keyPressTime = 1.2f;
                        testRound.Add(testro);
                    }

                    ObjectRecognitionMeasure.imagehitData.Rounds.Add(testRound);
                }

                float source3 = ObjectRecognitionMeasure.GetObjectRecognitionScore();
                Assert.True(source3 == 90, "message no Source");

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                for (int i = 0; i <= 10; i++)
                {
                    List<ImageHitRound> testRound = new List<ImageHitRound>();

                    for (int j = 0; j <= 10; j++)
                    {
                        ImageHitRound testro = new ImageHitRound();
                        testro.imageName = "111";
                        testro.isCorrectlyIdentified = true;
                        testro.keyPressTime = 1.8f;
                        testRound.Add(testro);
                    }

                    ObjectRecognitionMeasure.imagehitData.Rounds.Add(testRound);
                }

                float source4 = ObjectRecognitionMeasure.GetObjectRecognitionScore();
                Assert.True(source4 == 85, "message no Source");

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                for (int i = 0; i <= 10; i++)
                {
                    List<ImageHitRound> testRound = new List<ImageHitRound>();

                    for (int j = 0; j <= 10; j++)
                    {
                        ImageHitRound testro = new ImageHitRound();
                        testro.imageName = "111";
                        testro.isCorrectlyIdentified = true;
                        testro.keyPressTime = 2.3f;
                        testRound.Add(testro);
                    }

                    ObjectRecognitionMeasure.imagehitData.Rounds.Add(testRound);
                }
                float source5 = ObjectRecognitionMeasure.GetObjectRecognitionScore();
                Assert.True(source5 == 80, "message no Source");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if correct key pressing time is saved in both rounds.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetImageKeyPressTimeFromRound12()
            {
                yield return null;

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                List<ImageHitRound> testRound = new List<ImageHitRound>();

                for (int j = 0; j <= 10; j++)
                {
                    ImageHitRound testro = new ImageHitRound();
                    testro.imageName = "111";
                    testro.isCorrectlyIdentified = true;
                    testro.keyPressTime = 0.2f;
                    testRound.Add(testro);
                }

                testRound = null;

                float source1 = ObjectRecognitionMeasure.GetImageKeyTimeFromRound12(testRound);

                Assert.True(source1 == 0, "message no Source");

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                List<ImageHitRound> testRound2 = new List<ImageHitRound>();

                for (int j = 0; j <= 10; j++)
                {
                    ImageHitRound testro = new ImageHitRound();
                    testro.imageName = "111";
                    testro.isCorrectlyIdentified = true;
                    testro.keyPressTime = 0.2f;
                    testRound2.Add(testro);
                }

                float source2 = ObjectRecognitionMeasure.GetImageKeyTimeFromRound12(testRound2);
    
                Assert.True(source2.ToString() == "2.2", "message no Source");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test correct object recognition score is derived for both rounds.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetObjectRecognitionScoreFromRound12()
            {
                yield return null;

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                List<ImageHitRound> testRound = new List<ImageHitRound>();

                for (int j = 0; j <= 10; j++)
                {
                    ImageHitRound testro = new ImageHitRound();
                    testro.imageName = "111";
                    testro.isCorrectlyIdentified = true;
                    testro.keyPressTime = 0.2f;
                    testRound.Add(testro);
                }

                testRound = null;

                float source1 = ObjectRecognitionMeasure.GetObjectRecognitionScoreFromRound12(testRound);

                Assert.True(source1 == 0, "message no Source");

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                List<ImageHitRound> testRound2 = new List<ImageHitRound>();

                ImageHitRound testro1 = new ImageHitRound();
                testro1.isCorrectlyIdentified = true;
                testRound2.Add(testro1);

                float source2 = ObjectRecognitionMeasure.GetObjectRecognitionScoreFromRound12(testRound2);

                Assert.True(source2 == 8, "message no Source");

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                List<ImageHitRound> testRound3 = new List<ImageHitRound>();

                for (int j = 0; j <= 10; j++)
                {
                    ImageHitRound testro = new ImageHitRound();
                    testro.imageName = "111";
                    testro.isCorrectlyIdentified = true;
                    testro.keyPressTime = 0.2f;
                    testRound3.Add(testro);
                }

                float source3 = ObjectRecognitionMeasure.GetObjectRecognitionScoreFromRound12(testRound3);
                
                Assert.True(source3 == 8, "message no Source");

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                List<ImageHitRound> testRound4 = new List<ImageHitRound>();

                for (int j = 0; j <= 10; j++)
                {
                    ImageHitRound testro = new ImageHitRound();
                    testro.imageName = "111";
                    testro.isCorrectlyIdentified = false;
                    testro.keyPressTime = 0.2f;
                    testRound4.Add(testro);
                }

                float source4 = ObjectRecognitionMeasure.GetObjectRecognitionScoreFromRound12(testRound4);

                Assert.True(source4 == 0, "message no Source");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if correct image information is derived for both two rounds.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckGetImageFromRound12()
            {
                yield return null;

                ObjectRecognitionMeasure.imagehitData = null;
                ObjectRecognitionMeasure.imagehitData = new ImageHitStorage();
                ObjectRecognitionMeasure.imagehitData.Rounds = new List<List<ImageHitRound>>();

                List<ImageHitRound> testRound = ObjectRecognitionMeasure.GetImageFromRound12(new ImageHitRound(), null);
                Assert.True(testRound.Count == 0, "message no Source");

                ImageHitRound testro = new ImageHitRound();
                testro.imageName = "1";
                testro.imageTheme = "1";

                List<ImageHitRound> testRound1 = new List<ImageHitRound>();
                testRound1.Add(testro);

                List<ImageHitRound> testRound3 = ObjectRecognitionMeasure.GetImageFromRound12(testro, testRound1);

                Assert.True(testRound3.Count == 2, "message no Source");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test EvaluateImageHitScore function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_EvaluateImagheHit()
            {
                ClearObjectRecognitionMeasure();

                // Call tested function
                ObjectRecognitionMeasure.EvaluateImageHitScore();

                yield return null;
                Assert.AreEqual(AbilityName.OBJECT_RECOGNITION, ObjectRecognitionMeasure.subScoreImageHit.AbilityName);
                Assert.AreEqual(GameName.IMAGE_HIT, ObjectRecognitionMeasure.subScoreImageHit.GameName);
                Assert.AreEqual(0, ObjectRecognitionMeasure.subScoreImageHit.Score);
                Assert.AreEqual(2, ObjectRecognitionMeasure.subScoreImageHit.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test GetSubScoreForImageHit function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetSubScoreForImageHitFunctionCalled_ImageHiteSubScoreReturned()
            {
                ClearObjectRecognitionMeasure();

                // Set values for PointingMeasure.subScoreForImageHit
                ObjectRecognitionMeasure.subScoreImageHit.AbilityName = AbilityName.OBJECT_RECOGNITION;
                ObjectRecognitionMeasure.subScoreImageHit.GameName = GameName.IMAGE_HIT;
                ObjectRecognitionMeasure.subScoreImageHit.Score = 65;
                ObjectRecognitionMeasure.subScoreImageHit.Weight = 2;

                // Call tested function
                SubscoreStorage returnedSubscoreImageHit = ObjectRecognitionMeasure.GetSubScoreForImageHit();
                SubscoreStorage expectedSubscoreImageHit = ObjectRecognitionMeasure.subScoreImageHit;

                yield return null;
                // Test ObjectRecognitionMeasure.subScoreImegeHit is correctly returned
                Assert.AreEqual(expectedSubscoreImageHit.AbilityName, returnedSubscoreImageHit.AbilityName);
                Assert.AreEqual(expectedSubscoreImageHit.GameName, returnedSubscoreImageHit.GameName);
                Assert.AreEqual(expectedSubscoreImageHit.Score, returnedSubscoreImageHit.Score);
                Assert.AreEqual(expectedSubscoreImageHit.Weight, returnedSubscoreImageHit.Weight);
            }
        }
    }
}