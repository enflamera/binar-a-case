using UnityEngine;

public class EvidenceTrigger : MonoBehaviour
{
    public string title;
    [TextArea]
    public string content;

    public void Interact()
    {
        Debug.Log("INTERACT HIT: " + title);
        if (NoteManager.Instance == null)
            return;

        NoteManager.Instance.AddNote(title, content);
    }
}