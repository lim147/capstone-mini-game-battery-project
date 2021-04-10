using System;
using System.Collections.Generic;
using Games;
using Newtonsoft.Json;

namespace Storage
{
    /// <summary>
    /// This module implements [BatterySessionStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#batterysessionstorage-module)
    /// found in
    /// the Architecture and Module Design Document. This is the object which stores all the data recorded during a battery
    /// session. This is the object which will be saved to permanent storage.
    /// </summary>
    public class BatterySessionStorage : IEquatable<BatterySessionStorage>
    {
        /// <summary>
        /// Unique global id assigned to the battery session
        /// </summary>
        public Guid BatterySessionId { get; set; }

        /// <summary>
        /// Player informaation
        /// </summary>
        public PlayerStorage Player { get; set; }

        /// <summary>
        /// The sequence of subscores
        /// </summary>
        public List<SubscoreStorage> SubScoreSeq { get; set; }

        /// <summary>
        /// The sequence of overall scores
        /// </summary>
        public List<OverallScoreStorage> OverallScoreSeq { get; set; }

        /// <summary>
        /// The order of mini-games being played
        /// </summary>
        public List<GameName> MiniGameOrder { get; set; }

        /// <summary>
        /// Gameplay data for mini-game Balloons
        /// </summary>
        public BalloonsStorage BalloonsData { get; set; }

        /// <summary>
        /// Gameplay data for mini-game Squares
        /// </summary>
        public SquaresStorage SquaresData { get; set; }

        /// <summary>
        /// Gameplay data for mini-game Catch The Thief
        /// </summary>
        public CatchTheThiefStorage CatchTheThiefData { get; set; }

        /// Gameplay data for mini-game ImageHit
        /// </summary>
        public ImageHitStorage ImageHitData { get; set; }

        /// Gameplay data for mini-game Catch The Ball
        /// </summary>
        public CatchTheBallStorage CatchTheBallData { get; set; }

        /// Gameplay data for mini-game Save One Ball
        /// </summary>
        public SaveOneBallStorage SaveOneBallData { get; set; }

        /// Gameplay data for mini-game Judge The Ball
        /// </summary>
        public JudgeTheBallStorage JudgeTheBallData { get; set; }

        /// <summary>
        /// UI Interaction time for non-game scenes, used for external testing purpose
        /// </summary>
        public UIInteractionStorage UIInteractionData { get; set; }

        /// <summary>
        /// Two BatterySessionStorages are considered to be the same
        /// if their JSON serializations are the same.
        /// </summary>
        /// <param name="that">The BatterySessionStorage being compared to.</param>
        /// <returns>Whether the two BatterySessionStorages are the same.</returns>
        public bool Equals(BatterySessionStorage that)
        {
            return JsonConvert.SerializeObject(this)
                .Equals(JsonConvert.SerializeObject(that));
        }
    }
}