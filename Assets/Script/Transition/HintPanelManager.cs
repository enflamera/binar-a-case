using System.Collections;
using UnityEngine;

public class HintPanelManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject hintPanel;
    public RectTransform hintPanelRect;

    [Header("Animation")]
    public float animationSpeed = 3f;

    bool isAnimating = false;

    private void Start()
    {
        hintPanel.SetActive(false);
        hintPanelRect.localScale = Vector3.zero;
    }

    public void OpenHint()
    {
        if (isAnimating || hintPanel.activeSelf)
            return;

        StartCoroutine(WobbleIn());
    }

    public void CloseHint()
    {
        if (isAnimating || !hintPanel.activeSelf)
            return;

        StartCoroutine(WobbleOut());
    }

    IEnumerator WobbleIn()
    {
        isAnimating = true;

        hintPanel.SetActive(true);
        hintPanelRect.localScale = Vector3.zero;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * animationSpeed;

            float smooth = Mathf.SmoothStep(0f, 1f, t);
            float bounce = 1f + Mathf.Sin(t * 8f) * 0.15f * (1f - smooth);

            hintPanelRect.localScale = Vector3.one * smooth * bounce;

            yield return null;
        }

        hintPanelRect.localScale = Vector3.one;
        isAnimating = false;
    }

    IEnumerator WobbleOut()
    {
        isAnimating = true;

        Vector3 startScale = hintPanelRect.localScale;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * animationSpeed;

            float smooth = Mathf.SmoothStep(0f, 1f, t);
            float shake = (t > 0.7f)
                ? Mathf.Sin(t * 50f) * 0.05f * (t - 0.7f)
                : 0f;

            hintPanelRect.localScale =
                Vector3.Lerp(startScale, Vector3.zero, smooth)
                + new Vector3(shake, shake, 0);

            yield return null;
        }

        hintPanel.SetActive(false);
        hintPanelRect.localScale = Vector3.zero;

        isAnimating = false;
    }
}