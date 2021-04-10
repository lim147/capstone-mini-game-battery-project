using System;

namespace Storage
{
    /// <summary>
    /// This module implements [Storage Interface Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#istorage-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public interface IStorage
    {

        /// <summary>
        /// Stores the data recorded from a battery session
        /// to permanent storage.
        /// </summary>
        /// <param name="batterySession">The battery session data to be stored.</param>
        /// <param name="onStorage">An optional action to perform once
        /// the battery session storage has been successfully been saved.
        /// </param>
        void Store(BatterySessionStorage batterySession,
            Action onStorage = null);

        /// <summary>
        /// Retrieves the data recorded from a battery session.
        /// </summary>
        /// <param name="batterySessionId">The ID of the battery session to retrieve.
        /// </param>
        /// <param name="onRetrieval">The action to perform once the battery
        /// session storage has been successfully been retrieved.</param>
        void Retrieve(Guid batterySessionId,
            Action<BatterySessionStorage> onRetrieval = null);
    }
}

