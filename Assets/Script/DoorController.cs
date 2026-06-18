using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpen;

    public AudioSource audioSource;
    public AudioClip doorOpenSFX;
    public AudioClip footstepsSFX;

    public Image fadePanel;

    public string nextScene = "PartnerDialogue";

    public void OpenDoor()
    {
        if (GameManager.Instance.hasKey)
        {
            StartCoroutine(OpenSequence());
        }
    }

    IEnumerator OpenSequence()
    {
        doorClosed.SetActive(false);
        doorOpen.SetActive(true);

        audioSource.PlayOneShot(doorOpenSFX);

        yield return new WaitForSeconds(1.5f);

        audioSource.PlayOneShot(footstepsSFX);

        float time = 0;

        while(time < 1f)
        {
            time += Time.deltaTime;

            Color c = fadePanel.color;
            c.a = time;
            fadePanel.color = c;

            yield return null;
        }

        SceneManager.LoadScene(nextScene);
    }
}