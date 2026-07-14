using UnityEngine;

[CreateAssetMenu(fileName = "New Evidence", menuName = "Evidence/Item")]
public class EvidenceData : ScriptableObject
{
    public string evidenceName;
    public Sprite icon;
    public Sprite analyzedIcon;
    public bool isExamined;
    public bool isCollected;
    public bool canAnalyze;
    public bool isAnalyzed;
    [TextArea] public string description;
}