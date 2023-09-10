using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class WordGameManager : MonoBehaviour
{
    [Inject] private WordLists wordLists;
    [Inject] private LetterButtonGenerator letterButtonGenerator;


    public Image wordImage;
    private WordType currentType;
    private string[] currentWordList;
    [SerializeField] private string targetWord;

    public Text wordDisplayText;

    public Text wordDisplayTexwt;

    private List<char> correctLetters = new List<char>();
    private int currentLetterIndex = 0;

    [SerializeField] private List<WordSpritePair> wordSpritePairs;
    [SerializeField] private Dictionary<string, Sprite> wordSpriteDictionary = new Dictionary<string, Sprite>();

    private void Awake()
    {
       
        currentType = WordType.Animals; // Start with the first category
        SetWordList(currentType);
        UpdateWordDisplay();
        letterButtonGenerator.GenerateButtons(targetWord);
    }

    private void Start()
    {
        foreach (WordSpritePair pair in wordSpritePairs)
        {
            if (!wordSpriteDictionary.ContainsKey(pair.word))
            {
                wordSpriteDictionary.Add(pair.word, pair.sprite);
                Debug.Log(pair.word + " and " + pair.sprite);
            }
            else
            {
                // Обработка возможных дубликатов, если необходимо
                Debug.LogWarning("Duplicate word: " + pair.word);
            }
        }
    }
    private void FixedUpdate()
    {
        CheckDictionary(wordSpriteDictionary);
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
        targetWord = currentWordList[Random.Range(0, currentWordList.Length)];

        // Получите соответствующий спрайт
        if (wordSpriteDictionary.ContainsKey(targetWord))
        {
            Sprite correspondingSprite = wordSpriteDictionary[targetWord];
            // Установите спрайт
            wordImage.sprite = correspondingSprite;
        }
        else
        {
            // Обработка случая, если спрайт не найден
            Debug.LogError("Sprite not found for word: " + targetWord);
        }

        // Установите слово
        wordDisplayTexwt.text = targetWord;
       
        wordDisplayText.text = string.Empty;
      
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
    public void CheckDictionary(Dictionary<string, Sprite> dictionary)
    {
        foreach (var pair in dictionary)
        {
            string word = pair.Key;
            Sprite sprite = pair.Value;
            Debug.Log("Word: " + word + ", Sprite: " + sprite);
        }
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
