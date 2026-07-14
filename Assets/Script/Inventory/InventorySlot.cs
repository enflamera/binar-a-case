using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public Button analyzeButton;

    private EvidenceData evidence;

    public void Setup(EvidenceData data)
    {
        evidence = data;
        Debug.Log("Setup Slot: " + data.evidenceName);

        if (icon != null)
            icon.sprite = data.icon;

        if (analyzeButton != null)
        {
            bool showAnalyze = data.canAnalyze && !data.isAnalyzed;
            analyzeButton.gameObject.SetActive(showAnalyze);

            analyzeButton.onClick.RemoveAllListeners();
            analyzeButton.onClick.AddListener(OnAnalyzeClicked);
        }
    }

    private void OnAnalyzeClicked()
    {
        AnalyzeManager.Instance.Open(evidence);
    }
}