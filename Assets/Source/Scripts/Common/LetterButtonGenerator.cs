using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class LetterButtonGenerator : MonoBehaviour
{

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonsParent;
    public Vector2 buttonSpacing = new Vector2(0f, -0f);
    public float UpPositionOfButton;
    public void ClearButtons()
    {
        int childCount = buttonsParent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = buttonsParent.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public void GenerateButtons(string targetWord)
    {
        ClearButtons();

        List<char> letters = new List<char>(targetWord.ToCharArray());
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
