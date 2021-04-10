using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Storage;
using Helper;



namespace Games
{
    /// <summary>
    /// This module implements [ImageHit Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#imagehit-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// 
    /// This module is described in the [SRS](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#1-object-recognition-inhibition-imagehit).
    ///
    /// <div class="WARNING">
    ///     <h5>ASSUMPTION</h5>
    ///     <p>
    ///     <ul>
    ///         <li>Images used in this game are downloaded from unsplash.com
    ///         </li>
    ///     </ul>
    ///     </p>
    /// </div>
    /// </summary>
    public class ImageHit : MonoBehaviour
    {
        // The upper and lower bounds for the number of images of the tested theme 
        private int MIN_NumOfImagesOfTheme = 4;
        private int MAX_NumOfImagesOfTheme = 6;


        // The start and end time of pressing SPACE by the player for one image
        private float keyPressStartTime = 0;
        private float keyPressEndTime = 0;



        // The list of image themes
        public CTheme[] themes;

        // The instance of this class
        public static ImageHit imageHitInstance;

        // The image displayed
        public Image imageDisplay;

        // The current theme id of this round of Image Hit game
        public int currentThemeId;

        // The current theme of this round of Image Hit game
        public string specifiedTheme;

        // The images of current round
        public List<TImage> initialTestImages;

        // The list of images used in this round of game
        public List<TImage> currentRoundTestImages;

        // The current id of shown image
        public int serialNumber;

        // The game state where -1 is unstart;
        // 0 is round 0;
        // 1 is round 1;
        // 2 is game over
        public int gameState;

        // Time that image is shown for
        public float imageDisplayDuration;

        // Amount of time that image has been shown for, so far
        public float imageDisplayedTime;

        // The preparation time before each part of this game starts
        public float prepareTime;

        // To show if next image can be shown
        public bool canShowNextImage;

        // The gameinfo at top middle
        public Text gameInfo;
        // The timer at top right
        public Text timer;

        // Key that is identified by Image Hit game
        private KeyCode imageHitIdentifiedKey = KeyCode.Space;

        // Store the data of each image to currentRoundTestData
        public static ImageHitRound currentImageHitRound;

        // Data gathered for the whole Image Hit game, got from currentRoundTestData after the end of game
        public static ImageHitStorage imageHitData;
        private List<TImage> roundImages;

        // Duration of time between when images appear and player presses the ThiefIdentified key
        private float identifiedKeyPressTime;

        /// <summary>
        /// The sound to play when the spacebar is pressed and the image is correctly identified.
        /// </summary>
        public AudioSource Beep;

        /// <summary>
        /// The sound to play when the spacebar is pressed but the image is wrongly identified.
        /// </summary>
        public AudioSource WrongAudio;

        private List<string> images;


        // Data gathered from this round of Image Hit game
        public List<ImageHitRound> currentRoundTestData;
        public delegate List<TImage> dOnRoundStart(int n);
        public dOnRoundStart mRoundStart;
        /// <summary>
        /// This method will be called when the script is loaded. It initializes the image Hit instance as a reference.
        /// </summary>
        private void Awake()
        {
            imageHitInstance = this;
        }

        /// <summary>
        /// Called by the system before calling Update() for the first time to initialize data
        /// </summary>
        void Start()
        {
            currentRoundTestData = new List<ImageHitRound>();
            currentImageHitRound = new ImageHitRound();
            currentImageHitRound.UnidentifiedKeysPressed = new List<TimeAndKey>();
            {
                imageHitData = new ImageHitStorage();
                {
                    imageHitData.Rounds = new List<List<ImageHitRound>>();
                }
            }

            gameState = -1;
            mRoundStart = OnRoundStart;
            StartGame();
        }

