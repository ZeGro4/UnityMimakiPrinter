using UnityEngine;
using System.Collections;

public class ObjectMoveRotateOnKey : MonoBehaviour
{
    [System.Serializable]
    public class KeyMovePair
    {
        public KeyCode key = KeyCode.A;              // Клавиша активации
        public Vector3 targetPosition = Vector3.zero; // Целевая позиция
        public Vector3 targetRotation = Vector3.zero; // Целевая ротация (Euler углы)
        public string description = "";              // Описание для удобства
    }

    public float moveDuration = 1f;  // Длительность анимации (секунды)

    // Массив пар: клавиша + позиция + ротация (настраивается в инспекторе)
    public KeyMovePair[] keyMovePairs = new KeyMovePair[]
    {
        new KeyMovePair { key = KeyCode.A, targetPosition = new Vector3(5f, 0f, 0f), targetRotation = new Vector3(0f, 90f, 0f), description = "Клавиша A: Переместить вправо и повернуть" },
        new KeyMovePair { key = KeyCode.B, targetPosition = new Vector3(0f, 0f, 5f), targetRotation = new Vector3(0f, 180f, 0f), description = "Клавиша B: Переместить вперёд и повернуть" }
        // Добавь больше в инспекторе
    };

    private Coroutine currentCoroutine;

    void Start()
    {
        Debug.Log("ObjectMoveRotateOnKey запущен на объекте: " + gameObject.name);
        Debug.Log("Настроено пар: " + keyMovePairs.Length);

        // Логгируем все пары для отладки
        for (int i = 0; i < keyMovePairs.Length; i++)
        {
            var pair = keyMovePairs[i];
            Debug.Log("Пара " + i + ": Клавиша=" + pair.key + ", Позиция=" + pair.targetPosition + ", Ротация=" + pair.targetRotation + ", Описание='" + pair.description + "'");
        }
    }

    void Update()
    {
        // Цикл по всем парам: проверяем каждую клавишу
        for (int i = 0; i < keyMovePairs.Length; i++)
        {
            var pair = keyMovePairs[i];
            if (Input.GetKeyDown(pair.key))
            {
                Debug.Log("Нажата клавиша " + pair.key + " (пара " + i + ") для перемещения к позиции " + pair.targetPosition + " и ротации " + pair.targetRotation);
                MoveToTarget(pair.targetPosition, pair.targetRotation);
                return;  // Выходим, чтобы не обрабатывать несколько сразу
            }
        }
    }

    public void MoveToTarget(Vector3 targetPos, Vector3 targetRot)
    {
        // Проверяем, настроена ли цель (позиция != zero, но можно изменить)
        if (targetPos == Vector3.zero && targetRot == Vector3.zero)
        {
            Debug.LogError("Цель не настроена для этой пары!");
            return;
        }

        if (currentCoroutine != null)
        {
            Debug.Log("Останавливаем предыдущую корутину");
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(MoveToPosition(targetPos, targetRot));
        Debug.Log("Корутина запущена");
    }

    private IEnumerator MoveToPosition(Vector3 targetPos, Vector3 targetRot)
    {
        Debug.Log("Начало перемещения к позиции: " + targetPos + " и ротации: " + targetRot);

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Quaternion targetQuaternion = Quaternion.Euler(targetRot);  // Преобразуем Euler в Quaternion

        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = time / moveDuration;

            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Lerp(startRot, targetQuaternion, t);

            yield return null;
        }

        // Финальная точная позиция/ротация
        transform.position = targetPos;
        transform.rotation = targetQuaternion;

        Debug.Log("Перемещение завершено: текущая позиция " + transform.position + ", ротация " + transform.eulerAngles);
    }
}