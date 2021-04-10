using Measurement;

namespace Storage
{
    /// <summary>
    /// This module implements [OverallScoreStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#overallscorestorage-module)
    /// found in the Architecture and Module Design Document.
    /// 
    /// This object records the data for an overall score.
    ///
    /// An overall score represents the user's competency in a particular ability
    /// tested through all mini-games that measures the particular ability.
    /// </summary>
    public class OverallScoreStorage
    {
        /// <summary>
        /// The name of the ability
        /// </summary>
        public AbilityName AbilityName { get; set; }

        /// <summary>
        /// The overall score of the ability measured in all corresponding mini-games
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The level of the ability measured in all corresponding mini-games
        /// </summary>
        public AbilityLevel Level { get; set; }
    }
}
