using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The color of the button when it is not selected")]
    private Color normalColor;

    [SerializeField]
    [Tooltip("The color of the button when it is selected")]
    private Color selectedColor;

    private bool selected = false;

    private Animator animator;

    private Button button;

    private TMP_Text buttonText;

    private Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the same GameObject
        animator = GetComponent<Animator>();

        buttonText = GetComponentInChildren<TMP_Text>();

        // Get the Button component
        button = GetComponent<Button>();
        if (button != null)
        {
            // Get the Image component of the button
            buttonImage = button.image;

            // Set the base text
            buttonText.text = "Freeze UI";

            // Add a listener to the button's onClick event
            button.onClick.AddListener(ClickAnimation);
        }
    }

    void ClickAnimation()
    {
        if(animator != null)
        {
            animator.Play("Pressed");
        }

       
        // Change the color of the button (you can customize this color)
        if (selected && buttonImage != null)
        {
            buttonImage.color = normalColor;
            selected = false;
            buttonText.text = "Freeze UI";

        } else if (!selected && buttonImage != null)
        {
            buttonImage.color = selectedColor; 
            selected = true;
            buttonText.text = "Unfreeze UI";
        }
    }
}
