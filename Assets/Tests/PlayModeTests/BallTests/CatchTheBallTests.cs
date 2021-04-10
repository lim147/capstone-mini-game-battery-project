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
using Object = UnityEngine.Object;

namespace PlayModeTests
{
    namespace BallTests
    {
        public class CatchTheBallTests : InputTestFixture
        {
            private Keyboard keyboard;
            /// <summary>
            /// Load the scene for playmode unit tests.
            /// </summary>
            /// The methods with SetUp attribute will be run once before every unit test.
            [SetUp]
            public void LoadScene()
            {
                // Load game scene
                SceneManager.LoadScene(SceneName.CATCH_THE_BALL_SCENE);
                keyboard = InputSystem.AddDevice<Keyboard>();
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
            public IEnumerator WHEN_BallKicked_THEN_BallPositionDoesNotChange()
            {
                // Wait for soccer player to load, kick ball, and then ball to be spawned
                yield return new WaitUntil(() => Object.FindObjectOfType<SoccerPlayer>() != null);
                SoccerPlayer soccerPlayer = Object.FindObjectOfType<SoccerPlayer>();
                yield return new WaitUntil(() => soccerPlayer.PlayerHasKickedBall());
                yield return new WaitUntil(() => Object.FindObjectOfType<BallProjectile>() != null);

                BallProjectile ball = Object.FindObjectOfType<BallProjectile>();
                Vector3 startingPosition = ball.transform.position;

                float endingTime = Time.time + ball.ActualTimeToContact;
                float currentTime = Time.time;

                // For every frame before the ball arrives, its position should be
                // unchanged.
                while (endingTime - currentTime > 0)
                {
                    Assert.AreEqual(startingPosition, ball.transform.position);
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

                foreach (var _ in Enumerable.Range(1, CatchTheBall.NUMBER_OF_ROUNDS))
                {
                    // Wait for soccer player to load, kick ball, and then ball to be spawned
                    yield return new WaitUntil(() => Object.FindObjectOfType<SoccerPlayer>() != null);
                    SoccerPlayer soccerPlayer = Object.FindObjectOfType<SoccerPlayer>();
                    yield return new WaitUntil(() => soccerPlayer.PlayerHasKickedBall());
                    yield return new WaitUntil(() => Object.FindObjectOfType<BallProjectile>() != null);

                    BallProjectile ball = Object.FindObjectOfType<BallProjectile>();
                    actualTimesToContact.Add(ball.ActualTimeToContact);
                    ballInitialRadiuses.Add(ball.BallInitialRadius);

                    Press(keyboard.aKey);
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
            public IEnumerator BallMovesAtConstantVelocity()
            {
                // Wait for soccer player to load, kick ball, and then ball to be spawned
                yield return new WaitUntil(() => Object.FindObjectOfType<SoccerPlayer>() != null);
                SoccerPlayer soccerPlayer = Object.FindObjectOfType<SoccerPlayer>();
                yield return new WaitUntil(() => soccerPlayer.PlayerHasKickedBall());
                yield return new WaitUntil(() => Object.FindObjectOfType<BallProjectile>() != null);

                BallProjectile ball = Object.FindObjectOfType<BallProjectile>();
                Vector3 startingPosition = ball.transform.position;

                float endingTime = Time.time + ball.ActualTimeToContact;
                float currentTime = Time.time;

                // For every frame before the ball arrives, its position should be
                // unchanged.
                List<double> ballScales = new List<double>();
                while (endingTime - currentTime > 0)
                {
                    ballScales.Add(ball.transform.localScale.x);
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
                IEnumerable<double> ballScaleDifferences = Enumerable.Zip(ballScales,
                    ballScales.Skip(1),
                    (x1, x2) => Math.Abs(x2 - x1));

                // If the data is linear, then
                // (ballScale(i+1) - ballScale(i)) = (ballScale(j+1) - ballScale(j))
                // for any frame i and j. Thus, we can take the standard deviation
                // of the ball scale differences. If it is close to 0, then
                // the ball scale differences are all almost the same, thus indicating
                // a linear relationship.
                double standardDeviation = StandardDeviation(ballScaleDifferences);

                // Check if the standard deviation is equal to 0 with a possible error
                // of ± 0.1.
                Assert.AreEqual(0, standardDeviation, 0.1);
            }

            [Description(@"
            <ul>
                <li><b>Test type:</b> Unit Test</li>
                <li><b>Associated SRS requirements:</b> None</li>
                <li><b>Test description:</b> Tests that the gloves move
                horizontally inwards when any key is pressed after the ball
                has been kicked.
                </li>
            </ul>
            ")]
            [UnityTest]
            public IEnumerator WHEN_BallKickedAndAnyKeyPressed_THEN_GlovesMoveHorizontallyInwardsToTargetRing()
            {
                GameObject targetRing = GameObject.Find("Canvas").transform.Find("TargetRing").gameObject;

                foreach (var _ in Enumerable.Range(1, CatchTheBall.NUMBER_OF_ROUNDS))
                {
                    // Wait for soccer player to load, kick ball, and then ball to be spawned
                    yield return new WaitUntil(() => Object.FindObjectOfType<SoccerPlayer>() != null);
                    SoccerPlayer soccerPlayer = Object.FindObjectOfType<SoccerPlayer>();
                    yield return new WaitUntil(() => soccerPlayer.PlayerHasKickedBall());
                    yield return new WaitUntil(() => Object.FindObjectOfType<BallProjectile>() != null);

                    GameObject leftGlove = GameObject.Find("LeftGlove");
                    GameObject rightGlove = GameObject.Find("RightGlove");

                    Assert.False(leftGlove.transform.position.Equals(targetRing.transform.position.x));
                    Assert.False(rightGlove.transform.position.Equals(targetRing.transform.position.x));

                    Press(keyboard.aKey);

                    yield return new WaitForSeconds(
                        CatchTheBall.SECONDS_FOR_GLOVES_TO_MOVE +
                        0.5F * CatchTheBall.SECONDS_FOR_GLOVES_TO_REMAIN_IDLE
                    );

                    Assert.AreEqual(targetRing.transform.position.x, leftGlove.transform.position.x, 0.1F);
                    Assert.AreEqual(targetRing.transform.position.x, rightGlove.transform.position.x, 0.1F);
                }
            }
        }
    }
}