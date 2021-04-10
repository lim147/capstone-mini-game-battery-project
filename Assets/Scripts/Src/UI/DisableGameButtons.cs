using UnityEngine;
using Games;

namespace UI
{
    /// <summary>
    /// This module implements [DisableGameButtons Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#disablegamebuttons-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// 
    /// This module is used to disable game icons when the games are already played. It's to guarantee that
    /// each game could only be played once.
    /// </summary>
    public class DisableGameButtons : MonoBehaviour
    {
        /// <summary>
        /// DisableCTF makes CatchTheThief button not selectable,
        /// and insert the game to the game order list.
        /// It will be triggered as the game has been played, to make sure the game could be played only once.
        /// </summary>
        public void DisableCTF()
        {

            Globals.isCTFButtonOn = false;
            Globals.gameOrder.Add(GameName.CATCH_THE_THIEF);
        }

        /// <summary>
        /// DisableSquares makes Squares button not selectable.
        /// and insert the game to the game order list.
        /// It be triggered as the game has been played, to make sure the game could be played only once.
        /// </summary>
        public void DisableSquares()
        {

            Globals.isSquaresButtonOn = false;
            Globals.gameOrder.Add(GameName.SQUARES);
        }

        /// <summary>
        /// DisableBalloons makes Balloons button not selectable.
        /// and insert the game to the game order list.
        /// It will be triggered as the game has been played, to make sure the game could be played only once.
        /// </summary>
        public void DisableBalloons()
        {

            Globals.isBalloonsButtonOn = false;
            Globals.gameOrder.Add(GameName.BALLOONS);
        }

        /// <summary>
        /// DisableImageHit makes ImageHit button not selectable.
        /// and insert the game to the game order list.
        /// It will be triggered as the game has been played, to make sure the game could be played only once.
        /// </summary>
        public void DisableImageHit()
        {

            Globals.isImageHitButtonOn = false;
            Globals.gameOrder.Add(GameName.IMAGE_HIT);
        }

        /// <summary>
        /// DisableCatchTheBall makes Catch The Ball button not selectable.
        /// and insert the game to the game order list.
        /// It will be triggered as the game has been played, to make sure the game could be played only once.
        /// </summary>
        public void DisableCatchTheBall()
        {

            Globals.isCatchTheBallButtonOn = false;
            Globals.gameOrder.Add(GameName.CATCH_THE_BALL);
        }

        /// <summary>
        /// DisableSaveOneBall makes Save One Ball button not selectable.
        /// and insert the game to the game order list.
        /// It will be triggered as the game has been played, to make sure the game could be played only once.
        /// </summary>
        public void DisableSaveOneBall()
        {

            Globals.isSaveOneBallButtonOn = false;
            Globals.gameOrder.Add(GameName.SAVE_ONE_BALL);
        }

        /// <summary>
        /// DisableJudgeTheBall makes Judge The Ball button not selectable.
        /// and insert the game to the game order list.
        /// It will be triggered as the game has been played, to make sure the game could be played only once.
        /// </summary>
        public void DisableJudgeTheBall()
        {

            Globals.isJudgeTheBallButtonOn = false;
            Globals.gameOrder.Add(GameName.JUDGE_THE_BALL);
        }
    }
}
