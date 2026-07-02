using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NoteUIController : MonoBehaviour
{
    [Header("Parents")]
    public Transform notesParent;
    public Transform testimonyParent;

    [Header("Prefabs")]
    public GameObject notePrefab;
    public GameObject testimonyPrefab;

    [Header("UI Kontrol Halaman (TMPRO)")]
    public TextMeshProUGUI pageText;
    public Button nextButton;
    public Button prevButton;

    public enum NoteTab
    {
        Investigation,
        Testimony
    }

    public NoteTab currentTab = NoteTab.Investigation;

    private int currentPage;
    private int notesPerPage = 4;
    private int testimonyPerPage = 1;

    int GetItemsPerPage()
    {
        return currentTab == NoteTab.Investigation ? notesPerPage : testimonyPerPage;
    }

    IEnumerator Start()
    {
        yield return null;

        while (NoteManager.Instance == null)
            yield return null;

        NoteManager.Instance.OnNotesChanged += HandleNotesChanged;

        currentTab = NoteTab.Investigation;
        currentPage = 0;

        RenderPage();
    }

    void OnDestroy()
    {
        if (NoteManager.Instance != null)
            NoteManager.Instance.OnNotesChanged -= HandleNotesChanged;
    }

    void JumpToLastPage()
    {
        int totalPages = GetTotalPageCount();
        currentPage = Mathf.Max(0, totalPages - 1);
    }

    void HandleNotesChanged()
    {
        JumpToLastPage();
        RenderPage();
    }

    public void OpenInvestigationTab()
    {
        currentTab = NoteTab.Investigation;
        currentPage = 0;
        RenderPage();
    }

    public void OpenTestimonyTab()
    {
        currentTab = NoteTab.Testimony;
        JumpToLastPage();
        RenderPage();
    }

    int GetTotalPageCount()
    {
        if (NoteManager.Instance == null)
            return 1;

        int count = currentTab == NoteTab.Investigation
            ? NoteManager.Instance.notes.Count
            : NoteManager.Instance.testimonyPages.Count;

        return Mathf.Max(1, Mathf.CeilToInt((float)count / GetItemsPerPage()));
    }

    public void Next()
    {
        int totalPages = GetTotalPageCount();

        if (currentPage < totalPages - 1)
        {
            currentPage++;
            RenderPage();
        }
    }

    public void Prev()
    {
        if (currentPage > 0)
        {
            currentPage--;
            RenderPage();
        }
    }

    void Clear(Transform parent)
    {
        if (parent == null) return;

        for (int i = parent.childCount - 1; i >= 0; i--)
            Destroy(parent.GetChild(i).gameObject);
    }

    void RenderNotes()
    {
        if (NoteManager.Instance == null) return;

        var notes = NoteManager.Instance.notes;
        int start = currentPage * notesPerPage;

        for (int i = 0; i < notesPerPage; i++)
        {
            int index = start + i;
            if (index >= notes.Count) break;

            GameObject obj = Instantiate(notePrefab, notesParent);
            NoteItemUI ui = obj.GetComponentInChildren<NoteItemUI>();

            if (ui != null)
                ui.Set(notes[index].title, notes[index].content);
        }
    }

    void RenderTestimony()
    {
        if (NoteManager.Instance == null) return;

        var testimonies = NoteManager.Instance.testimonyPages;
        int start = currentPage * testimonyPerPage;

        for (int i = 0; i < testimonyPerPage; i++)
        {
            int index = start + i;
            if (index >= testimonies.Count) break;

            GameObject obj = Instantiate(testimonyPrefab, testimonyParent);
            Image img = obj.GetComponent<Image>();

            if (img != null && testimonies[index] != null)
                img.sprite = testimonies[index];
        }
    }

    public void RenderPage()
    {
        if (NoteManager.Instance == null) return;
        if (notesParent == null || testimonyParent == null) return;

        Clear(notesParent);
        Clear(testimonyParent);

        if (currentTab == NoteTab.Investigation)
            RenderNotes();
        else
            RenderTestimony();

        int totalPages = GetTotalPageCount();

        if (pageText != null)
        {
            pageText.text = $"Halaman {currentPage + 1}/{totalPages}";
        }

        if (prevButton != null)
            prevButton.interactable = currentPage > 0;

        if (nextButton != null)
            nextButton.interactable = currentPage < totalPages - 1;
    }
}