using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class EvidenceScoreEntry
{
    public string evidenceID;
    public ScoreCategory category;
    public int points = 100;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<EvidenceData> inventory = new List<EvidenceData>();
    public List<string> completedEvidence = new List<string>();

    public bool hasKey;

    public HashSet<string> seenIntroIDs = new HashSet<string>();

    public bool tkp2Completed;
    public bool jodiInterrogationCompleted;

    public bool hasUnseenEvidence;

    public EvidenceScoreEntry[] scoreEntries;

    public event Action OnInventoryChanged;

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

    public void AddEvidenceToInventory(EvidenceData data)
    {
        if (data == null || inventory.Contains(data)) return;

        inventory.Add(data);
        hasUnseenEvidence = true;
        OnInventoryChanged?.Invoke();
    }

    public void CompleteEvidence(string id)
    {
        if (!string.IsNullOrEmpty(id) && !completedEvidence.Contains(id))
        {
            completedEvidence.Add(id);
            AwardEvidenceScore(id);
        }
    }

    private void AwardEvidenceScore(string evidenceID)
    {
        foreach (EvidenceScoreEntry entry in scoreEntries)
        {
            if (entry.evidenceID == evidenceID)
            {
                ScoreManager.Instance?.AddScore(entry.points, entry.category, $"Evidence_{evidenceID}");
                return;
            }
        }
    }

    public bool IsEvidenceCompleted(string id)
    {
        return completedEvidence.Contains(id);
    }

    public bool HasSeenIntro(string locationID)
    {
        return !string.IsNullOrEmpty(locationID) && seenIntroIDs.Contains(locationID);
    }

    public void MarkIntroSeen(string locationID)
    {
        if (!string.IsNullOrEmpty(locationID))
            seenIntroIDs.Add(locationID);
    }

    public void ResetState()
    {
        inventory.Clear();
        completedEvidence.Clear();
        hasKey = false;
        seenIntroIDs.Clear();
        tkp2Completed = false;
        jodiInterrogationCompleted = false;
        hasUnseenEvidence = false;
    }
}