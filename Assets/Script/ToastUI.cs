using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToastUI : MonoBehaviour
{
    public RectTransform rect;
    public CanvasGroup canvasGroup;
    public TMP_Text text;
    public Button actionButton;
    public Vector2 hiddenPosition;
    public Vector2 shownPosition;
    public float moveDuration = 0.4f;
    public float typingSpeed = 0.03f;
    public float autoHideDelay = 1.5f;

    Action onButtonClicked;
    Action onFinished;
    Coroutine routine;

    void Awake()
    {
        actionButton.onClick.AddListener(HandleButtonClick);
        actionButton.gameObject.SetActive(false);
        canvasGroup.alpha = 0f;
        rect.anchoredPosition = hiddenPosition;
        gameObject.SetActive(false);
    }

    public void Show(string message, bool showButton, bool isDani, Action onButton, Action finished)
    {
        onButtonClicked = onButton;
        onFinished = finished;

        if (routine != null)
            StopCoroutine(routine);

        gameObject.SetActive(true);
        actionButton.gameObject.SetActive(false);
        routine = StartCoroutine(ShowRoutine(message, showButton && isDani));
    }

    IEnumerator ShowRoutine(string message, bool showButton)
    {
        float t = 0f;
        rect.anchoredPosition = hiddenPosition;
        canvasGroup.alpha = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;
            rect.anchoredPosition = Vector2.Lerp(hiddenPosition, shownPosition, t);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        rect.anchoredPosition = shownPosition;
        canvasGroup.alpha = 1f;

        text.text = "";

        foreach (char c in message)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (showButton)
        {
            actionButton.gameObject.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(autoHideDelay);
            yield return HideRoutine();
        }
    }

    void HandleButtonClick()
    {
        actionButton.gameObject.SetActive(false);
        onButtonClicked?.Invoke();
        StartCoroutine(HideRoutine());
    }

    IEnumerator HideRoutine()
    {
        float t = 0f;
        Vector2 startPos = rect.anchoredPosition;
        float startAlpha = canvasGroup.alpha;

        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;
            rect.anchoredPosition = Vector2.Lerp(startPos, hiddenPosition, t);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t);
            yield return null;
        }

        rect.anchoredPosition = hiddenPosition;
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);

        onFinished?.Invoke();
    }
}