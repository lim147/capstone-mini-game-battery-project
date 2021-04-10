using System.Collections.Generic;
using Helper;

namespace Storage
{
    /// <summary>
    /// This module implements [SquaresStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#squaresstorage-module)
    /// found in the Architecture and Module Design Document.
    /// 
    /// This is data type for storing the measurements
    /// collected during the <see cref="Games.Squares"/> mini-game.
    /// </summary>
    public class SquaresStorage
    {
        /// <summary>
        /// Sequence for gameplay data collected in each round.
        /// </summary>
        public List<SquaresRound> Rounds { get; set; }
    }

    /// <summary>
    /// Information for a particular a round in the Squares mini-game.
    /// </summary>
    public class SquaresRound
    {
        /// <summary>
        /// A list of index and positions of squres in the order that they are highlighted
        /// </summary>
        public List<IndexAndPosition> HighlightedSquares { get; set; }

        /// <summary>
        /// A list of index and positions of squres in the order that the player recalls the highlighted squares
        /// </summary>
        public List<IndexAndPosition> RecalledSquares { get; set; }

        /// <summary>
        /// Total time duration that player recalls the sequence of highlighted squares
        /// </summary>
        public double RecallTime { get; set; }

        /// <summary>
        /// Time duration between when one square ends highlighted and the next square is highlighted
        /// </summary>
        public double SquareHighlightInterval { get; set; }

        /// <summary>
        /// Time duration that a highlighted square is shown to the user
        /// </summary>
        public double SquareHighlightDuration { get; set; }
    }
}
