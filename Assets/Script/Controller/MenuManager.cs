using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuTab : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public GameObject forensicPanel;
    public GameObject buktiPanel;
    public GameObject suspectPanel;
    public GameObject notulenPanel;

    public GameObject forensicActive;
    public GameObject buktiActive;
    public GameObject suspectActive;
    public GameObject notulenActive;

    [Header("Forensic Lock")]
    public GameObject forensicLockIcon;
    public Image forensicButtonBG;
    public TMP_Text forensicButtonText;

    [Header("Notification Badges")]
    public GameObject buktiBadge;
    public GameObject suspectBadge;
    public GameObject notulenBadge;

    void Start()
    {
        UpdateForensicLock();
        UpdateAllBadges();

        if (ForensicManager.Instance != null &&
            ForensicManager.Instance.forensicMenuUnlocked)
        {
            ShowForensic();
        }
        else
        {
            ShowBukti();
        }
    }

    void OnEnable()
    {
        UpdateForensicLock();
        UpdateAllBadges();

        if (GameManager.Instance != null)
            GameManager.Instance.OnInventoryChanged += UpdateBuktiBadge;

        if (SuspectManager.Instance != null)
            SuspectManager.Instance.OnSuspectsChanged += UpdateSuspectBadge;

        if (NoteManager.Instance != null)
            NoteManager.Instance.OnNotesChanged += UpdateNotulenBadge;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnInventoryChanged -= UpdateBuktiBadge;

        if (SuspectManager.Instance != null)
            SuspectManager.Instance.OnSuspectsChanged -= UpdateSuspectBadge;

        if (NoteManager.Instance != null)
            NoteManager.Instance.OnNotesChanged -= UpdateNotulenBadge;
    }

    void UpdateAllBadges()
    {
        UpdateBuktiBadge();
        UpdateSuspectBadge();
        UpdateNotulenBadge();
    }

    void UpdateBuktiBadge()
    {
        if (buktiBadge != null)
            buktiBadge.SetActive(GameManager.Instance != null && GameManager.Instance.hasUnseenEvidence);
    }

    void UpdateSuspectBadge()
    {
        if (suspectBadge != null)
            suspectBadge.SetActive(SuspectManager.Instance != null && SuspectManager.Instance.hasUnseenSuspect);
    }

    void UpdateNotulenBadge()
    {
        if (notulenBadge != null)
            notulenBadge.SetActive(NoteManager.Instance != null && NoteManager.Instance.hasUnseenNotes);
    }

    public void UpdateForensicLock()
    {
        bool unlocked =
            ForensicManager.Instance != null &&
            ForensicManager.Instance.forensicMenuUnlocked;

        if (forensicLockIcon != null)
            forensicLockIcon.SetActive(!unlocked);

        if (unlocked)
            UndimForensicButton();
        else
            DimForensicButton();
    }

    void DimForensicButton()
    {
        if (forensicButtonBG != null)
        {
            Color bg = forensicButtonBG.color;
            bg.a = 0.35f;
            forensicButtonBG.color = bg;
        }

        if (forensicButtonText != null)
        {
            Color tc = forensicButtonText.color;
            tc.a = 0.35f;
            forensicButtonText.color = tc;
        }
    }

    void UndimForensicButton()
    {
        if (forensicButtonBG != null)
        {
            Color bg = forensicButtonBG.color;
            bg.a = 1f;
            forensicButtonBG.color = bg;
        }

        if (forensicButtonText != null)
        {
            Color tc = forensicButtonText.color;
            tc.a = 1f;
            forensicButtonText.color = tc;
        }
    }

    public void ShowForensic()
    {
        if (ForensicManager.Instance == null ||
            !ForensicManager.Instance.forensicMenuUnlocked)
            return;

        forensicPanel.SetActive(true);
        buktiPanel.SetActive(false);
        suspectPanel.SetActive(false);
        notulenPanel.SetActive(false);

        forensicActive.SetActive(true);
        buktiActive.SetActive(false);
        suspectActive.SetActive(false);
        notulenActive.SetActive(false);
    }

    public void ShowBukti()
    {
        forensicPanel.SetActive(false);
        buktiPanel.SetActive(true);
        suspectPanel.SetActive(false);
        notulenPanel.SetActive(false);

        forensicActive.SetActive(false);
        buktiActive.SetActive(true);
        suspectActive.SetActive(false);
        notulenActive.SetActive(false);

        if (inventoryManager != null)
        {
            inventoryManager.RefreshUI();
        }

        if (GameManager.Instance != null)
            GameManager.Instance.hasUnseenEvidence = false;

        UpdateBuktiBadge();
    }

    public void ShowSuspect()
    {
        forensicPanel.SetActive(false);
        buktiPanel.SetActive(false);
        suspectPanel.SetActive(true);
        notulenPanel.SetActive(false);

        forensicActive.SetActive(false);
        buktiActive.SetActive(false);
        suspectActive.SetActive(true);
        notulenActive.SetActive(false);

        if (SuspectUIController.Instance != null)
        {
            SuspectUIController.Instance.OpenFirstUnlocked();
        }

        if (SuspectManager.Instance != null)
            SuspectManager.Instance.hasUnseenSuspect = false;

        UpdateSuspectBadge();
    }

    public void ShowNotulen()
    {
        forensicPanel.SetActive(false);
        buktiPanel.SetActive(false);
        suspectPanel.SetActive(false);
        notulenPanel.SetActive(true);

        forensicActive.SetActive(false);
        buktiActive.SetActive(false);
        suspectActive.SetActive(false);
        notulenActive.SetActive(true);

        if (NoteManager.Instance != null)
            NoteManager.Instance.hasUnseenNotes = false;

        UpdateNotulenBadge();
    }
}