using UnityEngine;

public class MenuTab : MonoBehaviour
{
    // Pastikan nama variabelnya sama
    public InventoryManager inventoryManager; 
    public GameObject forensicPanel;
    public GameObject buktiPanel;
    public GameObject suspectPanel;
    public GameObject notulenPanel;

    public GameObject forensicActive;
    public GameObject buktiActive;
    public GameObject suspectActive;
    public GameObject notulenActive;

    void Start()
    {
        ShowForensic();
    }

    public void ShowForensic()
    {
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

        // PERBAIKAN: Menggunakan inventoryManager, bukan inventoryController
        if (inventoryManager != null) 
        {
            inventoryManager.RefreshUI();
        }
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
    }
}