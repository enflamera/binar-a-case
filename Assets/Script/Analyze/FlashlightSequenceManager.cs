using UnityEngine;
using UnityEngine.UI;

public class FlashlightSequenceManager : MonoBehaviour
{
    public GameObject searchUIRoot;
    public Button lihatButton;
    public FlashlightDetector detector;
    public ToastUI daniToast;
    public ToastUI jodiToast;
    public Image jodiImage;
    public Sprite jodiPokerNoFlashlightSprite;

    [Header("Note")]
    public string title;
    [TextArea]
    public string content;

    [Header("Evidence")]
    public EvidenceData itemData;

    const string DaniAskLine = "Pak Jodi, saya boleh minjem senternya?";
    const string JodiAllowLine = "Boleh pak silahkan";
    const string DaniThanksLine = "Terima kasih pak";

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

        daniToast.Show(DaniAskLine, false, true, null, ShowJodiAllow);
    }

    void ShowJodiAllow()
    {
        jodiToast.Show(JodiAllowLine, false, false, null, ShowDaniThanks);
    }

    void ShowDaniThanks()
    {
        daniToast.Show(DaniThanksLine, true, true, OnAmbilClicked, OnSequenceFinished);
    }

    public void OnAmbilClicked()
    {
        Debug.Log("[Flashlight] OnAmbilClicked dipanggil");
        jodiImage.sprite = jodiPokerNoFlashlightSprite;

        if (itemData != null)
        {
            itemData.isCollected = true;

            if (GameManager.Instance != null &&
                !GameManager.Instance.inventory.Contains(itemData))
            {
                GameManager.Instance.inventory.Add(itemData);
            }
        }

        ScoreManager.Instance?.AddScore(
            50,
            ScoreCategory.JodiFullBody,
            "Jodi_Search_Senter"
        );

        EvidenceProgressTracker.Instance?.ReportFound();
    }

    void OnSequenceFinished()
    {
        searchUIRoot.SetActive(true);
    }
}