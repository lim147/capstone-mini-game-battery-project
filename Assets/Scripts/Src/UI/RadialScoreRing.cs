using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// This module implements [RadialScoreRing Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#radialscorering-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// 
    /// Small script to show radial ring "progress bar" to indicate the player's score
    /// </summary>
    public class RadialScoreRing : MonoBehaviour
    {
        /// <summary>
        /// The colour when the ring is empty or near empty
        /// </summary>
        public Color BadColour = Color.red;

        /// <summary>
        /// The colour when the ring is full or near full
        /// </summary>
        public Color GoodColour = Color.green;

        /// <summary>
        /// The duration it will take for the ring to fill
        /// </summary>
        public float DurationInSeconds = 3;

        /// <summary>
        /// The score the player received (≤ 100)
        /// </summary>
        public int Score;

        private Image ring;

        void Start()
        {
            ring = GetComponent<Image>();
            ring.color = BadColour;
        }

        /// <summary>
        /// This method should be called when the animation is to be displayed
        /// (after providing Score).
        /// </summary>
        public void BeginAnimation()
        {
            if (Score > 0)
            {
                float scoreAsPercentage = Score / 100.0F;
                Color endingColour = Color.Lerp(BadColour, GoodColour, scoreAsPercentage);

                DOTween.To(() => ring.fillAmount, (x) => ring.fillAmount = x, scoreAsPercentage, DurationInSeconds);
                DOTween.To(() => ring.color, (x) => ring.color = x, endingColour, DurationInSeconds);
            }
        }
    }
}
