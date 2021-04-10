using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Measurement
{
    /// <summary>
    /// Enumerates the different abilities being measured in the
    /// battery.
    /// This module implements [AbilityName Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#abilityname-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AbilityName
    {
        /// <summary>
        /// Motor ability [Pointing](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#what-is-pointing).
        /// </summary>
        POINTING,

        /// <summary>
        /// Cognitive ability [Inhibition](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#what-is-inhibition)
        /// </summary>
        INHIBITION,

        /// <summary>
        /// Cognitive ability [Selective Visual](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#what-is-selective-visual)
        /// </summary>
        SELECTIVE_VISUAL,

        /// <summary>
        /// Cognitive ability [Visuospatial Sketchpad](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#what-is-the-visuospatial-sketchpad)
        /// </summary>
        VISUOSPATIAL_SKETCHPAD,

        /// <summary>
        /// Cognitive ability [Time To Contact](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#what-is-time-to-contact)
        /// </summary>
        TIME_TO_CONTACT,

        /// <summary>
        /// Cognitive ability [Object Recognition](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#what-is-object-recognition)
        /// </summary>
        OBJECT_RECOGNITION
    }
}