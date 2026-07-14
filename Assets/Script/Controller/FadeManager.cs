using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }
    private Image fadeImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        fadeImage = GetComponent<Image>();
    }
    
    public IEnumerator FadeOut(float duration = 1f)
    {
        float time = 0;
        Color c = fadeImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            c.a = Mathf.Clamp01(time / duration);
            fadeImage.color = c;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float duration = 1f)
    {
        float time = duration;
        Color c = fadeImage.color;

        while (time > 0)
        {
            time -= Time.deltaTime;
            c.a = Mathf.Clamp01(time / duration);
            fadeImage.color = c;
            yield return null;
        }
    }
}