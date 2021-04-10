using System.Collections.Generic;
using System;
using Helper;
using Storage;
using Games;
using UI;


namespace Measurement
{
    /// <summary>
    /// This module implements [VisuospatialSketchpadMeasure Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#visuospatialsketchpadmeasure-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class VisuospatialSketchpadMeasure
    {
        // SquaresStorage datatype variable to hold the data from the squares mini-game
        // notYetPlaySquares represents if the game Squares has been played
        private static bool notYetPlaySquares = Globals.isSquaresButtonOn;
        // If it's not played(notYetPlaySquares == true), declare an empty SquaresStorage object;
        // Else, fill in with gameplay data 
        public static SquaresStorage squaresData = notYetPlaySquares ?
            new SquaresStorage() { Rounds = new List<SquaresRound>()}
            : Squares.GetGameplayData();


        // Variable to hold the visuospatial sketchpad ability score from Squares mini-game
        private static int squaresScore = 0;

        // Subscore datatype variable to hold subscores for (visuospatial sketchpad, squares)
        public static SubscoreStorage subScoreSquares = new SubscoreStorage();

        /// <summary>
        /// Evaluate the visuospatial sketchpad ability score from squares mini-game
        /// and updates the subscore datatype with the score
        /// </summary>
        public static void EvaluateSquaresScore()
        {
            if (squaresData is null)
            {
                squaresScore = 0;
            }
            else if (!IsAtLeastOneSquareSelected()) // no square is selected in the whole gameplay
            {
                squaresScore = 0;
            }
            else
            {
                int sumOfTempScore = 0;
                int numOfRounds = squaresData.Rounds.Count;

                foreach (SquaresRound round in squaresData.Rounds)
                {
                    // List of square highlight sequence shown in the round
                    List<IndexAndPosition> highlightedSquares = round.HighlightedSquares;
                    // List of squares that were recalled by the player
                    List<IndexAndPosition> recalledSquares = round.RecalledSquares;
                    int numberOfHighlightedSquares = highlightedSquares.Count;
                    // Amount of time that the player took to give their answer
                    double recallTime = round.RecallTime;

                    // Find the maximum possible score that can be obtained in this round
                    int tempScore = MatchNumOfHighlightedSquaresToMaxScore(numberOfHighlightedSquares);

                    // Reduce player's score depending on the amount of time the player took to give their answer
                    tempScore -= MatchRecallTimeToMarkCutDown(numberOfHighlightedSquares, recallTime);

                    // Reduce player's score depending on the incorrect recalled squares
                    // Give the player bonus mark if the person correctly recreates a long sequence
                    tempScore = CompareAnswerAndRecall(tempScore, highlightedSquares, recalledSquares);

                    // Ensure the score is greater or equal to 0 after the score reduction
                    if (tempScore <= 0)
                    {
                        tempScore = 0;
                    }

                    // Add the score for a round to the running total
                    sumOfTempScore += tempScore;
                }

                // Adjust the score based on the number of rounds completed
                int scoreChange = MatchCountOfRoundToScoreChange(numOfRounds);

                // Find the average of temp scores and the do the adjustment
                int avgTempScore;
                if (numOfRounds == 0)
                {
                    avgTempScore = 0;
                }
                else
                {
                    avgTempScore = (int)(sumOfTempScore / numOfRounds);
                }
                int adjustedScore = avgTempScore + scoreChange;

                // Ensure the score is between 0 and 100 after the score adjustment
                if (adjustedScore <= 0)
                {
                    adjustedScore = 0;
                }

                if (adjustedScore >= 100)
                {
                    adjustedScore = 100;
                }

                squaresScore = adjustedScore;
            }
            // Update the subscore value for Squares
            UpdateSubScoreForSquares();
        }

        /// <summary>
        /// Get the subscore for the Squares game
        /// </summary>
        /// <returns>The Subscore object with the Squares score in it</returns>
        public static SubscoreStorage GetSubScoreForSquares()
        {
            return subScoreSquares;
        }

        //Helper functions start:
        //-----------------------------------------------------------------

        /// <summary>
        /// Update the subscore object with the squares mini-game score
        /// </summary>
        private static void UpdateSubScoreForSquares()
        {
            // Set all related fields for the squares mini-game
            subScoreSquares.AbilityName = AbilityName.VISUOSPATIAL_SKETCHPAD;
            subScoreSquares.GameName = GameName.SQUARES;
            subScoreSquares.Score = squaresScore;
            subScoreSquares.Weight = 2;
        }


        /// <summary>
        /// CompareAnswerAndRecall iterates through HighlightedSquares and RecalledSquares sequences,
        /// computes the gaps and mismatches and does the mark cut down
        /// then, returns the new score
        /// </summary>
        /// <param name="oldScore">Old score</param>
        /// <param name="highlightedSquares">A sequence of index and positions of highlited squares.</param>
        /// <param name="recalledSquares">A sequence of index and positions of recalled squares.</param>
        /// <returns>New computed score.</returns>
        private static int CompareAnswerAndRecall(
            int oldScore,
            List<IndexAndPosition> highlightedSquares,
            List<IndexAndPosition> recalledSquares)
        {
            int numberOfHighlightedSquares = highlightedSquares.Count;

            string answer = "";
            string recall = "";

            // Create a string of the char indices of highlighted squares
            foreach (IndexAndPosition squareIndexAndPos in highlightedSquares)
            {
                int intIndex = squareIndexAndPos.Index;
                char charIndex = MatchIntIndexToCharIndex(intIndex);
                answer = answer + charIndex;
            }

            // Create a string of the indices of recalled squares
            foreach (IndexAndPosition squareIndexAndPos in recalledSquares)
            {
                int intIndex = squareIndexAndPos.Index;
                char charIndex = MatchIntIndexToCharIndex(intIndex);
                recall = recall + charIndex;
            }

            // Compute string similarity on the above two strings
            StringSimilarity.ComputeSimilarity(answer, recall);

            // Derive the computed strings
            // Computed strings are in char indices
            string computedAnswer = StringSimilarity.GetComputedString1();
            string computedRecall = StringSimilarity.GetComputedString2();

            int newScore = oldScore;

            // Boolean for recording if the player correctly recalls in the current round
            bool recallCorrect = true;

            // Iterate through computed strings and cut down marks:
            int lengthOfComputedAnswer = computedAnswer.Length;

            for (int i = 0; i < lengthOfComputedAnswer; i++)
            {
                // If there is a missing square in the player's answer 
                if (computedAnswer[i] == '-' || computedRecall[i] == '-')
                {
                    newScore -= MatchGapToMarkCutDown(numberOfHighlightedSquares);
                    recallCorrect = false;
                }
                // If there is an incorrect square in the player's answer
                else if (computedAnswer[i] != computedRecall[i])
                {
                    //get the int index based on the char index
                    int answerIntIndex = MatchCharIndexToIntIndex(computedAnswer[i]);
                    int recallIntIndex = MatchCharIndexToIntIndex(computedRecall[i]);

                    int markCutDown = MatchMismatchToMarkCutDown(
                        answerIntIndex, recallIntIndex,
                        highlightedSquares, recalledSquares);

                    newScore -= markCutDown;
                    recallCorrect = false;
                }
            }

            // Check to see if player is eligible to get a bonus mark
            int bonusMark = GetBonusMark(numberOfHighlightedSquares, recallCorrect);
            newScore += bonusMark;

            return newScore;
        }

        /// <summary>
        /// MatchRecallTimeToMarkCutDown calculates the mark cutdown based on recall time and
        /// number of highlighted squares for each round
        /// </summary>
        /// <param name="numOfHighlightedSquares">Number of highlighted squares.</param>
        /// <param name="recallTime">Amount of time the player took to recreate the sequence.</param>
        /// <returns>Mark deduction.</returns>
        private static int MatchRecallTimeToMarkCutDown(int numOfHighlightedSquares, double recallTime)
        {
            // The expected time for recalling the sequence:
            // Each square takes 0.5s to click, leaving 1.0s to click the done button
            double expectedTime = numOfHighlightedSquares * 0.5 + 1.0;

            double diff = recallTime - expectedTime;

            // Any additional time spent will be used to deduct marks from the score
            if (diff <= 0)
            {
                return 0;
            }
            else if (diff <= 0.3)
            {
                return 5;
            }
            else if (diff <= 0.6)
            {
                return 10;
            }
            else if (diff <= 0.9)
            {
                return 15;
            }
            else if (diff <= 1.2)
            {
                return 20;
            }
            else
            {
                return 30;
            }

        }

        /// <summary>
        /// MatchNumOfHighlightedSquaresToMaxScore calculates the highest score the player is able to
        /// get in the current round
        /// </summary>
        /// <param name="numOfHighlightedSquares">Number of highlighted squares.</param>
        /// <returns>Max score the player is able to get.</returns>
        private static int MatchNumOfHighlightedSquaresToMaxScore(int numOfHighlightedSquares)
        {
            if (numOfHighlightedSquares <= 3)
            {
                return 60;
            }
            else if (numOfHighlightedSquares == 4)
            {
                return 70;
            }
            else if (numOfHighlightedSquares == 5)
            {
                return 80;
            }
            else if (numOfHighlightedSquares == 6)
            {
                return 90;
            }
            else //numOfHighlightedSquares >= 6
            {
                return 100;
            }
        }

        /// <summary>
        /// GetBonusMark calculates the bonus mark a player is able to get in the round.
        /// </summary>
        /// <param name="numOfHighlightedSquares">Number of highlighted squares.</param>
        /// <param name="recallCorrect">If the player recalls the sequence correctly.</param>
        /// <returns>bonus marks</returns>
        /// <remarks>A player could get bonus marks if the person recreates a long sequence correctly.</remarks>
        private static int GetBonusMark(int numOfHighlightedSquares, bool recallCorrect)
        {
            // If the player correctly recreate a sequence of length > 5, they get a bonus to their score
            if (numOfHighlightedSquares > 5 && recallCorrect == true)
            {
                if (numOfHighlightedSquares == 6)
                {
                    return 5;
                }
                if (numOfHighlightedSquares == 7)
                {
                    return 10;
                }
                else
                {
                    return 20;
                }

            }
            else // Player either did not recreate a sequence correctly, or the length is not long enough
            {
                return 0;
            }
        }

        /// <summary>
        /// MatchCountOfRoundToScoreChange calculates a score deduction based on number of rounds
        /// completed by the player.
        /// </summary>
        /// <param name="countOfRound">Number of rounds played by the player.</param>
        /// <returns>Score deduction</returns>
        private static int MatchCountOfRoundToScoreChange(int countOfRound)
        {
            if (countOfRound == 0) // No round was played
            {
                return -100;
            }
            else if (countOfRound == 1) // Number of squares = 3
            {
                return -60;
            }
            else if (countOfRound == 2)// Number of squares = 4
            {
                return -50;
            }
            else if (countOfRound == 3)// Number of squares = 5
            {
                return -40;
            }
            else if (countOfRound == 4)// Number of squares = 6
            {
                return -30;
            }
            else if (countOfRound == 5)// Number of squares = 7
            {
                return -10;
            }
            else if (countOfRound == 6)// Number of squares = 8(is in normal within 55 seconds limit)
            {
                return 0;
            }
            else
            {
                return 5;
            }
        }

        /// <summary>
        /// MatchGapToMarkCutDown calculates the mark deduction for a gap (when the player misses a square).
        /// </summary>
        /// <param name="numberOfHighlightedSquares">Number of highlighted squares.</param>
        /// <returns>Score reduction for a missing square in the player's recall sequence.</returns>
        /// <remarks>The cost of gap differs in each level.</remarks>
        private static int MatchGapToMarkCutDown(int numberOfHighlightedSquares)
        {
            int maxScore = MatchNumOfHighlightedSquaresToMaxScore(numberOfHighlightedSquares);
            int costOfAGap = (int)(maxScore / numberOfHighlightedSquares);
            return costOfAGap;
        }

        /// <summary>
        /// MatchMismatchToMarkCutDown calculates the mark cutdown for a mismatch between
        /// two squares represented by their indices.
        /// </summary>
        /// <param name="numberOfHighlightedSquares">Number of highlighted squares</param>
        /// <param name="answerIndex">Index of correct square</param>
        /// <param name="recallIndex">Index of wrongly recalled square</param>
        /// <param name="highlightedSquares">Highlighted square sequence</param>
        /// <param name="recalledSquares">Recalled square dequence</param>
        /// <returns>mark cutdown</returns>
        private static int MatchMismatchToMarkCutDown(
            int answerIndex,
            int recallIndex,
            List<IndexAndPosition> highlightedSquares,
            List<IndexAndPosition> recalledSquares)
        {
            int numberOfHighlightedSquares = highlightedSquares.Count;

            // Get the position for answerIndex
            Position2D answerPos = MatchIndexToPositionFromAnswer(answerIndex, highlightedSquares);

            // Get the position for recallIndex
            Position2D recallPos = MatchIndexToPositionFromRecall(recallIndex, recalledSquares);

            double distance = Position2D.Distance2D(answerPos, recallPos);

            int costOfMismatch = 8 + (int)(distance / 40);

            // Make sure cost of mismatch < cost of gap
            int costOfGap = MatchGapToMarkCutDown(numberOfHighlightedSquares);

            if (costOfMismatch < costOfGap)
            {
                return costOfMismatch;
            }
            else
            {
                return costOfGap;
            }
        }

        /// <summary>
        /// MatchIndexToPositionFromAnswer finds the position of a highlighted square
        /// based on its index.
        /// </summary>
        /// <param name="indexOfSquare">Index of a square from highlighted square sequence.</param>
        /// <param name="highlightedSquares">Highlighted square sequence.</param>
        /// <returns>The position of the square.</returns>
        private static Position2D MatchIndexToPositionFromAnswer(
            int indexOfSquare,
            List<IndexAndPosition> highlightedSquares)
        {
            Position2D posOfSquare = new Position2D(0, 0);

            foreach (IndexAndPosition squareIndexAndPos in highlightedSquares)
            {
                if (indexOfSquare == squareIndexAndPos.Index)
                {
                    posOfSquare = squareIndexAndPos.Position;
                }
            }
            return posOfSquare;
        }

        /// <summary>
        /// MatchIndexToPositionFromRecall finds the position of a recalled square
        /// based on its index,
        /// </summary>
        /// <param name="indexOfSquare">Index of a square from recalled square sequence,</param>
        /// <param name="recalledSquares">Recalled square sequence,</param>
        /// <returns>The position of the square,</returns>
        private static Position2D MatchIndexToPositionFromRecall(
            int indexOfSquare,
            List<IndexAndPosition> recalledSquares)
        {
            Position2D posOfSquare = new Position2D(0, 0);

            foreach (IndexAndPosition squareIndexAndPos in recalledSquares)
            {
                if (indexOfSquare == squareIndexAndPos.Index)
                {
                    posOfSquare = squareIndexAndPos.Position;
                }
            }
            return posOfSquare;
        }

        /// <summary>
        /// MatchIntIndexToCharIndex matches an integer index to an unique char index.
        /// The mapping follws: 0->a, 1->b, ... 15->p
        /// </summary>
        /// <param name="intIndex">integer index</param>
        /// <returns>char index.</returns>
        /// <remarks>The need for this function is to make sure the index is only one char,
        /// as required by string similarity algorithm.</remarks>
        private static char MatchIntIndexToCharIndex(int intIndex)
        {
            int intA = 'a';
            char charIndex = Convert.ToChar(intIndex + intA);
            return charIndex;
        }

        /// <summary>
        /// MatchCharIndexToIntIndex returns back the integer index by the given char index.
        /// </summary>
        /// <param name="charIndex">char index</param>
        /// <returns>Integer index.</returns>
        private static int MatchCharIndexToIntIndex(char charIndex)
        {
            int intIndex = charIndex - 'a';
            return intIndex;
        }

        /// <summary>
        /// IsAtLeastOneSquareSelected checks if at least one square is selected in the game. If no square
        /// is selected, returns false; otherwise returns true. It's for validating if the player actually
        /// interacts with the game.
        /// </summary>
        /// <returns></returns>
        private static bool IsAtLeastOneSquareSelected()
        {
            int numberOfSquaresSelected = 0;
            foreach (SquaresRound round in squaresData.Rounds)
            {
                numberOfSquaresSelected += round.RecalledSquares.Count;
            }

            bool result = (numberOfSquaresSelected > 0) ? true : false;
            return result;

        }
    }
}
