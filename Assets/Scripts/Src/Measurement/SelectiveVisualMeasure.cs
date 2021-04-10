using System.Collections.Generic;
using System.Linq;
using Helper;
using Storage;
using Games;
using UI;
using System;

namespace Measurement
{
    /// <summary>
    /// This module implements [SelectiveVisualMeasure Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#selectivevisualmeasure-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class SelectiveVisualMeasure
    {
        // BalloonsStorage datatype variable to hold the data from the balloons mini-game
        // notYetPlayBalloons represents if the game Balloons has been played
        private static bool notYetPlayBalloons = Globals.isBalloonsButtonOn;
        // If it's not played(notYetPlayBalloons == true), declare an empty BalloonsStorage object;
        // Else, fill in with gameplay data 
        public static BalloonsStorage balloonsData = notYetPlayBalloons ?
            new BalloonsStorage() { Rounds = new List<BalloonsRound>()}
            : Balloons.GetGameplayData();

        // SquaresStorage datatype variable to hold the data from the squares mini-game
        // notYetPlaySquares represents if the game Squares has been played
        private static bool notYetPlaySquares = Globals.isSquaresButtonOn;
        // If it's not played(notYetPlaySquares == true), declare an empty SquaresStorage object;
        // Else, fill in with gameplay data 
        public static SquaresStorage squaresData = notYetPlaySquares ?
            new SquaresStorage() { Rounds = new List<SquaresRound>()}
            : Squares.GetGameplayData();

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

        

        // Variable to hold the selective visual ability score from balloons mini-game
        private static int balloonsScore = 0;
        // Variable to hold the selective visual ability score from squares mini-game
        private static int squaresScore = 0;
        // Variable to hold the selective visual ability score from catch the thief mini-game
        private static int ctfScore = 0;
        // Variable to hold the inhibition score for imagehit mini-game
        private static int imagehitScore = 0;

        // Subscore datatype variable to hold subscores for (selective visual, balloons)
        public static SubscoreStorage subScoreBalloons = new SubscoreStorage();
        // Subscore datatype variable to hold subscores for (selective visual, squares)
        public static SubscoreStorage subScoreSquares = new SubscoreStorage();
        // Subscore datatype variable to hold subscores for (selective visual, catch the thief)
        public static SubscoreStorage subScoreCTF = new SubscoreStorage();
        // Subscore datatype variable to hold subscores for inhibition ability
        public static SubscoreStorage subScoreImageHit = new SubscoreStorage();

        /// <summary>
        /// Gets the selective visual ability score from balloons mini-game
        /// and updates the subscore datatype with the score
        /// </summary>
        public static void EvaluateBalloonsScore()
        {
            // Calculate the average move time
            double avgMoveTime = FindAvgMoveTimeBalloons();
            
            
            // Calculate the corresponding score to the average move time
            balloonsScore = MatchMoveTimeToScoreBalloons(avgMoveTime);

            // Update the subScore record for the balloons mini-game in the selective visual ability
            UpdateSubScoreFoBalloons();
        }

        /// <summary>
        /// Gets the selective visual ability score from Squares mini-game
        /// and updates the subscore datatype with the score
        /// </summary>
        public static void EvaluateSquaresScore()
        {
            int sumOfTempScore = 0;
            int numOfRounds = squaresData.Rounds.Count;

            // Calculate the corresponding score based on recall time
            foreach (SquaresRound round in squaresData.Rounds)
            {
                // List of square highlight sequence shown in the round
                List<IndexAndPosition> highlightedSquares = round.HighlightedSquares;
                // List of squares that were recalled by the player
                List<IndexAndPosition> recalledSquares = round.RecalledSquares;

                int tempScore = CompareAnswerAndRecall(highlightedSquares, recalledSquares);

                sumOfTempScore += tempScore;
            }

            // No round is played
            if (numOfRounds == 0)
            {
                squaresScore = 0;
            }
            else
            {
                squaresScore = (int)(sumOfTempScore / numOfRounds);
            }
            // Update the subScoreSquares record for the squares mini-game in the selective visual ability
            UpdateSubScoreForSquares();
        }


        /// <summary>
        /// Gets the selective visual ability score from Catch The Thief mini-game
        /// and updates the subscore datatype with the score
        /// </summary>
        public static void EvaluateCTFScore()
        {
            int sumOfTempScore = 0;
            int numberOfValidRounds = 0;

            foreach (CatchTheThiefRound round in ctfData.Rounds)
            {
                bool isKeyPressed = round.IsIdentifiedKeyPressed;
                bool isThiefAppeared = round.ThiefAppearInRound;
                bool isPersonAppeared = round.PersonAppearInRound;

                // Only the keyPreseTime for pressing the key when the thief image appears will be counted
                if (isThiefAppeared && isKeyPressed)
                {
                    double keyPressTime = round.identifiedKeyPressTime;
                    sumOfTempScore += MatchKeyPressTimeToScore(keyPressTime);
                    numberOfValidRounds += 1;
                }
                // tempScpre will be 0 in the round where only the thief image appears, however
                // the player did not press
                else if (isThiefAppeared && !isPersonAppeared && !isKeyPressed)
                {
                    numberOfValidRounds += 1;
                }

            }

            // No round is played
            if (numberOfValidRounds == 0)
            {
                ctfScore = 0;
            }
            else
            {
                ctfScore = (int)(sumOfTempScore / numberOfValidRounds);
            }
            
            // Update the subScoreSquares record for the catch the thief mini-game in the selective visual ability
            UpdateSubScoreForCTF();
        }


        /// <summary>
        /// Get the objrecognition ability score for the ImageHit mini-game and store it
        /// </summary>

        public static void EvaluateImageHitScore()
        {
            imagehitScore = (int)GetScore();

            // Update the subScore record for the inhibition ability in the ImageHit mini-game
            UpdateSubScoreForImageHit();
        }


        /// <summary>
        /// Get the subscore for the Balloons game
        /// </summary>
        /// <returns>The Subscore object with the Balloons score in it</returns>
        /// <remarks>The returned value is ≥ 0.</remarks>
        public static SubscoreStorage GetSubScoreForBalloons()
        {
            return subScoreBalloons;
        }

        /// <summary>
        /// Get the subscore for the Squares game
        /// </summary>
        /// <returns>The Subscore object with the Squares score in it</returns>
        public static SubscoreStorage GetSubScoreForSquares()
        {
            return subScoreSquares;
        }

        /// <summary>
        /// Get the subscore for the Catch The Thief game
        /// </summary>
        /// <returns>The Subscore object with the Catch The Thief score in it</returns>
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
        private static void UpdateSubScoreFoBalloons()
        {
            // Set all related fields for the balloons mini-game
            subScoreBalloons.AbilityName = AbilityName.SELECTIVE_VISUAL;
            subScoreBalloons.GameName = GameName.BALLOONS;
            subScoreBalloons.Score = balloonsScore;
            subScoreBalloons.Weight = 2;
        }

        /// <summary>
        /// Update the subscore object with the Squares mini-game score
        /// </summary>
        private static void UpdateSubScoreForSquares()
        {
            // Set all related fields for the squares mini-game
            subScoreSquares.AbilityName = AbilityName.SELECTIVE_VISUAL;
            subScoreSquares.GameName = GameName.SQUARES;
            subScoreSquares.Score = squaresScore;
            subScoreSquares.Weight = 1;
        }

        /// <summary>
        /// Update the subscore object with the Catch Th Thief mini-game score
        /// </summary>
        private static void UpdateSubScoreForCTF()
        {
            // Set all related fields for the catch the thief mini-game
            subScoreCTF.AbilityName = AbilityName.SELECTIVE_VISUAL;
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
            subScoreImageHit.AbilityName = AbilityName.SELECTIVE_VISUAL;
            subScoreImageHit.GameName = GameName.IMAGE_HIT;
            subScoreImageHit.Score = imagehitScore;
            subScoreImageHit.Weight = 1;
        }


        //Balloons Helper functions:
        //-------------------------------------------------------

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
                // Ensure that the score is never above 100
                else if (temporaryScore > 100)
                {
                    temporaryScore = 100;
                }
                return (int)temporaryScore;
            }
        }

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

        //Squares Helper functions:
        //-------------------------------------------------------

        /// <summary>
        /// CompareAnswerAndRecall iterates through HighlightedSquares and RecalledSquares sequences,
        /// and computes the score based on the successfully identified square regardless of the order
        /// </summary>
        /// <param name="highlightedSquares">List of the index and screen positions of the square sequence
        /// highlighted by the game.</param>
        /// <param name="recalledSquares">List of the index and screen positions of the square sequence
        /// given by the player.</param>
        /// <returns></returns>
        private static int CompareAnswerAndRecall(
            List<IndexAndPosition> highlightedSquares,
            List<IndexAndPosition> recalledSquares)
        {
            int numberOfHighlightedSquares = highlightedSquares.Count;

            // Create a list of the indices of highlighted squares
            List<int> highlighedSquareIndices = highlightedSquares.Select(s => s.Index).ToList();

            // Create a list of the indices of recalled squares
            List<int> recalledSquareIndices = recalledSquares.Select(s => s.Index).ToList();

            //the mark reduction for missing one square in the recalled sequence
            int costOfOneMissing = (int)Math.Ceiling((double)100 / numberOfHighlightedSquares);

            //number of missing squares
            //detect a missing: some highlighted square is not in the recalled sequence
            int numberOfMissing = highlighedSquareIndices.Except(recalledSquareIndices).Count();

            int score = 100 - numberOfMissing * costOfOneMissing;

            // Make sure that the score is no less than 0 for each round
            return score;
        }

        //Catch The Thief Helper functions:
        //-------------------------------------------------------

        /// <summary>
        /// Match the key press time to a score for Catch The Thief mini-game
        /// </summary>
        /// <returns>A score out of 100 for key press time</returns>
        public static int MatchKeyPressTimeToScore(double keyPressTime)
        {
            // Variable to hold the score temporarily
            double temporaryScore = 100;

            // The value of the fastest possible conscious human reactions
            // are about 0.15. This is the baseline for this score.
            if (keyPressTime <= 0.15)
            {
                return 100;
            }
            // Deduct 1.5 points from the score for every 0.015 units
            else
            {
                // Do not deduct points for the fastest possible time
                keyPressTime -= 0.15;

                // Calculate the amount of 0.015 units the click was from the center and deduct points
                temporaryScore -= 1 * (keyPressTime / 0.015);
                // In the situation that the player performed very poorly, ensure that score is not
                // less than 0
                if (temporaryScore < 0)
                {
                    temporaryScore = 0;
                }

                return (int)temporaryScore;

            }

        }

        //ImageHit Helper functions:
        //-------------------------------------------------------
        /// <summary>
        /// Get the seletive visual ability score for the Image Hit mini-game and store it
        /// </summary>
        /// <returns> The selective visual score for the image hit game </returns>
        public static float GetScore()     
        {
            if (imagehitData == null || imagehitData.Rounds == null)
            {
                return 0;
            }
            
            int id = imagehitData.Rounds.Count-1;

            float score = 0;

            if (id >= 0 && id < imagehitData.Rounds.Count)   
            {
                float onScore = GetKeyNum(id);

                List<ImageHitRound> round12 = imagehitData.Rounds[id];  
                for (int i = 0; i < round12.Count; i++)  
                {
                    score += GetScoreFromOneImage(round12[i], onScore);  
                }
            }
            else
            {
                return 0;
            }

            return score;
        }

         /// <summary>
        /// Get the seletive visual ability score each image and store it
        /// </summary>
        /// <param name="tmp"> the data of the game</param>
        /// <returns> The selective visual score for the image hit game </returns>
        public static float GetScoreFromOneImage(ImageHitRound tmp, float oneKeyScore)   
        {
            if(tmp==null)
            {
                return 0;
            }
            
            if(!tmp.isKeyPressed) // Only when the space bar is pressed, this ability score can count
            {
                return 0;
            }

            float keyPressTime = tmp.keyPressTime;
            float  level = 0;
            // In the case of pressing the space, the basic score is 0 points. 
            // If the player presses the space n times, if it is correct to press the space, 
            // each response time is less than 0.5 seconds to get 100/npoints,
            // 0.5-1 second to get 100/n*85% points, 1-1.5 seconds to get 100/n *75% points, 
            // 1.5-2 seconds to get 100/n*60% points, 2-2.5 seconds to get 100/n*45% points, 2.5-3 seconds to get 100/n*30% points. 
            // In the case of an error by pressing the space, the score will be reduced by two levels according to the correct situation, 
            // that is, 100/n*75% points will be obtained within 0.5 seconds, and so on. In the case of an error, 
            // the reaction time of more than 2 seconds will not be scored.
            if (tmp.isCorrectlyIdentified)  
            {
                if (keyPressTime <= 0.5f)
                {
                    level = 1f;
                }
                else if (keyPressTime <= 1.0f)
                {
                    level = 0.85f;
                }
                else if (keyPressTime <= 1.5f)
                {
                    level = 0.75f;
                }
                else if (keyPressTime <= 2.0f)
                {
                    level = 0.6f;
                }
                else if (keyPressTime <= 2.5f)
                {
                    level = 0.45f;
                }
                else if (keyPressTime <= 3.0f)
                {
                    level = 0.3f;
                }
            }
            else 
            {
                if (keyPressTime <= 0.5f)
                {
                    level = 0.75f;
                }
                else if (keyPressTime <= 1.0f)
                {
                    level = 0.6f;
                }
                else if (keyPressTime <= 1.5f)
                {
                    level = 0.45f;
                }
                else if (keyPressTime <= 2.0f)
                {
                    level = 0.3f;
                }
            }

            float fscore = level * oneKeyScore;  
            return fscore;
        }

        /// <summary>
        /// Get the number of time when the player presses the space bar
        /// </summary>
        /// <param name="id"> the id of the image </param>
        /// <returns>the number of time when the player presses the space bar</returns>
        public static float GetKeyNum(int id)   
        {
            int keyCount = 0;  
            float oneKeyScore = 0;


            if(imagehitData==null)
            {
                return 0;
            }


            if (id >= 0 && id < imagehitData.Rounds.Count)   
            {
                int RoundNum = imagehitData.Rounds[id].Count;

                if(RoundNum>10)
                {
                    List<ImageHitRound> round1 = imagehitData.Rounds[id].GetRange(0, 10);  
                    List<ImageHitRound> round2 = imagehitData.Rounds[id].GetRange(10, RoundNum - 10);  

                    for (int i = 0; i < round1.Count; i++)  
                    {
                        if (round1[i].isKeyPressed) 
                        {

                            keyCount++;
                        }
                    }
                    for (int i = 0; i < round2.Count; i++)    
                    {
                        if (round2[i].isKeyPressed)
                        {
                            keyCount++;
                        }
                    }

                    if (keyCount > 0)
                    {
                        oneKeyScore = 100.0f / keyCount;  
                    }
                    else
                    {
                        oneKeyScore = 0;
                    }

                }
            }
            
            return oneKeyScore;
        }





        

    }
}