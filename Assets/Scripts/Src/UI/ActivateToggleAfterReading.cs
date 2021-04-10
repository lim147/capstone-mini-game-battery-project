using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This module implements [ActivateToggleAfterReading Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#activatetoggleafterreading-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// The module is to only make a toggle interactble after the user has fully
    /// read some text as indicated by scrolling all the way down.
    ///
    /// This script should be attached to the Toggle in the Info scene.
    /// </summary>
    public class ActivateToggleAfterReading : MonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// The Scrollbar associated with the text to be scrolled.
        /// </summary>
        public Scrollbar Scrollbar;

        /// <summary>
        /// Some text indicating to the user to read the text before accepting the
        /// conditions.
        /// </summary>
        public Text PleaseFullyReadFirstText;

        private const int BOTTOM = 0;

        void Start()
        {
            Scrollbar.onValueChanged.AddListener((float value) =>
            {
                if (value <= BOTTOM)
                {
                    gameObject.GetComponent<Toggle>().interactable = true;
                }
            });
        }

        /// <summary>
        /// If the toggle is clicked but is not interactable, show text
        /// indicating user to read before accepting.
        /// </summary>
        public void OnPointerClick(PointerEventData _)
        {
            if (!gameObject.GetComponent<Toggle>().IsInteractable())
            {
                PleaseFullyReadFirstText.gameObject.SetActive(true);
            }
        }
    }
}
