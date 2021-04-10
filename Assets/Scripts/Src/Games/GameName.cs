using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Games
{
    /// <summary>
    /// Enumerates the names of the five mini-games in the battery.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameName
    {
        /// <summary>
        /// The game name Balloons.
        /// </summary>
        BALLOONS,

        /// <summary>
        /// The game name Catch The Thief.
        /// </summary>
        CATCH_THE_THIEF,

        /// <summary>
        /// The game name Image Hit.
        /// </summary>
        IMAGE_HIT,

        /// <summary>
        /// The game game Squares.
        /// </summary>
        SQUARES,

        /// <summary>
        /// The game name Catc The Ball.
        /// </summary>
        CATCH_THE_BALL,

        /// <summary>
        /// The game name Save One Ball.
        /// </summary>
        SAVE_ONE_BALL,

        /// <summary>
        /// The game name Judge The Ball.
        /// </summary>
        JUDGE_THE_BALL
    }
}