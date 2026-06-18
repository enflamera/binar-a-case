using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableObject : MonoBehaviour, IPointerClickHandler
{
    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = originalScale * 1.08f;
    }

    public void Deselect()
    {
        transform.localScale = originalScale;
    }
}