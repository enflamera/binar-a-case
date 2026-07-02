using System;
using UnityEngine;

[Serializable]
public class DialogueData
{
    public bool isDani;
    public string text;
    public bool isWitness;

    public DialogueData(bool isDani, string text, bool isWitness = false)
    {
        this.isDani = isDani;
        this.text = text;
        this.isWitness = isWitness;
    }
}

[Serializable]
public class EvidenceDialogueData : DialogueData
{
    public Sprite popupSprite;
    public Sprite tvSprite;
    public string evidenceTitle;
    public string evidenceID;

    public EvidenceDialogueData(
        bool isDani,
        string text,
        Sprite popupSprite,
        Sprite tvSprite,
        string evidenceTitle,
        string evidenceID,
        bool isWitness = false)
        : base(isDani, text, isWitness)
    {
        this.popupSprite = popupSprite;
        this.tvSprite = tvSprite;
        this.evidenceTitle = evidenceTitle;
        this.evidenceID = evidenceID;
    }
}