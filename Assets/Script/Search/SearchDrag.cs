using UnityEngine;
using UnityEngine.EventSystems;

public class SearchDrag : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }
}