using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static Storage.JudgeTheBallStorage;

namespace Storage
{
    /// <summary>
    /// This module implements [JudgeTheBallStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#judgetheballstorage-module)
    /// found in the Architecture and Module Design Document.
    /// This is a data type for storing the measurements
    /// collected during the JudgeTheBall mini-game.
    /// </summary>
    public class JudgeTheBallStorage : IBallStorage<BallRound>, IEquatable<JudgeTheBallStorage>
    {
        public float TargetRadius { get; set; }
        /// <summary>
        /// Sequence for gameplay data collected in each round.
        /// </summary>
        public List<BallRound> Rounds { get; set; }

        public bool Equals(JudgeTheBallStorage that)
        {
            return JsonConvert.SerializeObject(this)
                .Equals(JsonConvert.SerializeObject(that));
        }

        /// <summary>
        /// Information for round in the JudgeTheBall mini-game.
        /// </summary>
        public class BallRound
        {
            public float InitialSlowBallRadius { get; set; }
            public float InitialPlayerBallRadius { get; set; }
            public float InitialFastBallRadius { get; set; }
            public float SlowBallActualTimeToContact { get; set; }
            public float PlayerBallActualTimeToContact { get; set; }
            public float FastBallActualTimeToContact { get; set; }
            public float TimeBeforeDisappearence { get; set; }
            public float PredictedTimeToContact { get; set; }
        }
    }
}