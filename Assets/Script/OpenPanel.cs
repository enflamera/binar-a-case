using UnityEngine;
using UnityEngine.EventSystems;

public class OpenPanel : MonoBehaviour, IPointerClickHandler
{
    public GameObject panelToOpen;
    public EvidenceUI evidenceUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        panelToOpen.SetActive(true);

        if (evidenceUI != null)
            evidenceUI.Dim();
    }
}