using NUnit.Framework;
using Helper;

namespace EditModeTests
{
    namespace HelperTests
    {
        public class StringSimilarityTests
        {
            private string testString1;
            private string testString2;
            private int expectedScore;
            private string expectedResultString;

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When both strings are empty, score should be 0.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityOfTwoEmptyStrings_THEN_ScoreIs0()
            {
                testString1 = "";
                testString2 = "";
                expectedScore = 0;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When both strings are empty, computed string string is as expected.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityOfTwoEmptyStrings_THEN_ComputedString1IsEmpty()
            {
                testString1 = "";
                testString2 = "";
                expectedResultString = "";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> When both strings are empty, computed string is as expected.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityOfTwoEmptyStrings_THEN_ComputedString2IsEmpty()
            {
                testString1 = "";
                testString2 = "";
                expectedResultString = "";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityOfTwoIdenticalStrings_THEN_ScoreIs0()
            {
                testString1 = "abc";
                testString2 = "abc";
                expectedScore = 0;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityOfTwoIdenticalStrings_THEN_ComputedString1IsOriginal()
            {
                testString1 = "abc";
                testString2 = "abc";
                expectedResultString = "abc";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityOfTwoIdenticalStrings_THEN_ComputedString2IsOriginal()
            {
                testString1 = "abc";
                testString2 = "abc";
                expectedResultString = "abc";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Empty_THEN_ComputedScoreHas1ScoreDeduction()
            {
                testString1 = "";
                testString2 = "a";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Empty_THEN_ComputedString1ShowsGap()
            {
                testString1 = "";
                testString2 = "a";
                expectedResultString = "-";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Empty_THEN_ComputedString2ShowsOriginal()
            {
                testString1 = "";
                testString2 = "a";
                expectedResultString = "a";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Empty_THEN_ComputedScoreHas1ScoreDeduction()
            {
                testString1 = "a";
                testString2 = "";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Empty_THEN_ComputedString1ShowsOriginal()
            {
                testString1 = "a";
                testString2 = "";
                expectedResultString = "a";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Empty_THEN_ComputedString2ShowsGap()
            {
                testString1 = "1";
                testString2 = "";
                expectedResultString = "-";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing1CharAfter_THEN_ComputedString2Shows1Gap()
            {
                testString1 = "ab";
                testString2 = "a";
                expectedResultString = "a-";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing1CharAfter_THEN_ComputedString1ShowsSame()
            {
                testString1 = "ab";
                testString2 = "a";
                expectedResultString = "ab";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing1CharAfter_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "ab";
                testString2 = "a";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing1CharAfter_THEN_ComputedString2ShowsSame()
            {
                testString1 = "a";
                testString2 = "ab";
                expectedResultString = "ab";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing1CharAfter_THEN_ComputedString1ShowsGap()
            {
                testString1 = "a";
                testString2 = "ab";
                expectedResultString = "a-";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing1CharAfter_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "a";
                testString2 = "ab";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing1CharBefore_THEN_ComputedString2ShowsSame()
            {
                testString1 = "b";
                testString2 = "ab";
                expectedResultString = "ab";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing1CharBefore_THEN_ComputedString1ShowsGap()
            {
                testString1 = "b";
                testString2 = "ab";
                expectedResultString = "-b";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing1CharBefore_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "b";
                testString2 = "ab";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing1CharBefore_THEN_ComputedString2Shows1Gap()
            {
                testString1 = "ab";
                testString2 = "b";
                expectedResultString = "-b";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing1CharBefore_THEN_ComputedString1ShowsSame()
            {
                testString1 = "ab";
                testString2 = "b";
                expectedResultString = "ab";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing1CharBefore_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "ab";
                testString2 = "b";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }


            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing1CharBetween_THEN_ComputedString2ShowsSame()
            {
                testString1 = "ac";
                testString2 = "abc";
                expectedResultString = "abc";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing1CharBetween_THEN_ComputedString1ShowsGap()
            {
                testString1 = "ac";
                testString2 = "abc";
                expectedResultString = "a-c";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing1CharBetween_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "ac";
                testString2 = "abc";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing1CharBetween_THEN_ComputedString2Shows1Gap()
            {
                testString1 = "abc";
                testString2 = "ac";
                expectedResultString = "a-c";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing1CharBetween_THEN_ComputedString1ShowsSame()
            {
                testString1 = "abc";
                testString2 = "ac";
                expectedResultString = "abc";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing1CharBetween_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "abc";
                testString2 = "ac";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing2CharBetween_THEN_ComputedString2ShowsSame()
            {
                testString1 = "ad";
                testString2 = "abcd";
                expectedResultString = "abcd";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing2CharBetween_THEN_ComputedString1Shows2Gap()
            {
                testString1 = "ad";
                testString2 = "abcd";
                expectedResultString = "a--d";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Missing2CharBetween_THEN_ComputedScoreHas2Deduction()
            {
                testString1 = "ad";
                testString2 = "abcd";
                expectedScore = 2;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            // Since the cost of gap and mismatch is the same, the string that was chosen for return could've been a--d or
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing2CharBetween_THEN_ComputedString2Shows1Gap1Mismatch()
            {
                testString1 = "abcd";
                testString2 = "ad";
                expectedResultString = "a--d";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing2CharBetween_THEN_ComputedString1ShowsSame()
            {
                testString1 = "abcd";
                testString2 = "ad";
                expectedResultString = "abcd";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Missing2CharBetween_THEN_ComputedScoreHas2Deduction()
            {
                testString1 = "abcd";
                testString2 = "ad";
                expectedScore = 2;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong1CharAfter_THEN_ComputedString2Shows1Mismatch()
            {
                testString1 = "ab";
                testString2 = "ac";
                expectedResultString = "ac";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong1CharAfter_THEN_ComputedString1Shows1Gap()
            {
                testString1 = "ab";
                testString2 = "ac";
                expectedResultString = "ab";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong1CharAfter_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "ab";
                testString2 = "ac";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong1CharAfter_THEN_ComputedString2ShowsSame()
            {
                testString1 = "ac";
                testString2 = "ab";
                expectedResultString = "ab";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong1CharAfter_THEN_ComputedString1ShowsGap()
            {
                testString1 = "ac";
                testString2 = "ab";
                expectedResultString = "ac";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong1CharAfter_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "ac";
                testString2 = "ab";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong1CharBefore_THEN_ComputedString2ShowsSame()
            {
                testString1 = "cb";
                testString2 = "ab";
                expectedResultString = "ab";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong1CharBefore_THEN_ComputedString1ShowsGap()
            {
                testString1 = "cb";
                testString2 = "ab";
                expectedResultString = "cb";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong1CharBefore_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "cb";
                testString2 = "ab";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong1CharBefore_THEN_ComputedString2Shows1Gap()
            {
                testString1 = "ab";
                testString2 = "cb";
                expectedResultString = "cb";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong1CharBefore_THEN_ComputedString1ShowsSame()
            {
                testString1 = "ab";
                testString2 = "cb";
                expectedResultString = "ab";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong1CharBefore_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "ab";
                testString2 = "cb";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong1CharBetween_THEN_ComputedString2ShowsSame()
            {
                testString1 = "adc";
                testString2 = "abc";
                expectedResultString = "abc";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong1CharBetween_THEN_ComputedString1ShowsGap()
            {
                testString1 = "adc";
                testString2 = "abc";
                expectedResultString = "adc";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong1CharBetween_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "adc";
                testString2 = "abc";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong1CharBetween_THEN_ComputedString2Shows1Gap()
            {
                testString1 = "abc";
                testString2 = "adc";
                expectedResultString = "adc";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong1CharBetween_THEN_ComputedString1ShowsSame()
            {
                testString1 = "abc";
                testString2 = "adc";
                expectedResultString = "abc";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong1CharBetween_THEN_ComputedScoreHas1Deduction()
            {
                testString1 = "abc";
                testString2 = "adc";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong2CharBetween_THEN_ComputedString2ShowsSame()
            {
                testString1 = "aefd";
                testString2 = "abcd";
                expectedResultString = "abcd";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong2CharBetween_THEN_ComputedString1Shows2Gap()
            {
                testString1 = "aefd";
                testString2 = "abcd";
                expectedResultString = "aefd";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString1Wrong2CharBetween_THEN_ComputedScoreHas2Deduction()
            {
                testString1 = "aefd";
                testString2 = "abcd";
                expectedScore = 2;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong2CharBetween_THEN_ComputedString2Shows2Gap()
            {
                testString1 = "abcd";
                testString2 = "aefd";
                expectedResultString = "aefd";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString2());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong2CharBetween_THEN_ComputedString1ShowsSame()
            {
                testString1 = "abcd";
                testString2 = "aefd";
                expectedResultString = "abcd";

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedResultString, StringSimilarity.GetComputedString1());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on string combinations.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereString2Wrong2CharBetween_THEN_ComputedScoreHas2Deduction()
            {
                testString1 = "abcd";
                testString2 = "aefd";
                expectedScore = 2;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on numerical input.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereStringIsNumbers_THEN_ComputedScoreIsCorrect()
            {
                testString1 = "1234";
                testString2 = "1234";
                expectedScore = 0;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on input that need the escape character.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereStringHasEscapeCharacters_THEN_ComputedScoreIsCorrect()
            {
                // \' single quote, \" double quote, \\ backslash, \0 null character \a alert character \b backspace \f form feed \n new line \r carriage return \t horizontal tab \v vertical tab
                testString1 = "\'\"\\\0\a\b\f\n\r\t\v";
                testString2 = "a";
                expectedScore = 11;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on unicode input.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereStringHasUnicodeCharacters_THEN_ComputedScoreIsCorrect()
            {
                // Unicode character format: \uxxxx for a unicode character hex value such as \u0020
                // \x is the same as \u, but you dont need leading zeros \x20

                testString1 = "\u0020";
                testString2 = "a";
                expectedScore = 1;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on symbol characters.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereStringHasSymbolCharacters_THEN_ComputedScoreIsCorrect()
            {
                testString1 = "~`!@#$%^&*()_+-=[]\\{}|;':\",./<>?";
                testString2 = "a";
                expectedScore = 32;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit test</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Testing computation on large input.
            </li>
        </ul>
        ")]
            [Test]
            public void WHEN_ComputeSimilarityWhereStringIs1000Characters_THEN_ComputedScoreIsCorrect()
            {
                testString1 = new string('a', 1000);
                testString2 = "a";
                expectedScore = 999;

                StringSimilarity.ComputeSimilarity(testString1, testString2);

                Assert.AreEqual(expectedScore, StringSimilarity.GetTotalDiff());
            }
        }
    }
}
