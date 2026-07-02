using UnityEngine;
using UnityEngine.EventSystems;

public class MapOpenTrigger : MonoBehaviour, IPointerClickHandler
{
    public MapEvidenceController controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.ShowOpenButton();
    }
}