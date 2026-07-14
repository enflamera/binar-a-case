using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SceneAudio : MonoBehaviour
{
    public static SceneAudio Instance;

    [Header("Fade")]
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;

    AudioSource audioSource;
    float defaultVolume;

    void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
    }

    void Start()
    {
        audioSource.volume = 0f;

        if (!audioSource.isPlaying)
            audioSource.Play();

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, defaultVolume, t / fadeInDuration);
            yield return null;
        }

        audioSource.volume = defaultVolume;
    }

    public IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float t = 0f;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutDuration);
            yield return null;
        }

        audioSource.volume = 0f;
    }
}