        /// <summary>
        /// Update is called once per frame. It updates time related variables,
        /// controls the process of image hit game.
        /// </summary>
        void Update()
        {
            // Image Hit is ongoing
            // There is a preparation time before the game starts
            // After the game starts, each picture stays on the screen for three seconds (if the player does not press the space bar)
            // If the player presses the space bar, the data will be recorded
            if (gameState != -1)
            {
                if (prepareTime > 0)
                {
                    prepareTime -= Time.deltaTime;
                    if ((gameState + 1) % 2 == 0)
                    {
                        gameInfo.text = string.Format("Preparing for an extra round...");
                        timer.text = "";
                    }
                    else
                    {
                        gameInfo.text = string.Format("Preparing...");
                        timer.text = "";
                    }
                    imageDisplay.color = Color.black;
                }
                else // Enter the game after prepareTime is 0
                {
                    imageDisplay.color = Color.white;
                    NextImage();
                    imageDisplayedTime += Time.deltaTime;
                    if (gameState == 0 || gameState == 1)
                    {
                        // Displays text information:
                        // Set the theme color green
                        gameInfo.text = string.Format("Theme: <b><color=#3D9806ff>{0}</color></b>", specifiedTheme);
                        // Set the timer color to red when its value is smaller than 1
                        if (3 - imageDisplayedTime < 1)
                        {
                            timer.text = string.Format("Time: <color=#ff0000ff>{0:0.0}</color>", 3 - imageDisplayedTime);
                        }
                        else
                        {
                            timer.text = string.Format("Time: {0:0.0}", 3 - imageDisplayedTime);
                        }

                        if (imageDisplayedTime >= imageDisplayDuration)
                        {
                            imageDisplayedTime = 0;
                            ImagePass();
                        }

                        // For all other keys that are pressed, add the key to UnidentifiedKeys list
                        foreach (KeyCode keyThatWasPressed in System.Enum.GetValues(typeof(KeyCode)))
                        {
                            if (Input.GetKeyDown(keyThatWasPressed) && Input.GetKeyDown(imageHitIdentifiedKey))
                            {
                                OnKey(true, new TimeAndKey(0, keyThatWasPressed));
                            }
                            else if (Input.GetKeyDown(keyThatWasPressed) && !Input.GetKeyDown(imageHitIdentifiedKey))
                            {
                                OnKey(false, new TimeAndKey(identifiedKeyPressTime, keyThatWasPressed));
                            }
                        }
                    }

                }

            }

        }

        /// <summary>
        /// Add data to the storage file of each round of game
        /// </summary>
        public void AddRoundTestData()
        {
            List<ImageHitRound> round12Data = new List<ImageHitRound>(currentRoundTestData);
            imageHitData.Rounds.Add(round12Data);

        }

        public List<string> GetInitialTestImages()
        {
            return images;
        }

        /// <summary>
        /// The first part of Image Hit is over
        /// If the player has done all the correct operations in the first ten images, they will choose three random images to join the image list and start the second part game.
        /// If the player makes less than 7 errors in the first ten images, the wrong images and three random correct images will be added to the image list to start the second part of the game.
        /// If the player makes 7 or more errors in the first ten images, the ten images will be re-added to the image list to enter the second stage of the game.
        /// </summary>
        public void OnRound1Over()
        {

            List<TImage> correctImages = new List<TImage>();
            List<TImage> incorrectImages = new List<TImage>();
            int correctCount = 0;
            TImage img = null;
            for (int i = 0; i < currentRoundTestData.Count; i++)
            {
                img = FindImage(currentRoundTestImages, currentRoundTestData[i]);  // To know which image it is during the current round
                if (currentRoundTestData[i].isCorrectlyIdentified)
                {
                    correctCount++;
                    correctImages.Add(img);     // To add correct images
                }
                else
                {

                    if (!currentRoundTestData[i].isKeyPressed ||  // If the space bar is not pressed
                        currentRoundTestData[i].isKeyPressed && currentRoundTestData[i].isSpaceKey) // If the pressed bar is not space bar
                    {
                        incorrectImages.Add(img);     // To add wrong images
                    }
                }
            }


            if (correctCount <= 3)
            {
                initialTestImages = currentRoundTestImages;
            }
            else
            {
                for (int i = 0; i < 3; i++)  // To select 3 correct images
                {
                    if (correctImages.Count <= 0)
                    {
                        break;
                    }

                    int id = UnityEngine.Random.Range(0, correctImages.Count);
                    incorrectImages.Add(correctImages[id]);
                    correctImages.RemoveAt(id);
                }
                initialTestImages = incorrectImages;  //The list for the second part of the game
            }

            OnRoundStart(1);
            RandSortImages(initialTestImages);     // Randomize the list
            serialNumber = 0;
            canShowNextImage = true;
        }


