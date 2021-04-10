using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// This module is to make text clickable, similar to buttons.
    /// This module implements [TextButton Module](https://gitlab.cas.mcmaster.ca/bucklj4/capstone-mini-game-battery-project/-/wikis/Architecture-And-Module-Design#textbutton-module)
    /// found in the Architecture and Module Design Document.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler,
        IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    
    {
        /// <summary>
        /// Add callbacks in the inspector like for buttons.
        /// </summary>
        public UnityEvent onClick;

        /// <summary>
        /// The text component that will shown when it's not selected.
        /// </summary>
        public string normalTextComponent;

        /// <summary>
        /// The text component that will shown when it's clicked or hovered.
        /// </summary>
        public string selectedTextComponent;

        /// <summary>
        /// Text that can be clicked.
        /// </summary>
        private TextMeshProUGUI TextComponent;


        void Start()
        {
            TextComponent  = gameObject.GetComponent<TextMeshProUGUI>();
            normalTextComponent = TextComponent.text;
            selectedTextComponent = "<u>" + TextComponent.text + "</u>";
        }

        /// <summary>
        /// Implements the IPointerClickHandler interface.
        /// </summary>
        /// <param name="pointerEventData"></param>
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            // invoke your event
            onClick.Invoke();
        }


        /// <summary>
        /// Implement IPointerDownHandler interface: will add underline to text when it's pressed.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            TextComponent.text = selectedTextComponent;
        }

        /// <summary>
        /// Implement IPointerUpHandler interface: will remove underline to text when it's released.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            TextComponent.text = normalTextComponent;
        }

        /// <summary>
        /// Implement IPointerEnterHandler interface: will add underline to text when it's hovered.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            TextComponent.text = selectedTextComponent;
        }

        /// <summary>
        /// Implements the IPointerExitHandler interface: will remove underline to text when it's not selected.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            TextComponent.text = normalTextComponent;
        }
    }
}