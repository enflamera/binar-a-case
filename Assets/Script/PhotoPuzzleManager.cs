using System;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPuzzleManager : MonoBehaviour
{
    public static PhotoPuzzleManager Instance;

    [Serializable]
    public class NeighborPair
    {
        public PuzzlePiece pieceA;
        public PuzzlePiece pieceB;
    }

    [Header("Puzzle")]
    [Tooltip("Daftar pasangan piece yang SEHARUSNYA bertetangga (nempel) di foto asli")]
    public NeighborPair[] correctPairs;

    [Header("Complete")]
    public Sprite analyzedIcon;

    [Header("Note")]
    public string noteTitle;
    [TextArea]
    public string noteContent;

    private readonly HashSet<(PuzzlePiece, PuzzlePiece)> connected = new();
    private bool isCompleted = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        connected.Clear();
        isCompleted = false;
    }

    private (PuzzlePiece, PuzzlePiece) MakeKey(PuzzlePiece a, PuzzlePiece b)
    {
        return a.GetInstanceID() < b.GetInstanceID() ? (a, b) : (b, a);
    }

    private bool IsExpectedPair(PuzzlePiece a, PuzzlePiece b)
    {
        foreach (var pair in correctPairs)
        {
            if ((pair.pieceA == a && pair.pieceB == b) ||
                (pair.pieceA == b && pair.pieceB == a))
                return true;
        }
        return false;
    }

    public void NotifyTouch(PuzzlePiece a, PuzzlePiece b, bool isTouching)
    {
        if (isCompleted) return;
        if (!IsExpectedPair(a, b)) return;

        var key = MakeKey(a, b);

        if (isTouching)
        {
            if (connected.Add(key))
                Debug.Log($"Connected: {a.name} - {b.name} ({connected.Count}/{correctPairs.Length})");
        }
        else
        {
            if (connected.Remove(key))
                Debug.Log($"Terlepas: {a.name} - {b.name} ({connected.Count}/{correctPairs.Length})");
        }

        if (connected.Count >= correctPairs.Length)
            CompletePuzzle();
    }

    private void CompletePuzzle()
    {
        if (isCompleted) return;
        isCompleted = true;

        Debug.Log("COMPLETE PUZZLE");

        EvidenceData evidence = AnalyzeManager.Instance.GetCurrentEvidence();
        evidence.isAnalyzed = true;

        if (analyzedIcon != null)
            evidence.icon = analyzedIcon;

        NoteManager.Instance?.AddNote(noteTitle, noteContent);
        FindFirstObjectByType<InventoryManager>()?.RefreshUI();

        ScoreManager.Instance?.AddScore(
            50,
            ScoreCategory.TKP2_MejaCloseup,
            "TKP2_Foto_Analyze"
        );

        AnalyzeManager.Instance.Close();
    }
}