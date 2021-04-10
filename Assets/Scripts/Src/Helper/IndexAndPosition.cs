namespace Helper
{
    /// <summary>
    /// This module implements [IndexAndPosition Type Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#indexandposition-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class IndexAndPosition
    {
        /// <summary>
        /// Variable to hold a index value, or the identifier of the ojbect.
        /// </summary>
        public int Index { get;}

        /// <summary>
        /// Variable to hold x and y position values.
        /// </summary>
        public Position2D Position { get;}

        /// <summary>
        /// Creates a new object to hold the index and position values of a game object.
        /// </summary>
        /// <param name="index">The index of the object, identifier</param>
        /// <param name="position">The position of the object</param>
        public IndexAndPosition(int index, Position2D position)
        {
            Index = index;
            Position = position;
        }
    }
}

