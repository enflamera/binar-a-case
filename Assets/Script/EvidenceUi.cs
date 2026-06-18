using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvidenceUI : MonoBehaviour
{
    private Image img;
    private TMP_Text txt;

    void Awake()
    {
        img = GetComponent<Image>();
        txt = GetComponentInChildren<TMP_Text>();
    }

    public void Dim()
    {
        Color bg = img.color;
        bg.a = 0.35f;
        img.color = bg;
        
        if (txt != null)
        {
            Color textColor = txt.color;
            textColor.a = 0.35f;
            txt.color = textColor;
        }
    }
}