using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject2 : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    [Header("Evidence")]
    public string evidenceID;
    public EvidenceUI evidenceUI;

    private bool triggered = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!triggered)
        {
            triggered = true;

            GameManager.Instance.CompleteEvidence(evidenceID);

            if (evidenceUI != null)
                evidenceUI.Dim();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}