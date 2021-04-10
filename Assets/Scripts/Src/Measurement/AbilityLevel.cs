using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Measurement
{
    /// <summary>
    /// Represents how good the player's competency in an ability is.
    /// This module implements [AbilityLevel Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#abilitylevel-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AbilityLevel
    {
        /// <summary>
        /// Ability level Excellent.
        /// </summary>
        EXCELLENT,

        /// <summary>
        /// Ability level Good.
        /// </summary>
        GOOD,

        /// <summary>
        /// Ability level  Ok.
        /// </summary>
        OK,

        /// <summary>
        /// Ability level Poor.
        /// </summary>
        POOR,

        /// <summary>
        /// Ability level Very Poor.
        /// </summary>
        VERY_POOR,

        /// <summary>
        /// The ability is not evaluated.
        /// </summary>
        NOT_KNOWN
    }
}
