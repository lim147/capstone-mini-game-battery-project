using System.Collections.Generic;
using NUnit.Framework;
using Helper;
using System.Linq; // For finding duplicate elements

namespace EditModeTests
{
    namespace HelperTests
    {
        /// <summary>
        /// This module implements tests for the validation of requirements such as [FG-5](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fg-5)
        /// found in the SRS document.
        /// </summary>
        public class RamGeneratorTests
        {
            private bool testBoolValue;
            private int testIntValue;
            private float testFloatValue;
            private List<int> testIntList;
            private List<int> resultIntList;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Random boolean generator generates values equally.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_GenerateARandomBool_THEN_TrueAndFalseAppearEqually()
            {
                int trueCountTotal = 0;

                // Run the random boolean generator 1000 times 
                for (int i = 0; i < 1000; i++)
                {
                    testBoolValue = RamGenerator.GenerateARandomBool();
                    if (testBoolValue)
                        trueCountTotal += 1;
                }

                Assert.IsTrue(trueCountTotal < 600, "GenerateARandomBool: Out of 1000 calls, number of times true appeared " + trueCountTotal);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When picking 0 random elements, 0 elements returned.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_Pick0RandomElems_THEN_EmptyListReturned()
            {
                // Predetermined test list
                testIntList = new List<int>() { 1, 2, 3, 4, 5 };

                // Random items picked out by the PickNRandomElems function
                List<int> ramGeneratorList = RamGenerator.PickNRandomElems(testIntList, 0);

                Assert.AreEqual(0, ramGeneratorList.Count());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When picking 0 elements from an empty list, 0 elements returned.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_Pick0RandomElemsFromEmptyList_THEN_EmptyListReturned()
            {
                // Predetermined test list
                testIntList = new List<int>() { };

                // Random items picked out by the PickNRandomElems function
                List<int> ramGeneratorList = RamGenerator.PickNRandomElems(testIntList, 0);

                Assert.AreEqual(0, ramGeneratorList.Count());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When picking 3 elements from an empty list, 0 elements returned.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_Pick3RandomElemsFromEmptyList_THEN_EmptyListReturned()
            {
                // Predetermined test list
                testIntList = new List<int>() { };

                // Random items picked out by the PickNRandomElems function
                List<int> ramGeneratorList = RamGenerator.PickNRandomElems(testIntList, 3);

                Assert.AreEqual(0, ramGeneratorList.Count());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When picking 2 elements from a list, 2 elements are returned.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_Pick2RandomElems_THEN_ListWith2ElemsReturned()
            {
                // Predetermined test list
                testIntList = new List<int>() { 1, 2, 3 };

                // Random items picked out by the PickNRandomElems function
                List<int> ramGeneratorList = RamGenerator.PickNRandomElems(testIntList, 2);

                Assert.AreEqual(2, ramGeneratorList.Count());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When picking 3 elements from a list with 2 elements, only 2 elements returned.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_Pick3RandomElemsFromListWith2Elems_THEN_ListWith2ElemsReturned()
            {
                // Predetermined test list
                testIntList = new List<int>() { 1, 2 };

                // Random items picked out by the PickNRandomElems function
                List<int> ramGeneratorList = RamGenerator.PickNRandomElems(testIntList, 3);

                Assert.AreEqual(2, ramGeneratorList.Count());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When picking elements from a list, there are no duplicated elements.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_PickNRandomElems_THEN_NoDuplicatedElements()
            {
                // Predetermined test list
                testIntList = new List<int>() { 1, 2, 3, 4, 5 };

                // Random items picked out by the PickNRandomElems function
                List<int> ramGeneratorList = RamGenerator.PickNRandomElems(testIntList, 3);

                // Check for duplicates
                IEnumerable<int> duplicatesList = ramGeneratorList.GroupBy(x => x)
                                            .Where(g => g.Count() > 1)
                                            .Select(x => x.Key);

                Assert.AreEqual(0, duplicatesList.Count());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When picking elements from a list, the values are equally picked.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_PickNRandomElems_THEN_ValuesGeneratedEqually()
            {
                testIntList = new List<int>() { 0, 1, 2, 3, 4 };
                resultIntList = new List<int>() { 0, 0, 0, 0, 0 };

                // Run the random boolean generator 1000 times 
                for (int i = 0; i < 1000; i++)
                {
                    List<int> ramGeneratorList = RamGenerator.PickNRandomElems(testIntList, 2);

                    // Record the values generated in the list
                    resultIntList[ramGeneratorList[0]] += 1;
                    resultIntList[ramGeneratorList[1]] += 1;
                }

                // Ensure that all numbers were generated at least 15% of the time out of 1000 calls to the generator
                for (int index = 0; index < 4; index++)
                {
                    Assert.IsTrue(resultIntList[index] > 150, "GenerateARamInt: Out of 1000 calls, " + index + " was generated " + resultIntList[index] + " times.");
                }
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When generating a random int, the values are generated equally.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_GenerateARamInt_THEN_ValuesGeneratedEqually()
            {
                resultIntList = new List<int>() { 0, 0, 0, 0, 0 };

                // Run the random boolean generator 1000 times 
                for (int i = 0; i < 1000; i++)
                {
                    // Generate a random number between 0 and 10
                    testIntValue = RamGenerator.GenerateARamInt(0, 4);
                    // Keep track of the occurrences of each number
                    resultIntList[testIntValue] += 1;
                }

                // Ensure that all numbers were generated at least 15% of the time out of 1000 calls to the generator
                for (int index = 0; index < 4; index++)
                {
                    Assert.IsTrue(resultIntList[index] > 150, "GenerateARamInt: Out of 1000 calls, " + index + " was generated " + resultIntList[index] + " times.");
                }
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When generating a random int, and the lower bound equals the upper bound, then the value generated is always equal to the lower bound.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_GenerateARamIntLowerEqualUpperBound_THEN_Only1NumGenerated()
            {
                int actualOnesCount = 0;
                int expectedOnesCount = 1000;

                // Run the random boolean generator 1000 times 
                for (int i = 0; i < 1000; i++)
                {
                    // Generate a random number between 0 and 0
                    testIntValue = RamGenerator.GenerateARamInt(1, 1);

                    // Keep track of the occurrences of each number
                    if (testIntValue == 1)
                    {
                        actualOnesCount += 1;
                    }
                }
                Assert.IsTrue(actualOnesCount == expectedOnesCount, "Invalid number generated from GenerateARamInt");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When generating a random int value, the value generated is within range.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_GenerateARamInt_THEN_ValuesGeneratedAreWithinRange()
            {
                int actualOutOfRangeCount = 0;
                int expectedOutOfRangeCount = 0;

                // Run the random boolean generator 1000 times 
                for (int i = 0; i < 1000; i++)
                {
                    // Generate a random number between 0 and 0
                    testIntValue = RamGenerator.GenerateARamInt(1, 100);

                    // Keep track of the occurrences of the number one
                    if (testIntValue < 1 || testIntValue > 100)
                    {
                        actualOutOfRangeCount += 1;
                    }
                }
                Assert.IsTrue(actualOutOfRangeCount == expectedOutOfRangeCount, "Invalid number generated from GenerateARamInt");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When generating a random float, the values are generated equally along the range.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_GenerateARamNum_THEN_ValuesGeneratedEqually()
            {
                resultIntList = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                // Run the random boolean generator 1000 times 
                for (int i = 0; i < 1000; i++)
                {
                    // Generate a random number between 0 and 10
                    testFloatValue = RamGenerator.GenerateARamNum(1, 11);
                    // Keep track of the occurrences of each number
                    if (testFloatValue < 2)
                        resultIntList[0] += 1;
                    else if (testFloatValue < 3)
                        resultIntList[1] += 1;
                    else if (testFloatValue < 4)
                        resultIntList[2] += 1;
                    else if (testFloatValue < 5)
                        resultIntList[3] += 1;
                    else if (testFloatValue < 6)
                        resultIntList[4] += 1;
                    else if (testFloatValue < 7)
                        resultIntList[5] += 1;
                    else if (testFloatValue < 8)
                        resultIntList[6] += 1;
                    else if (testFloatValue < 9)
                        resultIntList[7] += 1;
                    else if (testFloatValue < 10)
                        resultIntList[8] += 1;
                    else if (testFloatValue <= 11)
                        resultIntList[9] += 1;
                }

                // Ensure that all numbers were generated at least 80% of the time out of 1000 calls to the generator
                for (int index = 0; index < 9; index++)
                {
                    Assert.IsTrue(resultIntList[index] > 70, "GenerateARamInt: Out of 1000 calls, " + index + " was generated " + resultIntList[index] + " times.");
                }
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When generating a random float where the upper bound equals the lower bound, then the only value generated is the value of the lower bound.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_GenerateARamNumLowerEqualUpperBound_THEN_Only1NumGenerated()
            {
                int actualOnesCount = 0;
                int expectedOnesCount = 1000;

                // Run the random boolean generator 1000 times 
                for (int i = 0; i < 1000; i++)
                {
                    // Generate a random number between 0 and 0
                    testFloatValue = RamGenerator.GenerateARamNum(1.0f, 1.0f);

                    // Keep track of the occurrences of the number one
                    if (testFloatValue == 1.0)
                    {
                        actualOnesCount += 1;
                    }
                }
                Assert.IsTrue(actualOnesCount == expectedOnesCount, "Invalid number generated from GenerateARamNum");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When generating a random float value, the value generated is within range.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_GenerateARamNum_THEN_ValuesGeneratedAreWithinRange()
            {
                int actualOutOfRangeCount = 0;
                int expectedOutOfRangeCount = 0;

                // Run the random boolean generator 1000 times 
                for (int i = 0; i < 1000; i++)
                {
                    // Generate a random number between 0 and 0
                    testFloatValue = RamGenerator.GenerateARamNum(1.0f, 100.0f);

                    // Keep track of the occurrences of the number one
                    if (testFloatValue < 1.0 || testFloatValue > 100.0)
                    {
                        actualOutOfRangeCount += 1;
                    }
                }
                Assert.IsTrue(actualOutOfRangeCount == expectedOutOfRangeCount, "Invalid number generated from GenerateARamNum");
            }
        }
    }
}
