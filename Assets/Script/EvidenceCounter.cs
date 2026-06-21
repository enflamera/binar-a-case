using UnityEngine;
using TMPro;

public class EvidenceCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public string[] evidenceIDs;

    void Update()
    {
        int count = 0;

        foreach (string id in evidenceIDs)
        {
            if (GameManager.Instance.IsEvidenceCompleted(id))
            {
                count++;
            }
        }

        counterText.text = count + "/" + evidenceIDs.Length;
    }
}