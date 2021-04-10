using System;

namespace Storage
{
    /// <summary>
    /// This module implements [PlayerStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#playerstorage-module)
    /// found in the Architecture and Module Design Document.
    /// 
    /// This is data type for storing the player information
    /// collected in <see cref="UI.QuestionnairePage"/>.
    /// </summary>
    public struct PlayerStorage
    {
        /// <summary>
        /// The name of the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The global unique id assigned to the player
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The age of the player
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Bool value to represent if the player has a keyboard
        /// </summary>
        public bool KeyBoard { get; set; }

        /// <summary>
        /// Bool value to represent if the player has a mouse
        /// </summary>
        public bool Mouse { get; set; }
    }
}
