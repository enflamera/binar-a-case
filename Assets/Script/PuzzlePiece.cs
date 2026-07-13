using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Canvas rootCanvas;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        rootCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float scale = rootCanvas != null ? rootCanvas.scaleFactor : 1f;
        rect.anchoredPosition += eventData.delta / scale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PuzzlePiece otherPiece = other.GetComponent<PuzzlePiece>();
        if (otherPiece == null) return;

        PhotoPuzzleManager.Instance.NotifyTouch(this, otherPiece, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PuzzlePiece otherPiece = other.GetComponent<PuzzlePiece>();
        if (otherPiece == null) return;

        PhotoPuzzleManager.Instance.NotifyTouch(this, otherPiece, false);
    }
}