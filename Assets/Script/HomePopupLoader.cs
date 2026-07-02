using UnityEngine;

public class HomePopupLoader : MonoBehaviour
{
    private void Start()
    {
        if (SuspectManager.Instance == null)
            return;

        int index = SuspectManager.Instance.pendingPopupSuspect;

        if (index >= 0)
        {
            NewSuspectPopup.Instance.Show(
                SuspectManager.Instance.suspects[index].popupImage
            );

            SuspectManager.Instance.pendingPopupSuspect = -1;
        }
    }
}