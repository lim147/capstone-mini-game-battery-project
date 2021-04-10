using System.Collections.Generic;
using UnityEngine;
using Helper;



namespace Storage
{
    /// <summary>
    /// This module implements [ImageHitStorage Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture%20And%20Module%20Design#imagehitstorage-module)
    /// found in
    /// the Architecture and Module Design Document. This is data type for storing the measurements
    /// collected during the <see cref="Games.ImageHit"/> mini-game.
    /// </summary>
    public class ImageHitStorage 
{

    /// <summary>
    /// Identified key in ImageHit game
    /// </summary>
    public KeyCode IdentifiedKeyCode = KeyCode.Space;

    /// <summary>
    /// Name of the identified key in ImageHit game
    /// </summary>
    public string IdentifiedKeyName = KeyCode.Space.ToString();


    public  List<List<ImageHitRound>> Rounds {get; set;} 

}

/// <summary>
/// Information for a particular a round in the Image Hit mini-game. 
/// </summary>
public class ImageHitRound
{

    /// <summary>
    /// If the recognition of the player is correct for the current image
    /// </summary>
    public bool isCorrectlyIdentified {get; set;}

    /// <summary>
    /// If the space bar is pressed by the player for the current image
    /// </summary>
    public bool isKeyPressed  {get; set;}

    /// <summary>
    /// If the pressed bar is space bar
    /// </summary>
    public bool isSpaceKey {get; set;}

    /// <summary>
    /// Other unidentified keys pressed during the round.
    /// </summary>
    public List<TimeAndKey> UnidentifiedKeysPressed { get; set; }

    /// <summary>
    /// the time duration between when the image shows on screen and when
    /// the player presses the button to react to the image
    /// </summary>
    public float keyPressTime {get; set;}

    /// </summary>
    /// The theme of the image
    /// </summary>
    public string imageTheme {get; set;}

    /// </summary>
    /// The name of the image
    /// </summary>
    public string imageName {get; set;}

    /// </summary>
    /// The specified theme of the this game
    /// </summary>
    public string testTheme { get; set; }




}
}