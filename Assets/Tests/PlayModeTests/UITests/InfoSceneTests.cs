using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;
using TMPro;
using UnityEngine.EventSystems;

namespace PlayModeTests
{
    namespace UITests
    {
        public class InfoSceneTests
        {
            private TextMeshProUGUI titleText;
            private TextMeshProUGUI agreeText;
            private TextMeshProUGUI infoText;
            private Toggle toggle;
            private Camera camera;
            private Scrollbar scrollbar;
            private Text readInformationFirstText;

            private Color TEXT_BLACK_COLOR = new Color32(0x0, 0x0, 0x0, 0xFF);
            private Color TEXT_BLUE_COLOR = new Color32(0x9, 0x26, 0xD9, 0xFF);
            private Color BACKGROUND_COLOR = new Color32(0xFF, 0xFF, 0xFF, 0x00);

            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.INFO_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                titleText = GameObject.Find("InfoTitle").GetComponent<TMPro.TextMeshProUGUI>();
                agreeText = GameObject.Find("AgreeText").GetComponent<TMPro.TextMeshProUGUI>();
                infoText = GameObject.Find("InfoText").GetComponent<TMPro.TextMeshProUGUI>();

                camera = GameObject.Find("Main Camera").GetComponent<Camera>();
                toggle = GameObject.Find("Toggle").GetComponent<Toggle>();
                scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();

                // We must go through the Canvas since this object is initially invisible (can't be found directly)
                readInformationFirstText = GameObject.Find("Canvas").transform
                    .Find("ReadInformationFirstText").GetComponent<Text>();

                // Remove event so this method is not re-evaluated
                // if a non-Questionnaire scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test if the Info Scene is exactly the
                second scene loaded in battery.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator InfoSceneIsSecondSceneLoadedInBattery()
            {
                SceneManager.LoadScene(1);

                yield return null;

                Assert.AreEqual(SceneName.INFO_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#ar-1'>AR-1</a></li>
            <li><b>Test description:</b> The player must accept the terms and
                conditions, and understand the requirements and recommendations
                regarding the application.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_InfoSceneTextNotRead_THEN_QuestionnaireSceneIsNotLoaded()
            {
                SimulateToggleClick();

                yield return null;

                Assert.AreEqual(SceneName.INFO_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [UnityTest]
            public IEnumerator WHEN_InfoSceneTextNotRead_THEN_ClickingOnToggleRevealsIndicatorText()
            {
                Assert.False(readInformationFirstText.IsActive());
                SimulateToggleClick();

                yield return null;

                Assert.True(readInformationFirstText.IsActive());
            }

            [UnityTest]
            public IEnumerator WHEN_InfoSceneIsRead_THEN_QuestionnaireSceneLoaded()
            {
                SimulateScrollingToEndOfText();
                SimulateToggleClick();

                yield return null;

                Assert.AreEqual(SceneName.QUESTIONNAIRE_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test if the background of the Info Scene is white.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator BackgroundWhite()
            {
                yield return null;
                Assert.AreEqual(BACKGROUND_COLOR,
                    camera.backgroundColor);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test all text in the Info Scene uses correct colour.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator AllTextIsTheCorrectColour()
            {
                yield return null;

                Assert.AreEqual(TEXT_BLACK_COLOR, titleText.color);
                Assert.AreEqual(TEXT_BLACK_COLOR, infoText.color);
                Assert.AreEqual(TEXT_BLUE_COLOR, agreeText.color);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test if the scroll bar is interactable.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator ScrollbarIsInteractable()
            {
                yield return null;

                Assert.AreEqual(true, scrollbar.interactable);
            }

            private void SimulateToggleClick()
            {
                ExecuteEvents.Execute(toggle.gameObject,
                    new PointerEventData(EventSystem.current),
                    ExecuteEvents.pointerClickHandler);
            }

            private void SimulateScrollingToEndOfText()
            {
                scrollbar.value = 0;
            }
        }
    }
}