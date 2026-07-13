using UnityEngine;

public class SearchCursor : MonoBehaviour
{
    RectTransform rect;

    Canvas canvas;

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 pos);

        rect.anchoredPosition = pos;
    }
}