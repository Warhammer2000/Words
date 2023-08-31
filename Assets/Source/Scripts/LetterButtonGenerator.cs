using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LetterButtonGenerator : MonoBehaviour
{
    //Этот скрипт генерирует столько кнопок сколько у тебя букв в слове
    //переменная  UpPositionOfButton - определяет позицию по Y выше ниже
    //+ у меня в проекте два скрипта LetterButtonGenerator чтобы я смог создавать больше кнопок чтобы запутать игрока
    [SerializeField] private string wordToSplit = "bridge"; 
    [SerializeField] private GameObject buttonPrefab; 
    [SerializeField] private Transform buttonsParent; 
    public  Vector2 buttonSpacing = new Vector2(235f, -300f);
    public float UpPositionOfButton;

    private void Start()
    {
        GenerateButtons();
    }

    private void GenerateButtons()
    {
        List<char> letters = new List<char>(wordToSplit.ToCharArray());
        Shuffle(letters);

        float totalWidth = letters.Count * buttonSpacing.x;
        Vector3 startPosition = new Vector3(-totalWidth / 2f, UpPositionOfButton, 0f); 
        foreach (char letter in letters)
        {
            GameObject buttonObject = Instantiate(buttonPrefab, buttonsParent);
            Button button = buttonObject.GetComponent<Button>();
            Text buttonText = buttonObject.GetComponentInChildren<Text>();

            buttonText.text = letter.ToString();

            buttonObject.transform.localPosition = startPosition;
            startPosition.x += buttonSpacing.x;
          
        }
    }
    //Вот как раз об этом алгоритме рыбака я говорил.. он работает лучше чем рандом
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
