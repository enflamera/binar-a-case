using System.Collections;
using UnityEngine;

public class MapEvidenceController : MonoBehaviour
{
    [Header("Map")]
    public GameObject mapClose;
    public GameObject mapOpen;
    public RectTransform mapOpenRect;

    [Header("Document")]
    public CanvasGroup docCanvas;

    [Header("Buttons")]
    public GameObject openButton;
    public GameObject submitButton;
    public CanvasGroup submitButtonCanvas;

    [Header("Toast")]
    public GameObject vincyToast;
    public RectTransform toastRect;
    public CanvasGroup toastCanvas;

    [Header("Stamp")]
    public GameObject stampDiserahkan;
    public RectTransform stampRect;

    [Header("Evidence")]
    public ItemPickup documentPickup;

    [Header("Animation")]
    public float slideDistance = 350f;
    public float slideDuration = 1f;

    [Header("Toast Animation")]
    public float toastSlideDuration = 0.5f;
    public float toastStayDuration = 2f;

    bool opened;
    bool submitted;

    Vector2 mapOriginalPos;
    Vector2 toastOriginalPos;

    void Start()
    {
        mapOriginalPos = mapOpenRect.anchoredPosition;
        toastOriginalPos = toastRect.anchoredPosition;

        mapOpen.SetActive(false);

        docCanvas.alpha = 0;

        openButton.SetActive(false);

        submitButton.SetActive(false);
        if (submitButtonCanvas != null)
            submitButtonCanvas.alpha = 0;

        vincyToast.SetActive(false);
        stampDiserahkan.SetActive(false);
    }

    public void ShowOpenButton()
    {
        if (opened)
            return;

        openButton.SetActive(true);
    }

    public void OpenMap()
    {
        if (opened)
            return;

        opened = true;

        openButton.SetActive(false);

        mapClose.SetActive(false);
        mapOpen.SetActive(true);

        StartCoroutine(OpenSequence());
    }

    IEnumerator OpenSequence()
    {
        yield return new WaitForSeconds(1f);

        Vector2 targetPos =
            mapOriginalPos + Vector2.left * slideDistance;

        float time = 0;

        while (time < slideDuration)
        {
            time += Time.deltaTime;

            float t = time / slideDuration;

            mapOpenRect.anchoredPosition =
                Vector2.Lerp(mapOriginalPos, targetPos, t);

            docCanvas.alpha =
                Mathf.Lerp(0, 1, t);

            yield return null;
        }

        mapOpenRect.anchoredPosition = targetPos;
        docCanvas.alpha = 1;

        yield return new WaitForSeconds(1f);

        StartCoroutine(ShowToast());
    }

    IEnumerator ShowToast()
    {
        vincyToast.SetActive(true);

        toastCanvas.alpha = 1;

        Vector2 startPos =
            toastOriginalPos + new Vector2(800f, 0);

        toastRect.anchoredPosition = startPos;

        float time = 0;

        while (time < toastSlideDuration)
        {
            time += Time.deltaTime;

            float t = time / toastSlideDuration;

            toastRect.anchoredPosition =
                Vector2.Lerp(startPos, toastOriginalPos, t);

            yield return null;
        }

        toastRect.anchoredPosition = toastOriginalPos;

        yield return new WaitForSeconds(toastStayDuration);

        submitButton.SetActive(true);

        if (submitButtonCanvas != null)
        {
            submitButtonCanvas.alpha = 0;

            time = 0;

            while (time < 0.25f)
            {
                time += Time.deltaTime;

                submitButtonCanvas.alpha =
                    Mathf.Lerp(0, 1, time / 0.25f);

                yield return null;
            }

            submitButtonCanvas.alpha = 1;
        }
    }

    public void SubmitDocument()
    {
        if (submitted)
            return;

        submitted = true;

        StartCoroutine(HideToastAndStamp());
    }

    IEnumerator HideToastAndStamp()
    {
        if (submitButtonCanvas != null)
        {
            float t = 0;

            while (t < 0.2f)
            {
                t += Time.deltaTime;

                submitButtonCanvas.alpha =
                    Mathf.Lerp(1, 0, t / 0.2f);

                yield return null;
            }
        }

        submitButton.SetActive(false);

        Vector2 startPos = toastRect.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(800f, 0);

        float time = 0;
        float duration = 0.3f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;

            toastRect.anchoredPosition =
                Vector2.Lerp(startPos, endPos, t);

            toastCanvas.alpha =
                Mathf.Lerp(1, 0, t);

            yield return null;
        }

        toastRect.anchoredPosition = endPos;
        toastCanvas.alpha = 0;

        vincyToast.SetActive(false);

        yield return new WaitForSeconds(0.08f);

        StartCoroutine(StampSequence());
    }

    IEnumerator StampSequence()
    {
        stampDiserahkan.SetActive(true);

        stampRect.localScale = Vector3.one * 4f;
        stampRect.localRotation = Quaternion.Euler(0, 0, -15f);

        float time = 0;
        float duration = 0.12f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;

            stampRect.localScale =
                Vector3.Lerp(
                    Vector3.one * 4f,
                    Vector3.one,
                    t);

            stampRect.localRotation =
                Quaternion.Lerp(
                    Quaternion.Euler(0, 0, -15f),
                    Quaternion.Euler(0, 0, -5f),
                    t);

            yield return null;
        }

        stampRect.localScale = Vector3.one;
        stampRect.localRotation = Quaternion.Euler(0, 0, -5f);

        yield return new WaitForSeconds(0.3f);

        if (documentPickup != null)
            documentPickup.InstantCollect();
    }
}