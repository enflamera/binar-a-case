using UnityEngine;

public class TKPMuteButton : MonoBehaviour
{
    public GameObject muteIcon;

    void Start()
    {
        RefreshIcon();
    }

    public void ToggleMute()
    {
        if (TKPAudioManager.Instance == null) return;
        TKPAudioManager.Instance.ToggleMute();
        RefreshIcon();
    }

    void RefreshIcon()
    {
        if (TKPAudioManager.Instance == null || muteIcon == null) return;
        muteIcon.SetActive(TKPAudioManager.Instance.IsMuted);
    }
}