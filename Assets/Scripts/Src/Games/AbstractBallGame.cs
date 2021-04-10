using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helper;
using UI;
using Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Games
{
    /// <summary>
    /// This module implements [AbstractBallGame Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#generic-abstractballgameballstorage-ballroundstorage-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// 
    /// This module is described in the [SRS](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#3-time-to-contact-ball).
    /// </summary>
    /// <typeparam name="BallStorage">The storage object of the concrete ball game.</typeparam>
    /// <typeparam name="BallRoundStorage">The storage object of a round of the concrete ball game.</typeparam>
    public abstract class AbstractBallGame<BallStorage, BallRoundStorage> : MonoBehaviour
        where BallStorage : IBallStorage<BallRoundStorage>, new()
    {
        // One or more instances of a ball projectile will be instantiated
        // in this mini-game depending on the round.
        public GameObject BallProjectilePrefab;
        // The prefab of the animated soccer player that appears to kick the ball.
        public GameObject SoccerPlayerPrefab;

        // Where the game data will be stored during the game.
        public static readonly BallStorage ballStorage = new BallStorage();

        public const int NUMBER_OF_ROUNDS = 3;
        public const float SECONDS_BETWEEN_ROUNDS = 1;
        private const float MIN_BALL_RADIUS_AS_PERCENTAGE_OF_TARGET_RING_RADIUS = 0.01F;
        private const float MAX_BALL_RADIUS_AS_PERCENTAGE_OF_TARGET_RING_RADIUS = 0.3F;
        private const int MIN_TIME_TO_CONTACT_IN_SECONDS = 5;
        private const int MAX_TIME_TO_CONTACT_IN_SECONDS = 10;

        /// <summary>
        /// This method computes the radius of the target ring(s) in
        /// the game.
        /// </summary>
        /// <returns>The radius of the target ring(s).</returns>
        public abstract float CalculateTargetRadius();

        /// <summary>
        /// Generates the data used in a round of the game.
        /// </summary>
        /// <param name="targetRingRadius">The computed target radius.</param>
        /// <returns>The data to be used in the round of the game.</returns>
        public abstract BallRoundStorage GenerateDataForRound(float targetRingRadius);

        /// <summary>
        /// Implements the logic of what should happen in a game round.
        /// </summary>
        public abstract IEnumerator StartRound();

        /// <summary>
        /// How all ball games start.
        /// </summary>
        public void Start()
        {
            ballStorage.Rounds = new List<BallRoundStorage>();
            ballStorage.TargetRadius = CalculateTargetRadius();
            StartCoroutine(StartGame());
        }

        /// <summary>
        /// Each ball-type game simply runs through each round sequentially
        /// before returning to the menu.
        /// </summary>
        /// <returns></returns>
        public IEnumerator StartGame()
        {
            SetupGame();

            foreach (var _ in Enumerable.Range(0, NUMBER_OF_ROUNDS))
            {
                yield return StartCoroutine(StartRound());
            }

            SceneManager.LoadScene(SceneName.MENU_SCENE);
        }

        /// <summary>
        /// This method should be overriden by implementing classes which need
        /// to do some pre-game setup before any rounds have started.
        /// </summary>
        public virtual void SetupGame()
        {
            
        }

        /// <summary>
        /// Instantiates a ball projectile that will appear to approach the player.
        /// </summary>
        /// <param name="ballPosition">The position of the ball in the scene.</param>
        /// <param name="targetRingRadius">The radius of the target ring, which
        /// also serves as the final radius of the ball.</param>
        /// <param name="ballInitialRadius">The radius of the ball before
        /// it has been "kicked".</param>
        /// <param name="actualTimeToContact">How long it actually takes
        /// the ball to reach the target ring from its original radius.</param>
        /// <param name="ballTimeBeforeDisappearance">The duration of time before the ball
        /// disappears from the screen.</param>
        /// <returns>The instantiated ball projectile with the given parameters.</returns>
        public BallProjectile InstantiateBallProjectile(Vector3 ballPosition,
            float targetRingRadius, float ballInitialRadius,
            float actualTimeToContact, float? ballTimeBeforeDisappearance)
        {
            BallProjectile ballProjectile = Instantiate(BallProjectilePrefab,
                ballPosition, Quaternion.identity, gameObject.GetComponent<Canvas>().transform)
                .GetComponent<BallProjectile>();

            ballProjectile.TargetRingRadius = targetRingRadius;
            ballProjectile.BallInitialRadius = ballInitialRadius;
            ballProjectile.ActualTimeToContact = actualTimeToContact;
            ballProjectile.BallTimeBeforeDisappearance = ballTimeBeforeDisappearance;

            return ballProjectile;
        }

        /// <summary>
        /// Creates a kicking soccer player at the specified ball position.
        /// </summary>
        /// <param name="ballPosition">The position where the player will kick.</param>
        /// <param name="initialBallRadius">The radius of the ball initially.</param>
        /// <returns>The created soccer player object.</returns>
        public SoccerPlayer InstantiateSoccerPlayer(Vector3 ballPosition, float initialBallRadius)
        {
            SoccerPlayer player = Instantiate(SoccerPlayerPrefab, ballPosition,
                Quaternion.identity).GetComponent<SoccerPlayer>();

            // This number is used to determine how big the player should
            // appear relative to the ball. This number was obtained by seeing
            // what looked good in gameplay, but can be modified as you see fit.
            float playerToBallScale = initialBallRadius / 30;

            player.transform.localScale = new Vector3(playerToBallScale,
                playerToBallScale, playerToBallScale);
            return player;
        }

        /// <summary>
        /// Generates an initial ball radius from the target ring radius.
        /// </summary>
        /// <param name="targetRingRadius">The radius of the target ring.</param>
        /// <returns>The initial ball radius.</returns>
        public float GenerateInitialBallRadius(float targetRingRadius)
        {
            // The initial ball radius is currently between 1% and 30% of that of
            // the target ring's.
            return targetRingRadius * RamGenerator.GenerateARamNum(MIN_BALL_RADIUS_AS_PERCENTAGE_OF_TARGET_RING_RADIUS,
                MAX_BALL_RADIUS_AS_PERCENTAGE_OF_TARGET_RING_RADIUS);
        }

        /// <summary>
        /// Generates an actual time to contact.
        /// </summary>
        /// <returns>The generated actual time to contact.</returns>
        public float GenerateActualTimeToContact()
        {
            return RamGenerator.GenerateARamNum(MIN_TIME_TO_CONTACT_IN_SECONDS,
                MAX_TIME_TO_CONTACT_IN_SECONDS);
        }

        /// <summary>
        /// This method retrieves the data recorded during the game.
        /// </summary>
        /// <remarks>This method should only be called when the game is over.</remarks>
        /// <returns>The data recorded during the game.</returns>
        public static BallStorage GetGameplayData()
        {
            return ballStorage;
        }
    }
}
