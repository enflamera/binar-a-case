using System;
using UnityEngine;

[Serializable]
public class InterrogationEvidenceData : DialogueData
{
    public Sprite popupSprite;
    public string evidenceTitle;

    public InterrogationEvidenceData(
        bool isDani,
        string text,
        Sprite popupSprite,
        string evidenceTitle,
        bool isWitness = false)
        : base(isDani, text, isWitness)
    {
        this.popupSprite = popupSprite;
        this.evidenceTitle = evidenceTitle;
    }
}