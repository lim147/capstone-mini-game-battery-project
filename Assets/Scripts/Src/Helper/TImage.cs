using System.Collections.Generic;
using UnityEngine;
using Storage;

namespace Helper
{
    /// <summary>
    /// This module implements [TImage Type Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#timage-type-module)
    /// found in
    /// the Architecture and Module Design Document.
    /// </summary>
    public class TImage
    {
        /// <summary>
        /// The image itself.
        /// </summary>
        public Sprite sprite;

        /// <summary>
        /// The theme of the image.
        /// </summary>
        public string imageTheme;



        /// <summary>
        /// Initial state for a TImage type
        /// </summary>
        public TImage()
        {
            sprite = null;
            imageTheme = "";


        }

        /// <summary>
        /// Game data will added to a TImage type during the game process.
        /// </summary>
        /// <param name="img"> to duplicate the data of an image </param>
        public TImage(TImage img)
        {
            if (img != null)
            {
                sprite = img.sprite;
                imageTheme = img.imageTheme;
            }
        }



        /// <summary>
        /// The average reaction time of the player for one image.
        /// </summary>
        /// <param name="imageRound12Data"> the data of one time of game which contains data of each round  </param>
        public static float GetAverageTime(List<ImageHitRound> imageRound12Data)
        {
            float f = GetTotalKeyTime(imageRound12Data);
            if (imageRound12Data.Count != 0)
            {
                f /= imageRound12Data.Count;
            }
            else
            {
                f = float.MaxValue;
            }
            return f;
        }


        /// <summary>
        /// The total reaction time of the player for one image
        /// </summary>
        /// <param name="imageRound12Data"> the data of one time of game which contains data of each round  </param>
        public static float GetTotalKeyTime(List<ImageHitRound> imageRound12Data)
        {
            float f = 0;
            for (int i = 0; i < imageRound12Data.Count; i++)
            {
                f += imageRound12Data[i].keyPressTime;
            }
            return f;
        }

        /// <summary>
        /// Get the number of images that the player got correct in the game.
        /// </summary>
        public static int GetRightCount(List<ImageHitRound> imageRound12Data)
        {
            int rightCount = 0;
            for (int i = 0; i < imageRound12Data.Count; i++)
            {
                if (imageRound12Data[i].isCorrectlyIdentified)
                {
                    rightCount++;
                }
            }
            return rightCount;
        }

        /// <summary>
        /// Returns the number of key presses in the ImageHit game.
        /// </summary>
        /// <param name="imagedata"> the data of one time of game </param>
        /// <returns>The number of key presses.</returns>
        public static int GetKeyPressCount(List<ImageHitRound> imagedata)
        {
            int keynum = 0;
            for (int i = 0; i < imagedata.Count; i++)
            {
                if (imagedata[i].isKeyPressed)
                {
                    keynum++;
                }
            }
            return keynum;
        }
    }
}