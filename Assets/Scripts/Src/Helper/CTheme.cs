using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    /// <summary>
    /// Type of themes of images
    /// This module implements [CTheme Type Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#ctheme-type-module)
    /// found in the Architecture and Module Design Document.
    /// </summary>
    [System.Serializable]
    public class CTheme
    {
        // The theme of a image.
        public string specifiedTheme;

        // The image object itself in editor.
        public Sprite[] sprites;

        // The list of images of one theme used for Image Hit game
        public List<TImage> images;


        /// <summary>
        /// Get image data for the image theme
        /// </summary>
        /// <param name="Theme"></param>
        /// The theme of the image
        /// <param name="data"> the list of image themes </param>
        public CTheme(string Theme, List<TImage> data)
        {
            specifiedTheme = Theme;
            if (data != null)
            {
                images = new List<TImage>();
                for (int i = 0; i < data.Count; i++)
                {
                    TImage timg = new TImage(data[i]);
                    images.Add(timg);
                }
            }

        }

        /// <summary>
        /// Select the images of the specified theme and add them to the list of images needed for this round of game
        /// </summary>
        /// <returns>The list of images </returns>
        public List<TImage> getimages()
        {
            specifiedTheme = specifiedTheme.Trim();

            images = new List<TImage>();

            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i] != null)
                {
                    TImage tmp = new TImage();
                    tmp.imageTheme = specifiedTheme.Trim();
                    tmp.sprite = sprites[i];
                    images.Add(tmp);
                }

            }
            return images;
        }


        /// <summary>
        /// In addition to the specified theme, Image Hit game also needs some random theme images
        /// Get these images by random numbers and add them to the list
        /// There are ten images in the image list when the game starts
        /// </summary>
        /// <param name="num"></param>
        /// <returns>The list of images</returns>
        public List<TImage> getrandimages(int num)
        {
            List<TImage> tmp = getimages(); // Images of all themes for randomly selecting
            List<TImage> res = new List<TImage>(); // To store images being randomly selected

            // To decide the number of images
            for (int i = 0; i < num; i++)
            {
                if (tmp.Count <= 0)
                {
                    break;
                }
                int id = UnityEngine.Random.Range(0, tmp.Count);
                res.Add(tmp[id]);
                tmp.RemoveAt(id);

            }
            return res;
        }
    }
}
