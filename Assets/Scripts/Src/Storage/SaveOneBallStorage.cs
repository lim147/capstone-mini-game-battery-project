using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static Storage.SaveOneBallStorage;

namespace Storage
{
    /// <summary>
    /// This module implements [SaveOneBallStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#saveoneballstorage-module)
    /// found in the Architecture and Module Design Document.
    /// This is a data type for storing the measurements
    /// collected during the SaveOneBall mini-game.
    /// </summary>
    public class SaveOneBallStorage : IBallStorage<BallRound>, IEquatable<SaveOneBallStorage>
    {
        public float TargetRadius { get; set; }
        /// <summary>
        /// Sequence for gameplay data collected in each round.
        /// </summary>
        public List<BallRound> Rounds { get; set; }

        public bool Equals(SaveOneBallStorage that)
        {
            return JsonConvert.SerializeObject(this)
                .Equals(JsonConvert.SerializeObject(that));
        }

        /// <summary>
        /// Information for round in the Save One Ball mini-game.
        /// </summary>
        public class BallRound
        {
            public float InitialLeftBallRadius { get; set; }
            public float InitialRightBallRadius { get; set; }
            public float LeftActualTimeToContact { get; set; }
            public float RightActualTimeToContact { get; set; }
            public bool DidPredictFirstArrivingBall { get; set; }
            public float PredictedTimeToContact { get; set; }
        }
    }
}