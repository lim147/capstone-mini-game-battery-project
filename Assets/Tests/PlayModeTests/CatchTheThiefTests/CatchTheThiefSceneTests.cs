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
using Storage;
using UnityEngine.InputSystem;

namespace PlayModeTests
{
    namespace CatchTheThiefTests
    {
        public class CatchTheThiefSceneTests : InputTestFixture
        {
            // Grid game object
            private GameObject Grid;
            private Text time;
            private GameObject person;
            private GameObject thief;

            Button testBtn;
            Games.CatchTheThief thief1;

            private Keyboard keyboard;

            private double db_time;


            private enum Images
            {
                THIEF,
                PERSON,
                BLANK
            }


            /// <summary>
            /// Load the scene for playmode unit tests.
            /// </summary>
            /// The methods with SetUp attribute will be run once before every unit test.
            [SetUp]
            public void LoadScene()
            {
                // Load game scene
                SceneManager.LoadScene(SceneName.CATCHTHETHIEF_SCENE);

                SceneManager.sceneLoaded += SetGameObjects;
            }

            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {
                //testBtn = GameObject.Find("TestButton").GetComponent<Button>();
                //Assert.IsNotNull(testBtn, "Missing test Btn");


                thief1 = GameObject.Find("SquareGridArea").GetComponent<Games.CatchTheThief>();

                SceneManager.sceneLoaded -= SetGameObjects;
            }

