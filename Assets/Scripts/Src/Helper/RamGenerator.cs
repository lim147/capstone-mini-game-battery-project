using System.Collections.Generic;
using System.Linq;
using System;

namespace Helper
{
    /// <summary>
    /// This module implements [RamGenerator Generic Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#ramgeneratort-generic-module)
    /// found in
    /// the Architecture and Module Design Document.
    ///
    /// This module satisfies numerous PUCs dealing with randomness in the
    /// [SRS](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#product-use-case-table)
    /// </summary>
    public class RamGenerator
    {
        private static System.Random rnd = new System.Random();

        /// <summary>
        /// GenerateARamBool() generates a random boolean value.
        /// </summary>
        /// <returns>a boolean value</returns>
        public static bool GenerateARandomBool()
        {
            // Variable to hold the random value calculated using the random object
            // seed. Next takes in two values, the first is the minimum value inclusive
            // The second value is the maximum value, exclusive
            // rvalue becomes either 0 or 1, for the values true or false
            int rvalue = rnd.Next(0,2);
            return Convert.ToBoolean(rvalue);
        }

        /// <summary>
        /// Generate a random float number within the "from" and "to" parameter values
        /// The from and to parameter values are inclusive
        /// </summary>
        /// <param name="from">The lower bound of the random number</param>
        /// <param name="to">The upper bound of the random number</param>
        /// <returns>A fandom float number</returns>
        public static float GenerateARamNum(float from, float to)
        {
            // UnityEngine.Random.Range returns a random float number between the from value (inclusive)
            // and the to value (inclusive)
            float fvalue = UnityEngine.Random.Range(from, to);
            return fvalue;

            
        }

        /// <summary>
        /// Choose n number of items out of a list of arbitrary type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="list">The list of objects to choose from</param>
        /// <param name="n">The number of items to be chosen</param>
        /// <returns>A list of the objects that were randomly chosen of size n</returns>
        public static List<T> PickNRandomElems<T>(List<T> list, int n)
        {
            return list.OrderBy(_ => rnd.Next()).Take(n).ToList();
        }

        /// <summary>
        /// Generate a random int number within the "from" and "to" parameter values
        /// </summary>
        /// <param name="from">The lower bound of the random number (inclusive)</param>
        /// <param name="to">The upper bound of the random number (inclusive)</param>
        /// <returns>A random boolean value</returns>
        public static int GenerateARamInt(int from, int to)
        {
            int ivalue = rnd.Next(from, to+1);
            return ivalue;
        }
    }
}