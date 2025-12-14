using UnityEngine;
using System.Collections;

public class CameraViewpointSystem : MonoBehaviour
{
    [System.Serializable]
    public class Viewpoint
    {
        public string name;
        public Vector3 position;
        public Vector3 rotation;
        public float fieldOfView = 60f;
        public bool isConfigured = false;
    }

    [System.Serializable]
    public class KeyViewpointPair
    {
        public KeyCode key = KeyCode.A;           // Клавиша активации (по умолчанию A)
        public int viewpointIndex = 4;            // Индекс viewpoint
        public float delayBeforeMove = 0.5f;      // Индивидуальная задержка перед перемещением (секунды)
        public string description = "";           // Описание для удобства (опционально)
    }

    public Viewpoint[] viewpoints;
    public Viewpoint defaultViewpoint;
    public float moveDuration = 1f;

    // МАССИВ ПАР: клавиша + индекс viewpoint (настраивается в инспекторе)
    public KeyViewpointPair[] keyViewpointPairs = new KeyViewpointPair[]
    {
        new KeyViewpointPair { key = KeyCode.A, viewpointIndex = 4, delayBeforeMove = 0.5f, description = "Клавиша A для viewpoint 4" },
        new KeyViewpointPair { key = KeyCode.B, viewpointIndex = 5, delayBeforeMove = 1f, description = "Клавиша B для viewpoint 5" }
        // Добавь больше здесь или в инспекторе (C, D и т.д.)
    };

    private Camera cam;
    private Coroutine currentCoroutine;

    void Start()
    {
        cam = GetComponent<Camera>();
        Debug.Log("CameraViewpointSystem запущен. Всего viewpoints: " + viewpoints.Length);
        Debug.Log("Настроено пар клавиша-viewpoint: " + keyViewpointPairs.Length);

        // Логгируем все пары для отладки
        for (int i = 0; i < keyViewpointPairs.Length; i++)
        {
            var pair = keyViewpointPairs[i];
            Debug.Log("Пара " + i + ": Клавиша=" + pair.key + ", Viewpoint=" + pair.viewpointIndex + ", Delay=" + pair.delayBeforeMove + ", Описание='" + pair.description + "'");
        }

        // Автопроверка конфигурации viewpoints
        for (int i = 0; i < viewpoints.Length; i++)
        {
            viewpoints[i].isConfigured = viewpoints[i].position != Vector3.zero;
            Debug.Log("Viewpoint " + i + ": " + viewpoints[i].name + " - настроен: " + viewpoints[i].isConfigured + " позиция: " + viewpoints[i].position);
        }
    }

    void Update()
    {
        // Цикл по всем парам: проверяем каждую клавишу
        for (int i = 0; i < keyViewpointPairs.Length; i++)
        {
            var pair = keyViewpointPairs[i];
            if (Input.GetKeyDown(pair.key))
            {
                Debug.Log("Нажата клавиша " + pair.key + " (пара " + i + ") для viewpoint " + pair.viewpointIndex + " с задержкой " + pair.delayBeforeMove);
                MoveToViewpoint(pair.viewpointIndex, pair.delayBeforeMove);
                return;  // Выходим, чтобы не обрабатывать несколько сразу
            }
        }
    }

    // ЭТОТ МЕТОД ВИДЕН В ИНСПЕКТОРЕ (1 параметр) - без задержки
    public void MoveToViewpoint(int viewpointIndex)
    {
        MoveToViewpoint(viewpointIndex, 0f);
    }

    // ЭТОТ МЕТОД ВИДЕН В ИНСПЕКТОРЕ (1 параметр) - с фиксированной задержкой 0.5 сек
    public void MoveToViewpointWithDelay(int viewpointIndex)
    {
        MoveToViewpoint(viewpointIndex, 0.5f);
    }

    // Внутренний метод с двумя параметрами (НЕ виден в инспекторе)
    private void MoveToViewpoint(int viewpointIndex, float delay)
    {
        Debug.Log("=== НАЖАТА КНОПКА ===");
        Debug.Log("Получен индекс: " + viewpointIndex);
        Debug.Log("Всего viewpoints: " + viewpoints.Length);

        // Проверяем валидность индекса
        if (viewpointIndex < 0 || viewpointIndex >= viewpoints.Length)
        {
            Debug.LogError("Индекс " + viewpointIndex + " вне диапазона! Допустимо: 0-" + (viewpoints.Length - 1));
            return;
        }

        // Проверяем настройку viewpoint
        if (!viewpoints[viewpointIndex].isConfigured)
        {
            Debug.LogError("Viewpoint " + viewpointIndex + " не настроен! Имя: " + viewpoints[viewpointIndex].name);
            return;
        }

        Debug.Log("Перемещаемся к: " + viewpoints[viewpointIndex].name + " позиция: " + viewpoints[viewpointIndex].position);

        if (currentCoroutine != null)
        {
            Debug.Log("Останавливаем предыдущую корутину");
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(MoveToPosition(viewpoints[viewpointIndex], delay));
        Debug.Log("Корутина запущена");
    }

    private IEnumerator MoveToPosition(Viewpoint target, float delay)
    {
        // Индивидуальная задержка перед перемещением
        if (delay > 0f)
        {
            Debug.Log("Задержка перед перемещением: " + delay + " секунд");
            yield return new WaitForSeconds(delay);
        }

        Debug.Log("Начало перемещения к: " + target.name);

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float startFOV = cam.fieldOfView;

        Debug.Log("Стартовая позиция: " + startPos);
        Debug.Log("Целевая позиция: " + target.position);

        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = time / moveDuration;

            transform.position = Vector3.Lerp(startPos, target.position, t);
            transform.rotation = Quaternion.Lerp(startRot, Quaternion.Euler(target.rotation), t);
            cam.fieldOfView = Mathf.Lerp(startFOV, target.fieldOfView, t);

            yield return null;
        }

        // Финальная точная позиция
        transform.position = target.position;
        transform.rotation = Quaternion.Euler(target.rotation);
        cam.fieldOfView = target.fieldOfView;

        Debug.Log("Перемещение завершено: " + target.name + " текущая позиция: " + transform.position);
    }

    // ВИДЕН В ИНСПЕКТОРЕ (0 параметров)
    public void ReturnToDefaultView()
    {
        Debug.Log("Возврат к общему виду");

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(MoveToPosition(defaultViewpoint, 0f));
    }

    // ВИДЕН В ИНСПЕКТОРЕ (1 параметр) - мгновенное перемещение
    public void TestInstantMove(int viewpointIndex)
    {
        if (viewpointIndex >= 0 && viewpointIndex < viewpoints.Length)
        {
            Debug.Log("ТЕСТ: Мгновенное перемещение к " + viewpoints[viewpointIndex].name);
            transform.position = viewpoints[viewpointIndex].position;
            transform.rotation = Quaternion.Euler(viewpoints[viewpointIndex].rotation);
            cam.fieldOfView = viewpoints[viewpointIndex].fieldOfView;
        }
    }
}