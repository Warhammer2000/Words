using UnityEngine;
using UnityEngine.UI;

public class LetterButton : MonoBehaviour
{
    [SerializeField]private WordGameManager gameManager;
    [SerializeField]private Text buttonText;
    private void Start()
    {
        gameManager = FindObjectOfType<WordGameManager>();
        buttonText = GetComponentInChildren<Text>();

        if (buttonText != null)
        {
            char letter = buttonText.text[0]; 
        }
    }
    public void OnClick()
    {
        if (buttonText != null && buttonText.text.Length > 0)
        {
            gameManager.LetterButtonClickedProcedure(buttonText.text[0]);
        }
    }
}
