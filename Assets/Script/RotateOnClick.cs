using UnityEngine;

public class RotateOnClick : MonoBehaviour
{
   
    bool isActive = false;

    public float rotationAngle = 45f;

    // Метод, который вызывается при клике мышью на объект
    private void OnMouseDown()
    {
        if (isActive) rotationAngle = -15f;
        else rotationAngle = 15f;
            // Поворачиваем объект по оси Y (можно изменить на X или Z)
            transform.Rotate(rotationAngle, 0f, 0f);
        isActive = !isActive;
    }
}