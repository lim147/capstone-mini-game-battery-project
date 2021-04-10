using Storage;
using Games;
using UI;
using System.Collections.Generic;
using UnityEngine;


namespace Measurement
{
    /// <summary>
    /// This module implements [InhibitionMeasure Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#inhibitionmeasure-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class InhibitionMeasure
    {
        // BalloonsStorage datatype variable to hold the data from the balloons mini-game
        // notYetPlayBalloons represents if the game Balloons has been played
        private static bool notYetPlayBalloonss = Globals.isBalloonsButtonOn;
        // If it's not played(notYetPlayBalloons == true), declare an empty BalloonsStorage object;
        // Else, fill in with gameplay data 
        public static BalloonsStorage balloonsData = notYetPlayBalloonss ?
            new BalloonsStorage() { Rounds = new List<BalloonsRound>()}
            : Balloons.GetGameplayData();

        // CatchTheThiefStorage datatype variable to hold the data from the catch the thief mini-game
        // notYetPlayCTF represents if the game Catch The Thief has been played
        private static bool notYetPlayCTF = Globals.isCTFButtonOn;
        // If it's not played(notYetPlayCTF == true), declare an empty CatchTheThiefStorage object;
        // Else, fill in with gameplay data 
        public static CatchTheThiefStorage ctfData = notYetPlayCTF ?
            new CatchTheThiefStorage() { Rounds = new List<CatchTheThiefRound>()}
            : CatchTheThief.GetGameplayData();

        // ImageHitStorage datatype variable to hold the data from the imagehit mini-game
        // notYetPlayImageHit represents if the game imagehit has been played
        private static bool notYetPlayImageHit = Globals.isImageHitButtonOn;
        // If it's not played(notYetPlayImageHit == true), declare an empty ImageHitStorage object;
        // Else, fill in with gameplay data 
        public static ImageHitStorage imagehitData = notYetPlayImageHit ?
            new ImageHitStorage() { Rounds = new List<List<ImageHitRound>>()}
            : ImageHit.GetGameplayData();


        // Variable to hold the inhibition score for balloons mini-game
        private static int balloonsScore = 0;
        // Variable to hold the selective visual ability score from catch the thief mini-game
        private static int ctfScore = 0;
        // Variable to hold the inhibition score for imagehit mini-game
        private static int imagehitScore = 0;

        // Subscore datatype variable to hold subscores for inhibition ability
        public static SubscoreStorage subScorBalloons = new SubscoreStorage();
        // Subscore datatype variable to hold subscores for (selective visual, catch the thief)
        public static SubscoreStorage subScoreCTF = new SubscoreStorage();
        // Subscore datatype variable to hold subscores for inhibition ability
        public static SubscoreStorage subScoreImageHit = new SubscoreStorage();



        /// <summary>
        /// Get the inhibition ability score for the Balloons mini-game and store it
        /// </summary>
        public static void EvaluateBalloonsScore()
        {
            // Calculate the number of unsuccessful clicks
            int numberOfUnsuccessClick = FindNumberOfUnsuccessClick();

            // Match number of unsuccessful clicks to the score multiplier
            double balloonUnsuccessClickMultiplier = MatchNumberOfUnsuccessClickToScoreMultiplier(numberOfUnsuccessClick);

            // Find the average move time for a click
            double avgMoveTime = FindAvgMoveTimeBalloons();

            // Match the average move time to a score
            int balloonsTempScore = MatchMoveTimeToScoreBalloons(avgMoveTime);

            // Calculate the score with the score multiplier
            balloonsScore = (int)(balloonsTempScore * balloonUnsuccessClickMultiplier);

            // Update the subScore record for the inhibition ability in the balloons mini-game
            UpdateSubScoreForBalloons();
        }

        /// <summary>
        /// Get the inhibition ability score for the Catch The Thief mini-game and store it
        /// </summary>
        public static void EvaluateCTFScore()
        {
            ctfScore = CheckStimuliTurnOut();

            // Update the subScore record for the inhibition ability in the catch the thief mini-game
            UpdateSubScoreForCTF();
        }

        /// <summary>
        /// Get the inhibition ability score for the ImageHit mini-game and store it
        /// </summary>

        public static void EvaluateImageHitScore()
        {

            imagehitScore = GetInhibitionScore();
            // Update the subScore record for the inhibition ability in the ImageHit mini-game
            UpdateSubScoreForImageHit();
        }


        /// <summary>
        /// Getter function for the subscore from the Squares game
        /// </summary>
        /// <returns>The subscore datatype with currently calculated scores</returns>
        /// <remarks>The returned value is ≥ 0.</remarks>
        public static SubscoreStorage GetSubScoreForBalloons()
        {
            return subScorBalloons;
        }


        /// <summary>
        /// Getter function for the subscore from the Catch The Thief game
        /// </summary>
        /// <returns>The subscore datatype with currently calculated scores</returns>
        /// <remarks>The returned value is ≥ 0.</remarks>
        public static SubscoreStorage GetSubScoreForCTF()
        {
            return subScoreCTF;
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
        /// Update the subscore object with the Balloons mini-game score
        /// </summary>
        private static void UpdateSubScoreForBalloons()
        {
            // Set all related fields for the balloons mini-game
            subScorBalloons.AbilityName = AbilityName.INHIBITION;
            subScorBalloons.GameName = GameName.BALLOONS;
            subScorBalloons.Score = balloonsScore;
            subScorBalloons.Weight = 2;
        }

        /// <summary>
        /// Update the subscore object with the CatchTheThief mini-game score
        /// </summary>
        private static void UpdateSubScoreForCTF()
        {
            // Set all related fields for the catch the thief mini-game
            subScoreCTF.AbilityName = AbilityName.INHIBITION;
            subScoreCTF.GameName = GameName.CATCH_THE_THIEF;
            subScoreCTF.Score = ctfScore;
            subScoreCTF.Weight = 2;
        }

        /// <summary>
        /// Update the subscore object with the CatchTheThief mini-game score
        /// </summary>
        private static void UpdateSubScoreForImageHit()
        {
            // Set all related fields for the ImageHit mini-game
            subScoreImageHit.AbilityName = AbilityName.INHIBITION;
            subScoreImageHit.GameName = GameName.IMAGE_HIT;
            subScoreImageHit.Score = imagehitScore;
            subScoreImageHit.Weight = 2;
        }

        // Weight can check the weightage table: https://docs.google.com/document/d/1ySxuZwQ3d8K1Sag0szvfDwiHfoN80YzLuk6hU1Mk92w/edit#heading=h.3v9wxj5zjznr



        //Balloons Helper functions:
        //-------------------------------------------------------

        /// <summary>
        /// Find the average move time for Balloons Minigame
        /// </summary>
        /// <returns> The average move time for Balloons Minigame</returns>
        private static double FindAvgMoveTimeBalloons()
        {
            double totalMoveTime = 0;
            double firstClickTime = 0;
            int numberOfRounds = balloonsData.Rounds.Count;

            foreach (BalloonsRound balloonsRound in balloonsData.Rounds)
            {
                // If there are unsuccessful clicks, then take the time of the first unsuccessful click
                if (balloonsRound.Clicks.Count >= 1)
                {
                    firstClickTime = balloonsRound.Clicks[0].Time;
                }
                else // Otherwise, take the time of the successful click
                {
                    firstClickTime = balloonsRound.DestinationClickTime;
                }
                totalMoveTime = totalMoveTime + firstClickTime;
            }

            // No round is played
            // Return a big number as avg move time, so that the score will be 0
            if (numberOfRounds == 0)
            {
                return 100;
            }
            else
            {

                return (totalMoveTime / numberOfRounds);
            }
        }

        /// <summary>
        /// Match the move time to a score for Balloons mini-game
        /// </summary>
        /// <returns>A score out of 100 for move time</returns>
        private static int MatchMoveTimeToScoreBalloons(double moveTime)
        {
            // Variable to hold the score temporarily
            double temporaryScore = 100;

            // The value of the fastest possible conscious human reactions
            // are about 0.15. This is the baseline for this score.
            if (moveTime <= 0.15)
            {
                return 100;
            }

            else // Deduct 1.5 points from the score for every 0.015 units between click position and balloon center
            {
                // Do not deduct points for the fastest possible time
                moveTime -= 0.15;

                // Calculate the amount of 0.015 units the click was from the center and deduct points
                temporaryScore -= 1 * (moveTime / 0.015);

                // In the situation that the player performed very poorly, ensure that score is not
                // less than 0
                if (temporaryScore < 0)
                {
                    temporaryScore = 0;
                }
                return (int)temporaryScore;
            }
        }

        /// <summary>
        /// Find the total number of unsuccessful clicks in the balloons mini-game
        /// </summary>
        /// <returns>Returns the total number of unsuccesful clicks in the balloons mini-game</returns>
        private static int FindNumberOfUnsuccessClick()
        {
            // Variable for subtotal of unsuccessful clicks
            int totalNumOfUnsuccessfulClicks = 0;

            // Loop through all the rounds in the balloons mini-game
            foreach (BalloonsRound balloonsRound in balloonsData.Rounds)
            {
                totalNumOfUnsuccessfulClicks += balloonsRound.Clicks.Count;
            }
            return totalNumOfUnsuccessfulClicks;
        }

        /// <summary>
        /// Match the number of unsuccessful clicks to a score
        /// </summary>
        /// <returns>Returns a score out of 100 for unsuccesful clicks</returns>
        private static double MatchNumberOfUnsuccessClickToScoreMultiplier(int numberOfUnsuccessClick)
        {
            if (numberOfUnsuccessClick == 0)
            {
                return 1.2;
            }
            else if (numberOfUnsuccessClick <= 3)
            {
                return 1.1;
            }
            else if (numberOfUnsuccessClick <= 5)
            {
                return 1;
            }
            else if (numberOfUnsuccessClick <= 10)
            {
                return 0.9;
            }
            else if (numberOfUnsuccessClick <= 15)
            {
                return 0.8;
            }
            else 
            {
                return 0.6;
            }
        }



        //Catch The Thief Helper functions:
        //-------------------------------------------------------

        /// <summary>
        /// Check the ability to turn out the distractive stimuli(people image)
        /// and derive a score
        /// </summary>
        /// <returns>Returns a score out of 100 for distractive stimuli turn out</returns>
        private static int CheckStimuliTurnOut()
        {
            //If no round is played, score is 0
            int numberOfRound = ctfData.Rounds.Count;
            if(numberOfRound == 0)
            {
                return 0;
            }


            int tempScore = 100;

            // Count of wrong presses when both police and people image appear
            int numOfWrongPressWhenBothAppear = 0;
            // Count of wrong presses when only people image(s) appears
            int numOfWrongPressWhenOnlyPeopleAppear = 0;
            // Count of missing presses when only thief image appears
            int numOfMissingPressWhenOnlyThiefAppear = 0;

            foreach (CatchTheThiefRound round in ctfData.Rounds)
            {
                bool isKeyPressed = round.IsIdentifiedKeyPressed;
                bool isThiefAppeared = round.ThiefAppearInRound;
                bool isPersonAppeared = round.PersonAppearInRound;

                // In the current round:
                // If both thief image and person image appear, and player presses the button
                if (isThiefAppeared && isPersonAppeared && isKeyPressed)
                {
                    numOfWrongPressWhenBothAppear += 1;
                }
                // If only person image appears, and player presses the button
                else if (!isThiefAppeared && isPersonAppeared && isKeyPressed)
                {
                    numOfWrongPressWhenOnlyPeopleAppear += 1;
                }
                // If only thief image appears, and player does not press the button
                else if (isThiefAppeared && !isPersonAppeared && !isKeyPressed)
                {
                    numOfMissingPressWhenOnlyThiefAppear += 1;
                }

            }

            int costOfWrongPressWhenBothAppear = 10;
            int costOfWrongPressWhenOnlyPeopleAppear = 10;
            int costOfMissingPressWhenOnlyThiefAppear = 5;

            tempScore -= costOfWrongPressWhenBothAppear * numOfWrongPressWhenBothAppear;
            tempScore -= costOfWrongPressWhenOnlyPeopleAppear * numOfWrongPressWhenOnlyPeopleAppear;
            tempScore -= costOfMissingPressWhenOnlyThiefAppear * numOfMissingPressWhenOnlyThiefAppear;

            // Make sure score >= 0
            int score = tempScore < 0 ? 0 : tempScore;

            return score;
        }



        //ImageHit Helper functions:
        //-------------------------------------------------------
        /// <summary>
        /// The way to calculate the inhibition score for Image Hit game
        /// </summary>
        /// <param name="data"> All data in one time of game</param>
        public static int GetInhibitionScoreFromRound12(List<ImageHitRound> data)
        {
        // If the player misrecognizes an image once, 10 points will be deducted.
        // If the player misrecognizes an image twice, there is a problem with the Object Recognition ability. The inhibition score will not be influenced.

            if (data == null)
            {
                return 0;
            }

            if (data.Count == 1)  
            {
                if (data[0].isCorrectlyIdentified)
                {
                    return 0;
                }
                else
                { 
                    if(!data[0].isSpaceKey&& data[0].isKeyPressed) //If the pressed bar is not space bar
                    {
                        return -10;
                    }

                }
            }
            else if (data.Count > 1)
            {  
                if (data[0].isCorrectlyIdentified && data[1].isCorrectlyIdentified) 
                {
                    return 0;
                }
                else 
                {
                    int s = 0;

                    if (data[0].isKeyPressed && !data[0].isSpaceKey)  //If the pressed bar is not space bar
                    {
                        s -= 10;
                    }
                    if (data[1].isKeyPressed && !data[1].isSpaceKey) //If the pressed bar is not space bar
                    {
                        s -= 10;
                    }
                
                    if (data[0].isCorrectlyIdentified && !data[1].isCorrectlyIdentified 
                        || !data[0].isCorrectlyIdentified && data[1].isCorrectlyIdentified)
                    {
                        s -= 10;
                    }
                    
                    return s;
                }
            
            }
            return -1;
        }

        /// <summary>
        /// Find the images corresponding to the first stage among the images used in the second stage.
        /// </summary>
        /// <param name="pround1">Game data (including theme name and image name)</param>
        /// <param name="pround2">Game data (including theme name and image name)</param>
        /// <returns></returns>
        public static List<ImageHitRound> GetImageFromRound12(ImageHitRound pround1,List<ImageHitRound> pround2) 
        {
        
            List<ImageHitRound> res = new List<ImageHitRound>();

            if (pround1==null||pround2==null)
            {
                return res;
            }
            int len = pround2.Count;
            for (int i = 0; i < len; i++)
            {
                if(pround2[i].imageName== pround1.imageName &&
                    pround2[i].imageTheme == pround1.imageTheme) //Images with identical theme and name are the same images.
                {
                    res.Add(pround2[i]);
                }
            }
            res.Add(pround1);
            return res;
        }

        /// <summary>
        /// Get the inhibition ability score for the Image Hit mini-game and store it
        /// </summary>
        /// <returns>The inhibition score for the image hit game </returns>
        public static int GetInhibitionScore() 
        {

            int scoreInhibition = 0;
            int id = imagehitData.Rounds.Count-1;
            // The score of inhibition ability for image hit game is initially 100. 
            // The mark will be deducted as the formula showed in getImagescore().


            if(id >= 0 && id < imagehitData.Rounds.Count)
            {
                int RoundNum = imagehitData.Rounds[id].Count;
                if (RoundNum > 10)
                {
                    List<ImageHitRound> round1 = imagehitData.Rounds[id].GetRange(0, 10); // The first ten images
                    List<ImageHitRound> round2 = imagehitData.Rounds[id].GetRange(10, RoundNum - 10); // Images from the second stage of the game
                    int checker1 = 0;
                    int checker2 = 0;
                    for (int i =0;i<round1.Count;i++){
                        if (round1[i].isKeyPressed == true){
                            checker1 += 1;
                        }
                    }
                    for (int i =0;i<round2.Count;i++){
                        if (round2[i].isKeyPressed == true){
                            checker2 += 1;
                        }
                    }
                    if (checker1 == round1.Count && checker2 == round2.Count){
                        return 0;
                    }
                    scoreInhibition = 100;
                    bool state = false;
                    if (round1 != null)
                    {
                        List<ImageHitRound> round12data;
                        int len = round1.Count;
  
                        for (int i = 0; i < len; i++)
                        {
                            round12data = GetImageFromRound12(round1[i], round2);  //The data of the second stage of the game
                            if (round12data[0].isKeyPressed)
                            {
                                state = true;
                            }
                            int f1 = GetInhibitionScoreFromRound12(round12data);  // To calculate the scores
                            scoreInhibition += f1;
                        }

                        if (!state)
                        {
                            scoreInhibition = 0;
                        }

                    }
                }
                                 
            }
            float sInhibition = PlayerPrefs.GetFloat("scoreInhibition");
            return scoreInhibition;
        }
    }
}


