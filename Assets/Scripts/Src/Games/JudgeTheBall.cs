using System;
using System.Collections;
using Helper;
using Storage;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Storage.JudgeTheBallStorage;

namespace Games
{
    /// <summary>
    /// This module implements [JudgeTheBall Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#judgetheball-module)
    /// found in the Architecture and Module Design Document.
    /// </summary>
    public class JudgeTheBall : AbstractBallGame<JudgeTheBallStorage, BallRound>
    {
        private const float MIN_PLAYER_BALL_RADIUS_AS_PERCENTAGE_OF_FAST_BALL_RADIUS = 0.01F;
        private const float MAX_PLAYER_BALL_RADIUS_AS_PERCENTAGE_OF_FAST_BALL_RADIUS = 0.3F;
        private const float MIN_SLOW_BALL_RADIUS_AS_PERCENTAGE_OF_FAST_BALL_RADIUS = 0.5F;
        private const float MAX_SLOW_BALL_RADIUS_AS_PERCENTAGE_OF_FAST_BALL_RADIUS = 0.8F;
        private const int MIN_ADDITIONAL_SECONDS_IN_TIME_TO_CONTACT = 1;
        private const int MAX_ADDITIONAL_SECONDS_IN_TIME_TO_CONTACT = 3;
        private const int MIN_SECONDS_BEFORE_BALL_DISAPPEARANCE = 2;
        private const int MAX_SECONDS_BEFORE_BALL_DISAPPEARANCE = 4;

        // The target ring of the first-arriving reference ball.
        public GameObject LeftTargetRing;

        // The target ring of the player's ball.
        public GameObject MiddleTargetRing;

        // The target ring of the last-arriving reference ball.
        public GameObject RightTargetRing;

        // Elements which will be dynamically shown once
        // the balls disappear.
        public Slider Slider;
        public Button SubmitButton;
        public Text FirstToArriveText;
        public Text LastToArriveText;

        public override IEnumerator StartRound()
        {
            BallRound ballRound = GenerateDataForRound(ballStorage.TargetRadius);

            Vector3 fastBallPosition = LeftTargetRing.transform.position;
            Vector3 playerBallPosition = MiddleTargetRing.transform.position;
            Vector3 slowBallPosition = RightTargetRing.transform.position;

            SoccerPlayer fastBallPlayer = InstantiateSoccerPlayer(fastBallPosition, ballRound.InitialFastBallRadius);
            SoccerPlayer playerBallPlayer = InstantiateSoccerPlayer(playerBallPosition, ballRound.InitialPlayerBallRadius);
            SoccerPlayer slowBallPlayer = InstantiateSoccerPlayer(slowBallPosition, ballRound.InitialSlowBallRadius);

            yield return new WaitUntil(() => fastBallPlayer.PlayerHasKickedBall()
                && playerBallPlayer.PlayerHasKickedBall() && slowBallPlayer.PlayerHasKickedBall()
            );

            BallProjectile fastBallProjectile = InstantiateBallProjectile(fastBallPosition,
                ballStorage.TargetRadius,
                ballRound.InitialFastBallRadius,
                ballRound.FastBallActualTimeToContact,
                ballRound.TimeBeforeDisappearence
            );

            BallProjectile playerBallProjectile = InstantiateBallProjectile(playerBallPosition,
                ballStorage.TargetRadius,
                ballRound.InitialPlayerBallRadius,
                ballRound.PlayerBallActualTimeToContact,
                ballRound.TimeBeforeDisappearence
            );

            BallProjectile slowBallProjectile = InstantiateBallProjectile(slowBallPosition,
               ballStorage.TargetRadius,
               ballRound.InitialSlowBallRadius,
               ballRound.SlowBallActualTimeToContact,
               ballRound.TimeBeforeDisappearence
           );

            // After the balls have disappeared,
            yield return new WaitForSeconds(ballRound.TimeBeforeDisappearence);

            // Show the slider and submit button
            Slider.gameObject.SetActive(true);
            SubmitButton.gameObject.SetActive(true);

            // Make the lower value of the slider equal to arrival
            // time of the faster ball; make the upper value of the slider
            // equal to the arrival time of the slower ball
            Slider.minValue = ballRound.FastBallActualTimeToContact;
            Slider.maxValue = ballRound.SlowBallActualTimeToContact;

            // Move slider to half-way position
            Slider.value = (Slider.minValue + Slider.maxValue) / 2;

            bool buttonHasBeenClicked = false;
            // When the round is about to end,
            UnityAction whenButtonClicked = () =>
            {
                // Record the player's predicted time to contact
                // by seeing how they judged it compared to the reference
                // first-arriving and last-arriving balls.
                ballRound.PredictedTimeToContact = Slider.value;

                // Save data for this round
                ballStorage.Rounds.Add(ballRound);

                // Hide the slider and submit button
                Slider.gameObject.SetActive(false);
                SubmitButton.gameObject.SetActive(false);

                buttonHasBeenClicked = true;
            };

            // When the submit button is pressed, the round should end
            SubmitButton.onClick.AddListener(whenButtonClicked);

            yield return new WaitUntil(() => buttonHasBeenClicked);
        }

