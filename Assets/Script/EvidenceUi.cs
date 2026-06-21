using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvidenceUI : MonoBehaviour
{
    public string evidenceID;

    private Image img;
    private TMP_Text txt;

    void Awake()
    {
        img = GetComponent<Image>();
        txt = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.IsEvidenceCompleted(evidenceID))
        {
            Dim();
        }
    }

    public void Dim()
    {
        Color bg = img.color;
        bg.a = 0.35f;
        img.color = bg;

        if (txt != null)
        {
            Color tc = txt.color;
            tc.a = 0.35f;
            txt.color = tc;
        }
    }
}