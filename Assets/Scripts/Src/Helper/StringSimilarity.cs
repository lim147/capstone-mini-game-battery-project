using System;

namespace Helper
{
    /// <summary>
    ///  This module implements [StringSimilarity Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#stringsimilarity-module)
    /// found in
    /// The module is to calculate the similarity between two strings, by finding the
    /// gaps and mismatches between the input strings.
    /// The module will be used to find the wrongly recalled squares in the "Squares" mini-game.
    /// </summary>
    public class StringSimilarity
    {
        // Score cost of mismatched values in the string
        private static int COST_OF_MISMATCH = 1;
        // Score cost of missing values in the string
        private static int COST_OF_GAP = 1;

        private static int totalDiff = 0;
        private static string computedString1 = "";
        private static string computedString2 = "";

        /// <summary>Calculates the similarity between the two given strings.</summary>
        /// <param name="string1">The first string to compare.</param>
        /// <param name="string2">The second string to compare.</param>
        public static void ComputeSimilarity(string string1, string string2)
        {
            var result = Compute(string1, string2);
            totalDiff = result.Item1;
            computedString1 = result.Item2;
            computedString2 = result.Item3;
        }

        /// <summary>Gets the computed string 1</summary>
        /// <returns>Computed string 1</returns> 
        public static string GetComputedString1()
        {
            return computedString1;
        }

        /// <summary>Gets the computed string 2</summary>
        /// <returns>Computed string 2</returns> 
        public static string GetComputedString2()
        {
            return computedString2;
        }

        /// <summary> Gets the total difference between the two strings</summary>
        /// <returns>The score of the player</returns> 
        public static int GetTotalDiff()
        {
            return totalDiff;
        }

        //Helper functions starts:
        //---------------------------------------------

        /// <summary> A recursive function that computes the similarity between the two strings.</summary>
        /// <param name="string1">The first string to compare.</param>
        /// <param name="string2">The second string to compare.</param>
        /// <returns>A tuple of the similarity score.</returns> 
        private static Tuple<int, string, string> Compute(string string1, string string2)
        {
            int m = string1.Length;
            int n = string2.Length;

            string resultString1 = "";
            string resultString2 = "";

            // Base cases:
            // If both s1 and s2 are empty
            if (m == 0 && n == 0)
            {
                var result = Tuple.Create(0, string1, string2);
                return result;
            }

            // If s1 is empty, and s2 is not empty
            // Fill s1 with '-' to indicate a gap
            if (m == 0 && n != 0)
            {
                int resultValue = n * COST_OF_GAP;
                resultString1 = Append('-', n, string1);
                var result = Tuple.Create(resultValue, resultString1, string2);
                return result;
            }

            // If s1 is not empty and s2 is empty
            // Fill s2 with '-' to indicate a gap
            if (m !=0 && n == 0)
            {
                int resultValue = m * COST_OF_GAP;
                resultString2 = Append('-', m, string2);
                var result = Tuple.Create(resultValue, string1, resultString2);

                return result;
            }

            string shrinkedS1 = string1.Remove(m - 1);
            string shrinkedS2 = string2.Remove(n - 1);

            // Recursive Step:
            // If the last characters of strings are not matched
            if (string1[m-1] != string2[n-1])
            {
                // Case 1: s1[m-1] and s2[n-1] is a mismatch
                var case1 = Compute(shrinkedS1, shrinkedS2);
                int costOfCase1 = COST_OF_MISMATCH + case1.Item1;

                // Case 2: s1[m-1] is matched with a gap
                var case2 = Compute(shrinkedS1, string2);
                int costOfCase2 = COST_OF_GAP + case2.Item1;

                // Case 3: s2[n-1] is matched with a gap
                var case3 = Compute(string1, shrinkedS2);
                int costOfCase3 = COST_OF_GAP + case3.Item1;

                // Find the min cost of the three cases
                int min = MinOfThree(costOfCase1, costOfCase2, costOfCase3);

                // If the min cost is Case 1
                if(costOfCase1 == min)
                {
                    resultString1 = case1.Item2 + string1[m - 1];
                    resultString2 = case1.Item3 + string2[n - 1];
                }

                // If the min cost is Case 2
                else if (costOfCase2 == min)
                {
                    resultString1 = case2.Item2 + string1[m - 1];
                    resultString2 = case2.Item3 + '-';
                }

                // If the min cost is Case 3
                else
                {
                    resultString1 = case3.Item2 + '-';
                    resultString2 = case3.Item3 + string2[n - 1];
                }

                // Return the min case value
                var result = Tuple.Create(min, resultString1, resultString2);
                return result;
            }

            // If the last characters of strings are matched
            else
            {
                var case4 = Compute(shrinkedS1, shrinkedS2);

                resultString1 = case4.Item2 + string1[m - 1];
                resultString2 = case4.Item3 + string2[n - 1];

                var result = Tuple.Create(case4.Item1, resultString1, resultString2);
                return result;
            }
        }

        /// <summary> Find the minimum of three numbers.</summary>
        /// <param name="number1">The first number to compare.</param>
        /// <param name="number2">The second number to compare.</param>
        /// <param name="number3">The third number to compare.</param>
        /// <returns>The smallest number of the three numbers given.</returns> 
        private static int MinOfThree(int number1, int number2, int number3)
        {
            int min = Math.Min(number1, Math.Min(number2, number3));
            return min;
        }

        /// <summary> Append a character 'c' to a string 's' n times</summary>
        /// <param name="characterToAppend">The first number to compare.</param>
        /// <param name="numberOfTimesToAppend">The second number to compare.</param>
        /// <param name="stringToAppend">The third number to compare.</param>
        /// <returns>The new string with the 'c' character appeneded to the string n times.</returns> 
        private static string Append(char characterToAppend, int numberOfTimesToAppend, string stringToAppend)
        {
            string newS = stringToAppend;
            for (int i = 0; i < numberOfTimesToAppend; i++)
            {
                newS += characterToAppend;
            }
            return newS;
        }
    }
}