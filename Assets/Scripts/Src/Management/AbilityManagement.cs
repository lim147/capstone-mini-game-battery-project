using System.Collections.Generic;
using System;
using System.Linq;
using Measurement;
using Storage;
using UI;

namespace Management
{
    /// <summary>
    /// This module implements [AbilityManagement ADT Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#abilitymanagement-adt-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class AbilityManagement
    {
        //A list of SubscoreStorage object to keep record of the subscore for each ability from each game
        public static List<SubscoreStorage> subScoreSeq = new List<SubscoreStorage>();

        //A list of OverallScoreStorage object to keep record of overall score for each ability
        //from all games that measure the specific ability
        public static List<OverallScoreStorage> overallScoreSeq= new List<OverallScoreStorage>();

        // Check if the Catch The Thief game is played or not
        public static bool notYetPlayBalloons = Globals.isBalloonsButtonOn;
        // Check if the Squares game is played or not
        public static bool notYetPlaySquares = Globals.isSquaresButtonOn;
        // Check if the Catch The Thief game is played or not
        public static bool notYetPlayCTF = Globals.isCTFButtonOn;
        // Check if the Image Hit game is played or not
        public static bool notYetPlayImageHit = Globals.isImageHitButtonOn;
        // Check if the Catch The Ball game is played or not
        public static bool notYetCatchTheBall = Globals.isCatchTheBallButtonOn;
        // Check if the Save One Ball game is played or not
        public static bool notYetSaveOneBall = Globals.isSaveOneBallButtonOn;
        // Check if the Judge The Ball game is played or not
        public static bool notYetJudgeTheBall = Globals.isJudgeTheBallButtonOn;

        /// <summary>
        /// Measure is called when all mini-games are played.
        /// It will call all measurement modules to derive the subscore, overall score as well as level
        /// for abilities.
        /// </summary>
        public static void Measure()
        {
            MeasureAllGames(); // measure all games
            UpdateSubScoreSeq(); //fill subScoreSeq
            CalculateOverallScoreSeq(); //fill overallScoreSeq
        }

        /// <summary>
        /// Getter function for subScoreSeq.
        /// It will be called in BatterySessionManagement module to store the result data into Elasticsearch.
        /// </summary>
        /// <returns>A list of subscores for each ability and each game.</returns>
        public static List<SubscoreStorage> GetSubScoreSeq()
        {
            return subScoreSeq;
        }

        /// <summary>
        /// Getter function for subScoreSeq.
        /// It will be called in BatterySessionManagement module to store the result data into Elasticsearch.
        /// </summary>
        /// <returns>A list of overall scores for each ability.</returns>
        public static List<OverallScoreStorage> GetOverallScoreSeq()
        {
            return overallScoreSeq;
        }

        //Helper functions start
        //---------------------------------------------------------
        //---------------------------------------------------------

        /// <summary>
        /// MeasureAllGames is called when all games end. It will call methods of
        /// evaluating scores for all abilities.
        /// </summary>
        private static void MeasureAllGames()
        {
            // Measure abilities for Balloons
            // If the game has been played, do the measurement; else don't do the measurement
            if (!notYetPlayBalloons)
            {
                PointingMeasure.EvaluateBalloonsScore();
                SelectiveVisualMeasure.EvaluateBalloonsScore();
                InhibitionMeasure.EvaluateBalloonsScore();
            }

            // Measure abilities for Squares
            // If the game has been played, do the measurement; else don't do the measurement
            if (!notYetPlaySquares)
            {
                SelectiveVisualMeasure.EvaluateSquaresScore();
                VisuospatialSketchpadMeasure.EvaluateSquaresScore();
            }

            // Measure abilities for Catch The Thief
            // If the game has been played, do the measurement; else don't do the measurement
            if (!notYetPlayCTF)
            {
                InhibitionMeasure.EvaluateCTFScore();
                SelectiveVisualMeasure.EvaluateCTFScore();
            }

            // Measure abilities for ImageHit
            // If the game has been played, do the measurement; else don't do the measurement
            if (!notYetPlayImageHit)
            {
                ObjectRecognitionMeasure.EvaluateImageHitScore();
                InhibitionMeasure.EvaluateImageHitScore();
                SelectiveVisualMeasure.EvaluateImageHitScore();
            }

            // Measure abilities for Catch The Ball
            // If the game has been played, do the measurement; else don't do the measurement
            if (!notYetCatchTheBall)
            {
                TimeToContact.EvaluateCatchTheBallScore();
            }

            // Measure abilities for Save One Ball
            // If the game has been played, do the measurement; else don't do the measurement
            if (!notYetSaveOneBall)
            {
                TimeToContact.EvaluateSaveOneBallScore();
            }

            // Measure abilities for Judge The Ball
            // If the game has been played, do the measurement; else don't do the measurement
            if (!notYetJudgeTheBall)
            {
                TimeToContact.EvaluateJudgeTheBallScore();
            }
        }

        /// <summary>
        /// UpdateSubScoreSeq is to derive all subscore records and
        /// add these records to the subScoreSeq.
        /// </summary>
        private static void UpdateSubScoreSeq()
        {
            // Update subscores of abilities tested by Balloons and add them to sequence
            // If the game has been played, update the score
            if (!notYetPlayBalloons)
            {
                //get subscore for (Flicking, Balloons)
                SubscoreStorage flicking_balloons = PointingMeasure.GetSubScoreForBalloons();
                //get subscore for (Inhibition, Balloons)
                SubscoreStorage inhibition_balloons = InhibitionMeasure.GetSubScoreForBalloons();
                //get subscore for (Selective Visual, Balloons)
                SubscoreStorage selectiveVisual_balloons = SelectiveVisualMeasure.GetSubScoreForBalloons();

                //add subScore to subScoreSeq
                subScoreSeq.Add(flicking_balloons);
                subScoreSeq.Add(inhibition_balloons);
                subScoreSeq.Add(selectiveVisual_balloons);
            }

            // Update subscores of abilities tested by Squares and add them to sequence
            // If the game has been played, update the score
            if (!notYetPlaySquares)
            {
                //get subscore for (Selective Visual, Squares)
                SubscoreStorage selectiveVisual_squares = SelectiveVisualMeasure.GetSubScoreForSquares();
                //get subscore for (Visuospatial Sketchpad, Squares)
                SubscoreStorage visuospatialSketchpad_squares = VisuospatialSketchpadMeasure.GetSubScoreForSquares();

                //add subScore to subScoreSeq
                subScoreSeq.Add(selectiveVisual_squares);
                subScoreSeq.Add(visuospatialSketchpad_squares);
            }

            // Update subscores of abilities tested by Catch The Thief and add them to sequence
            // If the game has been played, update the score
            if (!notYetPlayCTF)
            {
                //get subscore for (Inhibition, Catch The Thief)
                SubscoreStorage inhibition_ctf = InhibitionMeasure.GetSubScoreForCTF();
                //get subscore for  (Selective Visual, Catch The Thief)
                SubscoreStorage selectiveVisual_ctf = SelectiveVisualMeasure.GetSubScoreForCTF();

                //add subScore to subScoreSeq
                subScoreSeq.Add(inhibition_ctf);
                subScoreSeq.Add(selectiveVisual_ctf);
            }

            // Update subscores of abilities tested by ImageHit and add them to sequence
            // If the game has been played, update the score
            if (!notYetPlayImageHit)
            {
                //get subscore for (Object Recognition, ImageHit)
                SubscoreStorage objectRecognition_imageHit = ObjectRecognitionMeasure.GetSubScoreForImageHit();
                //get subscore for (Inhibition, ImageHit)
                SubscoreStorage inhibition_imageHit = InhibitionMeasure.GetSubScoreForImageHit();
                //get subscore for  (Selective Visual, ImageHit)
                SubscoreStorage selectiveVisual_imageHit = SelectiveVisualMeasure.GetSubScoreForImageHit();

                //add subScore to subScoreSeq
                subScoreSeq.Add(objectRecognition_imageHit);
                subScoreSeq.Add(inhibition_imageHit);
                subScoreSeq.Add(selectiveVisual_imageHit);
            }

            // Update subscores of abilities tested by Catch The Ball and add them to sequence
            // If the game has been played, update the score
            if (!notYetCatchTheBall)
            {
                //get subscore for (Time To Contact, Catch The Ball)
                SubscoreStorage timeToContact_catchTheBall = TimeToContact.GetSubScoreForCatchTheBall();

                //add subScore to subScoreSeq
                subScoreSeq.Add(timeToContact_catchTheBall);
            }

            // Update subscores of abilities tested by Save One Ball and add them to sequence
            // If the game has been played, update the score
            if (!notYetSaveOneBall)
            {
                //get subscore for (Time To Contact, Save One Ball)
                SubscoreStorage timeToContact_saveOneBall = TimeToContact.GetSubScoreForSaveOneBall();

                //add subScore to subScoreSeq
                subScoreSeq.Add(timeToContact_saveOneBall);
            }

            // Update subscores of abilities tested by Judge The Ball and add them to sequence
            // If the game has been played, update the score
            if (!notYetJudgeTheBall)
            {
                //get subscore for (Time To Contact, Judge The Ball)
                SubscoreStorage timeToContact_judgeTheBall = TimeToContact.GetSubScoreForJudgeTheBall();

                //add subScore to subScoreSeq
                subScoreSeq.Add(timeToContact_judgeTheBall);
            }

        }

        /// <summary>
        /// Calculates the overall score for each ability and adds it to the
        /// list of overall scores.
        /// </summary>
        public static void CalculateOverallScoreSeq()
        {
            //all abilities
            AbilityName[] abilityNames = {
                AbilityName.SELECTIVE_VISUAL,
                AbilityName.INHIBITION,
                AbilityName.VISUOSPATIAL_SKETCHPAD,
                AbilityName.POINTING,
                AbilityName.TIME_TO_CONTACT,
                AbilityName.OBJECT_RECOGNITION
            };
            
            foreach (AbilityName abilityName in abilityNames)
            {
                int score = CalculateOverallScoreForOneAbility(abilityName);
                AbilityLevel level = EvaluateLevel(score);

                OverallScoreStorage overallScore = new OverallScoreStorage
                {
                    AbilityName = abilityName,
                    Score = score,
                    Level = level
                };

                overallScoreSeq.Add(overallScore);
            }
        }

        /// <summary>
        /// Calculates the overall competency score of a player in one particular ability.
        /// The overall score is derived from finding the weighted average subscore in the specific ability.
        /// </summary>
        /// <param name="aName">The ability in question's name.</param>
        /// <returns>The player's overall score in this ability.</returns>
        /// <remarks>The returned score ≥ 0.</remarks>
        public static int CalculateOverallScoreForOneAbility(AbilityName aName)
        {
            int sumOfWeightedScores = 0;
            double sumOfWeight = 0.001;

            // Get a list of abilities that are being measured
            List<AbilityName> measuredAbilities = subScoreSeq.Select(s => s.AbilityName).ToList();

            // If aName exists in measuredAbilities
            if (measuredAbilities.Contains(aName))
            {
                // Iterate through subScoreSeq and find the average score for aName
                foreach (var subScore in subScoreSeq)
                {
                    if (subScore.AbilityName == aName)
                    {
                        sumOfWeightedScores += (subScore.Score * subScore.Weight);
                        sumOfWeight += subScore.Weight;
                    }
                }

                int overallScore = (int)Math.Ceiling(sumOfWeightedScores / sumOfWeight);
                return overallScore;
            }
            else // aName is not measured; return -1
            {
                return -1;
            }
        }

        /// <summary>
        /// Determines the level of competency in which a player's ability score
        /// lies.
        /// </summary>
        /// <param name="score">The overall score of an ability.</param>
        /// <returns>The level of competency in which a player's ability score
        /// lies.</returns>
        public static AbilityLevel EvaluateLevel(int score)
        {
            /*
            Excellent, good, okay, poor, very poor
            90          75     50     25     
            */

            if (score > 90)
            {
                return AbilityLevel.EXCELLENT;
            }

            else if (score > 75)
            {
                return AbilityLevel.GOOD;
            }

            else if (score > 50)
            {
                return AbilityLevel.OK;
            }

            else if (score > 25)
            {
                return AbilityLevel.POOR;
            }

            else if (score >= 0)
            {
                return AbilityLevel.VERY_POOR;
            }

            else //score = -1
            {
                return AbilityLevel.NOT_KNOWN;
            }
        }
    }
}