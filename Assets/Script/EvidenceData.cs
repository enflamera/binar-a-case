using UnityEngine;

[CreateAssetMenu(fileName = "New Evidence", menuName = "Evidence/Item")]
public class EvidenceData : ScriptableObject
{
    public string evidenceName;
    public Sprite icon;
    public bool isExamined;
    public bool isCollected;
    [TextArea] public string description;
}