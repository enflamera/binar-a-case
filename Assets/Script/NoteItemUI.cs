using UnityEngine;
using TMPro;

public class NoteItemUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;

    public void Set(string title, string content)
    {
        titleText.text = title;
        contentText.text = content;
    }
}