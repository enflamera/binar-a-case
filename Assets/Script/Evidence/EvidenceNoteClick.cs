using UnityEngine;
using UnityEngine.EventSystems;

public class EvidenceNoteClick : MonoBehaviour, IPointerClickHandler
{
    public string evidenceID;
    public EvidenceUI evidenceUI;
    public EvidenceTrigger evidenceTrigger;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.IsEvidenceCompleted(evidenceID))
            return;

        GameManager.Instance.CompleteEvidence(evidenceID);

        if (evidenceUI != null)
            evidenceUI.Dim();

        if (evidenceTrigger != null)
            evidenceTrigger.Interact();
    }
}