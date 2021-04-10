using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;
using Storage;
using Helper;

namespace EditModeTests
{
    namespace HelperTests
    {

        public class TimageTests
        {
            TImage testTimage = new TImage();

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the default theme name.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_DefaultThemeName()
            {
                // Use the Assert class to test conditions.
                // yield to skip a frame
                yield return null;
                bool temp = (testTimage.imageTheme == "");

                Assert.True(temp, "Default theme name should be empty");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing GetAverageTime function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_GetAverageTime_Method()
            {
                // Use the Assert class to test conditions.
                // yield to skip a frame


                yield return null;
                List<ImageHitRound> imagedata = new List<ImageHitRound>();
                float temp = TImage.GetAverageTime(imagedata);
                Assert.IsNotNull(temp, "Incorrect time...");
                imagedata.Add(new ImageHitRound());

                float temp1 = TImage.GetAverageTime(imagedata);
                Assert.IsNotNull(temp1, "Incorrect time...");

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing GetTotalKeyTime function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_GetTotalKeyTime_Method()
            {
                // Use the Assert class to test conditions.
                // yield to skip a frame
                List<ImageHitRound> imagedata = new List<ImageHitRound>();

                yield return null;
                float temp = TImage.GetTotalKeyTime(imagedata);

                Assert.IsTrue(temp == 0, "Incorrect GetTotalKeyTime...");

                List<ImageHitRound> imagedata1 = new List<ImageHitRound>();
                var testRound = new ImageHitRound();
                testRound.keyPressTime = 0.2f;
                imagedata1.Add(testRound);
                float temp1 = TImage.GetTotalKeyTime(imagedata1);

                Assert.IsTrue(temp1 == 0.2f, "Incorrect GetTotalKeyTime...");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing GetRightCounte function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_GetRightCounte_Method()
            {
                // Use the Assert class to test conditions.
                // yield to skip a frame
                yield return null;
                List<ImageHitRound> imagedata = new List<ImageHitRound>();

                float temp = TImage.GetRightCount(imagedata);

                Assert.IsTrue(temp == 0, "Incorrect GetRightCount...");

                List<ImageHitRound> imagedata1 = new List<ImageHitRound>();

                var testRound = new ImageHitRound();
                testRound.isCorrectlyIdentified = true;
                imagedata1.Add(testRound);

                float temp1 = TImage.GetRightCount(imagedata1);

                Assert.IsTrue(temp1 == 1, "Incorrect GetRightCount...");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing GetKeyPressCount function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_GetKeyPressCount_Method()
            {
                // Use the Assert class to test conditions.
                // yield to skip a frame
                yield return null;
                List<ImageHitRound> imagedata = new List<ImageHitRound>();

                int temp = TImage.GetKeyPressCount(imagedata);

                Assert.IsTrue(temp == 0, "Incorrect GetKeyPressCount value");

                List<ImageHitRound> imagedata1 = new List<ImageHitRound>();
                var testRound = new ImageHitRound();
                testRound.isKeyPressed = true;
                imagedata1.Add(testRound);

                int tem1p = TImage.GetKeyPressCount(imagedata1);

                Assert.IsTrue(tem1p == 1, "Incorrect GetKeyPressCount value");
            }
        }
    }
}