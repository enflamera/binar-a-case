using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TKPCompletionManager : MonoBehaviour
{
    [Header("Evidence")]
    public string[] evidenceIDs;

    [Header("Police Line")]
    public RectTransform policeLineLeft;
    public RectTransform policeLineRight;
    public float lineSpeed = 2f;

    [Header("Scene")]
    public string homeSceneName = "Beranda";
    public float delayBeforeLoad = 1f;

    [Header("Fade")]
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 0.5f;

    [Header("Finished Panel")]
    public TKPFinishedPanelController finishedPanel;

    Vector2 leftOpenPos;
    Vector2 rightOpenPos;
    Vector2 leftClosedPos;
    Vector2 rightClosedPos;

    bool animationPlaying = false;

    void Awake()
    {
        leftOpenPos = policeLineLeft.anchoredPosition;
        rightOpenPos = policeLineRight.anchoredPosition;

        leftClosedPos = Vector2.zero;
        rightClosedPos = Vector2.zero;

        if (fadeCanvas != null)
        {
            fadeCanvas.gameObject.SetActive(true);
            fadeCanvas.alpha = 0f;
            fadeCanvas.blocksRaycasts = false;
        }
    }

    void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.tkp2Completed)
            return;

        if (animationPlaying)
            return;

        foreach (string id in evidenceIDs)
        {
            if (!GameManager.Instance.IsEvidenceCompleted(id))
                return;
        }

        animationPlaying = true;
        StartCoroutine(ClosePoliceLine());
    }

    IEnumerator ClosePoliceLine()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * lineSpeed;

            float eased = Mathf.SmoothStep(0f, 1f, t);

            policeLineLeft.anchoredPosition =
                Vector2.Lerp(leftOpenPos, leftClosedPos, eased);

            policeLineRight.anchoredPosition =
                Vector2.Lerp(rightOpenPos, rightClosedPos, eased);

            yield return null;
        }

        GameManager.Instance.tkp2Completed = true;

        yield return new WaitForSeconds(delayBeforeLoad);

        int scoreToShow = ScoreManager.Instance.GetTkp2Score();
        finishedPanel.ShowThenCallback(scoreToShow, () => StartCoroutine(FadeOutAndLoad()));
    }

    IEnumerator FadeOutAndLoad()
    {
        yield return StartCoroutine(FadeOut());

        if (TKPAudioManager.Instance != null)
        {
            yield return StartCoroutine(TKPAudioManager.Instance.FadeOut());
        }

        SceneManager.LoadScene(homeSceneName);
    }

    IEnumerator FadeOut()
    {
        if (fadeCanvas == null)
            yield break;

        fadeCanvas.blocksRaycasts = true;

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = 1f;
    }
}