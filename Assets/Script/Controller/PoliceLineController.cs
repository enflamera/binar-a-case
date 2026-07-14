using System.Collections;
using UnityEngine;

public class PoliceLineController : MonoBehaviour
{
    [Header("Police Line")]
    public RectTransform policeLineLeft;
    public RectTransform policeLineRight;
    public float lineSpeed = 2f;

    public Vector2 leftOpenPos;
    public Vector2 rightOpenPos;
    public Vector2 leftClosedPos;
    public Vector2 rightClosedPos;

    [Header("Timeout Target")]
    public string nextSceneOnTimeout;
    public TransisiLokal transisi;

    private bool animationPlaying;

    private void OnEnable()
    {
        if (TKPTimerManager.Instance != null)
            TKPTimerManager.Instance.OnTimeUp += HandleTimeUp;
    }

    private void OnDisable()
    {
        if (TKPTimerManager.Instance != null)
            TKPTimerManager.Instance.OnTimeUp -= HandleTimeUp;
    }

    private void HandleTimeUp()
    {
        if (animationPlaying) return;
        StartCoroutine(ClosePoliceLine());
    }

    IEnumerator ClosePoliceLine()
    {
        animationPlaying = true;

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

        transisi?.PindahScene(nextSceneOnTimeout);
    }
}