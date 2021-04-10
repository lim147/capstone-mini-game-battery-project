namespace UI
{
    /// <summary>
    /// This module implements [SceneName Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#scenename-module)
    /// found in the Architecture and Module Design Document. 
    /// The SceneName module enumerates the name of scenes used in Unity.
    /// </summary>
    public class SceneName
    {
        /// <summary>
        /// Start scene, which is the first scene of the application.
        /// </summary>
        public const string START_SCENE = "StartScene";

        /// <summary>
        /// Information scene, which informs the player of the disclaimers and information about the application.
        /// </summary>
        public const string INFO_SCENE = "InfoScene";

        /// <summary>
        /// Questionnaire scene, which collects the necessary player information.
        /// </summary>
        public const string QUESTIONNAIRE_SCENE = "QuestionnaireScene";

        /// <summary>
        /// Menu scene, which shows all games to play.
        /// </summary>
        public const string MENU_SCENE = "MenuScene";

        /// <summary>
        /// No keyboard scene, which will be loaded if the player checks-on "No" toggle for keyboard.
        /// </summary>
        public const string NO_KEYBOARD_SCENE = "NoKeyboardScene";

        /// <summary>
        /// Scene for the Squares mini-game.
        /// </summary>
        public const string SQUARES_SCENE = "SquaresScene";

        /// <summary>
        /// Scene for the Squares mini-game instructions.
        /// </summary>
        public const string SQUARES_INSTRUCTIONS_SCENE = "SquaresInstructions";

        /// <summary>
        /// Scene for the Balloons mini-game.
        /// </summary>
        public const string BALLOONS_SCENE = "BalloonsScene";

        /// <summary>
        /// Scene for the Balloons mini-game instructions.
        /// </summary>
        public const string BALLOONS_INSTRUCTIONS_SCENE = "BalloonsInstructions";

        /// <summary>
        /// Scene for the Catch The Thief mini-game.
        /// </summary>
        public const string CATCHTHETHIEF_SCENE = "CatchTheThiefScene";

        /// <summary>
        /// Scene for the Catch The Thief mini-game instructions.
        /// </summary>
        public const string CATCHTHETHIEF_INSTRUCTIONS_SCENE = "CatchTheThiefInstructions";
 
        /// <summary>
        /// Scene for the ImageHit mini-game.
        /// </summary>
        public const string IMAGEHIT_SCENE = "ImageHitScene";

        /// <summary>
        /// Scene for the ImageHit mini-game instructions.
        /// </summary>
        public const string IMAGEHIT_INSTRUCTIONS_SCENE = "ImageHitInstructions";

        /// <summary>
        /// Scene for the Catch The Ball mini-game.
        /// </summary>
        public static string CATCH_THE_BALL_SCENE = "CatchTheBall";

        /// <summary>
        /// Scene for the Save One Ball mini-game.
        /// </summary>
        public static string SAVE_ONE_BALL_SCENE = "SaveOneBall";

        /// <summary>
        /// Scene for the Judge The Ball mini-game.
        /// </summary>
        public static string JUDGE_THE_BALL_SCENE = "JudgeTheBall";

        /// <summary>
        /// Scene for the CatchTheBall mini-game instructions.
        /// </summary>
        public const string CATCH_THE_BALL_INSTRUCTIONS_SCENE = "CatchTheBallInstructions";

        /// <summary>
        /// Scene for the SaveOneBall mini-game instructions.
        /// </summary>
        public const string SAVE_ONE_BALL_INSTRUCTIONS_SCENE = "SaveOneBallInstructions";

        /// <summary>
        /// Scene for the JudgeTheBall mini-game instructions.
        /// </summary>
        public const string JUDGE_THE_BALL_INSTRUCTIONS_SCENE = "JudgeTheBallInstructions";

        /// <summary>
        /// Result scene, which shows scores and levels for all measured abilities.
        /// </summary>
        public const string RESULT_SCENE = "ResultScene";

        /// <summary>
        /// Pointing result scene, which shows score and level for pointing ability.
        /// </summary>
        public const string POINTING_RESULT_SCENE = "PointingResultScene";

        /// <summary>
        /// Inhibition result scene, which shows score and level for inhibition ability.
        /// </summary>
        public const string INHIBITION_RESULT_SCENE = "InhibitionResultScene";

        /// <summary>
        /// Object Recognition result scene, which shows score and level for object recognition ability.
        /// </summary>
        public const string OBJECT_RECOGNITION_RESULT_SCENE = "ORResultScene";

        /// <summary>
        /// Selective Visual Result scene, which shows score and level for selective visual ability.
        /// </summary>
        public const string SELECTIVE_VISUAL_RESULT_SCENE = "SVResultScene";

        /// <summary>
        /// Time To Contact scene, which shows score and level for time to contact ability.
        /// </summary>
        public const string TIME_TO_CONTACT_RESULT_SCENE = "TTCResultScene";

        /// <summary>
        /// Visuospatial Sketchpad scene, which shows score and level for Visuospatial Sketchpad ability.
        /// </summary>
        public const string VISUOSPATIAL_SKETCHPAD_RESULT_SCENE = "VSResultScene";
    }
}