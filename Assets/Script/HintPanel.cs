using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HintPanel : MonoBehaviour
{
    public GameObject instructionPanel;
    public RectTransform instructionPanelRect;
    public Button mengertiButton;

    void Awake()
    {
        mengertiButton.onClick.AddListener(Close);
    }

    public void Show()
    {
        instructionPanel.SetActive(true);
        instructionPanelRect.localScale = Vector3.zero;
        StartCoroutine(WobbleIn());
    }

    public void Close()
    {
        StartCoroutine(WobbleOut());
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
}