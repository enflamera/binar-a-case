using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectClick : MonoBehaviour, IPointerClickHandler
{
    public ExamineManager manager;
    public ExamineObject data;
    public ItemPickup itemPickup;

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.ShowButton(
            transform.position,
            data.sceneName,
            transform,
            data.panelToOpen,
            itemPickup
        );
    }
}