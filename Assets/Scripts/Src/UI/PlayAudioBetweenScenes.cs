using UnityEngine;

namespace UI
{
    /// <summary>
    /// Simple script to ensure audio keeps playing between scenes.
    /// </summary>
    public class PlayAudioBetweenScenes : MonoBehaviour
    {
        public AudioSource Audio;

        void Start()
        {
            DontDestroyOnLoad(Audio);
        }
    }
}
