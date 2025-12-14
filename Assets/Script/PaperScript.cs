using System.Collections;
using UnityEngine;

public class PaperScript : MonoBehaviour
{
    [SerializeField] Material material;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Printing()
    {
        Debug.Log("Printing called on: " + gameObject.name);
        Debug.Log("Active Self: " + gameObject.activeSelf);
        Debug.Log("Active In Hierarchy: " + gameObject.activeInHierarchy);
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        gameObject.GetComponent<Renderer>().material = material;
        StartCoroutine(MoveAndScaleAnimation());
    }

    private IEnumerator MoveAndScaleAnimation()
    {
        Vector3 startPosition = transform.position;
        // Двигаемся на +0.5 по X (исправлено: убрал минус)
        Vector3 targetPosition = new Vector3(startPosition.x - 5f, startPosition.y, startPosition.z);
        Vector3 startScale = transform.localScale;
        // Увеличиваем только по оси X (например, на 20%)
        Vector3 targetScale = new Vector3(startScale.x * 4f, startScale.y, startScale.z);

        float duration = 15f; // 2 секунды для более плавной анимации (было 10)
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Используем SmoothStep для более плавной анимации
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            // Плавное движение и изменение масштаба
            transform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);
            transform.localScale = Vector3.Lerp(startScale, targetScale, smoothT);

            yield return null;
        }

        // Гарантируем точные конечные значения
        transform.position = targetPosition;
        transform.localScale = targetScale;
    }
}