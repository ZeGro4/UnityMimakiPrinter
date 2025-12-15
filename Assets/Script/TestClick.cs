using UnityEngine;
using UnityEngine.EventSystems;

public class TestClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("КЛИК по кнопке!");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Курсор НАД кнопкой");
    }
}