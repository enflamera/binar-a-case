using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteUIController : MonoBehaviour
{
    public Transform contentParent;
    public GameObject notePrefab;
    public TextMeshProUGUI pageText;
    public Button nextButton;
    public Button prevButton;

    int currentPage;
    int notesPerPage = 4;

    void Start()
    {
        if (NoteManager.Instance != null)
            NoteManager.Instance.OnNotesChanged += RenderPage;

        RenderPage();
    }

    void OnDestroy()
    {
        if (NoteManager.Instance != null)
            NoteManager.Instance.OnNotesChanged -= RenderPage;
    }

    public void Next()
    {
        if (currentPage < GetMaxPage() - 1)
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

    int GetMaxPage()
    {
        if (NoteManager.Instance == null) return 0;
        return Mathf.CeilToInt((float)NoteManager.Instance.notes.Count / notesPerPage);
    }

    public void RenderPage()
    {
        if (NoteManager.Instance == null) return;

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        var notes = NoteManager.Instance.notes;

        if (notes.Count == 0)
        {
            pageText.text = "0/0";
            nextButton.interactable = false;
            prevButton.interactable = false;
            return;
        }

        int startIndex = currentPage * notesPerPage;

        for (int i = 0; i < notesPerPage; i++)
        {
            int index = startIndex + i;

            if (index >= notes.Count) break;

            var note = notes[index];

            GameObject obj = Instantiate(notePrefab, contentParent);

            NoteItemUI ui = obj.GetComponentInChildren<NoteItemUI>();

            if (ui == null) return;

            ui.Set(note.title, note.content);
        }

        pageText.text = $"{currentPage + 1}/{GetMaxPage()}";

        prevButton.interactable = currentPage > 0;
        nextButton.interactable = currentPage < GetMaxPage() - 1;
    }
}