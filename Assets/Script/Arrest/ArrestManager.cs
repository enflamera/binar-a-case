using UnityEngine;

public class ArrestManager : MonoBehaviour
{
    public static ArrestManager Instance;

    public bool pendingWellDone;
    public int lastArrestedIndex = -1;
    public bool lastArrestWasCorrect;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterArrest(int suspectIndex, bool correct)
    {
        lastArrestedIndex = suspectIndex;
        lastArrestWasCorrect = correct;

        if (correct)
        {
            pendingWellDone = true;
        }
    }

    public void ResetState()
    {
        pendingWellDone = false;
        lastArrestedIndex = -1;
        lastArrestWasCorrect = false;
    }
}