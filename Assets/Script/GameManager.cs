using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<EvidenceData> inventory = new List<EvidenceData>();
    public List<string> completedEvidence = new List<string>();

    public bool hasKey;
    public bool hasSeenIntro;

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

    public void CompleteEvidence(string id)
    {
        if (!string.IsNullOrEmpty(id) && !completedEvidence.Contains(id))
        {
            completedEvidence.Add(id);
        }
    }

    public bool IsEvidenceCompleted(string id)
    {
        return completedEvidence.Contains(id);
    }
}