        /// <summary>
        /// Randomize the order of image elements in the list
        /// Randomly select 2 images (id1, id2) to exchange positions, the time complexity is n*m, n is the size of the array, and m is the number of exchanges
        /// </summary>
        /// <param name="img"></param>
        private void RandSortImages(List<TImage> img)
        {
            if (img == null)
            {
                return;
            }
            if (img.Count > 0)
            {
                UnityEngine.Random.InitState(DateTime.Now.Millisecond);
                int len = Mathf.Min(4, img.Count);
                for (int i = 0; i < len; i++) // Random exchange
                {
                    int id1 = UnityEngine.Random.Range(0, img.Count);
                    int id2 = i;
                    if (id1 != id2)
                    {
                        TImage tmp = img[id1];
                        img[id1] = img[id2];
                        img[id2] = tmp;

                    }
                }
                for (int k = 0; k < 4; k++)
                {
                    for (int i = 0; i < img.Count; i++) // Random exchange
                    {
                        int id1 = UnityEngine.Random.Range(0, img.Count);
                        int id2 = UnityEngine.Random.Range(0, img.Count);
                        if (id1 != id2)
                        {
                            TImage tmp = img[id1];
                            img[id1] = img[id2];
                            img[id2] = tmp;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// The action triggered by the player pressing the spacebar.
        /// The next image will be displayed immediately, the right and wrong conditions will be updated accordingly,
        /// and the key press time will be properly recorded.
        /// </summary>
        public void OnKey(bool isSpaceKey, TimeAndKey wrongKey)
        {
            if (initialTestImages != null)
            {
                if (gameState != -1 & prepareTime <= 0)
                {
                    if (serialNumber >= 0 && serialNumber < initialTestImages.Count) // OnKey() processes the results of user keystrokes and saves the results of user keystrokes to storage
                    {
                        currentImageHitRound.isKeyPressed = true;

                        if (currentImageHitRound.UnidentifiedKeysPressed == null)
                        {
                            currentImageHitRound.UnidentifiedKeysPressed = new List<TimeAndKey>();
                        }

                        keyPressEndTime = Time.time; // The time of pressing the space bar
                        currentImageHitRound.keyPressTime = keyPressEndTime - keyPressStartTime;
                        currentImageHitRound.isSpaceKey = isSpaceKey;

                        if (isSpaceKey)
                        {
                            if (specifiedTheme == initialTestImages[serialNumber].imageTheme) // If the theme is current specified theme
                            {
                                Beep.Play(); // Play the beep sound
                                currentImageHitRound.isCorrectlyIdentified = true;
                            }
                            else
                            {
                                WrongAudio.Play(); // Play the wrong audio
                                currentImageHitRound.isCorrectlyIdentified = false;
                            }
                            serialNumber++;  // Go to the next image
                            canShowNextImage = true;
                            imageDisplayedTime = 0;
                        }
                        else
                        {

                            currentImageHitRound.isCorrectlyIdentified = false;
                            currentImageHitRound.UnidentifiedKeysPressed.Add(wrongKey);

                        }



                    }
                }

            }

        }

        /// <summary>
        /// If the player does not press the spacebar key, it will automatically enter the next image after three seconds.
        /// Related data will be properly recorded
        /// </summary>
        private void ImagePass()
        {
            if (initialTestImages != null)
            {
                if (serialNumber >= 0 && serialNumber < initialTestImages.Count)
                {
                    keyPressEndTime = Time.time;
                    currentImageHitRound.keyPressTime = keyPressEndTime - keyPressStartTime;
                    if (!currentImageHitRound.isKeyPressed) // If the space bar is not pressed
                    {
                        if (initialTestImages[serialNumber].imageTheme == specifiedTheme) // if the current image is not part of the current theme
                        {
                            currentImageHitRound.isCorrectlyIdentified = false;
                        }
                        else
                        {
                            currentImageHitRound.isCorrectlyIdentified = true;
                        }
                    }
                }
            }
            canShowNextImage = true;
            serialNumber++; // Go to the next image
        }

        /// <summary>
        /// To display the next image
        /// </summary>
        public void NextImage()
        {

            if (canShowNextImage)
            {
                if (serialNumber >= 0 && serialNumber < initialTestImages.Count)
                {
                    currentImageHitRound = new ImageHitRound();  //The data of this game
                    currentImageHitRound.testTheme = specifiedTheme; //The image theme of this game
                    currentImageHitRound.imageName = initialTestImages[serialNumber].sprite.name; //The name of the specified image
                    currentImageHitRound.imageTheme = initialTestImages[serialNumber].imageTheme; //The theme of the specified image
                    keyPressStartTime = Time.time;  //To record the pressing time
                    currentRoundTestData.Add(currentImageHitRound);

                    imageDisplay.sprite = initialTestImages[serialNumber].sprite;  // The next image object
                    keyPressStartTime = Time.time;  // Start to display the time
                    canShowNextImage = false;
                }
                else // images display is done
                {

                    if (gameState == 0)  // The first part is over
                    {
                        gameState = 1;
                        OnRound1Over();

                        imageDisplayedTime = 0;
                        gameInfo.text = "";
                        timer.text = "";
                        imageDisplay.sprite = null;
                        imageDisplay.color = Color.black;
                        prepareTime = 1;
                    }
                    else  // the second part is over
                    {
                        gameState = 2;
                    }
                    if (gameState == 2)  // the game is over
                    {
                        OnRound2Over();
                        gameState = -1;
                        gameInfo.text = "ImageHit Test Complete";
                        timer.text = "";
                    }


                }

            }

        }


        /// <summary>
        /// Getter function for the Image Hit gameplay data
        /// </summary>
        /// <returns>The gameplay data of the Image Hit mini-game.
        /// </returns> 
        public static ImageHitStorage GetGameplayData()
        {
            return imageHitData;
        }

        /// <summary>
        /// To get scores of multiple rounds of the game
        /// </summary>
        public void OnRound2Over()
        {
            // To store the score of each round
            AddRoundTestData();
            currentRoundTestData.Clear();
            SceneManager.LoadScene("MenuScene");
        }

        /// <summary>
        /// To find images of TImage type.
        /// After the first stage of the game (the first ten images), the wrong images will be redisplayed. 
        /// FindImage looks for the image in the test image list imagesToFound according to the image name and theme in the previous part of the game.
        /// Images with identical theme and name are the same images.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="sp"></param>
        /// <returns>the images find of the images</returns>
        public TImage FindImage(List<TImage> imagesToFound, ImageHitRound round)
        {
            if (imagesToFound != null)
            {
                for (int i = 0; i < imagesToFound.Count; i++)
                {
                    if (imagesToFound[i].imageTheme == round.imageTheme &&
                       imagesToFound[i].sprite.name == round.imageName) //The theme of images is the same as the specified theme
                    {
                        return imagesToFound[i];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// To find image themes
        /// </summary>
        /// <param name="pthemename"> the name of image theme </param>
        /// <returns>The theme for the images</returns>
        private int FindTheme(string pthemeName)
        {
            if (themes != null)
            {
                for (int i = 0; i < themes.Length; i++)
                {
                    if (themes[i].specifiedTheme == pthemeName)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// To get the random theme used in one round of the game
        /// </summary>
        /// <param name="pthemename"></param>
        /// <returns>the random generated theme for the game</returns>
        private int GetRandTheme(string pthemeName)
        {
            int id = -1;
            List<string> themeNames = new List<string>();
            for (int i = 0; i < themes.Length; i++)
            {
                if (themes[i].specifiedTheme != pthemeName)  //ignore pthemename
                {
                    themeNames.Add(themes[i].specifiedTheme);
                }
            }
            id = UnityEngine.Random.Range(0, themeNames.Count);

            id = FindTheme(themeNames[id]);
            return id;
        }

        /// <summary>
        /// To get the images which do not belong to the theme specified for the current game round
        /// This method randomly selects count non-themed images from the theme library
        /// </summary>
        /// <param name="num"></param>
        /// <returns>the generated images for the game</returns>
        private List<TImage> GetRandImage(int count)
        {
            int randImageCount = count;
            List<TImage> results = new List<TImage>();
            int randThemeId;
            int extractCount = 0;
            int deleteCount = 0;
            while (count > 0)  // To get images with other themes
            {
                randThemeId = GetRandTheme(specifiedTheme);
                if (randThemeId >= 0 && randThemeId < themes.Length)
                {
                    deleteCount = UnityEngine.Random.Range(1, 3);
                    List<TImage> tmp = themes[randThemeId].getrandimages(deleteCount);  // To get get images with other themes of random numbers
                    if (tmp != null)
                    {
                        results.AddRange(tmp);
                        count -= deleteCount;
                    }

                }

                extractCount++;
                if (extractCount >= themes.Length * 20)
                {
                    break;
                }
            }
            deleteCount = results.Count - randImageCount;
            if (deleteCount > 0)
            {
                results.RemoveRange(randImageCount, deleteCount);
            }
            return results;
        }

        /// <summary>
        /// What happens after the game is over
        /// </summary>
        public void OnStop()
        {
            gameState = -1;
            gameInfo.text = "";
            imageDisplay.sprite = null;
            imageDisplay.gameObject.SetActive(false);
        }



        /// <summary>
        /// To start the Image hit game
        /// The game should be started at once after calling this method
        /// </summary>
        public void StartGame()
        {

            UnityEngine.Random.InitState(DateTime.Now.Second);

            //To get a random theme used for this round of game
            currentThemeId = GetRandTheme("");
            if (currentThemeId != -1)
            {
                themes[currentThemeId].specifiedTheme = themes[currentThemeId].specifiedTheme.Trim();
                specifiedTheme = themes[currentThemeId].specifiedTheme;

                // To get images of current themes of the number MIN_NumOfImagesOfTheme to MAX_NumOfImagesOfTheme
                int randThemeCount = RamGenerator.GenerateARamInt(MIN_NumOfImagesOfTheme, MAX_NumOfImagesOfTheme);
                currentRoundTestImages = themes[currentThemeId].getrandimages(randThemeCount);

                randThemeCount = 10 - randThemeCount;

                List<TImage> tmp = GetRandImage(randThemeCount);
                currentRoundTestImages.AddRange(tmp);

            }

            images = new List<string>();

            foreach (var item in currentRoundTestImages)
            {
                images.Add(item.sprite.name);
            }


            // Before the game start, the imagelist must have 10 images
            // All variables should be initialized
            serialNumber = 0;
            if (currentRoundTestImages.Count == 10)
            {
                currentRoundTestData.Clear();

                initialTestImages = currentRoundTestImages;
                gameInfo.text = "";
                prepareTime = 1;
                imageDisplayedTime = 0;
                canShowNextImage = true;
                gameState = 0;
                RandSortImages(initialTestImages);
                imageDisplay.gameObject.SetActive(true);
                mRoundStart(0);
            }
        }

        /// <summary>
        /// To generate the randomized image list from all categories of images.
        /// </summary>
        /// <returns>The random list of images</returns>
        public List<string> GetRandowImageList()
        {

            List<TImage> testList = new List<TImage>();
            CTheme[] mythemes = themes;
            images = new List<string>();
            if (testList != null)
            {
                testList.Clear();
            }

            UnityEngine.Random.InitState(DateTime.Now.Second);

            //To get a random theme used for this round of game
            var testID = GetRandTheme("");

            //currentThemeId = GetRandTheme("");
            if (testID != -1)
            {
                mythemes[testID].specifiedTheme = mythemes[testID].specifiedTheme.Trim();
                //specifiedTheme = themes[testID].specifiedTheme;

                // To get images of current themes of the number 3 or 4
                int randThemeCount = UnityEngine.Random.Range(3, 5);
                testList = mythemes[testID].getrandimages(randThemeCount);
                randThemeCount = 10 - randThemeCount;

                List<TImage> tmp = GetRandImage(randThemeCount);
                testList.AddRange(tmp);

            }

            foreach (var item in currentRoundTestImages)
            {
                images.Add(item.sprite.name);
            }

            return images;
        }

        /// <summary>
        /// Get image list from different round at the start of each round
        /// </summary>
        /// <param name="n">the round to get the image list from</param>
        /// <returns>the image list</returns>
        public List<TImage> OnRoundStart(int n)
        {
            List<TImage> roundimage12 = new List<TImage>();
            for (int i = 0; i < initialTestImages.Count; i++)
            {
                TImage tmp = new TImage(initialTestImages[i]);
                roundimage12.Add(tmp);
            }
            return roundimage12;
        }
    }
}