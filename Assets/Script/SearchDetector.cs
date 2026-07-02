using UnityEngine;

public class SearchDetector : MonoBehaviour
{
    public RectTransform safeTrigger;
    public GameObject openButton;

    bool found;

    void Update()
    {
        if (found)
            return;

        if (RectTransformUtility.RectangleContainsScreenPoint(
            safeTrigger,
            transform.position))
        {
            found = true;
            openButton.SetActive(true);
        }
    }
}