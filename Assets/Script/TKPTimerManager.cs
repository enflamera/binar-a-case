using System;
using UnityEngine;

public class TKPTimerManager : MonoBehaviour
{
    public static TKPTimerManager Instance { get; private set; }

    public event Action OnTimeUp;
    public event Action<float> OnTick;

    public float TimeRemaining { get; private set; }
    public bool IsRunning { get; private set; }
    public string CurrentTimerID { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!IsRunning) return;

        TimeRemaining -= Time.deltaTime;
        OnTick?.Invoke(TimeRemaining);

        if (TimeRemaining <= 0f)
        {
            TimeRemaining = 0f;
            IsRunning = false;
            OnTimeUp?.Invoke();
        }
    }

    public void StartTimer(string timerID, float durationSeconds)
    {
        if (IsRunning && CurrentTimerID == timerID) return;

        CurrentTimerID = timerID;
        TimeRemaining = durationSeconds;
        IsRunning = true;
    }

    public void StopTimer()
    {
        IsRunning = false;
    }

    public void PresetDisplay(float durationSeconds)
    {
        if (IsRunning) return;
        TimeRemaining = durationSeconds;
        OnTick?.Invoke(TimeRemaining);
    }
}