using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using Storage;
using Helper;

namespace EditModeTests
{
    namespace StorageTests
    {
        public class SquaresStorageTests
        {
            private SquaresStorage squaresData;
            private SquaresRound round;

            // Test for SquareHighlightDuration
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresRoundSquareHighlightDurationSet_THEN_SquaresRoundGetsCorrectValue()
            {
                round = new SquaresRound();
                round.SquareHighlightDuration = 4.9f; // float value

                float expectedHighlightDuration = 4.9f;

                yield return null;
                Assert.IsTrue(expectedHighlightDuration == round.SquareHighlightDuration, "SquareHighlightDuration getter returning incorrect value: " + expectedHighlightDuration + " , " + round.SquareHighlightDuration);
            }

            // Test for SquareHighlightInterval
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresRoundSquareHighlightIntervalSet_THEN_SquaresRoundGetsCorrectValue()
            {
                round = new SquaresRound();
                round.SquareHighlightInterval = 7f; // float value

                float expectedHighlightInterval = 7f;

                yield return null;
                Assert.IsTrue(expectedHighlightInterval == round.SquareHighlightInterval, "SquareHighlightInterval getter returning incorrect value.");
            }

            // Test for RecallTime
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresRoundRecallTimeSet_THEN_SquaresRoundGetsCorrectValue()
            {
                round = new SquaresRound();
                round.RecallTime = 7.59f; // float value

                float expectedRecallTime = 7.59f;

                yield return null;
                Assert.IsTrue(expectedRecallTime == round.RecallTime, "Recall time getter returning incorrect value.");
            }

            // Tests for HighlightedSquares
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing intialization of new list.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresHighlightedSquaresCreated_THEN_SquaresHighlightedSquaresIsEmpty()
            {
                round = new SquaresRound();
                round.HighlightedSquares = new List<IndexAndPosition>();
                int expectedSizeOfList = 0;

                yield return null;
                Assert.IsTrue(expectedSizeOfList == round.HighlightedSquares.Count, "HighlightedSquares list is not empty at creation.");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing that when one item is added to the list, then the list contains just one item.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresHighlightedSquaresAdd1Item_THEN_SquaresHighlightedSquaresHas1Item()
            {
                IndexAndPosition testIndexAndPosition = new IndexAndPosition(3, new Position2D(3, 5));
                round = new SquaresRound();
                round.HighlightedSquares = new List<IndexAndPosition>();
                round.HighlightedSquares.Add(testIndexAndPosition);
                int expectedSizeOfList = 1;

                yield return null;
                Assert.IsTrue(expectedSizeOfList == round.HighlightedSquares.Count, "When one item is added to HighlightedSquares, the size of HighlightedSquares is incorrect");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresHighlightedSquaresSet_THEN_SquaresRoundGetsCorrectValue()
            {
                IndexAndPosition testIndexAndPosition = new IndexAndPosition(3, new Position2D(3, 5));
                round = new SquaresRound();
                round.HighlightedSquares = new List<IndexAndPosition>();
                round.HighlightedSquares.Add(testIndexAndPosition);
                List<IndexAndPosition> expectedHighlightedSquaresList = new List<IndexAndPosition>() { testIndexAndPosition };

                yield return null;
                Assert.IsTrue(expectedHighlightedSquaresList[0] == round.HighlightedSquares[0], "HighlightedSquares getter returning incorrect value");
            }

            // Tests for RecalledSquares
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing initialization of list.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresRecalledSquaresCreated_THEN_SquaresHighlightedSquaresIsEmpty()
            {
                round = new SquaresRound();
                round.RecalledSquares = new List<IndexAndPosition>();
                int expectedSizeOfList = 0;

                yield return null;
                Assert.IsTrue(expectedSizeOfList == round.RecalledSquares.Count, "RecalledSquares list is not empty at creation.");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing that when 1 item is added to the list, then there is just 1 item in the list.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresRecalledSquaresAdd1Item_THEN_SquaresRecalledSquaresHas1Item()
            {
                round = new SquaresRound();
                round.RecalledSquares = new List<IndexAndPosition>();
                round.RecalledSquares.Add(new IndexAndPosition(1, new Position2D(2, 3)));
                int expectedSizeOfList = 1;

                yield return null;
                Assert.IsTrue(expectedSizeOfList == round.RecalledSquares.Count, "When one item is added to RecalledSquares, the size of RecalledSquares is incorrect");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquaresRecalledSquaresSet_THEN_SquaresRoundGetsCorrectValue()
            {
                IndexAndPosition testIndexAndPosition = new IndexAndPosition(3, new Position2D(3, 5));
                round = new SquaresRound();
                round.RecalledSquares = new List<IndexAndPosition>();
                round.RecalledSquares.Add(testIndexAndPosition);
                List<IndexAndPosition> expectedRecalledSquaresList = new List<IndexAndPosition>() { testIndexAndPosition };

                yield return null;
                Assert.IsTrue(expectedRecalledSquaresList[0] == round.RecalledSquares[0], "SquareHighlightInterval getter returning incorrect value");
            }

            // Tests for Rounds
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing initialization of list.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquareRoundCreated_THEN_RoundIsEmpty()
            {
                squaresData = new SquaresStorage();
                squaresData.Rounds = new List<SquaresRound>();
                int expectedSizeOfList = 0;

                yield return null;
                Assert.IsTrue(expectedSizeOfList == squaresData.Rounds.Count, "round list is not empty at creation.");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing that when 1 item is added to the list, the list just has 1 item.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquareRoundAdded1Round_THEN_SquareRoundSizeIs1()
            {
                squaresData = new SquaresStorage();
                squaresData.Rounds = new List<SquaresRound>();

                round = new SquaresRound();
                squaresData.Rounds.Add(round);

                int expectedSizeOfList = 1;

                yield return null;
                Assert.IsTrue(expectedSizeOfList == squaresData.Rounds.Count, "round list does not have the added round.");
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing getter function.
            </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator WHEN_SquareRoundAdded1Round_THEN_SquareRoundGetterReturnsCorrectValues()
            {
                squaresData = new SquaresStorage();
                squaresData.Rounds = new List<SquaresRound>();
                round = new SquaresRound();

                round.SquareHighlightDuration = 5.9f; // float value
                float expectedHighlightDuration = 5.9f;

                round.SquareHighlightInterval = 7.3f; // float value
                float expectedHighlightInterval = 7.3f;

                round.RecallTime = 8.59f; // float value
                float expectedRecallTime = 8.59f;

                IndexAndPosition testIndexAndPosition = new IndexAndPosition(3, new Position2D(3, 5));
                round.HighlightedSquares = new List<IndexAndPosition>();
                round.HighlightedSquares.Add(testIndexAndPosition);
                round.RecalledSquares = new List<IndexAndPosition>();
                round.RecalledSquares.Add(testIndexAndPosition);

                squaresData.Rounds.Add(round);
                SquaresRound actualSquaresRound = squaresData.Rounds[0];

                yield return null;
                Assert.IsTrue(expectedHighlightDuration == actualSquaresRound.SquareHighlightDuration, "");
                Assert.IsTrue(expectedHighlightInterval == actualSquaresRound.SquareHighlightInterval, "");
                Assert.IsTrue(expectedRecallTime == actualSquaresRound.RecallTime, "");
                Assert.IsTrue(testIndexAndPosition == round.RecalledSquares[0], "");
                Assert.IsTrue(testIndexAndPosition == round.HighlightedSquares[0], "");
            }
        }
    }
}