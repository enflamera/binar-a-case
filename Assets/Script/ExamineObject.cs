using UnityEngine;
using UnityEngine.EventSystems;

public class ExamineObject : MonoBehaviour, IPointerClickHandler
{
    public string sceneName;
    public GameObject panelToOpen;
    public ExamineManager manager;
    public EvidenceUI evidenceUI;
    public string evidenceID;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemPickup pickup = GetComponent<ItemPickup>();

        manager.ShowButton(
            transform.position + new Vector3(0, 100, 0),
            sceneName,
            transform,
            panelToOpen,
            pickup
        );

        if (pickup == null)
        {
            GameManager.Instance.CompleteEvidence(evidenceID);

            if (evidenceUI != null)
                evidenceUI.Dim();
        }
    }

    public void SetSelected()
    {
        transform.localScale = originalScale * 1.03f;
    }

    public void SetNormal()
    {
        transform.localScale = originalScale;
    }
}