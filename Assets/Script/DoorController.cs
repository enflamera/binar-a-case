using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class DoorController : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpen;

    public AudioSource audioSource;
    public AudioClip doorOpenSFX;

    public string nextScene = "PintuBelakangScene"; 

    public EvidenceData keyData;

    public void OpenDoor()
    {
        if (GameManager.Instance.inventory.Any(item => item != null && item.name == keyData.name))
        {
            StartCoroutine(OpenSequence());
        }
        else
        {
            Debug.Log("Butuh kunci!");
        }
    }

    IEnumerator OpenSequence()
    {
        if (doorOpen != null) doorOpen.SetActive(true);

        if (audioSource != null && doorOpenSFX != null)
        {
            audioSource.PlayOneShot(doorOpenSFX);
        }

        if (FadeManager.Instance != null)
        {
            yield return StartCoroutine(FadeManager.Instance.FadeOut(1f));
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(nextScene);
    }
}