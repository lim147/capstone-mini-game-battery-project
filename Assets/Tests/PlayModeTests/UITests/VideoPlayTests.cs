using System.Collections;
using NUnit.Framework;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace PlayModeTests
{
    namespace UITests
    {
        public class VideoPlayTests
        {
            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the video plays on Balloons Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator ExpectPossibleFailOnPipeline_InstructionVideoPlays_Balloons()
            {
                // Load Scene
                SceneManager.LoadScene(SceneName.BALLOONS_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return new WaitForSeconds(2f);

                UnityEngine.Video.VideoPlayer videoPlayerComponent = GameObject.Find("Video").GetComponent<UnityEngine.Video.VideoPlayer>();
                Assert.AreEqual(videoPlayerComponent.isPlaying,
                    true, "Video is playing is" + videoPlayerComponent.isPlaying);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the video plays on Catch The Ball Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator ExpectPossibleFailOnPipeline_InstructionVideoPlays_CatchTheBall()
            {
                // Load Scene
                SceneManager.LoadScene(SceneName.CATCH_THE_BALL_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return new WaitForSeconds(2f);

                UnityEngine.Video.VideoPlayer videoPlayerComponent = GameObject.Find("Video").GetComponent<UnityEngine.Video.VideoPlayer>();
                Assert.AreEqual(videoPlayerComponent.isPlaying,
                    true, "Video is playing is" + videoPlayerComponent.isPlaying);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the video plays on Save One Ball Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator ExpectPossibleFailOnPipeline_InstructionVideoPlays_SaveOneBall()
            {
                // Load Scene
                SceneManager.LoadScene(SceneName.SAVE_ONE_BALL_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return new WaitForSeconds(2f);

                UnityEngine.Video.VideoPlayer videoPlayerComponent = GameObject.Find("Video").GetComponent<UnityEngine.Video.VideoPlayer>();
                Assert.AreEqual(videoPlayerComponent.isPlaying,
                    true, "Video is playing is" + videoPlayerComponent.isPlaying);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the video plays on Judge The Ball Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator ExpectPossibleFailOnPipeline_InstructionVideoPlays_JudgeTheBall()
            {
                // Load Scene
                SceneManager.LoadScene(SceneName.JUDGE_THE_BALL_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return new WaitForSeconds(2f);
                
                UnityEngine.Video.VideoPlayer videoPlayerComponent = GameObject.Find("Video").GetComponent<UnityEngine.Video.VideoPlayer>();
                Assert.AreEqual(videoPlayerComponent.isPlaying,
                    true, "Video is playing is" + videoPlayerComponent.isPlaying);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the video plays on Squares Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator ExpectPossibleFailOnPipeline_InstructionVideoPlays_Squares()
            {
                // Load Scene
                SceneManager.LoadScene(SceneName.SQUARES_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return new WaitForSeconds(2f);

                UnityEngine.Video.VideoPlayer videoPlayerComponent = GameObject.Find("Video").GetComponent<UnityEngine.Video.VideoPlayer>();
                Assert.AreEqual(videoPlayerComponent.isPlaying,
                    true, "Video is playing is" + videoPlayerComponent.isPlaying);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the video plays on Image Hit Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator ExpectPossibleFailOnPipeline_InstructionVideoPlays_ImageHit()
            {
                // Load Scene
                SceneManager.LoadScene(SceneName.IMAGEHIT_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return new WaitForSeconds(2f);

                UnityEngine.Video.VideoPlayer videoPlayerComponent = GameObject.Find("Video").GetComponent<UnityEngine.Video.VideoPlayer>();
                Assert.AreEqual(videoPlayerComponent.isPlaying,
                    true, "Video is playing is" + videoPlayerComponent.isPlaying);
            }

            [Description(@"
        <ul>
            <li><b>Test type:</b> Unit</li>
            <li><b>Associated SRS requirements:</b> None</li>
            <li><b>Test description:</b> Ensure that the video plays on Catch The Thief Instructions Scene.
            </li>
            <li><b>Expect possibly to fail in the pipeline:</b> It passes on Unity editor playmode test but fails in
                   the pipeline because the pipeline has issue loading the videos attached to the scene.</li>
            <li><b>Expect error type:</b> Cannot play videos. </li>
        </ul>
        ")]
            [UnityTest]
            public IEnumerator ExpectPossibleFailOnPipeline_InstructionVideoPlays_CatchTheThief()
            {
                // Load Scene
                SceneManager.LoadScene(SceneName.CATCHTHETHIEF_INSTRUCTIONS_SCENE);
                // Wait for scene loading finished
                yield return new WaitForSeconds(2f);

                UnityEngine.Video.VideoPlayer videoPlayerComponent = GameObject.Find("Video").GetComponent<UnityEngine.Video.VideoPlayer>();
                Assert.AreEqual(videoPlayerComponent.isPlaying,
                    true, "Video is playing is" + videoPlayerComponent.isPlaying);
            }
        }
    }
}