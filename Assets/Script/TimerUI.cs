using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TMP_Text timerText;

    private void OnEnable()
    {
        if (TKPTimerManager.Instance == null) return;

        UpdateText(TKPTimerManager.Instance.TimeRemaining);
        TKPTimerManager.Instance.OnTick += UpdateText;
    }

    private void OnDisable()
    {
        if (TKPTimerManager.Instance != null)
            TKPTimerManager.Instance.OnTick -= UpdateText;
    }

    private void UpdateText(float remaining)
    {
        int minutes = Mathf.FloorToInt(remaining / 60f);
        int seconds = Mathf.FloorToInt(remaining % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}