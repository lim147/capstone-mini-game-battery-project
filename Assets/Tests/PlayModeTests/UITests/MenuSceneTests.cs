using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PlayModeTests
{
    namespace UITests
    {
        public class MenuSceneTests
        {
            // Game buttons:
            private Button CTFButton;
            private Button BalloonsButton;
            private Button SquaresButton;
            private Button ImageHitButton;
            private Button CatchTheBallButton;
            private Button SaveOneBallButton;
            private Button JudgeTheBallButton;
            private GameObject tooltip;

            // Finish button:
            private Button FinishButton;

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
                CTFButton = GameObject.Find("CatchTheThief").GetComponent<Button>();
                BalloonsButton = GameObject.Find("Balloons").GetComponent<Button>();
                SquaresButton = GameObject.Find("Squares").GetComponent<Button>();
                ImageHitButton = GameObject.Find("ImageHit").GetComponent<Button>();
                CatchTheBallButton = GameObject.Find("CatchTheBall").GetComponent<Button>();
                SaveOneBallButton = GameObject.Find("SaveOneBall").GetComponent<Button>();
                JudgeTheBallButton = GameObject.Find("JudgeTheBall").GetComponent<Button>();

                tooltip = GameObject.Find("Tooltip");
                Assert.IsNotNull(tooltip, "Missing tooltip.");

                // Remove event so this method is not re-evaluated
                // if a non-Questionnaire scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            //----------------- Tests driven by Functional Requirements -------------
            //-----------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-1'>FG-1</a></li>
            <li><b>Test description:</b> Tests that all five game buttons exist
                on the menu page, where players can freely choose the games they
                want to play.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestAllGameButtonsExist()
            {
                yield return null;
                Assert.IsNotNull(CTFButton);
                Assert.IsNotNull(BalloonsButton);
                Assert.IsNotNull(SquaresButton);
                Assert.IsNotNull(ImageHitButton);
                Assert.IsNotNull(CatchTheBallButton);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4'>FG-4</a></li>
            <li><b>Test description:</b> Test that the button to trigger Catch The Thief game behaves properly.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestCTFButton()
            {
                // Simulate a click on the button
                CTFButton.onClick.Invoke();
                yield return null;

                // After the click:
                // Jump to Catch The Thief Instructions Scene
                Assert.AreEqual(SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4'>FG-4</a></li>
            <li><b>Test description:</b> Test that the button to trigger Balloons game behaves properly.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBalloonsButton()
            {
                // Simulate a click on the button
                BalloonsButton.onClick.Invoke();
                yield return null;

                // After the click:
                // Jump to Balloons Instructions Scene
                Assert.AreEqual(SceneName.BALLOONS_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4'>FG-4</a></li>
            <li><b>Test description:</b> Test that the button to trigger Squares game behaves properly.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestSquaresButton()
            {
                // Simulate a click on the button
                SquaresButton.onClick.Invoke();
                yield return null;

                // After the click:
                // Jump to Squares Instructions Scene
                Assert.AreEqual(SceneName.SQUARES_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4'>FG-4</a></li>
            <li><b>Test description:</b> Test that the button to trigger Image Hit game behaves properly.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestImageHitButton()
            {
                // Simulate a click on the button
                ImageHitButton.onClick.Invoke();
                yield return null;

                // After the click:
                // Jump to Image Hit Instructions Scene
                Assert.AreEqual(SceneName.IMAGEHIT_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-4'>FG-4</a></li>
            <li><b>Test description:</b> Test that the button to trigger Ball game behaves properly.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator TestBallButton()
            {
                // Simulate a click on the button
                CatchTheBallButton.onClick.Invoke();
                yield return null;

                // After the click:
                // Jump to Image Hit Instructions Scene
                Assert.AreEqual(SceneName.CATCH_THE_BALL_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests JumpToCTFGame function.
            </li>
        </ul>
        ")]
            // Test game jumper functions:
            [UnityTest]
            public IEnumerator WHEN_JumpToCTFGameFunctionCalled_THEN_MoveToCTFInstructionsScene()
            {
                MenuPage menuPage = new MenuPage();
                menuPage.JumpToCTFGame();

                yield return null;
                Assert.AreEqual(SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests JumpToBalloonsGame function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_JumpToBalloonsGameFunctionCalled_THEN_MoveToBalloonsInstructionsScene()
            {
                MenuPage menuPage = new MenuPage();
                menuPage.JumpToBalloonsGame();

                yield return null;
                Assert.AreEqual(SceneName.BALLOONS_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests JumpToSquaresGame function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_JumpToSquaresGameFunctionCalled_THEN_MoveToSquaresInstructionsScene()
            {
                MenuPage menuPage = new MenuPage();
                menuPage.JumpToSquaresGame();

                yield return null;
                Assert.AreEqual(SceneName.SQUARES_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests JumpToImageHitGame function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_JumpToImageHitGameFunctionCalled_THEN_MoveToImageHitInstructionsScene()
            {
                MenuPage menuPage = new MenuPage();
                menuPage.JumpToImageHitGame();

                yield return null;
                Assert.AreEqual(SceneName.IMAGEHIT_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests JumpToCatchTheBallGame function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_JumpToCatchTheBallGameFunctionCalled_THEN_MoveToCatchTheBallInstructionsScene()
            {
                MenuPage menuPage = new MenuPage();
                menuPage.JumpToCatchTheBallGame();

                yield return null;
                Assert.AreEqual(SceneName.CATCH_THE_BALL_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests JumpToSaveOneBallGame function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_JumpToSaveOneBallGameFunctionCalled_THEN_MoveToSaveOneBallInstructionsScene()
            {
                MenuPage menuPage = new MenuPage();
                menuPage.JumpToSaveOneBallGame();

                yield return null;
                Assert.AreEqual(SceneName.SAVE_ONE_BALL_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests JumpToJudgeTheBallGame function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_JumpToJudgeTheBallGameFunctionCalled_THEN_MoveToJudgeTheBallInstructionsScene()
            {
                MenuPage menuPage = new MenuPage();
                menuPage.JumpToJudgeTheBallGame();

                yield return null;
                Assert.AreEqual(SceneName.JUDGE_THE_BALL_INSTRUCTIONS_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests JumpToResult function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_JumpToResultFunctionCalled_THEN_MoveToResultScene()
            {
                BalloonsButton.onClick.Invoke();
                MenuPage menuPage = new MenuPage();
                menuPage.JumpToResult();

                yield return null;
                Assert.AreEqual(SceneName.RESULT_SCENE, SceneManager.GetActiveScene().name);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests ShowTooltipCatchTheThief function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ShowTooltipCatchTheThiefFunctionCalled_THEN_TooltipDisplayed()
            {
                // Make Catch The Thief button not selectable
                CTFButton.interactable = false;

                // Trigger PointerEnter event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(CTFButton.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

                yield return null;
                Assert.IsTrue(tooltip.activeSelf);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests ShowTooltipBalloons function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ShowTooltipBalloonsFunctionCalled_THEN_TooltipDisplayed()
            {
                // Make Balloons button not selectable
                BalloonsButton.interactable = false;

                // Trigger PointerEnter event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(BalloonsButton.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

                yield return null;
                Assert.IsTrue(tooltip.activeSelf);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests ShowTooltipSquares function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ShowTooltipSquaresFunctionCalled_THEN_TooltipDisplayed()
            {
                // Make Squares button not selectable
                SquaresButton.interactable = false;

                // Trigger PointerEnter event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(SquaresButton.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

                yield return null;
                Assert.IsTrue(tooltip.activeSelf);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests ShowTooltipImageHit function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ShowTooltipImageHitFunctionCalled_THEN_TooltipDisplayed()
            {
                // Make ImageHit button not selectable
                ImageHitButton.interactable = false;

                // Trigger PointerEnter event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(ImageHitButton.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

                yield return null;
                Assert.IsTrue(tooltip.activeSelf);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests ShowTooltipCatchTheBall function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ShowTooltipCatchTheBallFunctionCalled_THEN_TooltipDisplayed()
            {
                // Make CatchTheBall button not selectable
                CatchTheBallButton.interactable = false;

                // Trigger PointerEnter event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(CatchTheBallButton.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

                yield return null;
                Assert.IsTrue(tooltip.activeSelf);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests ShowTooltipSaveOneBall function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ShowTooltipSaveOneBallFunctionCalled_THEN_TooltipDisplayed()
            {
                // Make SaveOneBall button not selectable
                SaveOneBallButton.interactable = false;

                // Trigger PointerEnter event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(SaveOneBallButton.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

                yield return null;
                Assert.IsTrue(tooltip.activeSelf);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests ShowTooltipJudgeTheBall function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ShowTooltipJudgeTheBallFunctionCalled_THEN_TooltipDisplayed()
            {
                // Make JudgeTheBall button not selectable
                JudgeTheBallButton.interactable = false;

                // Trigger PointerEnter event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(JudgeTheBallButton.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

                yield return null;
                Assert.IsTrue(tooltip.activeSelf);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Tests that when the cursor leaves the game button, the tooltip is not
                   displayed.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_CursorLeaveGameButton_THEN_TooltipNotDisplayed()
            {
                // Make Catch The Thief button not selectable
                CTFButton.interactable = false;

                // Trigger PointerEnter event
                var pointer = new PointerEventData(EventSystem.current);
                ExecuteEvents.Execute(CTFButton.gameObject, pointer, ExecuteEvents.pointerExitHandler);

                yield return null;
                Assert.IsFalse(tooltip.activeSelf);

            }
        }
    }
}