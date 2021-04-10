using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helper;
using Storage;
using UnityEngine;
using UnityEngine.InputSystem;
using static Storage.SaveOneBallStorage;

namespace Games
{
    /// <summary>
    /// This module implements [SaveOneBall Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#saveoneball-module)
    /// found in the Architecture and Module Design Document.
    /// </summary>
    public class SaveOneBall : AbstractBallGameWithKeyInput<SaveOneBallStorage, BallRound>
    {
        // The target rings are invisible, but in the game they are what is used
        // to calculate the actual time to contact.
        public GameObject LeftTargetRing;
        public GameObject RightTargetRing;

        // Note: Glove is only used for visual effect. All calculations are
        // done against the target rings. As a result, in the Unity editor, you should
        // ensure that the glove is scaled so it appears the same size as the target
        // ring and it is placed horizontally in between both target rings
        public GameObject Glove;

        private Vector3 GloveStartingPosition;

        private const float SECONDS_FOR_GLOVE_TO_MOVE = 0.5F;
        private const float SECONDS_FOR_GLOVE_TO_REMAIN_IDLE = 1F;

        // The key the player should use to indicate that the left
        // ball will appear first.
        public const Key LEFT_BALL_KEY = Key.A;
        // The key the player should use to indicate that the right
        // ball will appear first.
        public const Key RIGHT_BALL_KEY = Key.L;

        public override void SetupGame()
        {
            GloveStartingPosition = Glove.transform.position;
        }

        public override IEnumerator StartRound()
        {
            BallRound ballRound = GenerateDataForRound(ballStorage.TargetRadius);

            Vector3 leftBallPosition = LeftTargetRing.transform.position;
            Vector3 rightBallPosition = RightTargetRing.transform.position;

            SoccerPlayer leftSoccerPlayer = InstantiateSoccerPlayer(leftBallPosition, ballRound.InitialLeftBallRadius);
            SoccerPlayer rightSoccerPlayer = InstantiateSoccerPlayer(rightBallPosition, ballRound.InitialRightBallRadius);

            yield return new WaitUntil(() => leftSoccerPlayer.PlayerHasKickedBall()
                && rightSoccerPlayer.PlayerHasKickedBall()
            );

            BallProjectile leftBallProjectile = InstantiateBallProjectile(leftBallPosition,
                ballStorage.TargetRadius,
                ballRound.InitialLeftBallRadius,
                ballRound.LeftActualTimeToContact,
                // Ball doesn't disappear in Save One Ball
                ballTimeBeforeDisappearance: null
            );

            BallProjectile rightBallProjectile = InstantiateBallProjectile(rightBallPosition,
                ballStorage.TargetRadius,
                ballRound.InitialRightBallRadius,
                ballRound.RightActualTimeToContact,
                // Ball doesn't disappear in Save One Ball
                ballTimeBeforeDisappearance: null
            );

            // Record the time at which the "kick" takes place
            float beginTime = Time.time;

            // Make round end when the user hits any
            // key, or when they have waited too long (2 times the actual
            // time to contact)
            yield return StartCoroutine(
                WaitForInputOrTimeExpires(
                    beginTime: beginTime,
                    maxTimeToWait: 2 * Math.Min(ballRound.LeftActualTimeToContact, ballRound.RightActualTimeToContact),
                    inputReceived: () => Keyboard.current[LEFT_BALL_KEY].isPressed || Keyboard.current[RIGHT_BALL_KEY].isPressed,
                    ballProjectiles: new List<BallProjectile> { leftBallProjectile, rightBallProjectile },
                    ballRound: ballRound
                )
            );
        }

        public override BallRound GenerateDataForRound(float targetRingRadius)
        {
            BallRound ballRound = new BallRound
            {
                InitialLeftBallRadius = GenerateInitialBallRadius(targetRingRadius),
                InitialRightBallRadius = GenerateInitialBallRadius(targetRingRadius),

                LeftActualTimeToContact = GenerateActualTimeToContact(),
                RightActualTimeToContact = GenerateActualTimeToContact()
            };

            return ballRound;
        }

        public override float CalculateTargetRadius()
        {
            // Both target rings have the same radius, so using either is fine.
            CircleCollider2D targetRingCollider = LeftTargetRing.GetComponent<CircleCollider2D>();

            return targetRingCollider.radius * targetRingCollider.transform.localScale.x;
        }

        public override IEnumerator EndRound(BallRound ballRound, List<BallProjectile> ballProjectiles, float beginTime, float endTime)
        {
            bool predictedLeftBallArrivedFirst = Keyboard.current[LEFT_BALL_KEY].isPressed;
            // Record the player's predicted time to contact
            ballRound.PredictedTimeToContact = endTime - beginTime;
            bool leftBallActuallyArrivedFirst = ballRound.LeftActualTimeToContact
                        <= ballRound.RightActualTimeToContact;

            // Whether the player accurately predicted which ball would arrive first
            ballRound.DidPredictFirstArrivingBall = predictedLeftBallArrivedFirst
                == leftBallActuallyArrivedFirst;

            // Save all data about the round
            ballStorage.Rounds.Add(ballRound);

            MoveGloveToTargetRing(duration: SECONDS_FOR_GLOVE_TO_MOVE,
                moveLeftHand: predictedLeftBallArrivedFirst);

            yield return new WaitForSeconds(SECONDS_FOR_GLOVE_TO_MOVE);
            yield return new WaitForSeconds(SECONDS_FOR_GLOVE_TO_REMAIN_IDLE);

            // Clean up round game objects and move gloves back to original position
            ballProjectiles.ForEach(ballProjectile => Destroy(ballProjectile.gameObject));

            MoveGloveToOriginalPosition();

            yield return new WaitForSeconds(SECONDS_FOR_GLOVE_TO_MOVE);
            yield return new WaitForSeconds(SECONDS_BETWEEN_ROUNDS);
        }

        /// <summary>
        /// Moves the glove either left or right of where the
        /// invisible target rings are.
        /// </summary>
        /// <param name="duration">How long the animation should last.</param>
        /// <param name="moveLeftHand">Whether the left hand should be moved. If false,
        /// then it will be the right hand that will be moved.</param>
        private void MoveGloveToTargetRing(float duration, bool moveLeftHand)
        {
            if (moveLeftHand)
            {
                Glove.transform.DOMoveX(LeftTargetRing.transform.position.x, duration: duration);
            } else
            {
                Glove.transform.DOMoveX(RightTargetRing.transform.position.x, duration: duration);
            }
        }

        /// <summary>
        /// Move the glove back to its original position where it was when
        /// the round started.
        /// </summary>
        private void MoveGloveToOriginalPosition()
        {
            Glove.transform.DOMove(GloveStartingPosition, SECONDS_FOR_GLOVE_TO_MOVE);
        }
    }
}

