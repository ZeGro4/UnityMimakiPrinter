using UnityEngine;

public class RotateOnClick : MonoBehaviour
{
    // Угол поворота при клике (в градусах)
    public float rotationAngle = 90f;

    // Метод, который вызывается при клике мышью на объект
    private void OnMouseDown()
    {
        // Поворачиваем объект по оси Y (можно изменить на X или Z)
        transform.Rotate(rotationAngle, 0f, 0f);
    }
}