using System;
using System.Collections.Generic;
using UnityEngine;

public enum ScoreCategory
{
    TKP1_Kamar,
    TKP1_Meja,
    TKP1_Lemari,
    TKP1_PintuBelakang,
    TKP1_HalamanBelakang,
    ForensicDialogue,
    Testimony,
    SuspectFound,
    TKP2_Kantor,
    TKP2_Meja,
    TKP2_MejaCloseup,
    TKP2_Map,
    JodiChoice,
    JodiFullBody
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; }
    public int DisplayedScore { get; private set; }
    public int MaxScore = 4600;

    public event Action<int, ScoreCategory, string> OnScoreAdded;

    private readonly Dictionary<ScoreCategory, int> earnedByCategory = new Dictionary<ScoreCategory, int>();
    private readonly HashSet<string> claimedKeys = new HashSet<string>();

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

    public bool AddScore(int amount, ScoreCategory category, string uniqueKey)
    {
        if (claimedKeys.Contains(uniqueKey)) return false;
        claimedKeys.Add(uniqueKey);

        CurrentScore += amount;

        if (!earnedByCategory.ContainsKey(category))
            earnedByCategory[category] = 0;
        earnedByCategory[category] += amount;

        OnScoreAdded?.Invoke(amount, category, uniqueKey);
        return true;
    }

    public bool HasClaimed(string uniqueKey)
    {
        return claimedKeys.Contains(uniqueKey);
    }

    public int GetCategoryScore(ScoreCategory category)
    {
        return earnedByCategory.TryGetValue(category, out int val) ? val : 0;
    }

    public int GetPendingDelta()
    {
        return CurrentScore - DisplayedScore;
    }

    public void ConsumeDisplayed(int amount)
    {
        DisplayedScore = Mathf.Min(DisplayedScore + amount, CurrentScore);
    }

    public void SyncDisplayedToCurrent()
    {
        DisplayedScore = CurrentScore;
    }

    public int GetTkp1Score()
    {
        return GetCategoryScore(ScoreCategory.TKP1_Kamar)
             + GetCategoryScore(ScoreCategory.TKP1_Meja)
             + GetCategoryScore(ScoreCategory.TKP1_Lemari)
             + GetCategoryScore(ScoreCategory.TKP1_PintuBelakang)
             + GetCategoryScore(ScoreCategory.TKP1_HalamanBelakang);
    }

    public int GetTkp2Score()
    {
        return GetCategoryScore(ScoreCategory.TKP2_Kantor)
             + GetCategoryScore(ScoreCategory.TKP2_Meja)
             + GetCategoryScore(ScoreCategory.TKP2_MejaCloseup)
             + GetCategoryScore(ScoreCategory.TKP2_Map);
    }

    public int GetStarTier()
    {
        if (CurrentScore >= MaxScore) return 3;
        if (GetTkp1Score() <= 300) return 1;
        return 2;
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        DisplayedScore = 0;
        earnedByCategory.Clear();
        claimedKeys.Clear();
    }
}