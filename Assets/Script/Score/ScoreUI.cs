using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public float countDuration = 0.6f;

    private Coroutine countRoutine;
    private int shownValue;

    private void OnEnable()
    {
        if (ScoreManager.Instance == null) return;

        shownValue = ScoreManager.Instance.DisplayedScore;
        scoreText.text = shownValue.ToString();

        int pending = ScoreManager.Instance.GetPendingDelta();
        if (pending > 0)
        {
            StartCountUp(pending);
        }

        ScoreManager.Instance.OnScoreAdded += HandleScoreAdded;
    }

    private void OnDisable()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreAdded -= HandleScoreAdded;
    }

    private void HandleScoreAdded(int amount, ScoreCategory category, string key)
    {
        StartCountUp(amount);
    }

    private void StartCountUp(int amount)
    {
        if (countRoutine != null) StopCoroutine(countRoutine);
        countRoutine = StartCoroutine(CountUpRoutine(amount));
    }

    private IEnumerator CountUpRoutine(int amount)
    {
        int startValue = shownValue;
        int targetValue = shownValue + amount;
        float elapsed = 0f;

        while (elapsed < countDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / countDuration);
            shownValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, t));
            scoreText.text = shownValue.ToString();
            yield return null;
        }

        shownValue = targetValue;
        scoreText.text = shownValue.ToString();
        ScoreManager.Instance.ConsumeDisplayed(amount);
    }
}