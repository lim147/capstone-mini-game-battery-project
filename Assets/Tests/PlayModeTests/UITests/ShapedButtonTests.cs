using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UI;
using System;

namespace PlayModeTests
{
    namespace UITests
    {
        public class ShapedButtonTests
        {
            // Balloon game object
            private GameObject balloon;

            // Tolerance for comparing decimal valus
            private float tolerance = 0.01f;

            [SetUp]
            public void LoadScene()
            {
                // Load game scene
                SceneManager.LoadScene(SceneName.BALLOONS_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                // Link game object to local variable; throw out exception if there is no such game object
                balloon = GameObject.Find("Balloon");
                Assert.IsNotNull(balloon, "Missing gameobject: " + "balloon");

                // Remove event so this method is not re-evaluated
                // if a non-BalloonsInstruction scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            //------------------------ Unit Tests begin ------------------------------
            //------------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests if the button the balloon is
                attached to is shaped by checking its AlphaThreshold value.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator BalloonButtonIsShaped()
            {
                float actualThreshold = balloon.GetComponent<ShapedButton>().AlphaThreshold;
                float expectedThreshold = 1f;

                yield return null;
                Assert.IsTrue(Math.Abs(expectedThreshold - actualThreshold) <= tolerance);
            }
        }
    }
}