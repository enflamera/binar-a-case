using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;

    public List<NoteData> notes = new List<NoteData>();

    public System.Action OnNotesChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddNote(string title, string content)
    {
        notes.Add(new NoteData(title, content));
        NoteToastUI.Instance?.Show();
        OnNotesChanged?.Invoke();
    }

    public int GetTotalNotes()
    {
        return notes.Count;
    }
}