using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UI;
using Games;
using Helper;

namespace PlayModeTests
{
    namespace SquaresTests
    {
        public class SquareTests
        {
            // List of the square objects on screen
            private List<GameObject> displayedSquares;

            // Individual square object
            private GameObject square;

            // Colour to be changed to
            private Color HIGHLIGHTED_COLOR = new Color32(0xFF, 0xFF, 0, 0xFF);

            // SpriteRenderer of square object
            private SpriteRenderer squareSpriteRenderer;
            private Image squareImageComponent;

            // Square.cs script attached to a square gameobject containing colour change function
            private Square squareScript;

            // Squares.cs script attached to SquareGridArea, that contains the list of square objects
            private Squares squaresScript;

            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.SQUARES_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                // Gets the instance of the Squares.cs script attached to SquareGridArea
                squaresScript = GameObject.Find("SquareGridArea").GetComponent<Squares>();

                // Gets the list of displayed squares shown on the screen
                displayedSquares = squaresScript.displayedSquares;

                // Gets a single square out of the displayed squares list
                square = displayedSquares[0];
                squareScript = square.GetComponent<Square>();

                squareImageComponent = square.GetComponent<Image>();

                // Remove event so this method is not re-evaluated
                // if a non-Questionnaire scene is loaded
                SceneManager.sceneLoaded -= SetGameObjects;
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test </li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test if the ChangeColor function is called
                then the square changed its colour.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ChangeColorFunctionCalled_THEN_SquareChangedColor()
            {
                squareScript.ChangeColor(HIGHLIGHTED_COLOR);

                yield return null;

                Assert.AreEqual(squareImageComponent.color,
                HIGHLIGHTED_COLOR);
            }
        }
    }
}
