using System.Collections;
using TMPro;
using UnityEngine;

public class MainMenuLoader : MonoBehaviour
{
    public TMP_Text loadingText;
    public CanvasGroup bgMainGroup;

    public float loadingDuration = 3f;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(LoadingSequence());
    }

    IEnumerator LoadingSequence()
    {
        float timer = 0f;

        while (timer < loadingDuration)
        {
            loadingText.text = "Memuat.";
            yield return new WaitForSeconds(0.3f);

            loadingText.text = "Memuat..";
            yield return new WaitForSeconds(0.3f);

            loadingText.text = "Memuat...";
            yield return new WaitForSeconds(0.3f);

            timer += 0.9f;
        }

        yield return StartCoroutine(FadeOut(bgMainGroup));
    }

    IEnumerator FadeOut(CanvasGroup group)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            group.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);

            yield return null;
        }

        group.alpha = 0f;
        group.gameObject.SetActive(false);
    }
}