using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Helper;
using Storage;
using UnityEngine;
using UnityEngine.InputSystem;
using static Storage.CatchTheBallStorage;

namespace Games
{
    /// <summary>
    /// This module implements [CatchTheBall Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#catchtheball-module)
    /// found in the Architecture and Module Design Document.
    /// </summary>
    public class CatchTheBall : AbstractBallGameWithKeyInput<CatchTheBallStorage, BallRound>
    {
        // The target ring is invisible in the game but is what is used
        // to calculate the actual time to contact.
        public GameObject TargetRing;

        // Note: gloves are only used for visual effect. All calculations are
        // done against the target rings. As a result, in the Editor, you should
        // ensure that the gloves are scaled so they appear the same size as the target
        // ring and one glove is to the ring's left and one is to its right.
        public GameObject LeftGlove;
        public GameObject RightGlove;

        private Vector3 LeftGloveStartingPosition;
        private Vector3 RightGloveStartingPosition;

        public const float SECONDS_FOR_GLOVES_TO_MOVE = 0.5F;
        public const float SECONDS_FOR_GLOVES_TO_REMAIN_IDLE = 1F;

        public override void SetupGame()
        {
            LeftGloveStartingPosition = LeftGlove.transform.position;
            RightGloveStartingPosition = RightGlove.transform.position;
        }

        public override IEnumerator StartRound()
        {
            BallRound ballRound = GenerateDataForRound(ballStorage.TargetRadius);

            // The ball is at the centre of the screen in this round
            Vector3 ballPosition = new Vector3(0, 0, 0);

            SoccerPlayer soccerPlayer = InstantiateSoccerPlayer(ballPosition, ballRound.InitialBallRadius);

            yield return new WaitUntil(() => soccerPlayer.PlayerHasKickedBall());

            BallProjectile ballProjectile = InstantiateBallProjectile(ballPosition,
                ballStorage.TargetRadius,
                ballRound.InitialBallRadius,
                ballRound.ActualTimeToContact,
                // Balls don't disappear in Catch The Ball
                ballTimeBeforeDisappearance: null);

            // Record the time at which the "kick" takes place
            float beginTime = Time.time;

            // Make round end when the user hits any
            // key, or when they have waited too long (2 times the actual
            // time to contact)
            yield return StartCoroutine(
                WaitForInputOrTimeExpires(
                    beginTime: beginTime,
                    maxTimeToWait: 2 * ballRound.ActualTimeToContact,
                    inputReceived: () => Keyboard.current.anyKey.isPressed,
                    ballProjectiles: new List<BallProjectile> { ballProjectile },
                    ballRound: ballRound
                )
            );
        }

        public override BallRound GenerateDataForRound(float targetRingRadius)
        {
            BallRound ballRound = new BallRound
            {
                InitialBallRadius = GenerateInitialBallRadius(targetRingRadius),
                ActualTimeToContact = GenerateActualTimeToContact(),
            };

            return ballRound;
        }

        public override float CalculateTargetRadius()
        {
            CircleCollider2D targetRingCollider = TargetRing.GetComponent<CircleCollider2D>();

            // To get the actual radius of the target ring, we need to know
            // its scale. Since it is a circle, the x-scale and y-scale are equal.
            return targetRingCollider.radius * targetRingCollider.transform.localScale.x;
        }

        public override IEnumerator EndRound(BallRound ballRound, List<BallProjectile> ballProjectiles, float beginTime, float endTime)
        {
            // Record the player's predicted time to contact
            ballRound.PredictedTimeToContact = endTime - beginTime;
            // Save all data about the round
            ballStorage.Rounds.Add(ballRound);

            MoveGlovesToTargetRing(duration: SECONDS_FOR_GLOVES_TO_MOVE);

            yield return new WaitForSeconds(SECONDS_FOR_GLOVES_TO_MOVE);
            yield return new WaitForSeconds(SECONDS_FOR_GLOVES_TO_REMAIN_IDLE);

            // Clean up round game objects and move gloves back to original position
            ballProjectiles.ForEach(ballProjectile => Destroy(ballProjectile.gameObject));

            MoveGlovesToOriginalPosition();

            yield return new WaitForSeconds(SECONDS_FOR_GLOVES_TO_MOVE);
            yield return new WaitForSeconds(SECONDS_BETWEEN_ROUNDS);
        }

        /// <summary>
        /// Moves the left and right gloves inwards towards where the
        /// invisible target ring is.
        /// </summary>
        /// <param name="duration">How long the animation should last.</param>
        private void MoveGlovesToTargetRing(float duration)
        {
            LeftGlove.transform.DOMoveX(TargetRing.transform.position.x, duration: duration);
            RightGlove.transform.DOMoveX(TargetRing.transform.position.x, duration: duration);
        }

        /// <summary>
        /// Moves the left and right gloves back to where they were when the round
        /// started.
        /// </summary>
        private void MoveGlovesToOriginalPosition()
        {
            LeftGlove.transform.DOMove(LeftGloveStartingPosition, SECONDS_FOR_GLOVES_TO_MOVE);
            RightGlove.transform.DOMove(RightGloveStartingPosition, SECONDS_FOR_GLOVES_TO_MOVE);
        }
    }
}

