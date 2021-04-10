using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;

namespace PlayModeTests
{
    namespace UITests
    {
        public class StartSceneTests
        {
            private Color TEXT_BLACK_COLOR = new Color32(0x0, 0x0, 0x0, 0xFF);
            private Color TEXT_BLUE_COLOR = new Color32(0x9, 0x26, 0xD9, 0xFF);
            private Color BACKGROUND_COLOR = new Color32(0xFF, 0xFF, 0xFF, 0x00);

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the start button moves the game to the info scene.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_StartButtonPressed_THEN_GoesToInfoScene()
            {
                SceneManager.LoadScene(SceneName.START_SCENE);

                yield return null;

                Button startButton = GameObject.Find("StartButton").GetComponent<Button>();
                startButton.onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.INFO_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the background is white.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator BackgroundWhite()
            {
                SceneManager.LoadScene(SceneName.START_SCENE);
                
                yield return null;

                Color backgroundColor = GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor;
                Assert.AreEqual(BACKGROUND_COLOR, backgroundColor);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the text is black.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator AllTextIsTheCorrectColour()
            {
                SceneManager.LoadScene(SceneName.START_SCENE);

                yield return null;

                Color mainMenuTextColor = GameObject.Find("MainMenuText").GetComponent<TMPro.TextMeshProUGUI>().color;
                Assert.AreEqual(TEXT_BLACK_COLOR, mainMenuTextColor);

                Color buttonTextColor = GameObject.Find("StartText").GetComponent<TMPro.TextMeshProUGUI>().color;
                Assert.AreEqual(TEXT_BLACK_COLOR, buttonTextColor);
            }
        }
    }
}