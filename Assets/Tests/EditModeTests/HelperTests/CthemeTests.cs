using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Helper;

namespace EditModeTests
{
    namespace HelperTests
    {
        public class CthemeTests
        {
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getimages function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CThemeGetImages()
            {
                // Use the Assert class to test conditions.

                yield return null;

                List<TImage> xxxx = new List<TImage>();
                xxxx.Add(new TImage());
                CTheme temp = new CTheme("xxx", xxxx);
                temp.sprites = new Sprite[10];

                List<TImage> list = temp.getimages();

                Assert.IsTrue(list != null, "No getimages...");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getrandimages function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CThemeGetRandomImages()
            {
                // Use the Assert class to test conditions.
                // yield to skip a frame
                //List<ImageHitRound> imagedata = new List<ImageHitRound>();

                yield return null;

                CTheme temp = new CTheme("xxx", new List<TImage>());
                temp.sprites = new Sprite[10];
                List<TImage> list = temp.getrandimages(10);

                Assert.IsTrue(list != null, "No getimages...");
            }
        }
    }
}

