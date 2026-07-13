using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance;

    private const string HighscoreKey = "Highscore";

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

    public int SaveIfHigher(int score)
    {
        int current = PlayerPrefs.GetInt(HighscoreKey, 0);

        if (score > current)
        {
            PlayerPrefs.SetInt(HighscoreKey, score);
            PlayerPrefs.Save();
            return score;
        }

        return current;
    }

    public int GetHighscore()
    {
        return PlayerPrefs.GetInt(HighscoreKey, 0);
    }
}