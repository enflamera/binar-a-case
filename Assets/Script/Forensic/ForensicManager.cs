using System.Collections.Generic;
using UnityEngine;

public class ForensicManager : MonoBehaviour
{
    public static ForensicManager Instance { get; private set; }

    private List<ForensicEvidence> unlockedEvidences = new();

    public List<ForensicEvidence> collectedEvidence => unlockedEvidences;

    public bool forensicMenuUnlocked { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockForensicMenu()
    {
        forensicMenuUnlocked = true;
    }

    public void AddEvidence(ForensicEvidence evidence)
    {
        if (evidence == null)
            return;

        if (IsEvidenceUnlocked(evidence.evidenceID))
            return;

        unlockedEvidences.Add(evidence);

        Debug.Log($"[Forensic] Bukti baru didapat: {evidence.evidenceName}");
    }

    public bool IsEvidenceUnlocked(string evidenceID)
    {
        if (string.IsNullOrEmpty(evidenceID))
            return false;

        foreach (ForensicEvidence evidence in unlockedEvidences)
        {
            if (evidence != null && evidence.evidenceID == evidenceID)
                return true;
        }

        return false;
    }

    public ForensicEvidence GetEvidence(string evidenceID)
    {
        foreach (ForensicEvidence evidence in unlockedEvidences)
        {
            if (evidence != null && evidence.evidenceID == evidenceID)
                return evidence;
        }

        return null;
    }
}