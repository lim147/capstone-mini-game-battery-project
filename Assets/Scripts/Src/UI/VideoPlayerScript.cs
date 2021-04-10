using System.Collections;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// This module implements [VideoPlayerScript Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#videoplayerscript-module)
    /// found in the Architecture and Module Design Document. This script is used to display videos used in the instruction scenes
    /// of the various mini-games.
    /// </summary>
    public class VideoPlayerScript : MonoBehaviour
    {
        public GameObject videoPlayerSpace;
        public string nameOfVideoFile;
        private UnityEngine.Video.VideoPlayer videoPlayerComponent;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(PlayVideo());
        }

        /// <summary>
        /// PlayVideo is for playing video specified by the filepath given in the scene.
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayVideo()
        {
            yield return new WaitForSeconds(0.1f);

            videoPlayerComponent = videoPlayerSpace.GetComponent<UnityEngine.Video.VideoPlayer>();
            videoPlayerComponent.url = System.IO.Path.Combine(Application.streamingAssetsPath, nameOfVideoFile);

            videoPlayerComponent.Play();
        }
    }
}