using System;
using System.Collections;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace PlayModeTests
{
    namespace UITests
    {
        public class TooltipTests
        {
            private float tolerance = 0.01f;
            private GameObject tooltip;
            private RectTransform tooltipBackground;
            private Text tooltipText;

            /// <summary>
            /// Load the scene for the playmode tests.
            /// </summary>
            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.MENU_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                tooltip = GameObject.Find("Tooltip");
                Assert.IsNotNull(tooltip, "Missing tooltip.");

                tooltipBackground = GameObject.Find("tooltipBackground").GetComponent<RectTransform>();
                tooltipText = GameObject.Find("tooltipText").GetComponent<Text>();

                // Remove event so this method is not re-evaluated
                // if a non-BalloonsInstruction scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the tooltip's position when menu page is initially loaded.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestTooltipInitialPosition()
            {
                float xPos = tooltip.transform.localPosition.x;
                float actualXPos = -2000f;

                float yPos = tooltip.transform.localPosition.y;
                float actualYPos = -200f;

                yield return null;
                Assert.IsTrue(Math.Abs(xPos - actualXPos) < tolerance);
                Assert.IsTrue(Math.Abs(yPos - actualYPos) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the tooltip's initial background length.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestTooltipInitialBackgroundLength()
            {        
                float actualBackgroundWidth = tooltipBackground.sizeDelta.x;
                float actualBackgroundHeight = tooltipBackground.sizeDelta.y;

                float expectedBackgroundWidth = 100;
                float expectedBackgroundHeight = 30;

                yield return null;
                Assert.IsTrue(Math.Abs( expectedBackgroundWidth-actualBackgroundWidth) < tolerance);
                Assert.IsTrue(Math.Abs(expectedBackgroundHeight-actualBackgroundHeight) < tolerance);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test ShowGamePlayedTooltip function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_ShowGamePlayedTooltip_Method()
            {
                // Call tested function
                Tooltip.ShowGamePlayedTooltip();

                float actualBackgroundWidth = tooltipBackground.sizeDelta.x;
                float actualBackgroundHeight = tooltipBackground.sizeDelta.y;

                float textPaddingSize = 4f;

                float expectedBackgroundWidth = tooltipText.preferredWidth + textPaddingSize * 2;
                float expectedBackgroundHeight = tooltipText.preferredHeight + textPaddingSize * 2;

                yield return null;
                // Tooltip should be active
                Assert.IsTrue(tooltip.activeSelf);
                // Tooltip background width and height should depend on tooltip text legth
                Assert.IsTrue(Math.Abs(expectedBackgroundWidth - actualBackgroundWidth) < tolerance);
                Assert.IsTrue(Math.Abs(expectedBackgroundHeight - actualBackgroundHeight) < tolerance);
            }
        }
    }
}