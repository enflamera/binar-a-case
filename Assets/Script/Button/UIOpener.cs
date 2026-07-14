using UnityEngine;

public class UIOpener : MonoBehaviour
{
    public GameObject buktiPanel;

    public void OpenPanel()
    {
        if (buktiPanel != null)
        {
            buktiPanel.SetActive(true);
        }
    }
}