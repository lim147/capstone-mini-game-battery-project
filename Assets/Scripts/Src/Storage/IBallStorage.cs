using System.Collections.Generic;

namespace Storage
{
    /// <summary>
    /// This module implements [IBallStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#generic-iballstorageballroundstorage-interface-module)
    /// found in the Architecture and Module Design Document.
    /// This is a data type for storing the measurements
    /// collected during a ball-related mini-game.
    /// </summary>
    public interface IBallStorage<BallRoundStorage>
    {
        float TargetRadius { get; set; }
        /// <summary>
        /// Sequence for gameplay data collected in each round.
        /// </summary>
        List<BallRoundStorage> Rounds { get; set; }
    }
}
