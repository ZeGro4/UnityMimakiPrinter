using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageScript : MonoBehaviour
{
    private Coroutine moveCoroutine;
    private bool isMoving = false;
    private Vector3 originalPosition; // Сохраняем исходную позицию

    public Vector3 targetPosition;


    void Start()
    {
        targetPosition = new Vector3(
                transform.position.x,
                transform.position.y,
                Random.Range(3.39f, 3.91f)
            );
    }


    // Основная функция для перемещения туда-сюда
    public void MoveBackAndForth( float totalDuration)
    {
        
        // Если уже движется, останавливаем предыдущую корутину
        if (isMoving && moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        // Сохраняем текущую позицию как точку A
        originalPosition = transform.position;

        // Запускаем корутину движения туда-сюда
        moveCoroutine = StartCoroutine(MoveBackAndForthCoroutine(targetPosition, totalDuration));
    }

    // Корутина для движения туда-сюда
    private IEnumerator MoveBackAndForthCoroutine(Vector3 targetPosition, float totalDuration)
    {
        isMoving = true;
        float elapsedTime = 0f;

        // Вычисляем время для одного направления (половина общего времени)
        float oneWayDuration = totalDuration / 2f;

        // Движение ВПЕРЕД: от originalPosition к targetPosition
        while (elapsedTime < oneWayDuration)
        {
            float t = elapsedTime / oneWayDuration;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Гарантируем точное достижение целевой точки
        transform.position = targetPosition;

        // Сбрасываем время для обратного пути
        elapsedTime = 0f;

        // Движение НАЗАД: от targetPosition к originalPosition
        while (elapsedTime < oneWayDuration)
        {
            float t = elapsedTime / oneWayDuration;
            transform.position = Vector3.Lerp(targetPosition, originalPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Возвращаемся в исходную позицию
        transform.position = originalPosition;
        isMoving = false;
    }


    public void MoveBackAndForthSmooth(Vector3 targetPosition, float totalDuration)
    {
        if (isMoving && moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        originalPosition = transform.position;
        moveCoroutine = StartCoroutine(MoveBackAndForthSmoothCoroutine(targetPosition, totalDuration));
    }

    private IEnumerator MoveBackAndForthSmoothCoroutine(Vector3 targetPosition, float totalDuration)
    {
        isMoving = true;
        float elapsedTime = 0f;

        while (elapsedTime < totalDuration)
        {

            float t = Mathf.Sin(elapsedTime / totalDuration * Mathf.PI);

            // Преобразуем от -1..1 к 0..1
            t = (t + 1f) / 2f;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Гарантируем возврат в исходную позицию
        transform.position = originalPosition;
        isMoving = false;
    }

    // Функция для остановки перемещения
    public void StopMoving()
    {
        if (isMoving && moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            isMoving = false;
        }
    }

    // Проверка, движется ли объект
    public bool IsMoving()
    {
        return isMoving;
    }

    // Пример использования в Update
    void Update()
    {
        // Пример: при нажатии пробела двигаем объект туда-сюда
        if (Input.GetKeyDown(KeyCode.Space))
        {

           

            MoveBackAndForth(5f);

            // Или используйте плавную версию:
            // MoveBackAndForthSmooth(targetPosition, 5f);
        }
    }
}