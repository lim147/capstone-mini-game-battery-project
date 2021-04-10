using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This module implements [Tooltip Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#tooltip-module)
    /// found in the Architecture and Module Design Document.This is a module to display a tooltip for the grayed out game icons when the player hovers over the icons again.
    /// </summary>
    public class Tooltip : MonoBehaviour
    {
        public new Camera camera;
        private Text tooltipText;
        private RectTransform backgroundRect;
        private static Tooltip tooltipInstance;

        private void Start()
        {
            tooltipInstance = this;
            // Link the instance to tooltipBackground
            backgroundRect = GameObject.Find("tooltipBackground").GetComponent<RectTransform>();
            // Link the instance to tooltipText
            tooltipText = GameObject.Find("tooltipText").GetComponent<Text>();
            // When game starts, the tooltip is not visiable
            HideGamePlayedTooltip();
        }

        private void Update()
        {
            // Tooltip always follow the mouse
            TooltipFollowMouse();
        }

        /// <summary>
        /// Show the tooltip of the string that the game is already played.
        /// </summary>
        /// <param name="tooltipString"></param>
        public static void ShowGamePlayedTooltip()
        { 
            string tooltipString = "Oops, you have played this game.";
            tooltipInstance.ShowTooltip(tooltipString);    
        }

        /// <summary>
        /// Hide the tooltip text.
        /// </summary>
        public void HideGamePlayedTooltip()
        {
            tooltipInstance.HideTooltip();  
        }

        /// <sum ry>
        /// Show the tooltip on the screen with the specified string.
        /// </summary>
        private void ShowTooltip(string tooltipString)
        {
            // Set the tooltip to be active
            gameObject.SetActive(true);
            // Set the tooltip string
            tooltipText.text = tooltipString;

            // Padding size for the position of the tooltip string
            float textPaddingSize = 4f;

            // Set the backgound size based on the length of the tooltip string
            // Padding size is multiplied by 2, since we have padding on the left and right
            Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2,
                tooltipText.preferredHeight + textPaddingSize * 2);

            // Set the backgroundRect instance to have this background size
            backgroundRect.sizeDelta = backgroundSize;
        }

        /// <summary>
        /// Hide the tooltip.
        /// </summary>
        private void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Make the tooltip follow the mouse.
        /// </summary>
        private void TooltipFollowMouse()
        {
            // Convert mouse position from screen space to canvas space
            Vector2 screenMousePos = Input.mousePosition;
            Vector2 canvasMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(),
                Input.mousePosition, camera, out canvasMousePos);
            // Make tooltip position to be the same as cursor position
            transform.localPosition = canvasMousePos;
        }
    }
}