using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterrogateButton : MonoBehaviour
{
    public int suspectIndex;
    public string sceneName;
    public TransisiLokal transisi;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (button == null)
            Debug.LogWarning($"[InterrogateButton] Tidak ada komponen Button di GameObject '{gameObject.name}'.");
    }

    private void OnEnable()
    {
        RefreshState();
    }

    public void RefreshState()
    {
        if (button == null) return;

        if (SuspectManager.Instance == null)
        {
            Debug.LogWarning("[InterrogateButton] SuspectManager.Instance null. Pastikan scene diawali dari Beranda dulu.");
            return;
        }

        SuspectData suspect = SuspectManager.Instance.GetSuspect(suspectIndex);

        if (suspect == null)
        {
            button.interactable = false;
            return;
        }

        button.interactable = suspect.canInterrogate && !suspect.interrogated;
    }

    public void StartInterrogation()
    {
        if (SuspectManager.Instance != null)
        {
            SuspectManager.Instance.InterrogateSuspect(suspectIndex);
        }

        if (button != null)
            button.interactable = false;

        if (transisi != null)
        {
            transisi.PindahScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}