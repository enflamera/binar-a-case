using UnityEngine;

public class OpenSafeView : MonoBehaviour
{
    public GameObject bgMejaOpen;
    public GameObject examineButton;

    public void OpenSafe()
    {
        bgMejaOpen.SetActive(true);
        examineButton.SetActive(true);

        gameObject.SetActive(false);
    }
}