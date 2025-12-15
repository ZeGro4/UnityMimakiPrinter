using UnityEngine;
using UnityEngine.UI;

public class DebugButtonClick : MonoBehaviour
{
    public GameObject targetPanel;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("Button (2) нажата!");
            Debug.Log("Target Panel: " + (targetPanel != null ? targetPanel.name : "NULL"));
            Debug.Log("Panel активна: " + (targetPanel != null ? targetPanel.activeSelf.ToString() : "N/A"));

            if (targetPanel != null)
            {
                targetPanel.SetActive(true);
                Debug.Log("Panel активирована!");
            }
        });
    }
}