        public override BallRound GenerateDataForRound(float targetRingRadius)
        {
            float initialFastBallRadius = GenerateInitialBallRadius(targetRingRadius);
            float initialPlayerBallRadius = initialFastBallRadius * RamGenerator.GenerateARamNum(MIN_PLAYER_BALL_RADIUS_AS_PERCENTAGE_OF_FAST_BALL_RADIUS,
                MAX_PLAYER_BALL_RADIUS_AS_PERCENTAGE_OF_FAST_BALL_RADIUS);
            float initialSlowBallRadius = initialPlayerBallRadius * RamGenerator.GenerateARamNum(MIN_SLOW_BALL_RADIUS_AS_PERCENTAGE_OF_FAST_BALL_RADIUS,
                MAX_SLOW_BALL_RADIUS_AS_PERCENTAGE_OF_FAST_BALL_RADIUS);

            float fastBallActualTimeToContact = GenerateActualTimeToContact();
            float playerBallActualTimeToContact = fastBallActualTimeToContact + RamGenerator.GenerateARamNum(MIN_ADDITIONAL_SECONDS_IN_TIME_TO_CONTACT,
                MAX_ADDITIONAL_SECONDS_IN_TIME_TO_CONTACT);
            float slowBallActualTimeToContact = playerBallActualTimeToContact + RamGenerator.GenerateARamNum(MIN_ADDITIONAL_SECONDS_IN_TIME_TO_CONTACT,
                MAX_ADDITIONAL_SECONDS_IN_TIME_TO_CONTACT);

            BallRound ballRound = new BallRound
            {
                InitialSlowBallRadius = initialSlowBallRadius,
                InitialPlayerBallRadius = initialPlayerBallRadius,
                InitialFastBallRadius = initialFastBallRadius,

                SlowBallActualTimeToContact = slowBallActualTimeToContact,
                PlayerBallActualTimeToContact = playerBallActualTimeToContact,
                FastBallActualTimeToContact = fastBallActualTimeToContact
            };

            // The ball disappearance time is calculated by taking the
            // fastest time-to-contact ball time, and then
            // subtracting a random value from this.
            ballRound.TimeBeforeDisappearence = Math.Min(ballRound.SlowBallActualTimeToContact,
                ballRound.FastBallActualTimeToContact)
                - RamGenerator.GenerateARamNum(MIN_SECONDS_BEFORE_BALL_DISAPPEARANCE,
                MAX_SECONDS_BEFORE_BALL_DISAPPEARANCE);

            return ballRound;
        }

        public override float CalculateTargetRadius()
        {
            // All target rings have the same radius, so using either is fine.
            CircleCollider2D targetRingCollider = LeftTargetRing.GetComponent<CircleCollider2D>();

            return targetRingCollider.radius * targetRingCollider.transform.localScale.x;
        }
    }
}

