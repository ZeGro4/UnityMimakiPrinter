using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    // Панель, которую нужно показать/скрыть (перетащи в инспекторе)
    public GameObject panel;

    // Показать панель (для кнопки "Открыть")
    public void ShowPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Debug.Log("Панель показана: " + panel.name);
        }
    }

    // Скрыть панель (для кнопки "Закрыть")
    public void HidePanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
            Debug.Log("Панель скрыта: " + panel.name);
        }
    }

    // Переключить видимость (одна кнопка для открытия/закрытия)
    public void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
            Debug.Log("Панель переключена: " + panel.name + " — " + (panel.activeSelf ? "видима" : "скрыта"));
        }
    }
}