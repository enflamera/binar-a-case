using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Linq; // Tambahkan ini buat ngecek list inventory

public class DoorController : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpen;

    public AudioSource audioSource;
    public AudioClip doorOpenSFX;
    public AudioClip footstepsSFX;

    public Image fadePanel;
    public string nextScene = "PartnerDialogue";
    
    // Drag data kunci (KertasData atau KeyData) ke sini via Inspector
    public EvidenceData keyData; 

    public void OpenDoor()
    {
        // Mengecek apakah inventory mengandung data kunci tersebut
        if (GameManager.Instance.inventory.Contains(keyData))
        {
            StartCoroutine(OpenSequence());
        }
        else
        {
            Debug.Log("Butuh kunci!");
            // Tambahkan logika kalau pintu terkunci (misal: kasih notif UI)
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
        Color c = fadePanel.color;
        
        while(time < 1f)
        {
            time += Time.deltaTime;
            c.a = time;
            fadePanel.color = c;
            yield return null;
        }

        SceneManager.LoadScene(nextScene);
    }
}