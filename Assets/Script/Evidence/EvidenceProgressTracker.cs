using UnityEngine;

public class EvidenceProgressTracker : MonoBehaviour
{
    public static EvidenceProgressTracker Instance { get; private set; }

    public int requiredCount = 2;
    public TransisiLokal transisi;
    public string nextScene;

    int foundCount;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ReportFound()
    {
        foundCount++;
        Debug.Log($"[EvidenceProgressTracker] foundCount = {foundCount} / {requiredCount}");

        if (foundCount >= requiredCount)
        {
            Debug.Log("[EvidenceProgressTracker] Pindah scene!");
            transisi?.PindahScene(nextScene);
        }
    }
}