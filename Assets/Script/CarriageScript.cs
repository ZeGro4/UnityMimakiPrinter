using System.Collections;
using UnityEngine;

public class CarriageScript : MonoBehaviour
{
    private Coroutine moveCoroutine;
    private Vector3 originalPosition;

    // Скорость постоянная
    [SerializeField] private float constantSpeed = 4f;

    // Твои границы
    public float minZ = 3.39f;
    public float maxZ = 3.91f;

    void Start()
    {
        // Сохраняем, чтобы в конце вернуть каретку на место
        originalPosition = transform.position;
    }

    public void MoveWithConstantSpeed(float moveTime)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveRoutine(moveTime));
    }

    private IEnumerator MoveRoutine(float duration)
    {
        float timer = 0f;

        // 1. Сразу выбираем первую цель.
        // Если мы сейчас ближе к низу (minZ), то едем вверх (к maxZ), и наоборот.
        float currentTargetZ = (Mathf.Abs(transform.position.z - minZ) < Mathf.Abs(transform.position.z - maxZ))
                               ? maxZ
                               : minZ;

        while (timer < duration)
        {
            // Рассчитываем шаг (расстояние) на этот кадр
            float step = constantSpeed * Time.deltaTime;

            // MoveTowards - это функция, которая двигает число current к target
            // Она САМА следит, чтобы мы не перелетели и не дергались.
            float newZ = Mathf.MoveTowards(transform.position.z, currentTargetZ, step);

            // Применяем позицию (X и Y берем из originalPosition, чтобы не сбивались)
            transform.position = new Vector3(originalPosition.x, originalPosition.y, newZ);

            // Проверяем: доехали ли мы до цели? (с погрешностью 0.001f)
            if (Mathf.Abs(transform.position.z - currentTargetZ) < 0.001f)
            {
                // Если доехали, меняем цель на противоположную
                if (currentTargetZ == maxZ)
                    currentTargetZ = minZ;
                else
                    currentTargetZ = maxZ;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Время вышло - возвращаем в исходную точку (как было в твоем первом коде)
        transform.position = originalPosition;
        moveCoroutine = null;
    }

    // Твое управление для проверки
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveWithConstantSpeed(5f);
        }

        // Управление скоростью
        if (Input.GetKeyDown(KeyCode.UpArrow)) constantSpeed += 0.5f;
        if (Input.GetKeyDown(KeyCode.DownArrow)) constantSpeed = Mathf.Max(0.1f, constantSpeed - 0.5f);
    }
}