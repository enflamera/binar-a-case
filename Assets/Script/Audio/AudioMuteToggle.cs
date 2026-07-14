using UnityEngine;

public class AudioMuteToggle : MonoBehaviour
{
    [Header("Refs")]
    public AudioSource[] targetSources;
    public GameObject muteIcon;
    bool isMuted = false;

    void Start()
    {
        ApplyState();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        ApplyState();
    }

    void ApplyState()
    {
        foreach (var src in targetSources)
            if (src != null) src.mute = isMuted;

        if (muteIcon != null)
            muteIcon.SetActive(isMuted);
    }
}