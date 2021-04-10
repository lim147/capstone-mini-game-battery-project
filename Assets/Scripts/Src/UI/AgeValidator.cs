using UnityEngine;

namespace UI
{
    /// <summary>
    /// This module implements [AgeValidator Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#agevalidator-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// 
    /// This is a validator meant to be applied to a Text Mesh Pro input field
    /// that ensures that only 2-digit ages which do not start with
    /// the digit 0 can be entered.
    ///
    /// Note that this is a ScriptableObject, so you'll have to create
    /// an instance of it via the Assets -> Create -> Age Validator 
    /// </summary>
    [CreateAssetMenu(fileName = "Age Validator", menuName = "Age Validator")]
    public class AgeValidator : TMPro.TMP_InputValidator
    {
        /// <summary>
        /// Override Validate method to implement your own validation.
        /// </summary>
        /// <param name="text">This is a reference pointer to the actual text in the input field;
        /// changes made to this text argument will also result in changes made
        /// to text shown in the input field.
        /// </param>
        /// <param name="pos">This is a reference pointer to the input field's
        /// text insertion index position (your blinking caret cursor);
        /// changing this value will also change the index of the input field's insertion position.
        /// </param>
        /// <param name="ch">This is the character being typed into the input field.
        /// </param>
        /// <remarks>See https://www.reddit.com/r/Unity3D/comments/dfnfzv/how_to_create_a_custom_input_validator_for_text/
        /// for telephone number example.
        /// </remarks>
        /// <returns>Return the character you'd allow into the text.</returns>
        public override char Validate(ref string text, ref int pos, char ch)
        {
            if (FirstCharIsNonZeroDigit(ch, pos) || SecondCharIsDigit(ch, pos))
            {
                text += ch;
                pos++;
                return ch;
            }
            else
            {
                // Do not accept this character.
                return '\0';
            }
        }

        private bool FirstCharIsNonZeroDigit(char ch, int pos)
        {
            return pos == 0 && char.IsDigit(ch) && ch != '0';
        }

        private bool SecondCharIsDigit(char ch, int pos)
        {
            return pos == 1 && char.IsDigit(ch);
        }
    }
}