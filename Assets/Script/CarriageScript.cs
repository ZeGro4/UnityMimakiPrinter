using System.Collections;
using UnityEngine;

public class CarriageScript : MonoBehaviour
{
    [Header("Границы перемещения")]
    public float minZ = -3.39f;
    public float maxZ = 3.91f;

    [Header("Скорость (ед/сек)")]
    // Эта скорость НЕ зависит от времени работы таймера
    public float moveSpeed = 1.0f;

    // Храним ссылку на текущую корутину, чтобы можно было перезапустить таймер
    private Coroutine movementCoroutine;

    // Для теста в инспекторе (галочка, чтобы запустить движение)
    [Header("Тест (нажми в Play Mode)")]
    public float testDuration = 5.0f;
    public bool clickToStart = false;

    private void Update()
    {
        // Просто для удобного теста через инспектор
        if (clickToStart)
        {
            clickToStart = false;
            StartMoving(testDuration);
        }
    }

    // --- ГЛАВНАЯ ФУНКЦИЯ ---
    // Вызывайте её и передавайте время в секундах
    public void StartMoving(float duration)
    {
        // Если объект уже двигается, останавливаем старое движение и запускаем новое
        if (movementCoroutine != null) StopCoroutine(movementCoroutine);

        movementCoroutine = StartCoroutine(MoveRoutine(duration));
    }

    private IEnumerator MoveRoutine(float duration)
    {
        float timer = 0f;

        // Определяем начальную цель. 
        // Если мы ближе к minZ, то едем к maxZ, и наоборот.
        float targetZ = (Mathf.Abs(transform.position.z - minZ) < Mathf.Abs(transform.position.z - maxZ)) ? maxZ : minZ;

        // Цикл работает, пока таймер меньше заданного времени
        while (timer < duration)
        {
            // 1. Двигаем объект к текущей цели с заданной скоростью
            // Mathf.MoveTowards гарантирует линейную постоянную скорость
            float currentZ = transform.position.z;
            float step = moveSpeed * Time.deltaTime;

            float newZ = Mathf.MoveTowards(currentZ, targetZ, step);

            // Применяем позицию
            Vector3 pos = transform.position;
            pos.z = newZ;
            transform.position = pos;


            if (Mathf.Approximately(newZ, targetZ))
            {
                // Если доехали до maxZ, меняем цель на minZ и наоборот
                if (targetZ == maxZ)
                    targetZ = minZ;
                else
                    targetZ = maxZ;
            }

            // 3. Обновляем таймер
            timer += Time.deltaTime;

            yield return null; // Ждем следующий кадр
        }

        // (Опционально) Когда время вышло, объект просто останавливается там, где он был.
        Debug.Log("Время вышло, объект остановлен.");
        movementCoroutine = null;
    }

    public void UpCarriage()
    {
        transform.position += new Vector3(0, 0.1f, 0);
    }
    public void DefaultCarriage()
    {
        transform.position += new Vector3(0, 0.1f, 0);
    }
    public void DownCarriage()
    {
        transform.position -= new Vector3(0, 0.3f, 0);
    }
}