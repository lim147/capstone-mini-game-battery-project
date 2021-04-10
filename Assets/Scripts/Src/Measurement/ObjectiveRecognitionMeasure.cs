using Storage;
using Games;
using UI;
using System.Collections.Generic;
using UnityEngine;


namespace Measurement
{
    /// <summary>
    /// This module implements [ObjectiveRecognition Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#objectrecognitionmeasure-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>

    public class ObjectRecognitionMeasure
    {

        // ImageHitStorage datatype variable to hold the data from the imagehit mini-game
        // notYetPlayImageHit represents if the game imagehit has been played
        private static bool notYetPlayImageHit = Globals.isImageHitButtonOn;
        // If it's not played(notYetPlayImageHit == true), declare an empty ImageHitStorage object;
        // Else, fill in with gameplay data 
        public static ImageHitStorage imagehitData = notYetPlayImageHit ?
            new ImageHitStorage() { Rounds = new List<List<ImageHitRound>>()}
            : ImageHit.GetGameplayData();


        // Variable to hold the inhibition score for imagehit mini-game
        private static int imagehitScore = 0;

        // Subscore datatype variable to hold subscores for inhibition ability
        public static SubscoreStorage subScoreImageHit = new SubscoreStorage();


        /// <summary>
        /// Get the objrecognition ability score for the ImageHit mini-game and store it
        /// </summary>

        public static void EvaluateImageHitScore()
        {
            if (imagehitData is null)
            {
                imagehitScore = -1;
            }
            else
            {
                imagehitScore = GetObjectRecognitionScore();
            }
            // Update the subScore record for the inhibition ability in the ImageHit mini-game
            UpdateSubScoreForImageHit();
        }


        /// <summary>
        /// Getter function for the subscore from the ImageHit game
        /// </summary>
        /// <returns>The subscore datatype with currently calculated scores</returns>
        /// <remarks>The returned value is ≥ 0.</remarks>
        public static SubscoreStorage GetSubScoreForImageHit()
        {
            return subScoreImageHit;
        }


        //Helper functions start:
        //-----------------------------------------------------------------


        /// <summary>
        /// Update the subscore object with the CatchTheThief mini-game score
        /// </summary>
        private static void UpdateSubScoreForImageHit()
        {
            // Set all related fields for the ImageHit mini-game
            subScoreImageHit.AbilityName = AbilityName.OBJECT_RECOGNITION;
            subScoreImageHit.GameName = GameName.IMAGE_HIT;
            subScoreImageHit.Score = imagehitScore;
            subScoreImageHit.Weight = 2;
        }


