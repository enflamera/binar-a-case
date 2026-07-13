using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WellDonePanelController : MonoBehaviour
{
    public GameObject wellDonePanel;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public GameObject[] starIcons;

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

    public void RestartGame()
    {
        ScoreManager.Instance.ResetScore();
        SuspectManager.Instance.ResetState();

        SceneManager.LoadScene(firstSceneName);
    }

    public void QuitToMainMenu()
    {
        ScoreManager.Instance.ResetScore();
        SuspectManager.Instance.ResetState();

        SceneManager.LoadScene(mainMenuSceneName);
    }
}