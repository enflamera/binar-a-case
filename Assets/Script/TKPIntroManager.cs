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

    public AudioSource audioSource;
    public AudioClip introBeepClip; 

    public float lineMoveDistance = 2560f;
    public float lineSpeed = 2f;

    Vector2 leftOpenPos, rightOpenPos, leftClosedPos, rightClosedPos;

    private void Start()
    {
        audioSource.loop = false;
        instructionPanel.SetActive(true);
        countdownText.gameObject.SetActive(false);

        leftOpenPos = policeLineLeft.anchoredPosition;
        rightOpenPos = policeLineRight.anchoredPosition;
        leftClosedPos = leftOpenPos + Vector2.left * lineMoveDistance;
        rightClosedPos = rightOpenPos + Vector2.right * lineMoveDistance;

        policeLineLeft.anchoredPosition = leftOpenPos;
        policeLineRight.anchoredPosition = rightOpenPos;
        instructionPanelRect.localScale = Vector3.zero;
        StartCoroutine(WobbleIn());
    }

    public void OnClickUnderstood()
    {
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        yield return StartCoroutine(WobbleOut());
        
        // Memulai suara tepat setelah panel hilang
        audioSource.PlayOneShot(introBeepClip);
        
        // Hitungan mundur dan pergerakan garis berjalan beriringan dengan durasi audio
        yield return StartCoroutine(CountdownRoutine());
        yield return StartCoroutine(PoliceLineAnimation());
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
        // Menampilkan 3, 2, 1 dengan jeda yang pas dengan tempo audio
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
            policeLineLeft.anchoredPosition = Vector2.Lerp(leftOpenPos, leftClosedPos, eased);
            policeLineRight.anchoredPosition = Vector2.Lerp(rightOpenPos, rightClosedPos, eased);
            yield return null;
        }
    }
}