using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UI;
using TMPro;
using UnityEngine.EventSystems;

namespace PlayModeTests
{
    namespace UITests
    {
        public class TextButtonTests
        {
            /// <summary>
            /// Load the scene for the playmode tests.
            /// </summary>
            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.RESULT_SCENE);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test that when click on any ability title, the game jumps to the corresponding
                   single ability result scene.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickOnAbilityTitle_THEN_MoveToSingleResultScene()
            {
                TextButton textButton = GameObject.Find("ObjectRecognitionTitle").GetComponent<TextButton>();
                textButton.onClick.Invoke();

                yield return null;
                Assert.AreEqual(SceneName.OBJECT_RECOGNITION_RESULT_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing the start function to make sure it sets the appropriate fields
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TextButtonStartsWithoutUnderline()
            {
                // Object recognition TextButton script object
                TextButton textButton = GameObject.Find("ObjectRecognitionTitle").GetComponent<TextButton>();
                // Object recognition scene text object
                TextMeshProUGUI TextComponent = GameObject.Find("ObjectRecognitionTitle").GetComponent<TextMeshProUGUI>();

                yield return null;
                Assert.AreEqual(TextComponent.text, textButton.normalTextComponent);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing OnPointerClick function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_OnPointerClick_Method()
            {
                TextButton textButton = GameObject.Find("PointingTitle").GetComponent<TextButton>();

                // Trigger PointerClick event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(textButton.gameObject, pointer, ExecuteEvents.pointerClickHandler);

                yield return null;
                // Expect a scene switch
                Assert.AreEqual(SceneName.POINTING_RESULT_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing OnPointerDown function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_OnPointerDown_Method()
            {
                TextButton textButton = GameObject.Find("PointingTitle").GetComponent<TextButton>();

                // Trigger PointerDown event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(textButton.gameObject, pointer, ExecuteEvents.pointerDownHandler);

                yield return null;

                // Expect underline will be added to text
                string textComponent = textButton.GetComponent<TextMeshProUGUI>().text;

                yield return null;
                // First three characters are the front tag
                Assert.AreEqual("<u>", textComponent.Substring(0, 3));
                // Last four characters are the end tag
                Assert.AreEqual("</u>", textComponent.Substring(textComponent.Length - 4));
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing OnPointerUp function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_OnPointerUp_Method()
            {
                TextButton textButton = GameObject.Find("PointingTitle").GetComponent<TextButton>();

                // Trigger PointerUp event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(textButton.gameObject, pointer, ExecuteEvents.pointerUpHandler);

                yield return null;

                // Expect no underline of text
                string textComponent = textButton.GetComponent<TextMeshProUGUI>().text;

                yield return null;
                Assert.AreEqual("Pointing", textComponent);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing OnPointerEnter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_OnPointerEnter_Method()
            {
                TextButton textButton = GameObject.Find("InhibitionTitle").GetComponent<TextButton>();

                // Trigger PointerEnter event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(textButton.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

                yield return null;

                // Expect underline will be added to text
                string textComponent = textButton.GetComponent<TextMeshProUGUI>().text;

                yield return null;
                // First three characters are the front tag
                Assert.AreEqual("<u>", textComponent.Substring(0, 3));
                // Last four characters are the end tag
                Assert.AreEqual("</u>", textComponent.Substring(textComponent.Length - 4));
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing OnPointerExit function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TEST_OnPointerExit_Method()
            {
                TextButton textButton = GameObject.Find("InhibitionTitle").GetComponent<TextButton>();

                // Trigger PointerUp event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(textButton.gameObject, pointer, ExecuteEvents.pointerExitHandler);

                yield return null;

                // Expect no underline of text
                string textComponent = textButton.GetComponent<TextMeshProUGUI>().text;

                yield return null;
                Assert.AreEqual("Inhibition", textComponent);
            }
        }
    }
}