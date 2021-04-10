using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Games;

namespace UI
{
    /// <summary>
    /// This module is a static class which is used to store global variables.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// isCTFButtonOn represents the state of the Catch The Thief game button.
        /// If it's true, the button is selectable so that the game could be played;
        /// Else, the button is not selectable, which means the game gas been played and could be played again.
        /// </summary>
        public static bool isCTFButtonOn = true;

        /// <summary>
        /// isBalloonsButtonOn represents the state of the Balloons game button.
        /// </summary>
        public static bool isBalloonsButtonOn = true;

        /// <summary>
        /// isSquaresButtonOn represents the state of the Squares game button.
        /// </summary>
        public static bool isSquaresButtonOn = true;

        /// <summary>
        /// isImageHitButtonOn represents the state of the ImageHit game button.
        /// </summary>
        public static bool isImageHitButtonOn = true;

        /// <summary>
        /// isCatchTheBallButtonOn represents the state of the Catch The Ball game button.
        /// </summary>
        public static bool isCatchTheBallButtonOn = true;

        /// <summary>
        /// isSaveOneBallButtonOn represents the state of the Save One Ball game button.
        /// </summary>
        public static bool isSaveOneBallButtonOn = true;

        /// <summary>
        /// isJudgeTheBallButtonOn represents the state of the Judge The Ball game button.
        /// </summary>
        public static bool isJudgeTheBallButtonOn = true;

