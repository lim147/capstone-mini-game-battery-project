using System.Collections;
using System.Collections.Generic;
using Storage;
using UnityEngine;
using System;
using Games;
using UI;

namespace Management
{
    /// <summary>
    /// This module implements [BatterySessionManagement ADT Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#batterysessionmanagement-adt-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class BatterySessionManagement : MonoBehaviour
    {
        //An instance for storage 
        private IStorage storage;

        //An object to store all data for the battery session
        public BatterySessionStorage batterySessionStorage;

        //An unique identifer for the current battery session
        public Guid sessionId;

        //An object for the player
        public PlayerStorage player;

        //A list of GameName to take record of the order of games
        public List<GameName> gameOrder;

        //An object to store gameplay data for game Balloons
        public BalloonsStorage balloonsData = new BalloonsStorage();
        //An object to store gameplay data for game Squares
        public SquaresStorage squaresData = new SquaresStorage();
        //An object to store gameplay data for game Catch The Thief
        public CatchTheThiefStorage ctfData = new CatchTheThiefStorage();
        //An object to store gameplay data for game ImageHit
        public ImageHitStorage imageHitData = new ImageHitStorage();
        //An object to store gameplay data for game Catch The Ball
        public CatchTheBallStorage catchTheBallData =  new CatchTheBallStorage();
        //An object to store gameplay data for game Save One Ball
        public SaveOneBallStorage saveOneBallData = new SaveOneBallStorage();
        //An object to store gameplay data for game Judge The Ball
        public JudgeTheBallStorage judgeTheBallData = new JudgeTheBallStorage();

        //An object to store the UI Interaction time for non-game scenes,
        //used for external testing purpose
        public UIInteractionStorage uiInteractionData = new UIInteractionStorage();


        //Declare and initialize a bool variable to take record of whether
        //all measurement modules are called.
        //The bool variable will be used in the ResultPage.cs to check if
        //AbilityManagement.Measure() is called
        public static bool measureEnd = false;

        //A list of SubscoreStorage to take record of the subscore measured
        //for an ability from one game
        public List<SubscoreStorage> subScoreSeq;

        //A list of OverallScoreStorage to take record of the overall score measured
        //for an ability from all games that cover this specific ability
        public List<OverallScoreStorage> overallScoreSeq;

        // Check if the Catch The Thief game is played or not
        private bool notYetPlayBalloons = Globals.isBalloonsButtonOn;
        // Check if the Squares game is played or not
        private bool notYetPlaySquares = Globals.isSquaresButtonOn;
        // Check if the Balloons game is played or not
        private bool notYetPlayCTF = Globals.isCTFButtonOn;
        // Check if the ImageHit game is played or not
        private bool notYetPlayImageHit = Globals.isImageHitButtonOn;
        // Check if the Catch The Ball game is played or not
        private bool notYetCatchTheBall = Globals.isCatchTheBallButtonOn;
        // Check if the Save One Ball game is played or not
        private bool notYetSaveOneBall = Globals.isSaveOneBallButtonOn;
        // Check if the Judge The Ball game is played or not
        private bool notYetJudgeTheBall = Globals.isJudgeTheBallButtonOn;


        /// <summary>
        /// Start is a function called when the result scene is loaded.
        /// It will fill in all declared storage objects with the collected gameplay data
        /// and then send all data to the remote Elasticsearch dataset.
        /// </summary>
        private IEnumerator Start()
        {
            // Wait for 1 frame to upload the result
            yield return null;

            // Loading times count increases
            BatterySesstionLoadingTimesControl.resultSceneLoadingTimesCnt += 1;

            // Only execute methods when result page is first time loaded
            if (BatterySesstionLoadingTimesControl.resultSceneLoadingTimesCnt < 2)
            {
                //initialize storage
                storage = gameObject.AddComponent<ElasticsearchStorageManager>();

                //fill in state variables
                FillPlayer();
                FillSessionId();
                FillGameOrder();
                FillAllGameData();


                //call Measure() to execute all measurement modules
                AbilityManagement.Measure();

                //After Measure() being called, subScoreSeq and overallScoreSeq should be
                //completly filled in AbilityManagement.cs
                //Thus, set the bool variable to true
                measureEnd = true;

                //fill in result sequences
                FillSubScoreSeq();
                FillOverallScoreSeq();


                //fill in batterySessionStorage
                FillBatterySessionStorage();


                //send data to the Elasticsearch dataset
                storage.Store(batterySessionStorage);
            }
        }


        /// <summary>
        /// IfMeasureCalled is a function to check if all measurement modules are called.
        /// This method will guarantee that the subscoreSeq and overallScoreSeq variables
        /// will be correctly filled with measured results.
        /// It will be called in the ResultPage.cs.
        /// </summary>
        /// <returns>A bool value which represents if all measurement modules are called</returns>
        public static bool IfMeasureCalled()
        {
            return measureEnd;
        }





        //Helper functions start:
        //----------------------------------------------

        /// <summary>
        ///FillPlayer is to update Player object with player information.
        /// </summary>
        public void FillPlayer()
        {
            player = QuestionnairePage.GetPlayer();
        }


        /// <summary>
        /// FillSessionId is to update sessionId variable with a random generated guid.
        /// </summary>
        public void FillSessionId()
        {
            sessionId = Guid.NewGuid();
        }


        /// <summary>
        /// FillGameOrder is to add game name to the gameOrder list in the game played order.
        /// </summary>
        public void FillGameOrder()
        {
            gameOrder = Globals.gameOrder;
        }




        /// <summary>
        /// FillAllGameData is to update game data objects with corresponding raw gameplay data.
        /// It will only get gameplay data for the games that have been played.
        /// </summary>
        public void FillAllGameData()
        {
            // Get gameplay data for Balloons
            // If the game has been played, get the gameplay data; else, skip this game
            if (!notYetPlayBalloons)
            {
                balloonsData = Balloons.GetGameplayData();
            }

            // Get gameplay data for Squares
            // If the game has been played, get the gameplay data; else, skip this game
            if (!notYetPlaySquares)
            {
                squaresData = Squares.GetGameplayData();
            }


            // Get gameplay data for Catch The Thief
            // If the game has been played, get the gameplay data; else, skip this game
            if (!notYetPlayCTF)
            {
                ctfData = CatchTheThief.GetGameplayData();
            }

            // Get gameplay data for ImageHit
            // If the game has been played, get the gameplay data; else, skip this game
            if (!notYetPlayImageHit)
            {
                imageHitData = ImageHit.GetGameplayData();
            }

            // Get gameplay data for Catch The Ball
            // If the game has been played, get the gameplay data; else, skip this game
            if (!notYetCatchTheBall)
            {
                catchTheBallData = CatchTheBall.GetGameplayData();
            }

            // Get gameplay data for Save One Ball
            // If the game has been played, get the gameplay data; else, skip this game
            if (!notYetSaveOneBall)
            {
                saveOneBallData = SaveOneBall.GetGameplayData();
            }

            // Get gameplay data for Judge The Ball
            // If the game has been played, get the gameplay data; else, skip this game
            if (!notYetJudgeTheBall)
            {
                judgeTheBallData = JudgeTheBall.GetGameplayData();
            }

        }


        /// <summary>
        /// FillSubScoreSeq is to add subscore records to the subScoreSeq list.
        /// </summary>
        public void FillSubScoreSeq()
        {
            subScoreSeq = AbilityManagement.GetSubScoreSeq();
        }

        /// <summary>
        /// FillOverallScoreSeq is to add overall score records to the overallScoreSeq list.
        /// </summary>
        public void FillOverallScoreSeq()
        {
            overallScoreSeq = AbilityManagement.GetOverallScoreSeq();
        }



        /// <summary>
        /// FillBatterySessionStorage is to update batterySessionStorage with all data
        /// </summary>
        public void FillBatterySessionStorage()
        {
            // Initialize the batterySessionManagement
            batterySessionStorage = new BatterySessionStorage();
            // Fill with data
            batterySessionStorage.BatterySessionId = sessionId;
            batterySessionStorage.Player = player;
            batterySessionStorage.MiniGameOrder = gameOrder;

            batterySessionStorage.BalloonsData = balloonsData;
            batterySessionStorage.SquaresData = squaresData;
            batterySessionStorage.CatchTheThiefData = ctfData;
            batterySessionStorage.ImageHitData = imageHitData;
            batterySessionStorage.CatchTheBallData = catchTheBallData;
            batterySessionStorage.JudgeTheBallData = judgeTheBallData;
            batterySessionStorage.SaveOneBallData = saveOneBallData;

            batterySessionStorage.SubScoreSeq = subScoreSeq;
            batterySessionStorage.OverallScoreSeq = overallScoreSeq;

            batterySessionStorage.UIInteractionData = uiInteractionData;
        }




    }
}