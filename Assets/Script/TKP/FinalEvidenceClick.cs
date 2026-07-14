using UnityEngine;
using UnityEngine.EventSystems;

public class FinalEvidenceClick : MonoBehaviour, IPointerClickHandler
{
    public string evidenceID;
    public EvidenceUI evidenceUI;
    public EvidenceTrigger evidenceTrigger;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.CompleteEvidence(evidenceID);

        if (evidenceUI != null)
            evidenceUI.Dim();

        if (evidenceTrigger != null)
            evidenceTrigger.Interact();

        gameObject.SetActive(false);

        FinalEvidenceManager.Instance.CheckCompletion();
    }
}