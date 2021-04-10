using System.Collections.Generic;
using Helper;

namespace Storage
{
    /// <summary>
    /// This module implements [BalloonsStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#balloonsstorage-module)
    /// found in
    /// the Architecture and Module Design Document. This is a data type for storing the measurements
    /// collected during the <see cref="Games.Balloons"/> mini-game.
    /// </summary>
    public class BalloonsStorage
    {
        /// <summary>
        /// Sequence for gameplay data collected in each round.
        /// </summary>
        public List<BalloonsRound> Rounds { get; set; }
    }

    /// <summary>
    /// Information for a particular a round in the Balloons mini-game.
    /// </summary>
    public class BalloonsRound
    {
        /// <summary>
        /// Size of the balloon in the current level
        /// </summary>
        public double BalloonSize { get; set; }

        /// <summary>
        /// Center point of the destination balloon
        /// </summary>
        public Position2D DestinationPoint { get; set; }

        /// <summary>
        /// Amount of time for the player to successfully click on the random balloon
        /// </summary>
        public double DestinationClickTime { get; set; }

        /// <summary>
        /// List of Time and position values of unsuccessful clicks in a round
        /// </summary>
        public List<TimeAndPosition> Clicks { get; set; }

        /// <summary>
        /// Position value of successful click in a round
        /// </summary>
        public Position2D SuccessClickPoint { get; set; }
    }
}