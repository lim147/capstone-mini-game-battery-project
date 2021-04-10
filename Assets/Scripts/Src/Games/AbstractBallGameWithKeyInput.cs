using System;
using System.Collections;
using System.Collections.Generic;
using Helper;
using Storage;
using UnityEngine;

namespace Games
{
    /// <summary>
    /// This module implements [AbstractBallGameWithKeyInput Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#generic-abstractballgamewithkeyinputballstorage-ballroundstorage-module)
    /// found in the Architecture and Module Design Document.
    /// This abstract class is used in Ball-type games where the keyboard is used.
    /// </summary>
    /// <typeparam name="BallStorage">The storage object of the concrete Ball game.</typeparam>
    /// <typeparam name="BallRoundStorage">The storage object of a round of the concrete Ball game.</typeparam>
    public abstract class AbstractBallGameWithKeyInput<BallStorage, BallRoundStorage> : AbstractBallGame<BallStorage, BallRoundStorage>
        where BallStorage : IBallStorage<BallRoundStorage>, new()
    {
        /// <summary>
        /// Performs actions that should be done when a round has completed.
        /// </summary>
        /// <param name="ballRound">The data used during the round.</param>
        /// <param name="ballProjectiles">The ball projectiles used during this round.</param>
        /// <param name="beginTime">The time when the round began.</param>
        /// <param name="endTime">The time when the round completed.</param>
        /// <returns></returns>
        public abstract IEnumerator EndRound(BallRoundStorage ballRound, List<BallProjectile> ballProjectiles, float beginTime, float endTime);

        /// <summary>
        /// Waits for the player to enter certain input
        /// or else times out.
        /// </summary>
        /// <param name="beginTime">The time at which waiting begins.</param>
        /// <param name="maxTimeToWait">How long we should wait for the player to
        /// enter some input before timing out.</param>
        /// <param name="inputReceived">A boolean function which returns
        /// `true` when the player has entered acceptable input.</param>
        /// <param name="recordEndTime">A method which is called
        /// when the player has entered correct input, or the time
        /// has expired, that can be used to record the time duration
        /// of this event.</param>
        /// <returns></returns>
        public IEnumerator WaitForInputOrTimeExpires(float beginTime,
            float maxTimeToWait, Func<bool> inputReceived, BallRoundStorage ballRound, List<BallProjectile> ballProjectiles)
        {
            yield return new WaitUntil(() =>
                inputReceived() || Time.time - beginTime > maxTimeToWait
            );
            yield return StartCoroutine(EndRound(ballRound, ballProjectiles, beginTime, Time.time));
        }
    }
}
