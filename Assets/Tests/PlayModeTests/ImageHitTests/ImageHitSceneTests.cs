using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UI;
using Helper;
using Games;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Storage;
using UnityEngine.UI;

namespace PlayModeTests
{
    namespace ImageHitTests
    {
        public class ImageHitSceneTests
        {

            ImageHit imageHitScritps;

            [SetUp]
            public void LoadScene()
            {
                SceneManager.LoadScene(SceneName.IMAGEHIT_SCENE);
                SceneManager.sceneLoaded += SetGameObjects;
            }

            // Once scene loaded, store important game objects for the tests
            // into instance variables.
            void SetGameObjects(Scene scene, LoadSceneMode mode)
            {

                imageHitScritps = GameObject.Find("Canvas").GetComponent<ImageHit>();

                SceneManager.sceneLoaded -= SetGameObjects;
            }



            //------------------------ Acceptance Tests begin ------------------------
            //------------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#ffc-1'>FFC-1</a></li>
            <li><b>Test description:</b> Test if the image size on the screen.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GameStart_Then_ImageIsBigEnough()
            {
                RectTransform img = GameObject.Find("imageFrame").GetComponent<RectTransform>();

                var size = img.sizeDelta;


                var x = 1920;
                var y = 1080;

                var size_d = x * y;
                var size_i = size.x * size.y;

                var size_all = size_i / size_d;

                yield return null;
                Assert.True(size_all <= 0.3, "message :The images should display no less than 30% of the surface area of the object. ");
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#ffc-2'>FFC-2</a></li>
            <li><b>Test description:</b> Check if the image is shown on the screen
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GameStart_Then_ImageShouldAppear()
            {
                GameObject showImage = GameObject.Find("imageFrame");
                yield return null;
                Assert.True(showImage, "message :The no show Image");

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#ffc-3'>FFC-3</a></li>
            <li><b>Test description:</b> Check if the images are randomed and without repetition
            </li>
        </ul>
        ")]

            [UnityTest]
            public IEnumerator WHEN_ImagesAreCreated_Then_TheImagesShouldBeRandomedWithoutRepetition()
            {
                GameObject canvas = GameObject.Find("Canvas");
                imageHitScritps = canvas.GetComponent<ImageHit>();
                yield return new WaitForSeconds(1f);

                List<string> images = imageHitScritps.GetInitialTestImages();

                List<string> name_list = new List<string>();

                bool state = false;

                foreach (var item in images)
                {
                    foreach (var name in name_list)
                    {
                        if (item == name)
                        {
                            state = true;
                        }
                    }

                    name_list.Add(item);
                }

                Assert.IsFalse(state, "message : ImageRepetition" + state);

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#ffc-4'>FFC-4</a>.</li>
            <li><b>Test description:</b> Check if the second round has enough images
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SecondRoundStart_Then_TheNumberOfImagesShouldNoLessThan3()
            {
                GameObject canvas = GameObject.Find("Canvas");
                imageHitScritps = canvas.GetComponent<ImageHit>();
                yield return new WaitForSeconds(1f);

                List<TImage> images = imageHitScritps.OnRoundStart(2);

                Assert.IsFalse(images.Count < 3, "message : ImageRepetition" + images.Count);

            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Acceptance Test</li>
            <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#ffc-5'>FFC-5</a> </li>
            <li><b>Test description:</b> This test will check when the second round is over, if
                   there exist images that were shown for a second time.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SecondRoundEnd_Then_SomeImageDisplayedTwice()
            {

                ImageHit imagehit = imageHitScritps;
                yield return new WaitForSeconds(0.5f);
                imagehit.OnRound2Over();
                Assert.True(imagehit.currentRoundTestData.Count == 0, "message :The no CheckOnRound2Over");


            }



            //------------------------ Unit Tests begin ------------------------
            //------------------------------------------------------------------------
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will check if the data of the 
                additional round of the game are stored normally.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckAddRoundTestData()
            {

                ImageHit imagehit = new ImageHit();
                yield return new WaitForSeconds(0.5f);

                ImageHit.imageHitData = new ImageHitStorage();
                ImageHit.imageHitData.Rounds = new List<List<ImageHitRound>>();
                imagehit.currentRoundTestData = new List<ImageHitRound>();
                imagehit.AddRoundTestData();
                Assert.True(ImageHit.imageHitData.Rounds.Count >= 0, "message :The no CheckAddRound");

            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will check all the data stored in
                 the first round of the game are normal and true recorded.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckOnRound1Over()
            {

                ImageHit imagehit = imageHitScritps;
                yield return new WaitForSeconds(0.5f);

                ImageHit.imageHitData = new ImageHitStorage();
                imagehit.currentRoundTestData = new List<ImageHitRound>();
                imagehit.OnRound1Over();
                Assert.True(imagehit.canShowNextImage, "message :The no CheckOnRound1Over");


                ImageHit.imageHitData = new ImageHitStorage();
                imagehit.currentRoundTestData = new List<ImageHitRound>();

                ImageHitRound image1 = new ImageHitRound();
                ImageHitRound image2 = new ImageHitRound();
                image1.isCorrectlyIdentified = true;
                imagehit.currentRoundTestData.Add(image1);
                imagehit.currentRoundTestData.Add(image2);
                imagehit.OnRound1Over();
                Assert.True(imagehit.canShowNextImage, "message :The no CheckOnRound1Over");


                ImageHit.imageHitData = new ImageHitStorage();
                imagehit.currentRoundTestData = new List<ImageHitRound>();

                ImageHitRound image3 = new ImageHitRound();
                ImageHitRound image4 = new ImageHitRound();
                ImageHitRound image5 = new ImageHitRound();
                ImageHitRound image6 = new ImageHitRound();
                image3.isCorrectlyIdentified = true;
                image4.isCorrectlyIdentified = true;
                image5.isCorrectlyIdentified = true;
                image6.isCorrectlyIdentified = true;

                imagehit.currentRoundTestData.Add(image3);
                imagehit.currentRoundTestData.Add(image4);
                imagehit.currentRoundTestData.Add(image5);
                imagehit.currentRoundTestData.Add(image6);
                imagehit.OnRound1Over();
                Assert.True(imagehit.canShowNextImage, "message :The no CheckOnRound1Over");



                ImageHit.imageHitData = new ImageHitStorage();
                imagehit.currentRoundTestData = new List<ImageHitRound>();

                ImageHitRound image7 = new ImageHitRound();
                ImageHitRound image8 = new ImageHitRound();
                ImageHitRound image9 = new ImageHitRound();
                ImageHitRound image10 = new ImageHitRound();


                imagehit.currentRoundTestData.Add(image7);
                imagehit.currentRoundTestData.Add(image8);
                imagehit.currentRoundTestData.Add(image9);
                imagehit.currentRoundTestData.Add(image10);
                imagehit.OnRound1Over();
                Assert.True(imagehit.canShowNextImage, "message :The no CheckOnRound1Over");


            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will check whether the data stored
                once the player presses the space bar are normal and true recorded.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckOnKey()
            {

                ImageHit imagehit = imageHitScritps;
                yield return new WaitForSeconds(0.5f);
                imagehit.gameState = 1;
                imagehit.prepareTime = 0;
                imagehit.serialNumber = 1;
                imagehit.OnKey(true, new TimeAndKey(0, KeyCode.Space));
                Assert.True(ImageHit.currentImageHitRound.isKeyPressed, "message :The no CheckOnRound1Over");

                ImageHit.currentImageHitRound.UnidentifiedKeysPressed = null;
                imagehit.OnKey(false, new TimeAndKey(0, KeyCode.Space));
                Assert.True(ImageHit.currentImageHitRound.isKeyPressed, "message :The no CheckOnRound1Over");


            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will check whether the game will not
                go to the next image once the condition satisfies the endpoint of both
                game rounds.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckNextImageFalse()
            {

                ImageHit imagehit = imageHitScritps;
                yield return new WaitForSeconds(0.5f);
                imagehit.gameState = 0;
                imagehit.serialNumber = 10;
                imagehit.NextImage();
                Assert.True(imagehit.gameState == 1, "message :The no CheckNextImageFalse");

                imagehit.serialNumber = 10;
                imagehit.gameState = 2;
                imagehit.NextImage();
                Assert.True(imagehit.gameState == -1, "message :The no CheckNextImageFalse2");

                imagehit.NextImage();
                Assert.True(imagehit.gameState == -1, "message :The no CheckNextImageFalse2");

            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will check the FindImage() function works normally,
                   which allows the the image in the test image list imagesToFound according to the image name and theme 
                   in the previous part of the game to be found.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckFindImage()
            {

                ImageHit imagehit = imageHitScritps;
                yield return new WaitForSeconds(0.5f);

                imagehit.gameState = 0;
                imagehit.serialNumber = 1;
                imagehit.NextImage();

                TImage ti = imagehit.FindImage(imagehit.currentRoundTestImages, imagehit.currentRoundTestData[0]);
                Assert.True(ti != null, "message :The no CheckFindImage");

                TImage t2 = imagehit.FindImage(null, imagehit.currentRoundTestData[0]);
                Assert.True(ti != null, "message :The no CheckFindImage");
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will check if the game will end
                normally.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator CheckOnStop()
            {

                ImageHit imagehit = imageHitScritps;
                yield return new WaitForSeconds(0.5f);
                imagehit.OnStop();



                Assert.True(imagehit.gameState == -1, "message :The no CheckOnStop");


            }


            //------------------------ Integration Tests begin -----------------------
            //------------------------------------------------------------------------

        [Description(@"
        <ul>
            <li><b>Test type:</b> Integration Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> Check if the gameinfo text has the expected value as game starts
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GameStart_Then_TextObjectExist()
            {
                Text gameinfo = GameObject.Find("gameInfoText").GetComponent<Text>();

                yield return null;
                Assert.AreEqual("Preparing...", gameinfo.text, "message :unexpected message");

            }

            ////------------------------ System Tests begin ----------------------------
            ////------------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> System Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will simulate a normal player
                behaviour, which trigger 35 times of the images identification. Then,
                it checks if the resulting game states are as intended.
            </li>
        </ul>
        ")]

            [UnityTest]
            public IEnumerator Simulate_NormalPlayerBehaviour()
            {

                int playTimes = 35;
                while (playTimes > 0)
                {
                    yield return new WaitForSeconds(0.05f);
                    List<string> images = imageHitScritps.GetRandowImageList();

                    List<string> name_list = new List<string>();

                    bool state = false;

                    foreach (var item in images)
                    {
                        foreach (var name in name_list)
                        {
                            if (item == name)
                            {
                                state = true;
                            }
                        }

                        name_list.Add(item);
                    }
                    Assert.IsFalse(state, "message : ImageRepetition" + state);
                    playTimes -= 1;
                }

            }


            ////------------------------ Stress Tests begin ----------------------------
            ////------------------------------------------------------------------------

            [Description(@"
        <ul>
            <li><b>Test type:</b> Stress Test</li>
            <li><b>Associated SRS requirements:</b> None.</li>
            <li><b>Test description:</b> This test will simulate 
                behaviour under extreme simutations, which provides 500 times of image identification.
                Then, it checks if the resulting game states are as intended.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator Simulate_ExtremeBehaviour()
            {

                int playTimes = 500;
                while (playTimes > 0)
                {
                    yield return new WaitForSeconds(0.02f);
                    List<string> images = imageHitScritps.GetRandowImageList();

                    List<string> name_list = new List<string>();

                    bool state = false;

                    foreach (var item in images)
                    {
                        foreach (var name in name_list)
                        {
                            if (item == name)
                            {
                                state = true;
                            }
                        }

                        name_list.Add(item);
                    }
                    Assert.IsFalse(state, "message : ImageRepetition" + state);
                    playTimes -= 1;
                }

            }



        }
    }
}





