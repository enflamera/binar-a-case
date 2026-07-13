using UnityEngine;

public class ArrestButton : MonoBehaviour
{
    public int suspectIndex;

    public void OnArrestClicked()
    {
        SuspectUIController.Instance.AttemptArrest(suspectIndex);
    }
}