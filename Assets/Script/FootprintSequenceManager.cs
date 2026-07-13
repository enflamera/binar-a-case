using UnityEngine;
using UnityEngine.UI;

public class FootprintSequenceManager : MonoBehaviour
{
    public GameObject searchUIRoot;
    public Button lihatButton;
    public FootprintDetector detector;
    public ToastUI daniToast;

    [Header("Note")]
    public string title;
    [TextArea]
    public string content;

    const string DaniFootprintLine = "Sepertinya jejak di halaman belakang itu jejak sepatu seragam polisi";

    void Awake()
    {
        lihatButton.onClick.RemoveListener(OnLihatClicked);
        lihatButton.onClick.AddListener(OnLihatClicked);
    }

    void Start()
    {
        lihatButton.gameObject.SetActive(false);
    }

    void OnLihatClicked()
    {
        lihatButton.gameObject.SetActive(false);
        searchUIRoot.SetActive(false);
        detector.MarkCollected();

        if (NoteManager.Instance != null)
            NoteManager.Instance.AddNote(title, content);

        daniToast.Show(DaniFootprintLine, false, true, null, OnFinished);
    }

    void OnFinished()
    {
        Debug.Log("[Footprint] OnFinished dipanggil");
        searchUIRoot.SetActive(true);

        ScoreManager.Instance?.AddScore(
            100,
            ScoreCategory.JodiFullBody,
            "Jodi_Search_Sepatu"
        );

        EvidenceProgressTracker.Instance?.ReportFound();
    }
}