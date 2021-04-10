using UnityEngine;
using UnityEngine.UI;
using Storage;
using System;
using TMPro;

namespace UI
{
    /// <summary>
    /// This module implements [Questionnaire Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#questionnaire-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class QuestionnairePage : MonoBehaviour
    {
        /// <summary>
        /// String identifier for the player
        /// </summary>
        public TMP_InputField Username;

        /// <summary>
        /// Age of the player
        /// </summary>
        public TMP_InputField Userage;

        /// <summary>
        /// Boolean to store if the player has a keyboard
        /// </summary>
        public Toggle HasKeyboard;

        /// <summary>
        /// Boolean to store if the player has a mouse
        /// </summary>
        public Toggle HasMouse;

        /// <summary>
        /// Text that is shown if required fields are not entered.
        /// </summary>
        public Text EnterRequiredFieldsText;


        // Datatype for storing player information
        private static PlayerStorage player = new PlayerStorage();

        /// <summary>
        /// ClickOnSubmitButton is called when the player clicks on the
        /// Submit button on the Questionnaire scene. The information
        /// entered by the player will be used to create a player object
        /// </summary>
        public void ClickOnSubmitButton()
        {
            if (RequiredFieldsAreFilled())
            {
                // Call function to store player information
                CreatePlayer();
                GetPlayer();
                SceneSwitcher.NextScene();
            } else
            {
                EnterRequiredFieldsText.gameObject.SetActive(true);
            }
        }

        private bool RequiredFieldsAreFilled()
        {
            return Userage.text.Length > 0;
        }


        /// <summary>
        /// GetPlayer is the getter function to get the player object.
        /// </summary>
        public static PlayerStorage GetPlayer()
        {
            return player;
        }



        /// <summary>
        /// CreatePlayer is to create an player storage object to store the player
        /// information collected in the questionnaire page.
        /// </summary>
        private void CreatePlayer()
        {
            // Generate a userId
            Guid userId = Guid.NewGuid();

            // Get the username from the input field
            string name = Username.text;

            // Get the age from the input field
            int age = int.Parse(Userage.text);

            // Fill in player information in the player object
            player.UserId = userId;
            player.Age = age;
            player.Name = name;

            // Check toggle to see if keyboard option is on and then set the value
            if (HasKeyboard.isOn)
            {
                player.KeyBoard = true;
            }
            else
            {
                player.KeyBoard = false;
            }

            // Check toggle to see if mouse option is on and then set the value
            if (HasMouse.isOn)
            {
                player.Mouse = true;
            }
            else
            {
                player.Mouse = false;
            }

            Debug.Log("player created successfully");
        }

       
    }
}
