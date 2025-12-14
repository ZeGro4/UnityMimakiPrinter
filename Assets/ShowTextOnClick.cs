using UnityEngine;
using UnityEngine.UI;

public class ShowTextOnClick : MonoBehaviour
{
    // Текстовое поле (перетащи в инспекторе)
    public Text textField;

    // Текст, который появится
    public string textToShow = "print.png";

    public void ShowText()
    {
        if (textField != null)
        {
            textField.text = textToShow;
            Debug.Log("Текст показан: " + textToShow);
        }
    }

    public void ClearText()
    {
        if (textField != null)
        {
            textField.text = "";
            Debug.Log("Текст очищен");
        }
    }
}