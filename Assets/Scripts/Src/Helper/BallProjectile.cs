using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Helper
{
    /// <summary>
    /// This script is meant to control the projectile behaviour of a
    /// ball object in the Ball mini-game. It should be attached to the
    /// BallProjectile prefab.
    /// This module implements [BallProjectile Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#ballprojectile-module)
    /// found in
    /// the Architecture and Module Design Document.

    /// </summary>
    public class BallProjectile : MonoBehaviour
    {
        public float BallInitialRadius;
        // The radius of the target ring is needed as this is the final
        // size that the ball will grow to.
        public float TargetRingRadius;
        public float ActualTimeToContact;
        public float? BallTimeBeforeDisappearance;

        private SpriteRenderer ballSprite;
        private CircleCollider2D ballCircleCollider;

        /// <summary>
        /// Performs a tweening animation to make the ball projectile
        /// grow from its initial size to the size of the target ring. The
        /// values needed for the tweening library are calculated using some
        /// simple ratios.
        /// </summary>
        void Start()
        {
            ballSprite = gameObject.GetComponent<SpriteRenderer>();
            ballCircleCollider = gameObject.GetComponent<CircleCollider2D>();

            // Finds the scale of the ball using the desired radius and the
            // current radius of the circle collider.
            float BallInitialSizeScale = BallInitialRadius / ballCircleCollider.radius;

            // The ball is a 2D circle, so we need only scale the x and y values
            // and give them an equal value.
            ballCircleCollider.transform.localScale = new Vector3(BallInitialSizeScale,
                BallInitialSizeScale, 0);

            // The real pixel size of the ball radius is obtained from
            // the circle collider of the ball relative to the ball's scale in
            // the scene.
            float ballFinalScale = CalculateScaleToApplyToBall(TargetRingRadius,
                BallInitialRadius, BallInitialSizeScale);

            ballSprite.transform
                .DOScale(new Vector3(ballFinalScale, ballFinalScale, 0), ActualTimeToContact)
                .SetEase(Ease.Linear);

            if (BallTimeBeforeDisappearance.HasValue)
            {
                StartCoroutine(MakeBallInvisibleAfterNSeconds(BallTimeBeforeDisappearance.Value));
            }

            // Make the ball twirl as it approaches the player.
            ballSprite.transform
                .DORotate(new Vector3(0, 0, -10), 0.05F)
                .SetLoops(-1, LoopType.Incremental);
        }

        /// <summary>
        /// Renders the ball projectile invisible to the user `n` seconds
        /// after this method has been called.
        /// </summary>
        /// <param name="n">The duration to wait before making the ball
        /// invisible.</param>
        private IEnumerator MakeBallInvisibleAfterNSeconds(float n)
        {
            yield return new WaitForSeconds(n);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// The soccer ball is placed at the same position coordinates of the scene
        /// as those of the target ring. The soccer ball starts much smaller than the
        /// target ring and grows over time to give the pseudo-3D illusion of
        /// the soccer ball approaching the target ring. We obtain this illusion
        /// by performing a tween between the ball's initial scale size and
        /// its final scale size. The final scale size of the ball will make the
        /// soccer ball's radius the same as the target ring's (so that the
        /// soccer ball's outline just touches the target ring).
        /// </summary>
        /// <param name="targetRingRadius">The radius of the target ring.</param>
        /// <param name="ballRadius">The starting radius of the ball.</param>
        /// <param name="ballInitialScale">The initial scale of the ball before
        /// it has been "kicked".</param>
        /// <returns>The scale to apply to the ball.</returns>
        private float CalculateScaleToApplyToBall(float targetRingRadius,
            float ballRadius, float ballInitialScale)
        {
            return targetRingRadius * ballInitialScale / ballRadius;
        }
    }
}