using UnityEngine;
using UnityEngine.UI;


namespace Helper
{
    /// <summary>
    /// This module implements [Square Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#square-module)
    /// found in
    /// the Architecture and Module Design Document, and is used to fulfill [PUC-15](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Software-Requirements-Specification#puc-15)
    /// </summary>
    public class Square : MonoBehaviour
    {
        /// <summary>
        /// Variable holding x and y position of the top left corner of the square.
        /// </summary>
        public Position2D Position { get; set; }

        /// <summary>
        /// Index of the square
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The game object that this square.cs script is attached to.
        /// </summary>
        public GameObject square;

        // The sprite renderer for this object
        private Image squareImageComponent;

        /// <summary>
        /// Creates a new Square object, filling in the position value and sprite renderer
        /// given by the attached object.
        /// </summary>
        void Start()
        {
            // Get the position of the game object attached to this script
            //The x-axis difference is 90; x-axis: -145, -55, 35, 125
            //The y-axis difference is 90; y-axis: 120, 30, -60, -150
            Vector3 positionVector3 = square.transform.position;

            // Convert this position into a Position2D datatype, and store the position value
            Position = new Position2D(positionVector3.x, positionVector3.y);

            // Get the Image Component for this game object
            squareImageComponent = square.GetComponent<Image>();
        }

        /// <summary>
        /// Toggles the colour of the square.
        /// </summary>
        /// <param name="colourToChangeTo">The target colour to change to</param>
        public void ChangeColor(Color colourToChangeTo)
        {
            squareImageComponent.color = colourToChangeTo;
        }
    }
}

