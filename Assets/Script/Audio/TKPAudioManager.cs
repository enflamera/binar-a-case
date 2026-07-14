using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TKPAudioManager : MonoBehaviour
{
    public static TKPAudioManager Instance;
    public bool IsMuted { get; private set; }

    [Header("Audio")]
    public AudioClip music;

    [Header("Fade")]
    public float fadeDuration = 1f;

    AudioSource source;
    float targetVolume;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        targetVolume = source.volume;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            source.loop = true;
            source.playOnAwake = false;

            source.clip = music;
            source.volume = 0f;
            source.Play();

            StartCoroutine(FadeIn());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMute()
    {
        IsMuted = !IsMuted;
        source.mute = IsMuted;
    }

    IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            yield return null;
        }

        source.volume = targetVolume;
    }

    public IEnumerator FadeOut()
    {
        float start = source.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(start, 0f, t / fadeDuration);
            yield return null;
        }

        source.Stop();
        Instance = null;
        Destroy(gameObject);
    }
}