using System.Collections;
using UnityEngine;

public class FlashlightAnalyzeManager : MonoBehaviour
{
    public GameObject flashlightClosed;
    public GameObject flashlightOpen;

    public GameObject searchUI;
    public GameObject openButton;
    public GameObject resultPanel;

    public string noteTitle;

    [TextArea]
    public string noteContent;

    public void ShowOpenButton()
    {
        openButton.SetActive(true);
    }

    public void OpenBattery()
    {
        searchUI.SetActive(false);
        openButton.SetActive(false);

        flashlightClosed.SetActive(false);
        flashlightOpen.SetActive(true);

        StartCoroutine(ShowResult());
    }

    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1f);

        flashlightOpen.SetActive(false);
        resultPanel.SetActive(true);
    }

    public void FinishAnalyze()
    {
        EvidenceData evidence = AnalyzeManager.Instance.GetCurrentEvidence();

        evidence.isAnalyzed = true;

        if (evidence.analyzedIcon != null)
            evidence.icon = evidence.analyzedIcon;

        NoteManager.Instance.AddNote(noteTitle, noteContent);

        FindFirstObjectByType<InventoryManager>()?.RefreshUI();

        ScoreManager.Instance?.AddScore(
            50,
            ScoreCategory.JodiFullBody,
            "Jodi_Senter_Analyze"
        );

        resultPanel.SetActive(false);

        AnalyzeManager.Instance.Close();
    }
}