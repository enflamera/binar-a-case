using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("Boxes")]
    public GameObject daniBox;
    public GameObject nBox;

    [Header("Characters")]
    public GameObject daniCharacter;
    public GameObject nCharacter;

    [Header("Texts")]
    public TMP_Text daniText;
    public TMP_Text nText;

    [Header("Settings")]
    public string nextScene;
    public float typingSpeed = 0.05f;
    public TransisiLokal transisi;

    private DialogueData[] dialogues;

    private int currentIndex = 0;
    private bool isTyping = false;

    private string currentFullText;

    private Coroutine typingCoroutine;

    void Awake()
    {
        daniBox.SetActive(false);
        nBox.SetActive(false);

        daniCharacter.SetActive(false);
        nCharacter.SetActive(false);
    }

    public void SetDialogue(DialogueData[] newDialogues)
    {
        dialogues = newDialogues;

        currentIndex = 0;

        ShowDialogue();
    }

    void ShowDialogue()
    {
        if (dialogues == null || dialogues.Length == 0)
            return;

        DialogueData current = dialogues[currentIndex];

        currentFullText = current.text;

        daniText.text = "";
        nText.text = "";

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        if (current.isDani)
        {
            daniBox.SetActive(true);
            nBox.SetActive(false);

            daniCharacter.SetActive(true);
            nCharacter.SetActive(false);

            typingCoroutine = StartCoroutine(TypeText(daniText, current.text));
        }
        else
        {
            daniBox.SetActive(false);
            nBox.SetActive(true);

            daniCharacter.SetActive(false);
            nCharacter.SetActive(true);

            typingCoroutine = StartCoroutine(TypeText(nText, current.text));
        }
    }

    IEnumerator TypeText(TMP_Text targetText, string message)
    {
        isTyping = true;

        targetText.text = "";

        yield return new WaitForSeconds(0.15f);

        foreach (char letter in message)
        {
            targetText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void NextDialogue()
    {
        if (dialogues == null)
            return;

        // Kalau masih ngetik, langsung tampilkan full text
        if (isTyping)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            DialogueData current = dialogues[currentIndex];

            if (current.isDani)
                daniText.text = currentFullText;
            else
                nText.text = currentFullText;

            isTyping = false;
            return;
        }

        currentIndex++;

        if (currentIndex >= dialogues.Length)
        {
            transisi.PindahScene(nextScene);
            return;
        }

        ShowDialogue();
    }

    public void SkipDialogue()
    {
        transisi.PindahScene(nextScene);
    }
}