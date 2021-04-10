using NUnit.Framework;
using Storage;
using System.Collections;
using UnityEngine.TestTools;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class ImageHitStorageTests
        {
            private ImageHitStorage squaresData;
            private ImageHitRound round;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that ImageHit get correct identified value.
            </li>
        </ul>
        ")]
            //[UnityTest]
            public IEnumerator WHEN_ImageHitRoun_THEN_ImageHitStorageGetIsCorrectlyIdentifiedValue()
            {
                round = new ImageHitRound();
                round.isCorrectlyIdentified = false;

                bool isCorrectlyIdentified = false;

                yield return null;
                Assert.IsTrue(isCorrectlyIdentified == round.isCorrectlyIdentified, "ImageHit getter returning incorrect value:");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that ImageHit detect there is a key pressed.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ImageHitRoun_THEN_ImageHitStorageGetIsKeyPressedValue()
            {
                round = new ImageHitRound();
                round.isKeyPressed = false;

                bool isKeyPressed = false;

                yield return null;
                Assert.IsTrue(isKeyPressed == round.isKeyPressed, "ImageHit getter returning incorrect value:");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that ImageHit detect the space key is pressed.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ImageHitRoun_THEN_ImageHitStorageGeIsSpaceKeyValue()
            {
                round = new ImageHitRound();
                round.isSpaceKey = false;

                bool isSpaceKey = false;

                yield return null;
                Assert.IsTrue(isSpaceKey == round.isSpaceKey, "ImageHit getter returning incorrect value:");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that ImageHit get the key pressed value by minus the end time by start time.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ImageHitRound_THEN_ImageHitStorageGetskeyPressTimeValue()
            {
                round = new ImageHitRound();
                round.keyPressTime = 4.9f; // float value

                float keyPressTime = 4.9f;

                yield return null;
                Assert.IsTrue(keyPressTime == round.keyPressTime, "ImageHit getter returning incorrect value: ");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that ImageHit get correct image name.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ImageHitRound_THEN_ImageHitStorageGeimageNameValue()
            {
                round = new ImageHitRound();
                round.imageName = "Test"; // float value

                string imageName = "Test";

                yield return null;
                Assert.IsTrue(imageName == round.imageName, "ImageHit getter returning incorrect value: ");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test/li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that ImageHit get correct theme value.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ImageHitRound_THEN_ImageHitStorageGetestThemeValue()
            {
                round = new ImageHitRound();
                round.testTheme = "Test"; // float value

                string testTheme = "Test";

                yield return null;
                Assert.IsTrue(testTheme == round.testTheme, "ImageHit getter returning incorrect value: ");
            }
        }
    }
}