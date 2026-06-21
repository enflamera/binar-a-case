using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string evidenceID;
    public EvidenceData itemData;
    public GameObject examineUI;
    public EvidenceUI evidenceUI;
    public bool canBeCollected = true;
    public bool requireExamine = true;

    void Start()
    {
        if (itemData != null && itemData.isCollected)
        {
            gameObject.SetActive(false);
        }

        if (GameManager.Instance != null &&
            GameManager.Instance.IsEvidenceCompleted(evidenceID))
        {
            if (evidenceUI != null)
                evidenceUI.Dim();
        }
    }

    public void PerformExamineAction()
    {
        gameObject.SetActive(false);

        if (itemData != null)
        {
            itemData.isExamined = true;

            if (canBeCollected)
            {
                itemData.isCollected = true;

                if (!GameManager.Instance.inventory.Contains(itemData))
                {
                    GameManager.Instance.inventory.Add(itemData);
                }
            }
        }

        GameManager.Instance.CompleteEvidence(evidenceID);

        if (evidenceUI != null)
            evidenceUI.Dim();

        if (examineUI != null)
            examineUI.SetActive(true);
    }

    public void InstantCollect()
    {
        gameObject.SetActive(false);

        if (itemData != null)
        {
            itemData.isCollected = true;

            if (canBeCollected &&
                !GameManager.Instance.inventory.Contains(itemData))
            {
                GameManager.Instance.inventory.Add(itemData);
            }
        }

        GameManager.Instance.CompleteEvidence(evidenceID);

        if (evidenceUI != null)
            evidenceUI.Dim();
    }
}