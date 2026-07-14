using UnityEngine;
using TMPro;

public class WellDonePanelController : MonoBehaviour
{
    public GameObject wellDonePanel;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public GameObject[] starIcons;

    public TransisiLokal transisi;
    public string firstSceneName = "PartnerDialogue";
    public string mainMenuSceneName = "MainMenu";

    void Start()
    {
        if (ArrestManager.Instance != null && ArrestManager.Instance.pendingWellDone)
        {
            ArrestManager.Instance.pendingWellDone = false;
            ShowWellDone();
        }
    }

    void ShowWellDone()
    {
        int score = ScoreManager.Instance.CurrentScore;
        int highscore = HighscoreManager.Instance.SaveIfHigher(score);
        int starTier = ScoreManager.Instance.GetStarTier();

        if (scoreText != null) scoreText.text = score.ToString();
        if (highscoreText != null) highscoreText.text = highscore.ToString();

        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].SetActive(i < starTier);
        }

        wellDonePanel.SetActive(true);
    }

    void ResetAllProgress()
    {
        GameManager.Instance?.ResetState();
        SuspectManager.Instance?.ResetState();
        ArrestManager.Instance?.ResetState();
        ScoreManager.Instance?.ResetScore();
        TKPTimerManager.Instance?.StopTimer();
    }

    public void RestartGame()
    {
        ResetAllProgress();
        transisi?.PindahScene(firstSceneName);
    }

    public void QuitToMainMenu()
    {
        ResetAllProgress();
        transisi?.PindahScene(mainMenuSceneName);
    }
}