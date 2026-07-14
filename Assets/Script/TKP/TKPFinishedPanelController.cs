using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TKPFinishedPanelController : MonoBehaviour
{
    public GameObject panelRoot;
    public Canvas panelCanvas;
    public int forcedSortingOrder = 999;
    public CanvasGroup rootCanvasGroup;
    public RectTransform characterRect;
    public CanvasGroup toastCanvasGroup;
    public TMP_Text scoreText;

    public float rootFadeDuration = 0.25f;

    public Vector2 characterOffscreenPos;
    public Vector2 characterOnscreenPos;
    public float characterSlideDuration = 0.3f;

    public float toastFadeDuration = 0.3f;
    public float scoreCountDuration = 0.6f;
    public float holdDuration = 0.6f;

    public void ShowAndProceed(int scoreToShow, TransisiLokal transisi, string nextScene)
    {
        StartCoroutine(ShowThenTransisi(scoreToShow, transisi, nextScene));
    }

    public void ShowThenCallback(int scoreToShow, Action onComplete)
    {
        StartCoroutine(ShowThenCallbackRoutine(scoreToShow, onComplete));
    }

    IEnumerator ShowThenTransisi(int scoreToShow, TransisiLokal transisi, string nextScene)
    {
        yield return StartCoroutine(PlayIntro(scoreToShow));
        transisi?.PindahScene(nextScene);
    }

    IEnumerator ShowThenCallbackRoutine(int scoreToShow, Action onComplete)
    {
        yield return StartCoroutine(PlayIntro(scoreToShow));
        onComplete?.Invoke();
    }

    IEnumerator PlayIntro(int scoreToShow)
    {
        panelRoot.SetActive(true);
        panelRoot.transform.SetAsLastSibling();

        if (panelCanvas != null)
            panelCanvas.sortingOrder = forcedSortingOrder;

        rootCanvasGroup.alpha = 0f;
        characterRect.anchoredPosition = characterOffscreenPos;
        toastCanvasGroup.alpha = 0f;
        scoreText.text = "0";

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / rootFadeDuration;
            rootCanvasGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }

        rootCanvasGroup.alpha = 1f;

        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / characterSlideDuration;

            characterRect.anchoredPosition = Vector2.Lerp(
                characterOffscreenPos,
                characterOnscreenPos,
                Mathf.SmoothStep(0f, 1f, t)
            );

            yield return null;
        }

        characterRect.anchoredPosition = characterOnscreenPos;

        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / toastFadeDuration;
            toastCanvasGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }

        toastCanvasGroup.alpha = 1f;

        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / scoreCountDuration;

            int displayed = Mathf.RoundToInt(Mathf.Lerp(0, scoreToShow, t));
            scoreText.text = displayed.ToString();

            yield return null;
        }

        scoreText.text = scoreToShow.ToString();

        yield return new WaitForSeconds(holdDuration);
    }
}