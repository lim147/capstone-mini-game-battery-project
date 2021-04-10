using System.Collections.Generic;

namespace Storage
{
    /// <summary>
    /// This module implements [UIInteractionStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#uiinteractionstorage-module)
    /// found in the Architecture and Module Design Document.
    /// 
    /// This module is a static class(singleton) which is used to store the player interaction
    /// data on non-game scenes. It's used for external testing purpose.
    /// </summary>
    public static class UIInteractionStorageSingleton
    {
        /// <summary>
        /// Elapsed time in seconds between when the player enters the StartScene and when the person
        /// presses the "Start" button to leave the scene.
        /// </summary>
        public static double TimeStayOnStartScene = -1;

        /// <summary>
        /// Elapsed time in seconds between when the player enters the InfoScene and when the person
        /// clicks on the check-in box to leave the scene.
        /// </summary>
        public static double TimeStayOnInfoScene = -1;

        /// <summary>
        /// Elapsed time in seconds between when the player enters the QuestionnaireScene and when the person
        /// presses the "Submit" button to leave the scene.
        /// Since if the required field(Age) is not entered, pressing "Submit" will not go to the next scene.
        /// Therefore it's a list of double to also record this information.
        /// All items will be the elapsed time between the moment of scene entry and the moment the button is pressed.
        /// </summary>
        public static List<double> TimeStayOnQuestionnaireScene = new List<double>();

        /// <summary>
        /// Elapsed time in seconds between when the player enters the MenuScene and when the person either
        /// clicks on a game button or presses the "Finish" button to leave the scene.
        /// Since a player may go to the MenuScene multiple times, it's a list of double to store the
        /// navigation time in order.
        /// </summary>
        public static List<double> TimeStayOnMenuScene = new List<double>();

        /// <summary>
        /// Elapsed time in seconds between when the player enters the CatchTheBallInstructionsScene and when the person
        /// presses the "Start" button to leave the scene.
        /// </summary>
        public static double TimeStayOnCatchTheBallInstructionsScene = -1;

        /// <summary>
        /// Elapsed time in seconds between when the player enters the SaveOneBallInstructionsScene and when the person
        /// presses the "Start" button to leave the scene.
        /// </summary>
        public static double TimeStayOnSaveOneBallInstructionsScene = -1;

        /// <summary>
        /// Elapsed time in seconds between when the player enters the JudgeTheBallInstructionsScene and when the person
        /// presses the "Start" button to leave the scene.
        /// </summary>
        public static double TimeStayOnJudgeTheBallInstructionsScene = -1;

        /// <summary>
        /// Elapsed time in seconds between when the player enters the BalloonInstructionsScene and when the person
        /// presses the "Start" button to leave the scene.
        /// </summary>
        public static double TimeStayOnBalloonsInstructionsScene = -1;

        /// <summary>
        /// Elapsed time in seconds between when the player enters the SquaresInstructionsScene and when the person
        /// presses the "Start" button to leave the scene.
        /// </summary>
        public static double TimeStayOnSquaresInstructionsScene = -1;

        /// <summary>
        /// Elapsed time in seconds between when the player enters the CatchTheThiefInstructionsScene and when the person
        /// presses the "Start" button to leave the scene.
        /// </summary>
        public static double TimeStayOnCTFInstructionsScene = -1;

        /// <summary>
        /// Elapsed time in seconds between when the player enters the ImageHitInstructionsScene and when the person
        /// presses the "Start" button to leave the scene.
        /// </summary>
        public static double TimeStayOnImageHitInstructionsScene = -1;
    }


    /// <summary>
    /// This module is a non-static class version of [UIInteractionStorageSingleton](https://ludus-mini-game-battery.surge.sh/docs/api/Storage.UIInteractionStorageSingleton.html).
    /// It's needed as the static class can not be stored as a piblic field in other class.
    /// It's initialized to have the same fields with same values as UIInteractionStorageSingleton.
    /// </summary>
    public class UIInteractionStorage
    {
        public double TimeStayOnStartScene = UIInteractionStorageSingleton.TimeStayOnStartScene;

        public double TimeStayOnInfoScene = UIInteractionStorageSingleton.TimeStayOnInfoScene;

        public List<double> TimeStayOnQuestionnaireScene = UIInteractionStorageSingleton.TimeStayOnQuestionnaireScene;

        public List<double> TimeStayOnMenuScene = UIInteractionStorageSingleton.TimeStayOnMenuScene;

        public double TimeStayOnCatchTheBallInstructionsScene = UIInteractionStorageSingleton.TimeStayOnCatchTheBallInstructionsScene;
        public double TimeStayOnSaveOneBallInstructionsScene = UIInteractionStorageSingleton.TimeStayOnSaveOneBallInstructionsScene;
        public double TimeStayOnJudgeTheBallInstructionsScene = UIInteractionStorageSingleton.TimeStayOnJudgeTheBallInstructionsScene;

        public double TimeStayOnBalloonsInstructionsScene = UIInteractionStorageSingleton.TimeStayOnBalloonsInstructionsScene;
       
        public double TimeStayOnSquaresInstructionsScene = UIInteractionStorageSingleton.TimeStayOnSquaresInstructionsScene;

        public double TimeStayOnCTFInstructionsScene = UIInteractionStorageSingleton.TimeStayOnCTFInstructionsScene;

        public double TimeStayOnImageHitInstructionsScene = UIInteractionStorageSingleton.TimeStayOnImageHitInstructionsScene;
    }
}