        //ImageHit Helper functions:
        //-------------------------------------------------------
        /// <summary>
        /// Get the object recognition ability score for the Image Hit mini-game and store it
        /// </summary>
        /// <returns>The object recognition score for the image hit game </returns>
        public static int GetObjectRecognitionScore()
        {

            // The basic score of object recognition ability for image hit game is up to 80. 
            // The mark will be calculated as the formula showed in getImagescore().
            int id = imagehitData.Rounds.Count - 1;
            int scoreObjectRecognition = 0;

            if (imagehitData != null)
            {
                if (id >= 0 && id < imagehitData.Rounds.Count)
                {
                    int RoundNum = imagehitData.Rounds[id].Count;


                    if (RoundNum > 10)
                    {
                        List<ImageHitRound> round1 = imagehitData.Rounds[id].GetRange(0, 10);// The first ten images
                        List<ImageHitRound> round2 = imagehitData.Rounds[id].GetRange(10, RoundNum - 10); // Images from the second stage of the game
                        int rightCount = 0;

                        float averagePressTime = 0;

                        float keytime = 0;
                        if (round1 != null)
                        {
                            List<ImageHitRound> round12data;
                            int len = round1.Count;
                            int totalNumberOfThemes = 0;

                            for (int i = 0; i < len; i++)
                            {
                                round12data = GetImageFromRound12(round1[i], round2);  //The data of the second stage of the game
                                int f1 = GetObjectRecognitionScoreFromRound12(round12data);  // To calculate the scores
                                scoreObjectRecognition += f1;

                                if (round1[i].isCorrectlyIdentified) // The number of correctly recognized images
                                {
                                    rightCount++;
                                }

                                keytime = GetImageKeyTimeFromRound12(round12data);  //the time of pressing

                                if (keytime > 0 && round12data != null)
                                {
                                    averagePressTime += keytime;
                                    totalNumberOfThemes += round12data.Count;
                                }

                            }


                            for (int i = 0; i < round2.Count; i++)  //The number of correctly recognized images
                            {
                                if (round2[i].isCorrectlyIdentified)
                                {
                                    rightCount++;
                                }
                            }

                            if (scoreObjectRecognition > 80)
                            {
                                scoreObjectRecognition = 80;
                            }
                            
                            // For images that conform to the theme of this round, 
                            // the average time for the player to make a correct judgment can be used as the bonus of his object recognition score.
                            // If the average reaction time is less than 0.5 seconds, the player will get a bonus of 20 points. 
                            // If the average reaction time is between 0.5 second and 1 second, the player will get 15 points of bonus. 
                            // If the average reaction time is between 1 second and 1.5 seconds, the player will get a bonus of 10 points. 
                            // If the average reaction time is between 1.5 seconds and 2 seconds, the player will get a bonus of 5 points.

                            if (totalNumberOfThemes != 0)
                            {
                                averagePressTime = averagePressTime / totalNumberOfThemes; // The average pressing time of images of correct theme
                            }
                            else
                            {
                                averagePressTime = float.MaxValue;
                            }

                            if (averagePressTime < 0.5f)
                            {
                                scoreObjectRecognition += 20;
                            }
                            else if (averagePressTime < 1f)
                            {
                                scoreObjectRecognition += 15;
                            }
                            else if (averagePressTime < 1.5f)
                            {
                                scoreObjectRecognition += 10;
                            }
                            else if (averagePressTime < 2)
                            {
                                scoreObjectRecognition += 5;
                            }


                            float sObjectRecognition = PlayerPrefs.GetFloat("scoreObjectRecognition");
                            if (scoreObjectRecognition > sObjectRecognition)
                            {
                                PlayerPrefs.SetFloat("scoreObjectRecognition", scoreObjectRecognition);
                            }
                            int sfright = PlayerPrefs.GetInt("rightnum");
                            if (rightCount > sfright)
                            {
                                PlayerPrefs.SetInt("rightnum", rightCount);
                            }

                        }
                    }

                }
            }
            return scoreObjectRecognition;
        }

        /// <summary>
        /// This method is used to obtain the reaction time data of pressing the space bar in the entire game (including more than a dozen images in the first and second stages)
        /// </summary>
        /// <param name="data"> All data about pressing time in one time of game</param>
        /// <returns></returns>
        public static float GetImageKeyTimeFromRound12(List<ImageHitRound> data)
        {
            if (data == null)
            {
                return 0;
            }

            float f = 0;

            for (int i = 0; i < data.Count; i++) //The pressing time of images of correct theme
            {
                f += data[i].keyPressTime;
            }

            return f;
        }


        /// <summary>
        /// Calculates the object recognition score for Image Hit game
        /// </summary>
        public static int GetObjectRecognitionScoreFromRound12(List<ImageHitRound> data)
        {
            // For the images that appear in the game, every time a player makes a correct action, 
            // he/she can get 8 points in the object recognition score, and up to 80 points.
            if (data == null)
            {
                return 0;
            }

            
            if (data.Count == 1)
            {
                if (data[0].isCorrectlyIdentified)
                {
                    return 8;
                }

            }
            else if (data.Count > 1)
            {
                if (!data[0].isCorrectlyIdentified && !data[1].isCorrectlyIdentified)
                {
                    return 0;
                }
                else
                {
                    return 8;
                }
            }
            return 0;
        }

        /// <summary>
        /// Find the images corresponding to the first stage among the images used in the second stage.
        /// </summary>
        /// <param name="pround1">Game data (including theme name and image name)</param>
        /// <param name="pround2">Game data (including theme name and image name)</param>
        /// <returns></returns>
        public static List<ImageHitRound> GetImageFromRound12(ImageHitRound pround1, List<ImageHitRound> pround2)
        {

            List<ImageHitRound> res = new List<ImageHitRound>();
            if (pround1 == null || pround2 == null)
            {
                return res;
            }

            int len = pround2.Count;
            
            for (int i = 0; i < len; i++)
            {
                if (pround2[i].imageName == pround1.imageName &&
                    pround2[i].imageTheme == pround1.imageTheme) //Images with identical theme and name are the same images.
                {
                    res.Add(pround2[i]);
                }
            }
            
            res.Add(pround1);
            return res;
        }













    }





}
