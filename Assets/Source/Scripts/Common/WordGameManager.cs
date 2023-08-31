using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class WordGameManager : MonoBehaviour
{
    [Inject] private WordLists wordLists;
    [Inject] private LetterButtonGenerator letterButtonGenerator;

    private WordType currentType;
    private string[] currentWordList;
    [SerializeField] private string targetWord;
    public Text wordDisplayText;
    private List<char> correctLetters = new List<char>();
    private int currentLetterIndex = 0;

    private void Start()
    {
        currentType = WordType.Animals; // Start with the first category
        SetWordList(currentType);
        UpdateWordDisplay();
        letterButtonGenerator.GenerateButtons(targetWord);
    }

    private void SetWordList(WordType type)
    {
        switch (type)
        {
            case WordType.Animals:
                currentWordList = wordLists.Animals;
                break;
            case WordType.Cooking:
                currentWordList = wordLists.Cooking;
                break;
            case WordType.Gadgets:
                currentWordList = wordLists.Gadgets;
                break;
            case WordType.Films:
                currentWordList = wordLists.Films;
                break;
            case WordType.Sport:
                currentWordList = wordLists.Sport;
                break;
        }
        wordDisplayText.text = string.Empty;
        targetWord = currentWordList[Random.Range(0, currentWordList.Length)];
        letterButtonGenerator.GenerateButtons(targetWord);
        currentLetterIndex = 0;
        correctLetters.Clear();
      
    }

    public bool IsCorrectLetter(char letter)
    {
        if (currentLetterIndex < targetWord.Length && targetWord[currentLetterIndex] == letter)
        {
            return true;
        }
        return false;
    }

    public void LetterButtonClickedProcedure(char letter)
    {
        if (currentLetterIndex < targetWord.Length && targetWord[currentLetterIndex] == letter)
        {
            correctLetters.Add(letter);
            UpdateWordDisplay();
            currentLetterIndex++;

            if (IsWordComplete())
            {
                Debug.Log("Word completed : " + targetWord);
                SetWordList(currentType); 
            }
        }
    }

    private bool IsWordComplete()
    {
        foreach (char letter in targetWord)
        {
            if (!correctLetters.Contains(letter))
            {
                return false;
            }
        }
        return true;
    }

    private void UpdateWordDisplay()
    {
        string displayedWord = "";
        foreach (char letter in targetWord)
        {
            if (correctLetters.Contains(letter))
            {
                displayedWord += letter;
            }
            else
            {
                displayedWord += "_";
            }
        }
        wordDisplayText.text = displayedWord;
    }

}
