using UnityEngine;

public class TimerStarter : MonoBehaviour
{
    public string timerID;
    public float durationSeconds;

    private void Start()
    {
        TKPTimerManager.Instance?.StartTimer(timerID, durationSeconds);
    }
}