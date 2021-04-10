using UnityEngine;
using UnityEngine.SceneManagement;


namespace UI
{
	/// <summary>
	/// This module implements [SceneSwitcher Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#sceneswitcher-module)
	/// found in the Architecture and Module Design Document. 
	/// The SceneSwitcher module contains the functions responsible for moving between scenes in the mini-game.
	/// </summary>
	public class SceneSwitcher : MonoBehaviour
	{
		// Constant for the index of the first scene in the application 
		private const int FIRST_SCENE = 0;

		/// <summary>
		/// Moves to the next scene in the scene order after a script event
		/// </summary>
		public static void NextScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		/// <summary>
		/// Moves to the next scene in the scene order after a player input
		/// </summary>
		public void ButtonPressNextScene()
		{
			NextScene();
		}

		/// <summary>
		/// Moves to the first scene in the scene order
		/// </summary>
		public static void RestartGame()
		{
			SceneManager.LoadScene(FIRST_SCENE);
		}

		/// <summary>
		/// Move to the scene specified by the scene name
		/// </summary>
		/// <param name="sceneName">Name of the scene to be moved to</param>
		public static void MoveToScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		/// <summary>
		/// Move to overall menu scene
		/// </summary>
		public void MoveToMenuScene()
		{
			SceneManager.LoadScene(SceneName.MENU_SCENE);
		}

		/// <summary>
		/// Move to overall result scene
		/// </summary>
		public void MoveToResultScene()
		{
			SceneManager.LoadScene(SceneName.RESULT_SCENE);
		}

		/// <summary>
		/// Move to Pointing Result Scene
		/// </summary>
		public void MoveToPointingResultScene()
		{
			SceneManager.LoadScene(SceneName.POINTING_RESULT_SCENE);
		}

		/// <summary>
		/// Move to Inhibition Result Scene
		/// </summary>
		public void MoveToInhibitionResultScene()
		{
			SceneManager.LoadScene(SceneName.INHIBITION_RESULT_SCENE);
		}

		/// <summary>
		/// Move to Object Recognition Scene
		/// </summary>
		public void MoveToORResultScene()
		{
			SceneManager.LoadScene(SceneName.OBJECT_RECOGNITION_RESULT_SCENE);
		}

		/// <summary>
		/// Move to Selective Visual Result Scene
		/// </summary>
		public void MoveToSVResultScene()
		{
			SceneManager.LoadScene(SceneName.SELECTIVE_VISUAL_RESULT_SCENE);
		}

		/// <summary>
		/// Move to Time To Contact Result Scene
		/// </summary>
		public void MoveToTOCResultScene()
		{
			SceneManager.LoadScene(SceneName.TIME_TO_CONTACT_RESULT_SCENE);
		}

		/// <summary>
		/// Move to Visuospatial Sketchpad Result Scene
		/// </summary>
		public void MoveToVSResultScene()
		{
			SceneManager.LoadScene(SceneName.VISUOSPATIAL_SKETCHPAD_RESULT_SCENE);
		}

		/// <summary>
        /// Move to No Keyboard scene
        /// </summary>
		public void MoveToNoKeyboardScene()
        {
			SceneManager.LoadScene(SceneName.NO_KEYBOARD_SCENE);
		}

		/// <summary>
		/// Move to Questionnaire scene
		/// </summary>
		public void MoveToQuestionnaireScene()
		{
			SceneManager.LoadScene(SceneName.QUESTIONNAIRE_SCENE);
		}
	}
}