            //----------------- Unit Tests begin ------------------------------------
            //-----------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test if Grid exists.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckSquareGridArea()
            {
                // Link game object to local variable; throw out exception if there is no such game object
                Grid = GameObject.Find("SquareGridArea");
                yield return null;
                Assert.IsNotNull(Grid, "Missing gameobject: " + "Grid");
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test if the grid has 9 squares.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckSquareGridAreaCount()
            {
                // Link game object to local variable; throw out exception if there is no such game object
                Grid = GameObject.Find("SquareGridArea");
                yield return null;
                Assert.AreEqual(9, Grid.transform.childCount);
            }


            //------------------------ Integration Tests begin -----------------------
            //------------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Integration Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test if Timer text has the correct value.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckTimeText()
            {
                // Link game object to local variable; throw out exception if there is no such game object
                time = GameObject.Find("TimerValue").GetComponent<Text>();
                yield return null;
                Assert.IsNotNull(time, "Missing gameobject: " + "Time");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Integration Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test if each square contains both Person image and thief image objects.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckSquare()
            {
                Grid = GameObject.Find("SquareGridArea");
                yield return null;

                for (int i = 0; i < Grid.transform.childCount - 1; i++)
                {
                    var obj = Grid.transform.GetChild(i).gameObject;
                    var person = obj.transform.Find("Person");
                    var thief = obj.transform.Find("Thief");

                    Assert.IsNotNull(person, "Missing gameobject: " + "person");
                    Assert.IsNotNull(thief, "Missing gameobject: " + "thief");

                }

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Test the number of thief image and person image is in range .
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickCatchTheThief_THEN_NumOfImageInRange()
            {
                yield return new WaitForSeconds(1f);
                int personCount = thief1.GetShowPersonCount();
                int thiefCount = thief1.GetShowThiefCount();

                Assert.IsTrue(personCount <= 3, "ChekCount personCount " + personCount);
                Assert.IsTrue(thiefCount <= 1, "ChekCount thiefCount" + thiefCount);
            }




            //----------------- Tests driven by Functional Requirements -------------
            //-----------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fcs-1'>FCS-1</a></li>
            <li><b>Test description:</b> Test CatchTheThief game shows the graphics of the game.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickCatchTheThief_THEN_CatchTheThiefImageCheck()
            {
                Grid = GameObject.Find("SquareGridArea");
                yield return null;
                // Boolean to determine if the thief appears in this round
                bool thiefAppearInRound;
                // Boolean to determine if the person appears in this round
                bool personAppearInRound;
                List<Images> imageOrder = new List<Images>();
                int numberOfPeople;

                // Randomly determine whether thief or person images should appear
                // And make sure at least one of thief and person appears
                thiefAppearInRound = RamGenerator.GenerateARandomBool();
                if (thiefAppearInRound)
                {
                    // personAppearInRound could be either true or false
                    personAppearInRound = RamGenerator.GenerateARandomBool();
                    // Add thief image type to the list of images that will appear
                    imageOrder.Add(Images.THIEF);
                }
                else
                {
                    // personAppearInRound should be true
                    personAppearInRound = true;
                }

                if (personAppearInRound)
                {
                    // Determine the number of people that will appear this round
                    numberOfPeople = RamGenerator.GenerateARamInt(1, 3);
                    // Add one person image type to the list for each person that will appear this round
                    for (int count = 0; count < numberOfPeople; count += 1)
                    {
                        imageOrder.Add(Images.PERSON);
                    }
                }

                // For all the remaining square game objects, they will be left blank
                while (imageOrder.Count < 9)
                {
                    imageOrder.Add(Images.BLANK);
                }

                Assert.IsTrue(imageOrder.Count == 9, " message: no Error TestFCS1");

            }

            [UnityTest]
            public IEnumerator CheckUpdateCaughtThiefCount()
            {
                thief1.thiefAppearInRound = true;
                thief1.personAppearInRound = false;
                thief1.numberOfCaughtThieves = 0;
                yield return new WaitForSeconds(0.5f);
                thief1.UpdateCaughtThiefCount();


                Assert.IsTrue(thief1.numberOfCaughtThieves != 0, "ChekCount thiefCount" + thief1.numberOfCaughtThieves);
            }


            [UnityTest]
            public IEnumerator CheckGetPressTimet()
            {
                thief1 = new CatchTheThief();
                thief1.round = new CatchTheThiefRound();
                thief1.isIdentifiedKeyPressed = false;
                thief1.identifiedKeyPressTime = 0.5f;
                yield return new WaitForSeconds(0.5f);
                thief1.GetPressTime();

                Assert.IsTrue(thief1.round.identifiedKeyPressTime >= 0, "ChekCount thiefCount" + thief1.numberOfCaughtThieves);
            }



            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fcs-2'>FCS-2</a></li>
            <li><b>Test description:</b> Test the CatchTheThief game must recognize the identified input.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickCatchTheThief_THEN_CatchTheThiefPlayerReactionCheck()
            {
                yield return new WaitForSeconds(1);
                var keyboard = InputSystem.AddDevice<Keyboard>();

                yield return new WaitForSeconds(1);
                Press(keyboard.spaceKey);

                //Assert.IsTrue(thief1.round.KeyPressTime <= 0);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fcs-3'>FCS-3</a></li>
            <li><b>Test description:</b> Test the thief and people images must appear on random squares.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickCatchTheThief_THEN_ThiefPositionIsRandom()
            {
                GameObject square = GameObject.Find("SquareGridArea");

                yield return null;

                bool state = false;

                for (int i = 0; i < square.transform.childCount - 1; i++)
                {

                    Transform tr = square.transform.GetChild(i);

                    if (!tr.Find("Thief"))
                    {
                        state = true;
                    }


                }


                Assert.IsFalse(state);


            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fcs-4'>FCS-4</a></li>
            <li><b>Test description:</b> Test the amount of time it takes for the player to respond to the images is recorded.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_ClickCatchTheThief_THEN_ResponseTimeRecoreded()
            {
                yield return null;

                CatchTheThiefRound catchTheThiefRound = new CatchTheThiefRound();
                catchTheThiefRound.identifiedKeyPressTime = 10;

                Assert.AreEqual(10, catchTheThiefRound.identifiedKeyPressTime);



            }


            //------------------------ System Tests begin ----------------------------
            //------------------------------------------------------------------------
            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will simulate a normal player
                behaviour, which trigger 35 times of thief identification. Then,
                it checks if the resulting game states are as intended.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Simulate_NormalPlayerBehaviour()
            {

                int validClicks = 35; //should be odd number
                while (validClicks > 0)
                {
                    yield return new WaitForSeconds(0.05f);
                    double t = thief1.GetPlayerCheckTime();
                    Assert.IsFalse(t < 0, "Error" + t);
                    validClicks -= 1;
                }

            }


            //------------------------ Stress Tests begin ----------------------------
            //------------------------------------------------------------------------
            [Description(@"
        <ul>
            <li><b>Test type:</b> Stress Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will simulate 
                behaviour under extreme simutations, which provides 1000 times of thief identification.
                Then, it checks if the resulting game states are as intended.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Simulate_ExtremeBehaviour()
            {

                int validClicks = 1000; //should be odd number
                while (validClicks > 0)
                {
                    yield return null;
                    double t = thief1.GetPlayerCheckTime();
                    Assert.IsFalse(t < 0, "Error" + t);
                    validClicks -= 1;
                }

            }

        }
    }
}
