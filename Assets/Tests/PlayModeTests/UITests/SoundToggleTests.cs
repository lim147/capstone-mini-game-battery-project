using System.Collections;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Assert = UnityEngine.Assertions.Assert;

namespace PlayModeTests
{
    namespace UITests
    {
        public class SoundToggleTests
        {
            private Toggle soundToggle;

            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.START_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Needed because static values, like volume, don't reset between tests.
            [TearDown]
            public void ResetVolume()
            {
                soundToggle.isOn = true;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                soundToggle = GameObject.Find("SoundToggle").GetComponent<Toggle>();

                // Remove event so this method is not re-evaluated
                // if a non-SoundToggleScripts scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the sound is on in the info scene.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_InfoSceneLoaded_THEN_SoundIsOn()
            {
                yield return null;

                Assert.AreEqual(1, AudioListener.volume);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> When you turn off the sound, the sound is off.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_SoundOnAndTogglePressed_SoundIsOff()
            {
                Assert.AreEqual(1, AudioListener.volume);

                soundToggle.isOn = !soundToggle.isOn;

                yield return null;
                Assert.AreEqual(0, AudioListener.volume);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> When the sound is turned on, the sound is on.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_SoundOffAndTogglePressed_SoundIsOn()
            {
                soundToggle.isOn = false;

                yield return null;
                Assert.AreEqual(0, AudioListener.volume);

                soundToggle.isOn = !soundToggle.isOn;
                yield return null;
                Assert.AreEqual(1, AudioListener.volume);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the sound setting is preserved across scenes.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator SoundSettingPreservedAcrossScenes()
            {
                soundToggle.isOn = false;
                Assert.AreEqual(0, AudioListener.volume);

                yield return null;

                GameObject.Find("StartButton").GetComponent<Button>().onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.INFO_SCENE, SceneManager.GetActiveScene().name);

                soundToggle = GameObject.Find("SoundToggle").GetComponent<Toggle>();

                Assert.IsFalse(soundToggle.isOn);
                Assert.AreEqual(0, AudioListener.volume);
            }
        }
    }
}