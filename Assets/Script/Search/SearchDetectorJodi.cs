using UnityEngine;

public class SearchDetectorJodi : MonoBehaviour
{
    public System.Action<GameObject> OnHitEvidence;
    public RectTransform searchUI;

    void Update()
    {
        GameObject[] evidences = GameObject.FindGameObjectsWithTag("Evidence");

        foreach (GameObject e in evidences)
        {
            RectTransform eRect = e.GetComponent<RectTransform>();

            if (IsOverlapping(searchUI, eRect))
            {
                OnHitEvidence?.Invoke(e);
            }
        }
    }

    bool IsOverlapping(RectTransform a, RectTransform b)
    {
        Vector3[] aCorners = new Vector3[4];
        Vector3[] bCorners = new Vector3[4];

        a.GetWorldCorners(aCorners);
        b.GetWorldCorners(bCorners);

        Rect aRect = new Rect(aCorners[0], aCorners[2] - aCorners[0]);
        Rect bRect = new Rect(bCorners[0], bCorners[2] - bCorners[0]);

        return aRect.Overlaps(bRect);
    }
}