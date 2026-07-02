using System.Collections.Generic;
using UnityEngine;

public class SuspectManager : MonoBehaviour
{
    public static SuspectManager Instance;

    public List<SuspectData> suspects = new();
    public int pendingPopupSuspect = -1;

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

    public void UnlockSuspect(int index, bool showPopup = true)
    {
        if (index < 0 || index >= suspects.Count) return;
        if (suspects[index].unlocked) return;

        suspects[index].unlocked = true;

        Debug.Log($"Unlock suspect: {index}");

        if (showPopup)
        {
            pendingPopupSuspect = index;
        }

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
}