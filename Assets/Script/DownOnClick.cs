using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownOnClick : MonoBehaviour {

    // Вектор перемещения при клике (настрой в инспекторе)
    public Vector3 moveOffset = new Vector3(1f, 0f, 0f);  // Пример: +1 по X

    public void OnMouseDown()
    {
        // Перемещаем объект (относительно текущей позиции)
        transform.Translate(moveOffset, Space.Self);  // Space.Self = локальные оси объекта

             Debug.Log("Объект перемещён на: " + moveOffset);
    }
}
