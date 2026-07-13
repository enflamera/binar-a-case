using UnityEngine;

public class AnalyzeManager : MonoBehaviour
{
    public static AnalyzeManager Instance;

    public GameObject buktiPanel;
    public GameObject analyzePanel;

    public GameObject photoPuzzlePanel;
    public GameObject flashlightPanel;

    private EvidenceData currentEvidence;

    private void Awake()
    {
        Instance = this;
    }

    public void Open(EvidenceData evidence)
    {
        currentEvidence = evidence;

        buktiPanel.SetActive(false);
        analyzePanel.SetActive(true);

        photoPuzzlePanel.SetActive(false);
        flashlightPanel.SetActive(false);

        if (evidence.evidenceName == "Foto Sobek")
        {
            photoPuzzlePanel.SetActive(true);
        }
        else if (evidence.evidenceName == "Senter Jodi")
        {
            flashlightPanel.SetActive(true);
        }
    }

    public void Close()
    {
        analyzePanel.SetActive(false);
        photoPuzzlePanel.SetActive(false);
        flashlightPanel.SetActive(false);

        buktiPanel.SetActive(true);
    }

    public void CompleteAnalyze()
    {
        if (currentEvidence == null)
        {
            Close();
            return;
        }

        if (currentEvidence.evidenceName == "Foto Sobek")
        {
            ScoreManager.Instance?.AddScore(
                50,
                ScoreCategory.TKP2_MejaCloseup,
                "TKP2_Foto_Analyze"
            );
        }
        else if (currentEvidence.evidenceName == "Senter Jodi")
        {
            ScoreManager.Instance?.AddScore(
                50,
                ScoreCategory.JodiFullBody,
                "Jodi_Senter_Analyze"
            );
        }

        Close();
    }

    public EvidenceData GetCurrentEvidence()
    {
        return currentEvidence;
    }
}