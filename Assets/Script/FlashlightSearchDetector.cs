using UnityEngine;

public class FlashlightSearchDetector : MonoBehaviour
{
    public RectTransform searchUI;
    public RectTransform batteryTrigger;

    public FlashlightAnalyzeManager manager;

    bool found;

    void Update()
    {
        if (found)
            return;

        if (RectTransformUtility.RectangleContainsScreenPoint(
            batteryTrigger,
            searchUI.position))
        {
            found = true;
            manager.ShowOpenButton();
        }
    }
}