using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UI;
using Storage;
using TMPro;

namespace PlayModeTests
{
    namespace UITests
    {
        public class QuestionnaireSceneTests
        {
            private const float SMALL_AMOUNT_OF_TIME = 0.001F;
            private TMP_InputField nameInputField;
            private TMP_InputField ageInputField;
            private Button submitButton;
            private Toggle keyboardToggleYes;
            private Toggle keyboardToggleNo;
            private Toggle mouseToggleYes;
            private Toggle mouseToggleNo;
            private Text enterRequiredFieldsText;

            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.QUESTIONNAIRE_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                nameInputField = GameObject.Find("NameInputField").GetComponent<TMP_InputField>();
                ageInputField = GameObject.Find("AgeInputField").GetComponent<TMP_InputField>();
                submitButton = GameObject.Find("Button").GetComponent<Button>();
                keyboardToggleYes = GameObject.Find("KeyboardToggleYes").GetComponent<Toggle>();
                keyboardToggleNo = GameObject.Find("KeyboardToggleNo").GetComponent<Toggle>();
                mouseToggleYes = GameObject.Find("MouseToggleYes").GetComponent<Toggle>();
                mouseToggleNo = GameObject.Find("MouseToggleNo").GetComponent<Toggle>();

                // Since the text is not active by default, we have to go through
                // the Canvas to find it.
                enterRequiredFieldsText = GameObject.Find("Canvas").transform.Find("EnterRequiredFieldsText").GetComponent<Text>();

                // Remove event so this method is not re-evaluated
                // if a non-Questionnaire scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Checks that the user
                cannot submit the questionnaire without entering an age. 
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_NoAgeProvided_THEN_QuestionnaireNotSubmitted()
            {
                SimulateKeyboardInputFieldEntry(nameInputField, "Alan Turing");
                SimulateKeyboardInputFieldEntry(ageInputField, "");
                submitButton.onClick.Invoke();

                yield return null;

                Assert.AreEqual(SceneName.QUESTIONNAIRE_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Checks that if the user
                does not enter an age and clicks the submit button,
                text indicating that they need to fill in required fields is shown. 
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_NoAgeProvided_THEN_EnterRequiredFieldsTextShown()
            {
                Assert.False(enterRequiredFieldsText.IsActive());

                SimulateKeyboardInputFieldEntry(nameInputField, "Alan Turing");
                SimulateKeyboardInputFieldEntry(ageInputField, "");
                submitButton.onClick.Invoke();

                yield return null;

                Assert.True(enterRequiredFieldsText.IsActive());
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Checks that leading zeros in an
                age are ignored.
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_AgeWithPrefixedZerosEntered_THEN_PrefixedZerosIgnored()
            {
                Assert.False(enterRequiredFieldsText.IsActive());

                SimulateKeyboardInputFieldEntry(nameInputField, "Alan Turing");
                SimulateKeyboardInputFieldEntry(ageInputField, "0023");
                submitButton.onClick.Invoke();

                yield return null;

                Assert.AreEqual("23", ageInputField.text);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Checks that text is not allowed to
                be entered as an age.
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_TextEnteredAsAge_THEN_AgeIgnored()
            {
                Assert.False(enterRequiredFieldsText.IsActive());

                SimulateKeyboardInputFieldEntry(nameInputField, "Alan Turing");
                SimulateKeyboardInputFieldEntry(ageInputField, "Alan Turing");
                submitButton.onClick.Invoke();

                yield return null;

                Assert.True(string.IsNullOrEmpty(ageInputField.text));
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Checks that only ages of one or
                two digits can be submitted.
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_AgeLongerThan2DigitsEntered_THEN_AllButFirst2DigitsIgnored()
            {
                Assert.False(enterRequiredFieldsText.IsActive());

                SimulateKeyboardInputFieldEntry(nameInputField, "Alan Turing");
                SimulateKeyboardInputFieldEntry(ageInputField, "1998");
                submitButton.onClick.Invoke();

                yield return null;

                Assert.AreEqual("19", ageInputField.text);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Checks that - in name is allowed
                but not in age (age cannot be negative).
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator NegativeAgeNotAllowedButHypenInNameAllowed()
            {
                SimulateKeyboardInputFieldEntry(nameInputField, "Ann-Marie");
                SimulateKeyboardInputFieldEntry(ageInputField, "-99");
                submitButton.onClick.Invoke();

                yield return null;

                Assert.AreEqual("Ann-Marie", nameInputField.text);
                Assert.AreEqual("99", ageInputField.text);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Checks that the questionnaire is
                successfully submitted if correct information is entered.
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_CorrectNameAndAgeProvided_THEN_QuestionnaireSubmitted()
            {
                SimulateKeyboardInputFieldEntry(nameInputField, "Alan Turing");
                SimulateKeyboardInputFieldEntry(ageInputField, "99");
                submitButton.onClick.Invoke();

                yield return null;

                Assert.AreNotEqual(SceneName.QUESTIONNAIRE_SCENE,
                    SceneManager.GetActiveScene().name);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Checks that the information
                from the questionnaire was successfully recorded when entered correctly.
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_QuestionnaireSubmitted_THEN_PlayerInformationCorrectlyRecorded()
            {
                SimulateKeyboardInputFieldEntry(nameInputField, "Alan Turing");
                SimulateKeyboardInputFieldEntry(ageInputField, "99");
                keyboardToggleNo.isOn = true;
                mouseToggleYes.isOn = true;
                submitButton.onClick.Invoke();

                yield return null;

                PlayerStorage actualPlayerStorage = QuestionnairePage.GetPlayer();

                PlayerStorage expectedPlayerStorage = new PlayerStorage
                {
                    Name = "Alan Turing",
                    Age = 99,
                    KeyBoard = false,
                    Mouse = true,
                    UserId = actualPlayerStorage.UserId
                };

                Assert.AreEqual(expectedPlayerStorage, actualPlayerStorage);
            }

            /// <summary>
            /// Simulates the user entering text key by key into the input field.
            /// </summary>
            /// <param name="inputField">The input field text is being entered into.</param>
            /// <param name="text">The text being entered.</param>
            private void SimulateKeyboardInputFieldEntry(TMP_InputField inputField, string text)
            {
                foreach (char ch in text.ToCharArray())
                {
                    inputField.ProcessEvent(Event.KeyboardEvent(ch.ToString()));
                }
            }
        }
    }
}