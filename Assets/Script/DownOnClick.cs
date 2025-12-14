using UnityEngine;

public class DownOnClick : MonoBehaviour
{
    // Вектор перемещения при клике (настрой в инспекторе)
    public Vector3 moveOffset = new Vector3(1f, 0f, 0f);

    // Объект, который нужно показать (перетащи в инспекторе)
    public GameObject objectToShow;

    public void OnMouseDown()
    {
        // Перемещаем объект
        transform.Translate(moveOffset, Space.Self);
        Debug.Log("Объект перемещён на: " + moveOffset);

        // Показываем скрытый объект
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
            Debug.Log("Объект показан: " + objectToShow.name);
        }
    }
}