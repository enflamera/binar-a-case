using System;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;

    public List<NoteData> notes = new();
    public List<Sprite> testimonyPages = new();

    public Action OnNotesChanged;

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
        foreach (var note in notes)
        {
            if (note.title == title && note.content == content)
                return;
        }

        notes.Add(new NoteData(title, content));
        NoteToastUI.Instance?.Show();
        NotifyChange();
    }

    public void AddTestimony(Sprite page)
    {
        if (page == null)
            return;

        if (testimonyPages.Contains(page))
            return;

        testimonyPages.Add(page);
        NoteToastUI.Instance?.Show();
        NotifyChange();
    }

    public void ClearNotes()
    {
        notes.Clear();
        NotifyChange();
    }

    public void ClearTestimonies()
    {
        testimonyPages.Clear();
        NotifyChange();
    }

    public void ClearAll()
    {
        notes.Clear();
        testimonyPages.Clear();
        NotifyChange();
    }

    void NotifyChange()
    {
        OnNotesChanged?.Invoke();
    }

    public int GetNoteCount()
    {
        return notes.Count;
    }

    public int GetTestimonyCount()
    {
        return testimonyPages.Count;
    }
}