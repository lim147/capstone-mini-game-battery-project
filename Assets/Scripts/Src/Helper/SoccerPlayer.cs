using UnityEngine;

namespace Helper
{
    /// <summary>
    /// A small script used for the animation of a soccer player in the Ball
    /// game.
    /// This module implements [SoccerPlayer Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#soccerplayer-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class SoccerPlayer : MonoBehaviour
    {
        private bool hasPlayerKickedBall = false;

        public void Start()
        {
            // This is how quickly the player appears to kick.
            // The default speed appears too slow, so doubling it
            gameObject.GetComponent<Animator>().speed = 2;
        }

        /// <summary>
        /// This method will be called when the animation of the kicking
        /// soccer player has finished.
        /// </summary>
        void PlayerDidKickBall()
        {
            gameObject.SetActive(false);
            hasPlayerKickedBall = true;
        }

        /// <summary>
        /// The Ball script will use this method to wait until the player
        /// has appeared to kick before displaying the soccer ball.
        /// </summary>
        /// <returns></returns>
        public bool PlayerHasKickedBall()
        {
            return hasPlayerKickedBall;
        }
    }
}