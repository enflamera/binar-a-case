using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewSuspectPopup : MonoBehaviour
{
    public static NewSuspectPopup Instance;

    [Header("UI")]
    public GameObject popupPanel;
    public Image suspectImage;
    public Transform popupContentBox;

    [Header("Wobble Settings")]
    public float animDuration = 0.35f;

    private Coroutine activeAnimRoutine;

    private void Awake()
    {
        Instance = this;
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    public void Show(Sprite image)
    {
        if (popupPanel == null) return;

        suspectImage.sprite = image;
        popupPanel.SetActive(true);

        if (activeAnimRoutine != null) StopCoroutine(activeAnimRoutine);
        activeAnimRoutine = StartCoroutine(WobbleInRoutine());
    }

    public void ContinueButton()
    {
        if (popupPanel == null || !popupPanel.activeSelf) return;

        if (activeAnimRoutine != null) StopCoroutine(activeAnimRoutine);
        activeAnimRoutine = StartCoroutine(WobbleOutRoutine());
    }

    IEnumerator WobbleInRoutine()
    {
        Transform targetBox = popupContentBox != null ? popupContentBox : popupPanel.transform;
        
        float elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animDuration;

            float scale = Mathf.Lerp(0f, 1f, Mathf.SmoothStep(0f, 1f, t)) + Mathf.Sin(t * Mathf.PI * 2.5f) * 0.12f * (1f - t);
            targetBox.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        targetBox.localScale = Vector3.one;
        activeAnimRoutine = null;
    }

    IEnumerator WobbleOutRoutine()
    {
        Transform targetBox = popupContentBox != null ? popupContentBox : popupPanel.transform;
        Vector3 startScale = targetBox.localScale;

        float elapsed = 0f;
        float outDuration = animDuration * 0.6f;

        while (elapsed < outDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / outDuration;

            targetBox.localScale = Vector3.Lerp(startScale, Vector3.zero, Mathf.SmoothStep(0f, 1f, t));

            yield return null;
        }

        targetBox.localScale = Vector3.zero;
        popupPanel.SetActive(false);
        activeAnimRoutine = null;
    }
}