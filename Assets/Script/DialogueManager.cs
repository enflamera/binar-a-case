using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class DialogueManager : MonoBehaviour
{
    public Action OnDialogueEnded;

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

    [Header("Testimony")]
    public WitnessTestimony testimony;

    [Header("Evidence Popup")]
    public EvidencePopup evidencePopup;

    private DialogueData[] dialogues;
    private int currentIndex;
    private bool isTyping;
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

        if (evidencePopup != null)
            evidencePopup.Hide();

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

        if (current is EvidenceDialogueData evidenceData)
        {
            evidencePopup.Show(
                evidenceData.popupSprite,
                evidenceData.evidenceTitle
            );

            if (!ForensicManager.Instance.IsEvidenceUnlocked(evidenceData.evidenceID))
            {
                ForensicEvidence newEvidence = new ForensicEvidence();

                newEvidence.evidenceID = evidenceData.evidenceID;
                newEvidence.evidenceName = evidenceData.evidenceTitle;
                newEvidence.slotSprite = evidenceData.popupSprite;
                newEvidence.tvSprite = evidenceData.tvSprite;

                ForensicManager.Instance.AddEvidence(newEvidence);
            }

            if (evidenceData.evidenceID == "bukti_pundak")
            {
                StartCoroutine(HideEvidenceAfterTyping());
            }
        }

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

        foreach (char c in message)
        {
            targetText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;
    }

    public void NextDialogue()
    {
        if (dialogues == null || dialogues.Length == 0)
            return;

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
            typingCoroutine = null;
            return;
        }

        DialogueData finished = dialogues[currentIndex];

        if (finished.isWitness)
        {
            testimony?.SaveTestimony();
        }

        currentIndex++;

        if (currentIndex >= dialogues.Length)
        {
            if (evidencePopup != null)
                evidencePopup.Hide();

            OnDialogueEnded?.Invoke();

            transisi?.PindahScene(nextScene);
            return;
        }

        ShowDialogue();
    }

    public void SkipDialogue()
    {
        if (evidencePopup != null)
            evidencePopup.Hide();

        testimony?.SaveTestimony();

        OnDialogueEnded?.Invoke();

        transisi?.PindahScene(nextScene);
    }

    IEnumerator HideEvidenceAfterTyping()
    {
        while (isTyping)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        evidencePopup.Hide();
    }
}