using Games;
using Measurement;

namespace Storage
{
    /// <summary>
    /// This module implements [SubscoreStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#subscorestorage-module)
    /// found in the Architecture and Module Design Document.
    /// This object records the data for a subscore.
    ///
    /// A subscore represents the user's competency in a particular ability
    /// tested through one mini-game.
    /// </summary>
    public class SubscoreStorage
    {
        /// <summary>
        /// The name of the ability
        /// </summary>
        public AbilityName AbilityName { get; set; }

        /// <summary>
        /// The name of the mini-game
        /// </summary>
        public GameName GameName { get; set; }

        /// <summary>
        /// Subscore of an ability measured in the mini-game
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The weightage of the mini-game in terms of measuing the ability
        /// </summary>
        public int Weight { get; set; }
    }
}