using UnityEngine;

public class SceneStartFade : MonoBehaviour
{
    public float fadeDuration = 1.5f;

    void Start()
    {
        if (FadeManager.Instance != null)
        {
            StartCoroutine(FadeManager.Instance.FadeIn(fadeDuration));
        }
        else
        {
            Debug.LogWarning("FadeManager tidak ditemukan di scene ini!");
        }
    }
}