using Helper;
using Storage;
using Games;
using UI;
using System.Collections.Generic;

namespace Measurement
{
    /// <summary>
    /// This module implements [PointingMeasure Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#pointingmeasure-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class PointingMeasure
    {
        // BalloonsStorage datatype variable to hold the data from the balloons mini-game
        // notYetPlayBalloons represents if the game Balloons has been played
        private static bool notYetPlayBalloons = Globals.isBalloonsButtonOn;
        // If it's not played(notYetPlayBalloons == true), declare an empty BalloonsStorage object;
        // Else, fill in with gameplay data 
        public static BalloonsStorage balloonsData = notYetPlayBalloons ?
            new BalloonsStorage(){ Rounds = new List<BalloonsRound>()}
            : Balloons.GetGameplayData();

        // The variable to hold the score of the Balloons game.
        private static int balloonsScore = 0;

        // The datatype to hold sub-score values for abilities:
        public static SubscoreStorage subScoreBalloons = new SubscoreStorage();

        /// <summary>
        /// Calculates the pointinging score of a player in the Balloons mini-game
        /// by taking the average of the player's time to click, distance of 
        /// successful click, number of clicks in a round, and the size of the 
        /// successful click
        /// </summary>
        /// <returns>The pointinging score of the player in the Balloons mini-game.
        /// </returns>
        /// <remarks>The returned value is ≥ 0.</remarks>
        public static void EvaluateBalloonsScore()
        {
            double balloonClickScore = 0;
            double balloonDistanceScore = 0;
            double multipleClicksScore = 0;
            double balloonSuccessClickScore = 0;
            double distanceFromCenter = 0;
            double balloonRoundsPlayedMultiplier = 0;
            Position2D destinationPoint;
            Position2D clickPosition;
            int numberOfRounds = balloonsData.Rounds.Count;

            // Evaluate the score based on round record:
            foreach (BalloonsRound balloonsRound in balloonsData.Rounds)
            {
                balloonDistanceScore = 0;
                // Calculate score depending on amount of time it took to click on the balloon
                balloonClickScore = MatchDestinationClickTimeToScoreBalloons(balloonsRound.DestinationClickTime);

                // Calculate score depending on number of clicks before successful click
                multipleClicksScore = MatchMultipleClicksToScore(balloonsRound.Clicks.Count + 1);

                // x,y position of center of destination balloon
                destinationPoint = balloonsRound.DestinationPoint;
                // x,y, position of clickPosition set to successful click position
                clickPosition = balloonsRound.SuccessClickPoint;
                // Calculate score depending on the distance that the successful click was from the center of the balloon
                distanceFromCenter = Position2D.Distance2D(clickPosition, destinationPoint);
                // Add score to balloonDistanceScore
                balloonDistanceScore += MatchDistanceToScore(distanceFromCenter);
                
                // Calculate score depending on the distance of unsuccessful clicks were from the center of the balloon
                foreach(TimeAndPosition unsuccessfulClick in balloonsRound.Clicks)
                {
                    // x,y position of unsuccessfulclick
                    clickPosition = unsuccessfulClick.Position;
                    distanceFromCenter = Position2D.Distance2D(clickPosition, destinationPoint);
                    balloonDistanceScore += MatchDistanceToScore(distanceFromCenter);
                }

                // Find the average distance score
                balloonDistanceScore = balloonDistanceScore / (balloonsRound.Clicks.Count + 1);

                // Find the average of balloonClickScore, balloonDistanceScore and multipleClicksScore
                balloonSuccessClickScore += ( balloonDistanceScore + multipleClicksScore + balloonClickScore)/3;
            }

            if (numberOfRounds == 0)
            {
                balloonsScore = 0;
            }

            else
            {
                // Calculate the average score in the entire game
                if (numberOfRounds == 0)
                {
                    balloonClickScore = 0;
                }
                else
                {
                    balloonsScore = (int)(balloonSuccessClickScore / numberOfRounds);
                }

                // To ensure that the player has a consistent and accurate score,
                // Their current score is multiplied by a value depending on the number of rounds played
                balloonRoundsPlayedMultiplier = MatchRoundsPlayedToScore(numberOfRounds);
                balloonsScore = (int)(balloonsScore * balloonRoundsPlayedMultiplier);
            }

            // Update the subScore record for (Flciking, Balloons):
            UpdateSubScoreForBalloons();
        }

        /// <summary>
        /// Gets the pointinging score record of a player in the Balloons mini-game.
        /// </summary>
        /// <returns>The pointinging score of a player in the Balloons mini-game.
        /// </returns>
        public static SubscoreStorage GetSubScoreForBalloons()
        {
            return subScoreBalloons;
        }

        //Helper functions start:
        //-----------------------------------------------------------------

        /// <summary>
        /// Create an subscore object to store the score and weight for
        /// the balloons minigame pointing score
        /// </summary>
        /// <remarks><paramref name="newScore"/> ≥ 0.</remarks>
        private static void UpdateSubScoreForBalloons()
        {
            // The name of the cognitive/motor ability
            subScoreBalloons.AbilityName = AbilityName.POINTING;
            // The name of the game related to the cognitive/motor ability
            subScoreBalloons.GameName = GameName.BALLOONS;
            // The subscore calculated from the Balloons game
            subScoreBalloons.Score = balloonsScore;
            // The weightage of the game in relation to the ability
            subScoreBalloons.Weight = 2;
        }

        /// <summary>
        /// Determines the player's click score based on the time
        /// that they spent to click on the balloon
        /// </summary>
        /// <param name="clickTime">Time for player to move and click on the balloon</param>
        /// <returns>A score out of 100</returns>
        /// <remarks>https://www.ane.pl/pdf/4320.pdf is the site referenced for reaction time score basis</remarks>
        private static double MatchDestinationClickTimeToScoreBalloons(double clickTime)
        {
            // Variable to hold the score temporarily
            double temporaryScore = 100;

            // The value of the fastest possible conscious human reactions
            // are about 0.15. This is the baseline for this score.
            if (clickTime <= 0.15)
            {
                return 100;
            }
            else // Deduct 1.5 points from the score for every 0.015 units between click position and balloon center
            {
                // Do not deduct points for the fastest possible time
                clickTime -= 0.15; 

                // Calculate the amount of 0.015 units the click was from the center and deduct points
                temporaryScore -= 1.5 * (clickTime / 0.015);

                // In the situation that the player performed very poorly, ensure that score is not
                // less than 0
                if (temporaryScore < 0)
                {
                    temporaryScore = 0;
                }
                else if (temporaryScore > 100)
                {
                    temporaryScore = 100;
                }
                return temporaryScore;
            }
        }

        /// <summary>
        /// Calculates a score depending on how close the successful click
        /// was from the center of the balloon
        /// </summary>
        /// <param name="closenessToCenter">Distance between center of
        /// balloon and the successful click</param>
        /// <returns></returns>
        private static double MatchDistanceToScore(double closenessToCenter)
        {
            // Variable to hold the score temporarily
            double temporaryScore = 100;

            // Testing of the game showed that a value of less than 3 is barely
            // distinguishable from the center, and will be our baseline for this score
            if (closenessToCenter <= 3)
            {
                return 100;
            }
            else // For every 3 units that the player is from the center, deduct 5 points. 
            {
                closenessToCenter -= 3;
                temporaryScore -= 1.2 * closenessToCenter;
                if (temporaryScore < 0)
                {
                    temporaryScore = 0;
                }
                return temporaryScore;
            }
        }

        /// <summary>
        /// Calculates the accuracy of the clicks, if
        /// there is more than one click, with the other
        /// clicks being unsuccessful
        /// </summary>
        /// <param name="numOfClicks"></param>
        /// <returns>A score out of 100 indicating score based on number of clicks</returns>
        private static double MatchMultipleClicksToScore(int numOfClicks)
        {
            if (numOfClicks == 1)
            {
                return 100;
            }
            else if (numOfClicks == 2)
            {
                return 95;
            }
            else if (numOfClicks == 3)
            {
                return 90;
            }
            else
            {
                return 85;
            }
        }

        /// <summary>
        /// Calculates a consistency score depending on the
        /// number of rounds the player played. If a player
        /// does not play enough rounds, their score may inaccurate,
        /// or too high, as the earlier balloons are easier to click on.
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private static double MatchRoundsPlayedToScore(int roundsPlayed)
        {
            if (roundsPlayed < 10)
            {
                return 0.4;
            }
            else if (roundsPlayed < 15)
            {
                return 0.6;
            }
            else if (roundsPlayed < 20)
            {
                return 0.8;
            }
            else if (roundsPlayed < 23) //a normal player could be able to play 23-25 rounds
            {
                return 0.9;
            }
            else
            {
                return 1;
            }
        }
    }
}