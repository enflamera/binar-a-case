using UnityEngine;

public class ExamineSafe : MonoBehaviour
{
    public GameObject keypadPanel;

    public void OpenKeypad()
    {
        keypadPanel.SetActive(true);
    }
}