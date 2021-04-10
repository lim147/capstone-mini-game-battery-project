using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This module implements [ShapedButton Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#shapedbutton-module)
    /// found in the Architecture and Module Design Document. 
    /// The ShapedButton module is for creating a button with a flexible shape. The button's shape will be the same as the shape of the attached image.
    /// To apply this module, attach this module to the button object.
    /// </summary>
    public class ShapedButton : MonoBehaviour
    {
        /// <summary>
        /// AlphaThreshold is to set in which pixel level of the image the button's shape will be adjusted to
        /// </summary>
        public float AlphaThreshold = 1f;

        void Start()
        {
            this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        }
    }
}