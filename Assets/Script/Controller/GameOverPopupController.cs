using UnityEngine;
using System.Collections;

public class GameOverPopupController : MonoBehaviour
{
    public GameObject popupPanel;
    public Transform popupContentBox;

    public float animDuration = 0.35f;

    public TransisiLokal transisi;
    public string firstSceneName;
    public string mainMenuSceneName;

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

    public void OnRestartClicked()
    {
        ResetAllProgress();
        transisi?.PindahScene(firstSceneName);
    }

    public void OnQuitClicked()
    {
        ResetAllProgress();
        transisi?.PindahScene(mainMenuSceneName);
    }

    void ResetAllProgress()
    {
        GameManager.Instance?.ResetState();
        SuspectManager.Instance?.ResetState();
        ArrestManager.Instance?.ResetState();
        ScoreManager.Instance?.ResetScore();
        TKPTimerManager.Instance?.StopTimer();
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