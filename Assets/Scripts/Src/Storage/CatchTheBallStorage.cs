using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static Storage.CatchTheBallStorage;

namespace Storage
{
    /// <summary>
    /// This module implements [CatchTheBallStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#catchtheballstorage-module)
    /// found in the Architecture and Module Design Document.
    /// This is a data type for storing the measurements
    /// collected during the CatchTheBall mini-game.
    /// </summary>
    public class CatchTheBallStorage : IBallStorage<BallRound>, IEquatable<CatchTheBallStorage>
    {
        public float TargetRadius { get; set; }

        /// <summary>
        /// Sequence for gameplay data collected in each round.
        /// </summary>
        public List<BallRound> Rounds { get; set; }

        public bool Equals(CatchTheBallStorage that)
        {
            return JsonConvert.SerializeObject(this)
                .Equals(JsonConvert.SerializeObject(that));
        }

        /// <summary>
        /// Information for a round in the Ball mini-game.
        /// </summary>
        public class BallRound
        {
            public float InitialBallRadius { get; set; }
            public float ActualTimeToContact { get; set; }
            public float PredictedTimeToContact { get; set; }
        }
    }
}