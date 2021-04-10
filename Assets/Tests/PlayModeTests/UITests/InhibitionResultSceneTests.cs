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
        public class InhibitionResultSceneTests
        {
            private Color TEXT_BLACK_COLOR = new Color32(0x0, 0x0, 0x0, 0xFF);
            private Color TEXT_BLUE_COLOR = new Color32(0x9, 0x26, 0xD9, 0xFF);
            private Color BACKGROUND_COLOR = new Color32(0xFF, 0xFF, 0xFF, 0xFF);

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Make sure the back button moves the game to the general result scene.</li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_BackButtonPressed_THEN_GoesToResultScene()
            {
                SceneManager.LoadScene(SceneName.INHIBITION_RESULT_SCENE);

                yield return null;

                Button backButton = GameObject.Find("Button").GetComponent<Button>();
                backButton.onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.RESULT_SCENE,
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
                SceneManager.LoadScene(SceneName.INHIBITION_RESULT_SCENE);

                yield return null;

                Color backgroundColor = GameObject.Find("Background").GetComponent<Image>().color;
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
                SceneManager.LoadScene(SceneName.INHIBITION_RESULT_SCENE);

                yield return null;

                Color AbilityTitleTextColour = GameObject.Find("AbilityTitle").GetComponent<TMPro.TextMeshProUGUI>().color;
                Assert.AreEqual(TEXT_BLACK_COLOR, AbilityTitleTextColour);

                Color AbilityDescriptionTextColour = GameObject.Find("AbilityDescription").GetComponent<TMPro.TextMeshProUGUI>().color;
                Assert.AreEqual(TEXT_BLACK_COLOR, AbilityDescriptionTextColour);

                Color ScoreLabelTextColour = GameObject.Find("ScoreLabelText").GetComponent<TMPro.TextMeshProUGUI>().color;
                Assert.AreEqual(TEXT_BLACK_COLOR, ScoreLabelTextColour);

                Color ScoreValueTextColour = GameObject.Find("ScoreValue").GetComponent<TMPro.TextMeshProUGUI>().color;
                Assert.AreEqual(TEXT_BLACK_COLOR, ScoreValueTextColour);

                Color ScoreLevelTextColour = GameObject.Find("ScoreLevelText").GetComponent<TMPro.TextMeshProUGUI>().color;
                Assert.AreEqual(TEXT_BLACK_COLOR, ScoreLevelTextColour);

                Color LevelValueTextColour = GameObject.Find("LevelValue").GetComponent<TMPro.TextMeshProUGUI>().color;
                Assert.AreEqual(TEXT_BLACK_COLOR, LevelValueTextColour);
            }
        }
    }
}