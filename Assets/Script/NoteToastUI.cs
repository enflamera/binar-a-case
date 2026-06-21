using System.Collections;
using UnityEngine;

public class NoteToastUI : MonoBehaviour
{
    public static NoteToastUI Instance;

    public float slideDistance = 200f;
    public float slideDuration = 0.3f;
    public float showTime = 1.2f;
    public float fadeOutDuration = 0.3f;

    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Vector2 startPos;

    private void Awake()
    {
        Instance = this;

        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        startPos = rect.anchoredPosition;
        canvasGroup.alpha = 0f;
    }

    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        rect.anchoredPosition = startPos + new Vector2(-slideDistance, 0);
        canvasGroup.alpha = 0f;

        float t = 0f;

        while (t < slideDuration)
        {
            t += Time.deltaTime;
            float k = t / slideDuration;

            rect.anchoredPosition = Vector2.Lerp(
                startPos + new Vector2(-slideDistance, 0),
                startPos,
                k
            );

            canvasGroup.alpha = k;

            yield return null;
        }

        rect.anchoredPosition = startPos;
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(showTime);

        t = 0f;
        float startAlpha = canvasGroup.alpha;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            float k = t / fadeOutDuration;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, k);

            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}