        /// <summary>
        /// Order of games that are played
        /// </summary>
        public static List<GameName> gameOrder = new List<GameName>();
    }

    /// <summary>
    /// This module implements [Menu Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#menu-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// It will be used to navigate the mini-games on the menu page.
    /// </summary>
    public class MenuPage : MonoBehaviour
    {
        // Buttons for mini-games

        /// <summary>
        /// Button for selecting and playing mini-game Catch The Thief
        /// </summary>
        public Button CatchTheThief;

        /// <summary>
        /// Button for selecting and playing mini-game Squares
        /// </summary>
        public Button Squares;

        /// <summary>
        /// Button for selecting and playing mini-game Balloons
        /// </summary>
        public Button Balloons;

        /// <summary>
        /// Button for selecting and playing mini-game Image Hit
        /// </summary>
        public Button ImageHit;

        /// <summary>
        /// Button for selecting and playing mini-game Catch The Ball
        /// </summary>
        public Button CatchTheBall;

        /// <summary>
        /// Button for selecting and playing mini-game Save One Ball
        /// </summary>
        public Button SaveOneBall;

        /// <summary>
        /// Button for selecting and playing mini-game Judge The Ball
        /// </summary>
        public Button JudgeTheBall;

        /// <summary>
        /// Button for finishing the games and checking the result scene
        /// </summary>
        public Button Finish;

        /// <summary>
        /// Initialize the state of the game buttons before the first frame update.
        /// </summary>
        private void Start()
        {
            // The status of the Finish button
            SetFinishButtonStatus();

            // The status of the game buttons
            CatchTheThief.interactable = Globals.isCTFButtonOn;
            Squares.interactable = Globals.isSquaresButtonOn;
            Balloons.interactable = Globals.isBalloonsButtonOn;
            ImageHit.interactable = Globals.isImageHitButtonOn;
            CatchTheBall.interactable = Globals.isCatchTheBallButtonOn;
            SaveOneBall.interactable = Globals.isSaveOneBallButtonOn;
            JudgeTheBall.interactable = Globals.isJudgeTheBallButtonOn;
        }
       
        /// <summary>
        /// State of the game buttons will be updated once per frame.
        /// </summary>
        private void Update()
        {
            // The status of the game buttons
            CatchTheThief.interactable = Globals.isCTFButtonOn;
            Squares.interactable = Globals.isSquaresButtonOn;
            Balloons.interactable = Globals.isBalloonsButtonOn;
            ImageHit.interactable = Globals.isImageHitButtonOn;
            CatchTheBall.interactable = Globals.isCatchTheBallButtonOn;
            SaveOneBall.interactable = Globals.isSaveOneBallButtonOn;
            JudgeTheBall.interactable = Globals.isJudgeTheBallButtonOn;
        }

        /// <summary>
        /// SetFinishButtonStatus determines if the Finish button will appear on the screen.
        /// It is used to make sure that Finish button will only appear after at least one game is played.
        /// </summary>
        private void SetFinishButtonStatus()
        {
            // If button is on, the game is not yet played
            bool isBalloonsPlayed = !Globals.isBalloonsButtonOn;
            bool isSquaresPlayed = !Globals.isSquaresButtonOn;
            bool isCTFPlayed = !Globals.isCTFButtonOn;
            bool isImageHitPlayed = !Globals.isImageHitButtonOn;
            bool isCatchTheBallPlayed = !Globals.isCatchTheBallButtonOn;
            bool isSaveOneBallPlayed = !Globals.isSaveOneBallButtonOn;
            bool isJudgeTheBallPlayed = !Globals.isJudgeTheBallButtonOn;

            // The expression will be "true" if at least one game is played
            bool atLeastOneGamePlayed = isBalloonsPlayed || isSquaresPlayed
                || isCTFPlayed || isImageHitPlayed || isCatchTheBallPlayed
                || isSaveOneBallPlayed || isJudgeTheBallPlayed;
            

            if(atLeastOneGamePlayed)
            {
                Finish.gameObject.SetActive(true);
            }
            else
            {
                Finish.gameObject.SetActive(false);
            }

        }

        // Game jumper functions start:
        //--------------------------------------------------------

        /// <summary>
        /// JumpToCTFGame controls the system to jump to the Catch The Thief game instruction scene.
        /// It will will be triggered as the game button is clicked.
        /// </summary>
        public void JumpToCTFGame()
        {
            SceneSwitcher.MoveToScene(SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE);
        }

        /// <summary>
        /// JumpToSquaresGame controls the system to jump to the Squares instruction scene.
        /// It will be triggered as the game button is clicked.
        /// </summary>
        public void JumpToSquaresGame()
        {
            SceneSwitcher.MoveToScene(SceneName.SQUARES_INSTRUCTIONS_SCENE);
        }

        /// <summary>
        /// JumpToBalloonsGame controls the system to jump to the Balloons game instruction scene.
        /// It will be triggered as the game button is clicked.
        /// </summary>
        public void JumpToBalloonsGame()
        {
            SceneSwitcher.MoveToScene(SceneName.BALLOONS_INSTRUCTIONS_SCENE);
        }

        /// <summary>
        /// JumpToImageHitGame controls the system to jump to the ImageHit game instruction scene.
        /// It will be triggered as the game button is clicked.
        /// </summary>
        public void JumpToImageHitGame()
        {
            SceneSwitcher.MoveToScene(SceneName.IMAGEHIT_INSTRUCTIONS_SCENE);
        }

        /// <summary>
        /// JumpToCatchTheBallGame controls the system to jump to the CatchTheBall
        /// game instruction scene.
        /// It will be triggered as the game button is clicked.
        /// </summary>
        public void JumpToCatchTheBallGame()
        {
            SceneSwitcher.MoveToScene(SceneName.CATCH_THE_BALL_INSTRUCTIONS_SCENE);

        }

        /// <summary>
        /// JumpToSaveOneBallGame controls the system to jump to the SaveOneBall
        /// game instruction scene.
        /// It will be triggered as the game button is clicked.
        /// </summary>
        public void JumpToSaveOneBallGame()
        {
            SceneSwitcher.MoveToScene(SceneName.SAVE_ONE_BALL_INSTRUCTIONS_SCENE);

        }

        /// <summary>
        /// JumpToSaveOneBallGame controls the system to jump to the SaveOneBall
        /// game instruction scene.
        /// It will be triggered as the game button is clicked.
        /// </summary>
        public void JumpToJudgeTheBallGame()
        {
            SceneSwitcher.MoveToScene(SceneName.JUDGE_THE_BALL_INSTRUCTIONS_SCENE);

        }

        /// <summary>
        /// JumpToResult controls the system to jump to the Result scene.
        /// It will be trigger as the "Check Result" button is clicked.
        /// </summary>
        public void JumpToResult()
        {
            SceneSwitcher.MoveToScene(SceneName.RESULT_SCENE);
        }

        // Game tooltip display functions start:
        //--------------------------------------------------------

        /// <summary>
        /// Show message of "Game already played" tooltip for Catch The Thief game
        /// when the game is not selectable and the mouse goes over the game.
        /// </summary>
        public void ShowTooltipCatchTheThief()
        {
            // Catch The Thief button is not selectable
            if (!CatchTheThief.IsInteractable())
            {
                Tooltip.ShowGamePlayedTooltip();
            }
        }

        /// <summary>
        /// Show message of "Game already played" tooltip for Balloons game
        /// when the game is not selectable and the mouse goes over the game.
        /// </summary>
        public void ShowTooltipBalloons()
        {
            // Balloons button is not selectable
            if (!Balloons.IsInteractable())
            {
                Tooltip.ShowGamePlayedTooltip();
            }
        }

        /// <summary>
        /// Show message of "Game already played" tooltip for Squares game
        /// when the game is not selectable and the mouse goes over the game.
        /// </summary>
        public void ShowTooltipSquares()
        {
            // Squares button is not selectable
            if (!Squares.IsInteractable())
            {
                Tooltip.ShowGamePlayedTooltip();
            }
        }

        /// <summary>
        /// Show message of "Game already played" tooltip for ImageHit game
        /// when the game is not selectable and the mouse goes over the game.
        /// </summary>
        public void ShowTooltipImageHit()
        {
            // ImageHit button is not selectable
            if (!ImageHit.IsInteractable())
            {
                Tooltip.ShowGamePlayedTooltip();
            }
        }

        /// <summary>
        /// Show message of "Game already played" tooltip for Catch The Ball game
        /// when the game is not selectable and the mouse goes over the game.
        /// </summary>
        public void ShowTooltipCatchTheBall()
        {
            // CatchTheBall button is not selectable
            if (!CatchTheBall.IsInteractable())
            {
                Tooltip.ShowGamePlayedTooltip();
            }
        }

        /// <summary>
        /// Show message of "Game already played" tooltip for Save One Ball game
        /// when the game is not selectable and the mouse goes over the game.
        /// </summary>
        public void ShowTooltipSaveOneBall()
        {
            // SaveOneBall button is not selectable
            if (!SaveOneBall.IsInteractable())
            {
                Tooltip.ShowGamePlayedTooltip();
            }
        }

        /// <summary>
        /// Show message of "Game already played" tooltip for Judge The Ball game
        /// when the game is not selectable and the mouse goes over the game.
        /// </summary>
        public void ShowTooltipJudgeTheBall()
        {
            // JudgeTheBall button is not selectable
            if (!JudgeTheBall.IsInteractable())
            {
                Tooltip.ShowGamePlayedTooltip();
            }
        }

    }
}