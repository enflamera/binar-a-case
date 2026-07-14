using System.Collections;
using UnityEngine;

public class FinalTKPManager : MonoBehaviour
{
    public static FinalTKPManager Instance;

    public RectTransform policeLineLeft;
    public RectTransform policeLineRight;

    public Vector2 leftOpenPos;
    public Vector2 rightOpenPos;

    public Vector2 leftClosedPos;
    public Vector2 rightClosedPos;

    public float lineSpeed = 2f;

    public TransisiLokal transisi;

    public TKPFinishedPanelController finishedPanel;

    void Awake()
    {
        Instance = this;
    }

    public void FinishTKP()
    {
        StartCoroutine(FinishRoutine());
    }

    IEnumerator FinishRoutine()
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

        yield return new WaitForSeconds(0.5f);

        if (TKPAudioManager.Instance != null)
        {
            yield return StartCoroutine(TKPAudioManager.Instance.FadeOut());
        }

        int scoreToShow = ScoreManager.Instance.GetTkp1Score();
        finishedPanel.ShowAndProceed(scoreToShow, transisi, "PartnerDialogue4");
    }
}