using UnityEngine;
using UnityEngine.UI;

public class ForensicSlot : MonoBehaviour
{
    public Image icon;

    private ForensicEvidence evidence;
    private ForensicUI forensicUI;

    public void Setup(
        ForensicEvidence newEvidence,
        ForensicUI ui)
    {
        evidence = newEvidence;
        forensicUI = ui;

        icon.sprite = evidence.slotSprite;

        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        forensicUI.ShowEvidence(evidence);
    }
}