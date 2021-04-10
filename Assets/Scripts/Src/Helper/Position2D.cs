using System;

namespace Helper
{
    /// <summary>
    /// This module implements [Position2D Type Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#position2d-type-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class Position2D
    {
        /// <summary>
        /// The x-axis value of the 2D position.
        /// </summary>
        public double X { get; }

        /// <summary>
        /// The y-axis value of the 2D position.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Creates a new Position2D.
        /// </summary>
        /// <param name="x">The x-coordinate of the position.</param>
        /// <param name="y">The y-coordinate of the position.</param>
        public Position2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the x-coordinate. Implemented to match the MIS contract.
        /// The x-coordinate can also be accessed using the property.
        /// </summary>
        /// <returns>The x-coordinate of the position</returns>
        public double GetX()
        {
            return X;
        }

        /// <summary>
        /// Gets the y-coordinate. Implemented to match the MIS contract.
        /// </summary>
        /// The y-coordinate can also be accessed using the property.
        /// <returns>The y-coordinate of the position.</returns>
        public double GetY()
        {
            return Y;
        }

        /// <summary>
        /// Distance2D calculates the local distance between two Position2D objects
        /// </summary>
        /// <param name="p1">The Position2D of the origin position.</param>
        /// <param name="p2">The Position2D of the destination position.</param>
        /// <returns>Distance between two Position2D objects.</returns>
        public static double Distance2D(Position2D p1, Position2D p2)
        {
            return Math.Sqrt(Math.Pow(p1.GetX() - p2.GetX(), 2) + Math.Pow(p1.GetY() - p2.GetY(), 2));
        }

    }
}