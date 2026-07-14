using UnityEngine;

public class ClickSoundManager : MonoBehaviour
{
    public static ClickSoundManager Instance;

    public AudioSource audioSource;
    public AudioClip clickSound;

    [Range(0f, 1f)]
    public float volume = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }

    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, volume);
        }
    }
}