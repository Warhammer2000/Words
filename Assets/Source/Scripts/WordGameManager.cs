using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WordGameManager : MonoBehaviour
{
    //�������� ������� ������ ���� ���� �� ��� ����
    
    public string targetWord = "bridge";
    public Text wordDisplayText;
    private List<char> correctLetters = new List<char>();
    private int currentLetterIndex = 0;

    private void Start()
    {
        UpdateWordDisplay();
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
                Debug.Log("����� ���������: " + targetWord);
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
