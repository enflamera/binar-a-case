using UnityEngine;
using System.Collections;

public class TrialSuccessPopupController : MonoBehaviour
{
    public GameObject popupPanel;
    public Transform popupContentBox;

    public float animDuration = 0.35f;

    public TransisiLokal transisi;
    public string wellDoneSceneName = "WellDone";

    private Coroutine activeAnimRoutine;

    private void Awake()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    public void Show()
    {
        if (popupPanel == null) return;

        popupPanel.SetActive(true);

        if (activeAnimRoutine != null) StopCoroutine(activeAnimRoutine);
        activeAnimRoutine = StartCoroutine(WobbleInRoutine());
    }

    public void OnUnderstandClicked()
    {
        transisi?.PindahScene(wellDoneSceneName);
    }

    IEnumerator WobbleInRoutine()
    {
        Transform targetBox = popupContentBox != null ? popupContentBox : popupPanel.transform;
        targetBox.localScale = Vector3.zero;

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
}