using System.Collections.Generic;
using UnityEngine;
using Helper;

namespace Storage
{
    /// <summary>
    /// This module implements [CatchTheThiefStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#catchthethiefstorage-module)
    /// found in
    /// the Architecture and Module Design Document. This is a data type for storing the measurements
    /// collected during the <see cref="Games.CatchTheThief"/> mini-game.
    /// </summary>
    public class CatchTheThiefStorage
    {
        /// <summary>
        /// Identified key in Catch The Thief game
        /// </summary>
        public KeyCode IdentifiedKeyCode = KeyCode.Space;

        /// <summary>
        /// Name of the identified key in Catch The Thief game
        /// </summary>
        public string IdentifiedKeyName = KeyCode.Space.ToString();
        
        /// <summary>
        /// Sequence for gameplay data collected in each round.
        /// </summary>
        public List<CatchTheThiefRound> Rounds { get; set; }
    }

    /// <summary>
    /// Information for a particular a round in the Catch The Thief mini-game.
    /// </summary>
    public class CatchTheThiefRound
    {
        /// <summary>
        /// If the identified key is pressed in the current round.
        /// </summary>
        public bool IsIdentifiedKeyPressed { get; set; }

        /// <summary>
        /// Amount of time player takes to react to images after round begins.
        /// </summary>
        public double identifiedKeyPressTime { get; set; }

        /// <summary>
        /// Other unidentified keys pressed during the round.
        /// </summary>
        public List<TimeAndKey> UnidentifiedKeysPressed { get; set; }

        /// <summary>
        /// Boolean for whether the thief image appears in this round.
        /// </summary>
        public bool ThiefAppearInRound { get; set; }

        /// <summary>
        /// Boolean for whether an ordinary person appears in this round.
        /// </summary>
        public bool PersonAppearInRound { get; set; }
    }
}
