using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Games;
using Helper;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PlayModeTests
{
    namespace BallTests
    {
        public class JudgeTheBallTests : InputTestFixture
        {
            /// <summary>
            /// Load the scene for playmode unit tests.
            /// </summary>
            /// The methods with SetUp attribute will be run once before every unit test.
            [SetUp]
            public void LoadScene()
            {
                // Load game scene
                SceneManager.LoadScene(SceneName.JUDGE_THE_BALL_SCENE);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Acceptance Test</li>
                <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fb-1'>FB-1</a></li>
                <li><b>Test description:</b> Tests that the position of the ball
                    does not change while it is being kicked.
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_BallsKicked_THEN_BallPositionsDoNotChange()
            {
                // Wait for soccer player to load, kick ball, and then ball to be spawned
                yield return new WaitUntil(() => Object.FindObjectOfType<SoccerPlayer>() != null);
                SoccerPlayer soccerPlayer = Object.FindObjectOfType<SoccerPlayer>();
                yield return new WaitUntil(() => soccerPlayer.PlayerHasKickedBall());
                yield return new WaitUntil(() => Object.FindObjectOfType<BallProjectile>() != null);

                BallProjectile[] balls = Object.FindObjectsOfType<BallProjectile>();
                BallProjectile ball1 = balls[0];
                BallProjectile ball2 = balls[1];
                BallProjectile ball3 = balls[2];

                float endingTime = Time.time + Math.Max(Math.Max(ball1.ActualTimeToContact, ball2.ActualTimeToContact), ball3.ActualTimeToContact);
                float currentTime = Time.time;

                Vector3 ball1StartingPosition = ball1.transform.position;
                Vector3 ball2StartingPosition = ball2.transform.position;
                Vector3 ball3StartingPosition = ball3.transform.position;

                // For every frame before the last ball arrives, their positions should be
                // unchanged.
                while (endingTime - currentTime > 0)
                {
                    Assert.AreEqual(ball1StartingPosition, ball1.transform.position);
                    Assert.AreEqual(ball2StartingPosition, ball2.transform.position);
                    Assert.AreEqual(ball3StartingPosition, ball3.transform.position);
                    yield return new WaitForSeconds(0.05f);
                    currentTime = Time.time;
                }
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Acceptance Test</li>
                <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fb-2'>FB-2</a></li>
                <li><b>Test description:</b> Tests that actual time to contact and
                initial radius of the ball changes between rounds.
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator ActualTimeToContactAndInitialRadiusOfBallIsRandom()
            {
                ISet<float> actualTimesToContact = new HashSet<float>();
                ISet<float> ballInitialRadiuses = new HashSet<float>();

                foreach (var i in Enumerable.Range(1, JudgeTheBall.NUMBER_OF_ROUNDS))
                {
                    // Wait for soccer players to load, kick balls, and then balls to be spawned
                    yield return new WaitUntil(() => Object.FindObjectOfType<SoccerPlayer>() != null);
                    SoccerPlayer soccerPlayer = Object.FindObjectOfType<SoccerPlayer>();
                    yield return new WaitUntil(() => soccerPlayer.PlayerHasKickedBall());
                    yield return new WaitUntil(() => Object.FindObjectOfType<BallProjectile>() != null);

                    foreach (BallProjectile ball in Object.FindObjectsOfType<BallProjectile>())
                    {
                        actualTimesToContact.Add(ball.ActualTimeToContact);
                        ballInitialRadiuses.Add(ball.BallInitialRadius);
                    }

                    yield return new WaitUntil(() => Object.FindObjectOfType<Button>() != null);
                    Button submitButton = Object.FindObjectOfType<Button>();
                    submitButton.onClick.Invoke();
                }

                int numberOfUniqueActualTimesToContact = actualTimesToContact.Count;
                int numberOfUniqueBallInitialRadiuses = ballInitialRadiuses.Count;

                Assert.AreNotEqual(1, numberOfUniqueActualTimesToContact);
                Assert.AreNotEqual(1, numberOfUniqueBallInitialRadiuses);
            }

            [Description(@"
                <ul>
                    <li><b>Test type:</b> Acceptance Test</li>
                    <li><b>Associated SRS requirements:</b> <a href='https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#fb-3'>FB-3</a></li>
                    <li><b>Test description:</b> Tests that the ball approaches the 
                        player (grows in size) in a linear fashion.
                    </li>
                </ul>
                ")]
            [UnityTest]
            public IEnumerator BallsMoveAtConstantVelocity()
            {
                // Wait for soccer player to load, kick ball, and then ball to be spawned
                yield return new WaitUntil(() => Object.FindObjectOfType<SoccerPlayer>() != null);
                SoccerPlayer soccerPlayer = Object.FindObjectOfType<SoccerPlayer>();
                yield return new WaitUntil(() => soccerPlayer.PlayerHasKickedBall());
                yield return new WaitUntil(() => Object.FindObjectOfType<BallProjectile>() != null);

                BallProjectile[] balls = Object.FindObjectsOfType<BallProjectile>();
                BallProjectile ball1 = balls[0];
                BallProjectile ball2 = balls[1];
                BallProjectile ball3 = balls[2];

                float endingTime = Time.time + Math.Max(Math.Max(ball1.ActualTimeToContact, ball2.ActualTimeToContact), ball3.ActualTimeToContact);
                float currentTime = Time.time;

                // For every frame before the ball arrives, its position should be
                // unchanged.
                List<double> ball1Scales = new List<double>();
                List<double> ball2Scales = new List<double>();
                List<double> ball3Scales = new List<double>();

                while (endingTime - currentTime > 0)
                {
                    ball1Scales.Add(ball1.transform.localScale.x);
                    ball2Scales.Add(ball2.transform.localScale.x);
                    ball3Scales.Add(ball3.transform.localScale.x);
                    yield return new WaitForSeconds(0.05f);
                    currentTime = Time.time;
                }

                // Utility method for calculating standard deviation
                // Credit: https://stackoverflow.com/a/6252351
                double StandardDeviation(IEnumerable<double> values)
                {
                    double avg = values.Average();
                    return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
                }

                // Find the absolute difference between two any
                // ball scales.
                IEnumerable<double> ball1ScaleDifferences = Enumerable.Zip(ball1Scales,
                    ball1Scales.Skip(1),
                    (x1, x2) => Math.Abs(x2 - x1));
                IEnumerable<double> ball2ScaleDifferences = Enumerable.Zip(ball2Scales,
                    ball2Scales.Skip(1),
                    (x1, x2) => Math.Abs(x2 - x1));
                IEnumerable<double> ball3ScaleDifferences = Enumerable.Zip(ball3Scales,
                    ball3Scales.Skip(1),
                    (x1, x2) => Math.Abs(x2 - x1));

                // If the data is linear, then
                // (ballScale(i+1) - ballScale(i)) = (ballScale(j+1) - ballScale(j))
                // for any frame i and j. Thus, we can take the standard deviation
                // of the ball scale differences. If it is close to 0, then
                // the ball scale differences are all almost the same, thus indicating
                // a linear relationship.
                double ball1StandardDeviation = StandardDeviation(ball1ScaleDifferences);
                double ball2StandardDeviation = StandardDeviation(ball2ScaleDifferences);
                double ball3StandardDeviation = StandardDeviation(ball3ScaleDifferences);

                // Check if the standard deviation is equal to 0 with a possible error
                // of ± 0.1.
                Assert.AreEqual(0, ball1StandardDeviation, 0.1);
                Assert.AreEqual(0, ball2StandardDeviation, 0.1);
                Assert.AreEqual(0, ball3StandardDeviation, 0.1);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Tests that when the slider is shown,
                the balls are no longer visible.
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_SliderShown_THEN_NoBallsVisible()
            {
                Slider slider = GameObject.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();

                foreach (var _ in Enumerable.Range(1, SaveOneBall.NUMBER_OF_ROUNDS))
                {
                    yield return new WaitUntil(() => slider.IsActive());
                    Assert.IsEmpty(Object.FindObjectsOfType<BallProjectile>());

                    Button submitButton = Object.FindObjectOfType<Button>();
                    submitButton.onClick.Invoke();
                }
            }
        }
    }
}