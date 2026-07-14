using UnityEngine;

public class ExitConfirmManager : MonoBehaviour
{
    public static ExitConfirmManager Instance;

    [Header("UI")]
    public GameObject confirmPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (confirmPanel != null)
                confirmPanel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (confirmPanel.activeSelf)
                HideConfirm();
            else
                ShowConfirm();
        }
    }

    public void ShowConfirm()
    {
        confirmPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideConfirm()
    {
        confirmPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }
}