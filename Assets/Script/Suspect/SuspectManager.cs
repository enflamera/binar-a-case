using System;
using System.Collections.Generic;
using UnityEngine;

public class SuspectManager : MonoBehaviour
{
    public static SuspectManager Instance;

    public List<SuspectData> suspects = new();
    public int pendingPopupSuspect = -1;

    public bool hasUnseenSuspect;

    public Action OnSuspectsChanged;

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

    public SuspectData GetSuspect(int index)
    {
        if (index < 0 || index >= suspects.Count)
            return null;

        return suspects[index];
    }

    public void UnlockSuspect(int index, bool showPopup = true)
    {
        if (index < 0 || index >= suspects.Count) return;
        if (suspects[index].unlocked) return;

        suspects[index].unlocked = true;

        ScoreManager.Instance?.AddScore(
            100,
            ScoreCategory.SuspectFound,
            $"Suspect_Unlock_{index}"
        );

        Debug.Log($"Unlock suspect: {index}");

        if (showPopup)
        {
            pendingPopupSuspect = index;
        }

        hasUnseenSuspect = true;
        OnSuspectsChanged?.Invoke();

        if (SuspectUIController.Instance != null)
        {
            SuspectUIController.Instance.OpenFirstUnlocked();
        }
    }

    public bool IsUnlocked(int index)
    {
        if (index < 0 || index >= suspects.Count)
            return false;

        return suspects[index].unlocked;
    }

    public void InterrogateSuspect(int index)
    {
        if (index < 0 || index >= suspects.Count) return;
        if (suspects[index].interrogated) return;

        suspects[index].interrogated = true;
        suspects[index].canInterrogate = false;
    }

    public bool IsInterrogated(int index)
    {
        if (index < 0 || index >= suspects.Count)
            return false;

        return suspects[index].interrogated;
    }

    public void ResetState()
    {
        foreach (SuspectData suspect in suspects)
        {
            suspect.unlocked = false;
            suspect.interrogated = false;
            suspect.canInterrogate = false;
        }
        pendingPopupSuspect = -1;
        hasUnseenSuspect = false;
    }
}