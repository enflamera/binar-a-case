using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvidencePopup : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Image evidenceImage;
    [SerializeField] private TMP_Text titleText;

    private void Start()
    {
        Hide();
    }

    public void Show(Sprite sprite, string title)
    {
        if (panel == null)
            return;

        panel.SetActive(true);

        if (evidenceImage != null)
            evidenceImage.sprite = sprite;

        if (titleText != null)
            titleText.text = title;
    }

    public void Hide()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}