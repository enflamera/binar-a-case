using UnityEngine;
using UnityEngine.UI;

public class ForensicUI : MonoBehaviour
{
    public Transform itemSlotForensic;
    public GameObject slotPrefab;

    public Image tvImage;

    void Start()
    {
        LoadEvidence();
    }

    public void LoadEvidence()
    {
        foreach (Transform child in itemSlotForensic)
        {
            Destroy(child.gameObject);
        }

        foreach (var evidence in ForensicManager.Instance.collectedEvidence)
        {
            GameObject slotObj =
                Instantiate(
                    slotPrefab,
                    itemSlotForensic);

            ForensicSlot slot =
                slotObj.GetComponent<ForensicSlot>();

            slot.Setup(
                evidence,
                this);
        }

        if (ForensicManager.Instance.collectedEvidence.Count > 0)
        {
            ShowEvidence(
                ForensicManager.Instance.collectedEvidence[0]
            );
        }
    }

    public void ShowEvidence(
        ForensicEvidence evidence)
    {
        if (evidence == null)
            return;

        tvImage.sprite = evidence.tvSprite;
    }
}