using UnityEngine;

public class FinalEvidenceManager : MonoBehaviour
{
    public static FinalEvidenceManager Instance;

    public string[] evidenceIDs;

    void Awake()
    {
        Instance = this;
    }

    public void CheckCompletion()
    {
        foreach (string id in evidenceIDs)
        {
            if (!GameManager.Instance.IsEvidenceCompleted(id))
                return;
        }

        FinalTKPManager.Instance.FinishTKP();
    }
}