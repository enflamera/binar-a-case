using UnityEngine;
using UnityEngine.EventSystems;

public class OpenPanel : MonoBehaviour, IPointerClickHandler
{
    public GameObject panelToOpen;
    public EvidenceUI evidenceUI;
    public string evidenceID;

    public void OnPointerClick(PointerEventData eventData)
    {
        panelToOpen.SetActive(true);

        GameManager.Instance.CompleteEvidence(evidenceID);

        if (evidenceUI != null)
            evidenceUI.Dim();
    }
}