using UnityEngine;

public class SearchManager : MonoBehaviour
{
    public GameObject searchUI;
    public GameObject seeButton;

    public SearchDetectorJodi detector;

    public JodiDialogueManager dialogue;

    GameObject currentHit;

    void Start()
    {
        searchUI.SetActive(false);
        seeButton.SetActive(false);

        dialogue.OnSearchStarted.AddListener(StartSearch);

        detector.OnHitEvidence += HandleHit;
    }

    void StartSearch()
    {
        searchUI.SetActive(true);
    }

    void HandleHit(GameObject obj)
    {
        currentHit = obj;
        seeButton.SetActive(true);
    }

    public void OnClickSee()
    {
        seeButton.SetActive(false);

        if (currentHit == null) return;

        if (currentHit.name.Contains("Senter"))
        {
            Toast("Dani: boleh pinjem senternya?");
            Toast("Jodi: iya silakan");

            dialogue.ChangeJodiExpression(JodiState.Nervous);
        }

        if (currentHit.name.Contains("Sepatu"))
        {
            Toast("Jejak sepatu ditemukan...");
            Debug.Log("Evidence sepatu tercatat");
        }

        Destroy(currentHit);
        currentHit = null;
    }

    void Toast(string msg)
    {
        Debug.Log(msg);
    }
}