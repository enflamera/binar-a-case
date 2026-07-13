using System.Collections;
using UnityEngine;
using TMPro;

public class TKPIntroManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public RectTransform instructionPanelRect;
    public TextMeshProUGUI countdownText;
    public RectTransform policeLineLeft;
    public RectTransform policeLineRight;
    public GameObject transitionPanel;
    public AudioSource audioSource;
    public AudioClip introBeepClip;

    public float lineMoveDistance = 2560f;
    public float lineSpeed = 2f;

    public string timerID;
    public float timerDuration;

    public string locationID = "TKP2";

    Vector2 leftOpenPos, rightOpenPos, leftClosedPos, rightClosedPos;

    private void Awake()
    {
        leftOpenPos = policeLineLeft.anchoredPosition;
        rightOpenPos = policeLineRight.anchoredPosition;
        leftClosedPos = Vector2.zero;
        rightClosedPos = Vector2.zero;
    }

    private void Start()
    {
        audioSource.loop = false;

        if (!string.IsNullOrEmpty(timerID))
        {
            TKPTimerManager.Instance?.PresetDisplay(timerDuration);
        }

        if (GameManager.Instance.HasSeenIntro(locationID))
        {
            instructionPanel.SetActive(false);
            countdownText.gameObject.SetActive(false);
            if (transitionPanel != null) transitionPanel.SetActive(false);

            policeLineLeft.anchoredPosition = leftOpenPos;
            policeLineRight.anchoredPosition = rightOpenPos;

            StartTKPTimer();
        }
        else
        {
            GameManager.Instance.MarkIntroSeen(locationID);
            instructionPanel.SetActive(true);
            countdownText.gameObject.SetActive(false);

            policeLineLeft.anchoredPosition = leftClosedPos;
            policeLineRight.anchoredPosition = rightClosedPos;

            instructionPanelRect.localScale = Vector3.zero;
            StartCoroutine(WobbleIn());
        }
    }

    public void OnClickUnderstood()
    {
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        yield return StartCoroutine(WobbleOut());
        audioSource.PlayOneShot(introBeepClip);
        yield return StartCoroutine(CountdownRoutine());
        yield return StartCoroutine(PoliceLineAnimation());

        StartTKPTimer();
    }

    void StartTKPTimer()
    {
        if (!string.IsNullOrEmpty(timerID))
        {
            TKPTimerManager.Instance?.StartTimer(timerID, timerDuration);
        }
    }

    IEnumerator WobbleIn()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 3f;
            float smooth = Mathf.SmoothStep(0f, 1f, t);
            float bounce = 1f + Mathf.Sin(t * 8f) * 0.15f * (1f - smooth);
            instructionPanelRect.localScale = Vector3.one * smooth * bounce;
            yield return null;
        }
        instructionPanelRect.localScale = Vector3.one;
    }

    IEnumerator WobbleOut()
    {
        Vector3 startScale = instructionPanelRect.localScale;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 3f;
            float smooth = Mathf.SmoothStep(0f, 1f, t);
            float shake = (t > 0.7f) ? Mathf.Sin(t * 50f) * 0.05f * (t - 0.7f) : 0f;
            instructionPanelRect.localScale = Vector3.Lerp(startScale, Vector3.zero, smooth) + new Vector3(shake, shake, 0);
            yield return null;
        }
        instructionPanel.SetActive(false);
    }

    IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            countdownText.transform.localScale = Vector3.one * 1.5f;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 1f;
                countdownText.transform.localScale = Vector3.Lerp(Vector3.one * 1.5f, Vector3.one, Mathf.SmoothStep(0f, 1f, t));
                yield return null;
            }
        }
        countdownText.gameObject.SetActive(false);
    }

    IEnumerator PoliceLineAnimation()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * lineSpeed;
            float eased = Mathf.SmoothStep(0f, 1f, t);
            policeLineLeft.anchoredPosition = Vector2.Lerp(leftClosedPos, leftOpenPos, eased);
            policeLineRight.anchoredPosition = Vector2.Lerp(rightClosedPos, rightOpenPos, eased);
            yield return null;
        }
    }
}