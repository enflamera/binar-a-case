using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExamineManager : MonoBehaviour
{
    public RectTransform examineButton;

    private string selectedScene;
    private GameObject selectedPanel;

    private Transform currentSelected;
    private Vector3 originalScale;

    void Start()
    {
        examineButton.gameObject.SetActive(false);
    }

    public void ShowButton(
        Vector3 pos,
        string sceneName,
        Transform clickedObject,
        GameObject panelToOpen = null)
    {
        selectedScene = sceneName;
        selectedPanel = panelToOpen;

        if (currentSelected != null)
        {
            currentSelected.localScale = originalScale;
        }

        currentSelected = clickedObject;
        originalScale = clickedObject.localScale;

        clickedObject.localScale = originalScale * 1.03f;

        examineButton.position = pos;
        examineButton.gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(PopAnimation());
    }

    public void HideButton()
    {
        examineButton.gameObject.SetActive(false);

        if (currentSelected != null)
        {
            currentSelected.localScale = originalScale;
            currentSelected = null;
        }
    }
    
    public void OpenTarget()
    {
        if (selectedPanel != null)
        {
            selectedPanel.SetActive(true);
            HideButton();
            return;
        }

        if (!string.IsNullOrEmpty(selectedScene))
        {
            SceneManager.LoadScene(selectedScene);
        }
    }

    IEnumerator PopAnimation()
    {
        examineButton.localScale = Vector3.zero;

        float t = 0f;

        while (t < 0.15f)
        {
            t += Time.deltaTime;

            examineButton.localScale =
                Vector3.Lerp(
                    Vector3.zero,
                    Vector3.one * 1.15f,
                    t / 0.15f
                );

            yield return null;
        }

        t = 0f;

        while (t < 0.08f)
        {
            t += Time.deltaTime;

            examineButton.localScale =
                Vector3.Lerp(
                    Vector3.one * 1.15f,
                    Vector3.one,
                    t / 0.08f
                );

            yield return null;
        }
    }
}