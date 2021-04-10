using System;
using System.Collections.Generic;
using System.Linq;
using Games;
using Storage;
using UI;

namespace Measurement
{
    /// <summary>
    /// This module implements [TimeToContactMeasurement Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#timetocontactmeasurement-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class TimeToContact
    {
        // notYetPlayCatchTheBall represents if the game Catch The Ball has been played
        private static bool notYetPlayCatchTheBall = Globals.isCatchTheBallButtonOn;
        // notYetPlaySaveOneBall represents if the game Save One Ball has been played
        private static bool notYetPlaySaveOneBall = Globals.isSaveOneBallButtonOn;
        // notYetPlayJudgeTheBall represents if the game Judge The Ball has been played
        private static bool notYetPlayJudgeTheBall = Globals.isJudgeTheBallButtonOn;

        /// The datatype for the raw gameplay data in the Catch The Ball game.
        public static CatchTheBallStorage catchTheBallData = notYetPlayCatchTheBall ?
            new CatchTheBallStorage() { Rounds = new List<CatchTheBallStorage.BallRound>()}
            : CatchTheBall.GetGameplayData();

        /// The datatype for the raw gameplay data in the Save One Ball game.
        public static SaveOneBallStorage saveOneBallData = notYetPlaySaveOneBall ?
            new SaveOneBallStorage() { Rounds = new List<SaveOneBallStorage.BallRound>()}
            : SaveOneBall.GetGameplayData();

        /// The datatype for the raw gameplay data in the Judge The Ball game.
        public static JudgeTheBallStorage judgeTheBallData = notYetPlayJudgeTheBall ?
            new JudgeTheBallStorage() { Rounds = new List<JudgeTheBallStorage.BallRound>()}
            : JudgeTheBall.GetGameplayData();

        // The variable to hold the score of the Catch The Ball game.
        private static int catchTheBallScore = 0;
        // The variable to hold the score of the Save One Ball game.
        private static int saveOneBallScore = 0;
        // The variable to hold the score of the Judge The Ball game.
        private static int judgeTheBallScore = 0;

        /// <summary>
        /// Evaluates the overall score for the Catch The Ball game.
        /// </summary>
        public static void EvaluateCatchTheBallScore()
        {
            if (catchTheBallData.Rounds is null)
            {
                catchTheBallScore = -1;
            }
            else
            {
                catchTheBallScore = (int)CalculateAccuracyForCatchTheBall();
            }
        }

        /// <summary>
        /// Evaluates the overall score for the Save One Ball game.
        /// </summary>
        public static void EvaluateSaveOneBallScore()
        {
            if (saveOneBallData.Rounds is null)
            {
                saveOneBallScore = -1;
            }
            else
            {
                saveOneBallScore = (int)CalculateAccuracyForSaveOneBall();
            }
        }

        /// <summary>
        /// Evaluates the overall score for the Judge The Ball game.
        /// </summary>
        public static void EvaluateJudgeTheBallScore()
        {
            if (judgeTheBallData.Rounds is null)
            {
                judgeTheBallScore = -1;
            }
            else
            {
                judgeTheBallScore = (int)CalculateAccuracyForJudgeTheBall();
            }
        }

        /// <summary>
        /// Generates the subscore storage once the Catch The Ball game is over.
        /// </summary>
        /// <returns>The generated subscore storage for the Catch The Ball game.</returns>
        public static SubscoreStorage GetSubScoreForCatchTheBall()
        {
            return new SubscoreStorage
            {
                AbilityName = AbilityName.TIME_TO_CONTACT,
                GameName = GameName.CATCH_THE_BALL,
                Score = catchTheBallScore,
                Weight = 2
            };
        }

        /// <summary>
        /// Generates the subscore storage once the Save One Ball game is over.
        /// </summary>
        /// <returns>The generated subscore storage for the Save One Ball game.</returns>
        public static SubscoreStorage GetSubScoreForSaveOneBall()
        {
            return new SubscoreStorage
            {
                AbilityName = AbilityName.TIME_TO_CONTACT,
                GameName = GameName.SAVE_ONE_BALL,
                Score = saveOneBallScore,
                Weight = 1
            };
        }

        /// <summary>
        /// Generates the subscore storage once the Judge The Ball game is over.
        /// </summary>
        /// <returns>The generated subscore storage for the Judge The Ball game.</returns>
        public static SubscoreStorage GetSubScoreForJudgeTheBall()
        {
            return new SubscoreStorage
            {
                AbilityName = AbilityName.TIME_TO_CONTACT,
                GameName = GameName.JUDGE_THE_BALL,
                Score = judgeTheBallScore,
                Weight = 1
            };
        }

        /// <summary>
        /// Calculates the absolute error percentage between the the predicted time
        /// to contact and the actual time to contact.
        /// </summary>
        /// <param name="predicted">The predicted time to contact.</param>
        /// <param name="actual">The actual time to contact.</param>
        /// <returns>The absolute error percentage.</returns>
        private static float TimeToContactErrorPercentage(float predicted, float actual)
        {
            return Math.Abs(1 - predicted / actual) * 100;
        }

        /// <summary>
        /// Calculates the accuracy precentage between the predicted time to
        /// contact and the actual time to contact.
        /// </summary>
        /// <param name="predicted">The predicted time to contact.</param>
        /// <param name="actual">The actual time to contact.</param>
        /// <returns>The accuracy percentage.</returns>
        private static float TimeToContactAccuracyPercentage(float predicted, float actual)
        {
            return 100 - TimeToContactErrorPercentage(predicted, actual);
        }

        /// <summary>
        /// Calculates the accuracy percentage for the first round, which
        /// simply calculates the accuracy percentage between the predicted
        /// time to contact and the actual time to contact.
        /// </summary>
        /// <returns>The accuracy percentage of the first round.</returns>
        private static float CalculateAccuracyForCatchTheBall()
        {
            if (catchTheBallData.Rounds.Count == 0)
            {
                return 0;
            }

            return catchTheBallData.Rounds
                .Select(round => TimeToContactAccuracyPercentage(
                    round.PredictedTimeToContact, round.ActualTimeToContact)
                )
                .Average();
        }

        /// <summary>
        /// Calculates the accuracy percentage for the second round. If
        /// the player predicted the wrong ball would arrive first, they receive
        /// an accuracy of zero; otherwise, they receive the accuracy
        /// calculated between the predicted
        /// time to contact and the actual time to contact of the first-arriving
        /// ball.
        /// </summary>
        /// <returns>The accuracy percentage of the second round.</returns>
        private static float CalculateAccuracyForSaveOneBall()
        {
            if (saveOneBallData.Rounds.Count == 0)
            {
                return 0;
            }

            return saveOneBallData.Rounds
                .Select(round => round.DidPredictFirstArrivingBall
                    ? TimeToContactAccuracyPercentage(round.PredictedTimeToContact,
                        Math.Min(round.LeftActualTimeToContact,
                        round.RightActualTimeToContact)
                      )
                    : 0
                )
                .Average();
        }

        /// <summary>
        /// Calculates the accuracy percentage for the third round, which
        /// simply calculates the accuracy percentage between the predicted
        /// time to contact and the actual time to contact.
        /// </summary>
        /// <returns>The accuracy percentage of the third round.</returns>
        private static float CalculateAccuracyForJudgeTheBall()
        {
            if (judgeTheBallData.Rounds.Count == 0)
            {
                return 0;
            }

            return judgeTheBallData.Rounds
                .Select(round => TimeToContactAccuracyPercentage(
                    round.PredictedTimeToContact, round.PlayerBallActualTimeToContact)
                )
                .Average();
        }
    }
} 