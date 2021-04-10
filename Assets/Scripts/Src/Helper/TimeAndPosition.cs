namespace Helper
{
    /// <summary>
    /// A helper object that associates a Position2D with the time
    /// that the position was obtained.
    /// This module implements [TimeAndPosition Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#timeandposition-module)
    /// found in the Architecture and Module Design Document.
    /// </summary>
    public class TimeAndPosition
    {
        /// <summary>
        /// Variable to hold a time value.
        /// </summary>
        public double Time { get; }

        /// <summary>
        /// Variable to hold x and y position values.
        /// </summary>
        public Position2D Position { get; }

        /// <summary>
        /// Creates a new TimeAndPosition object.
        /// </summary>
        /// <param name="time">The time</param>
        /// <param name="position">The position</param>
        public TimeAndPosition(double time, Position2D position)
        {
            Time = time;
            Position = position;
        }
    }
}
