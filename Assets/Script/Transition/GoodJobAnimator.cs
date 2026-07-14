using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class GoodJobAnimator : MonoBehaviour
{
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;

    public Vector2 startPos = new Vector2(-600f, 0f);
    public Vector2 centerPos = Vector2.zero;
    public Vector2 slowEndPos = new Vector2(80f, 0f);
    public Vector2 exitPos = new Vector2(900f, 0f);

    public float fastInDuration = 0.25f;
    public float slowDuration = 0.6f;
    public float pauseDuration = 2f;
    public float fastOutDuration = 0.25f;

    public AnimationCurve fastEase = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public AnimationCurve slowEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine currentRoutine;

    private void Reset()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Play(Action onComplete = null)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        gameObject.SetActive(true);
        currentRoutine = StartCoroutine(PlayRoutine(onComplete));
    }

    private IEnumerator PlayRoutine(Action onComplete)
    {
        rectTransform.anchoredPosition = startPos;
        canvasGroup.alpha = 0f;

        yield return Animate(startPos, centerPos, fastInDuration, fastEase, fadeFrom: 0f, fadeTo: 1f);
        yield return Animate(centerPos, slowEndPos, slowDuration, slowEase);
        yield return new WaitForSeconds(pauseDuration);
        yield return Animate(slowEndPos, exitPos, fastOutDuration, fastEase, fadeFrom: 1f, fadeTo: 0f);

        gameObject.SetActive(false);
        currentRoutine = null;
        onComplete?.Invoke();
    }

    private IEnumerator Animate(Vector2 from, Vector2 to, float duration, AnimationCurve ease,
        float? fadeFrom = null, float? fadeTo = null)
    {
        if (duration <= 0f)
        {
            rectTransform.anchoredPosition = to;
            if (fadeTo.HasValue) canvasGroup.alpha = fadeTo.Value;
            yield break;
        }

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float p = ease.Evaluate(Mathf.Clamp01(t / duration));

            rectTransform.anchoredPosition = Vector2.LerpUnclamped(from, to, p);

            if (fadeFrom.HasValue && fadeTo.HasValue)
                canvasGroup.alpha = Mathf.Lerp(fadeFrom.Value, fadeTo.Value, p);

            yield return null;
        }

        rectTransform.anchoredPosition = to;
        if (fadeTo.HasValue) canvasGroup.alpha = fadeTo.Value;
    }
}