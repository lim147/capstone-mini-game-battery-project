using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using Measurement;
using Storage;
using Helper;
using Games;

namespace EditModeTests
{
    namespace MeasurementTests
    {
        public class VisuospatialSketchpadMeasureTests
        {
            // Variable to hold predetermined round information for rounds in the Squares mini-game
            SquaresRound squareRound = new SquaresRound();
            SquaresRound squareRound_misMatch = new SquaresRound();
            SquaresRound squareRound_gap = new SquaresRound();
            SquaresRound squareRound_moreCorrectSquares = new SquaresRound();

            // Variable to hold predetermined round information for a different recall times
            SquaresRound squareRound_3 = new SquaresRound();
            SquaresRound squareRound_3_3 = new SquaresRound();
            SquaresRound squareRound_3_6 = new SquaresRound();
            SquaresRound squareRound_3_9 = new SquaresRound();
            SquaresRound squareRound_4_2 = new SquaresRound();

            // Number of squares
            SquaresRound squareRound_6Squares = new SquaresRound();
            SquaresRound squareRound_8Squares = new SquaresRound();

            [SetUp]
            public void SetUp()
            {
                // Example squares
                IndexAndPosition IndexAndPositionIndex0 = new IndexAndPosition(0, new Position2D(0, 0));
                IndexAndPosition IndexAndPositionIndex1 = new IndexAndPosition(1, new Position2D(1, 1));
                IndexAndPosition IndexAndPositionIndex2 = new IndexAndPosition(2, new Position2D(2, 2));


                // Set data for regular round without mismatch in Squares mini-game
                squareRound.HighlightedSquares = new List<IndexAndPosition>();
                squareRound.HighlightedSquares.Add(IndexAndPositionIndex0);
                squareRound.HighlightedSquares.Add(IndexAndPositionIndex1);

                squareRound.RecalledSquares = new List<IndexAndPosition>();
                squareRound.RecalledSquares.Add(IndexAndPositionIndex0);
                squareRound.RecalledSquares.Add(IndexAndPositionIndex1);

                squareRound.RecallTime = 5f;
                squareRound.SquareHighlightInterval = 0.7f;
                squareRound.SquareHighlightInterval = 0.7f;

                // Set data for round with a mismatch in Squares mini-game
                squareRound_misMatch.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_misMatch.HighlightedSquares.Add(IndexAndPositionIndex0);
                squareRound_misMatch.HighlightedSquares.Add(IndexAndPositionIndex1);

                squareRound_misMatch.RecalledSquares = new List<IndexAndPosition>();
                squareRound_misMatch.RecalledSquares.Add(IndexAndPositionIndex0);
                squareRound_misMatch.RecalledSquares.Add(IndexAndPositionIndex2);

                squareRound_misMatch.RecallTime = 5f;
                squareRound_misMatch.SquareHighlightInterval = 0.7f;
                squareRound_misMatch.SquareHighlightInterval = 0.7f;

                // Set data for round with a gap in Squares mini-game
                squareRound_gap.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_gap.HighlightedSquares.Add(IndexAndPositionIndex0);
                squareRound_gap.HighlightedSquares.Add(IndexAndPositionIndex1);

                squareRound_gap.RecalledSquares = new List<IndexAndPosition>();
                squareRound_gap.RecalledSquares.Add(IndexAndPositionIndex0);

                squareRound_gap.RecallTime = 5f;
                squareRound_gap.SquareHighlightInterval = 0.7f;
                squareRound_gap.SquareHighlightInterval = 0.7f;

                // Set data for round with more correct squares in Squares mini-game
                squareRound_moreCorrectSquares.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_moreCorrectSquares.RecalledSquares = new List<IndexAndPosition>();
                for (int g = 0; g < 7; g++)
                {
                    squareRound_moreCorrectSquares.HighlightedSquares.Add(IndexAndPositionIndex0);
                    squareRound_moreCorrectSquares.RecalledSquares.Add(IndexAndPositionIndex0);
                }
                squareRound_moreCorrectSquares.RecallTime = 5f;
                squareRound_moreCorrectSquares.SquareHighlightInterval = 0.7f;
                squareRound_moreCorrectSquares.SquareHighlightInterval = 0.7f;

                // Set data for rounds with different recall times

                squareRound_3.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_3.RecalledSquares = new List<IndexAndPosition>();
                squareRound_3.RecallTime = 3.5f;
                squareRound_3.SquareHighlightInterval = 0.7f;
                squareRound_3.SquareHighlightInterval = 0.7f;

                squareRound_3_3.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_3_3.RecalledSquares = new List<IndexAndPosition>();
                squareRound_3_3.RecallTime = 3.8f;
                squareRound_3_3.SquareHighlightInterval = 0.7f;
                squareRound_3_3.SquareHighlightInterval = 0.7f;

                squareRound_3_6.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_3_6.RecalledSquares = new List<IndexAndPosition>();
                squareRound_3_6.RecallTime = 4.1f;
                squareRound_3_6.SquareHighlightInterval = 0.7f;
                squareRound_3_6.SquareHighlightInterval = 0.7f;

                squareRound_3_9.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_3_9.RecalledSquares = new List<IndexAndPosition>();
                squareRound_3_9.RecallTime = 4.4f;
                squareRound_3_9.SquareHighlightInterval = 0.7f;
                squareRound_3_9.SquareHighlightInterval = 0.7f;

                squareRound_4_2.HighlightedSquares = new List<IndexAndPosition>();
                squareRound_4_2.RecalledSquares = new List<IndexAndPosition>();
                squareRound_4_2.RecallTime = 4.8f;
                squareRound_4_2.SquareHighlightInterval = 0.7f;
                squareRound_4_2.SquareHighlightInterval = 0.7f;

                for (int g = 0; g < 5; g++)
                {
                    squareRound_3.HighlightedSquares.Add(IndexAndPositionIndex0);
                    squareRound_3.RecalledSquares.Add(IndexAndPositionIndex0);

                    squareRound_3_3.HighlightedSquares.Add(IndexAndPositionIndex0);
                    squareRound_3_3.RecalledSquares.Add(IndexAndPositionIndex0);

                    squareRound_3_6.HighlightedSquares.Add(IndexAndPositionIndex0);
                    squareRound_3_6.RecalledSquares.Add(IndexAndPositionIndex0);

                    squareRound_3_9.HighlightedSquares.Add(IndexAndPositionIndex0);
                    squareRound_3_9.RecalledSquares.Add(IndexAndPositionIndex0);

                    squareRound_4_2.HighlightedSquares.Add(IndexAndPositionIndex0);
                    squareRound_4_2.RecalledSquares.Add(IndexAndPositionIndex0);

                    squareRound_6Squares.HighlightedSquares = new List<IndexAndPosition>();
                    squareRound_6Squares.RecalledSquares = new List<IndexAndPosition>();
                    squareRound_6Squares.RecallTime = 3.5f;
                    squareRound_6Squares.SquareHighlightInterval = 0.7f;
                    squareRound_6Squares.SquareHighlightInterval = 0.7f;

                    squareRound_8Squares.HighlightedSquares = new List<IndexAndPosition>();
                    squareRound_8Squares.RecalledSquares = new List<IndexAndPosition>();
                    squareRound_8Squares.RecallTime = 3.5f;
                    squareRound_8Squares.SquareHighlightInterval = 0.7f;
                    squareRound_8Squares.SquareHighlightInterval = 0.7f;

                }

                for (int p = 0; p < 6; p++)
                {
                    squareRound_6Squares.HighlightedSquares.Add(IndexAndPositionIndex0);
                    squareRound_6Squares.RecalledSquares.Add(IndexAndPositionIndex0);

                    squareRound_8Squares.HighlightedSquares.Add(IndexAndPositionIndex0);
                    squareRound_8Squares.RecalledSquares.Add(IndexAndPositionIndex0);
                }
                squareRound_8Squares.HighlightedSquares.Add(IndexAndPositionIndex0);
                squareRound_8Squares.RecalledSquares.Add(IndexAndPositionIndex0);

                squareRound_8Squares.HighlightedSquares.Add(IndexAndPositionIndex0);
                squareRound_8Squares.RecalledSquares.Add(IndexAndPositionIndex0);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the <code>EvaluateSquaresScore</code> function
                and ensure that it returns the correct value.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_EvaluateSquaresScoreFunctionCalledOnEmptyInput_SquareSubScoreReturnsBaseScore()
            {
                // Initialize the measurement module
                InitializeVisuospatialMeasure();

                // Call the evaluate score function
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();

                yield return null;

                // SquaresStorage datatype variable to hold the data from the squares mini-game
                SquaresStorage squaresData = Squares.GetGameplayData();

                // Ensure that SubscoreSquares values are correctly set
                Assert.AreEqual(AbilityName.VISUOSPATIAL_SKETCHPAD, VisuospatialSketchpadMeasure.subScoreSquares.AbilityName);
                Assert.AreEqual(GameName.SQUARES, VisuospatialSketchpadMeasure.subScoreSquares.GameName);
                Assert.AreEqual(0, VisuospatialSketchpadMeasure.subScoreSquares.Score);
                Assert.AreEqual(2, VisuospatialSketchpadMeasure.subScoreSquares.Weight);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit Test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Test the <code>GetSubScoreForSquares</code> function
                and ensure that it returns the correct value.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_GetSubScoreForSquaresCalled_SquareSubScoreIsCorrect()
            {
                int squaresScore = 0;
                // Set the subscoreSquare values in the measurement module
                VisuospatialSketchpadMeasure.subScoreSquares.AbilityName = AbilityName.VISUOSPATIAL_SKETCHPAD;
                VisuospatialSketchpadMeasure.subScoreSquares.GameName = GameName.SQUARES;
                VisuospatialSketchpadMeasure.subScoreSquares.Score = squaresScore;
                VisuospatialSketchpadMeasure.subScoreSquares.Weight = 2;

                // Call the GetSubScoreForSquares function and store the value
                SubscoreStorage actualSubscoreStorage = VisuospatialSketchpadMeasure.GetSubScoreForSquares();
                SubscoreStorage expectedSubscoreStorage = VisuospatialSketchpadMeasure.subScoreSquares;

                yield return null;

                // Ensure that the SubscoreStorage returned by the function matches what was set
                Assert.AreEqual(expectedSubscoreStorage.AbilityName, actualSubscoreStorage.AbilityName);
                Assert.AreEqual(expectedSubscoreStorage.GameName, actualSubscoreStorage.GameName);
                Assert.AreEqual(expectedSubscoreStorage.Score, actualSubscoreStorage.Score);
                Assert.AreEqual(expectedSubscoreStorage.Weight, actualSubscoreStorage.Weight);
            }


            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Test that number of rounds played affects the score. Keep in mind that if the module calculates a negative score, it is rounded up to 0. If the round information is exactly the same, but the only difference is the number of rounds, they may end up having the same score of 0. As well, the score is affected by the number of rounds up to 8 rounds. 
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator TestSquaresScoreNumberOfRounds()
            {
                int scoreForLowerNumOfRounds;
                int scoreForHigherNumOfRounds;
                int numOfRoundsInThisTestCase = 2;

                // Score for 1 round
                InitializeVisuospatialMeasure();
                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreForLowerNumOfRounds = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Make sure that the scores for rounds 2 - 8 increase depending on the number of rounds
                for (int j = 2; j < 8; j++)
                {
                    InitializeVisuospatialMeasure();
                    for (int k = 0; k < numOfRoundsInThisTestCase; k++)
                    {
                        VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                    }

                    // Call the EvaluateSquaresScore function
                    VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                    // Get the score
                    scoreForHigherNumOfRounds = VisuospatialSketchpadMeasure.subScoreSquares.Score;
                    yield return null;
                    // Test that fewer rounds played should result in a lower score
                    Assert.IsTrue(scoreForLowerNumOfRounds <= scoreForHigherNumOfRounds, "Number of rounds: " + numOfRoundsInThisTestCase + " Low rounds score: " + scoreForLowerNumOfRounds + " High rounds score: " + scoreForHigherNumOfRounds);

                    // Set variables for next round comparison
                    scoreForLowerNumOfRounds = scoreForHigherNumOfRounds;
                    numOfRoundsInThisTestCase += 1;
                }
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Test that a mismatch in the recall sequence results in a lower score. Both cases have the same number of rounds, but one case has a round with a mismatch.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator TestSquaresScoreMismatch()
            {
                int scoreWithMisMatch;
                int scoreWithoutMisMatch;

                // Score for round without mismatch
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 6; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreWithoutMisMatch = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Score for round with mismatch
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }
                // Add one round with mismatch
                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_misMatch);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreWithMisMatch = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                yield return null;
                Assert.IsTrue(scoreWithMisMatch < scoreWithoutMisMatch);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Test that a gap in the recall sequence results in a lower score. Both cases have the same number of rounds, but one case has a round with a gap.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator TestSquaresScoreGap()
            {
                int scoreWithGap;
                int scoreWithoutGap;

                // Score for round without gap
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 6; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreWithoutGap = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Score for round with gap
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }
                // Add one round with gap
                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_gap);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreWithGap = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                yield return null;
                Assert.IsTrue(scoreWithGap < scoreWithoutGap);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Test that recall time in the recall sequence affects the score. Both cases have the same number of rounds, but one case has a round with a lower recall time.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator TestSquaresRecallTime()
            {
                int scoreForRecallTime3;
                int scoreForRecallTime3_3;
                int scoreForRecallTime3_6;
                int scoreForRecallTime3_9;
                int scoreForRecallTime4_2;
                int scoreForRecallTime5;

                // Score for round with recall time 3
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }

                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_3);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreForRecallTime3 = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Score for round with recall time 3.3
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }

                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_3_3);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreForRecallTime3_3 = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Score for round with recall time 3.6
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }

                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_3_6);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreForRecallTime3_6 = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Score for round with recall time 3.9
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }

                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_3_9);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreForRecallTime3_9 = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Score for round with recall time 4.2
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }

                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_4_2);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreForRecallTime4_2 = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Score for round with recall time 5
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }
                // Add one round with the recall time
                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreForRecallTime5 = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                yield return null;

                Assert.IsTrue(scoreForRecallTime3 > scoreForRecallTime3_3, "3 seconds: " + scoreForRecallTime3 + " 3.3 seconds: " + scoreForRecallTime3_3);
                Assert.IsTrue(scoreForRecallTime3_3 > scoreForRecallTime3_6, "3.3 seconds: " + scoreForRecallTime3_3 + " 3.6 seconds: " + scoreForRecallTime3_6);
                Assert.IsTrue(scoreForRecallTime3_6 > scoreForRecallTime3_9, "3.6 seconds: " + scoreForRecallTime3_6 + " 3.9 seconds: " + scoreForRecallTime3_9);
                Assert.IsTrue(scoreForRecallTime3_9 > scoreForRecallTime4_2,"3 seconds: " + scoreForRecallTime3 + ", 3.3 seconds: " + scoreForRecallTime3_3 + ", 3.6 seconds: " +  scoreForRecallTime3_6 +  ", 3.9 seconds: " + scoreForRecallTime3_9 + ", 4.2 seconds " + scoreForRecallTime4_2 + ", 5 seconds " + scoreForRecallTime5);
                Assert.IsTrue(scoreForRecallTime4_2 > scoreForRecallTime5, "4.2 seconds: " + scoreForRecallTime4_2 + " 5 seconds " + scoreForRecallTime5);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Test that getting a correct recall sequence on a higher number of highlighted squares results in a higher score. Both cases have the same number of rounds, but one case has a round with more squares recalled correctly.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator TestSquaresNumberOfCorrectHighlightedSquaresInLargeNumberOfHighlighted()
            {
                int scoreWithLowNumberOfCorrectSquares;
                int scoreWithHighNumberOfCorrectSquares;

                // Score for round with correct squares, but lower number of squares
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreWithLowNumberOfCorrectSquares = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                // Score for round with correct squares, but higher number of squares
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 4; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }
                // Add one round with more correct squares
                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_moreCorrectSquares);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                scoreWithHighNumberOfCorrectSquares = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                yield return null;
                Assert.IsTrue(scoreWithLowNumberOfCorrectSquares < scoreWithHighNumberOfCorrectSquares);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Test that the maximum score that can be returned is 100 with perfect rounds.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator TestMaxScore100()
            {
                int actualScore;

                // Score for round with high number of correct squares
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 7; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_moreCorrectSquares);
                }
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                actualScore = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                yield return null;
                Assert.IsTrue(actualScore == 100, "Score: " + actualScore);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Test that the maximum score that can be returned is 100 with perfect rounds.
                </li>
            </ul>
        ")]
            [UnityTest]
            public IEnumerator NumberOfRoundsCorrectScoreIncrease()
            {
                int score6Squares, score8Squares;
                yield return null;
                
                // 6 squares
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }
                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_6Squares);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                score6Squares = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                yield return null;

                // 8 squares
                InitializeVisuospatialMeasure();
                for (int a = 0; a < 5; a++)
                {
                    VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound);
                }
                VisuospatialSketchpadMeasure.squaresData.Rounds.Add(squareRound_8Squares);
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
                score8Squares = VisuospatialSketchpadMeasure.subScoreSquares.Score;

                Assert.IsTrue(score6Squares < score8Squares, "6 squares score: " + score6Squares + " 8 squares score: " + score8Squares);
            }

            //------------------------ Helper functions begin ----------------------------
            //------------------------------------------------------------------------

            // Initialize the public variables of the measurement module for testing
            private void InitializeVisuospatialMeasure()
            {
                // Initialize the game data storage in the measurement module
                VisuospatialSketchpadMeasure.squaresData = new SquaresStorage();
                VisuospatialSketchpadMeasure.squaresData.Rounds = new List<SquaresRound>();
                // Initialize the score storage variable in the measurement module
                VisuospatialSketchpadMeasure.subScoreSquares = new SubscoreStorage();
            }
        }
    }
}