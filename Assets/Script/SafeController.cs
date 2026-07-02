using TMPro;
using UnityEngine;

public class SafeController : MonoBehaviour
{
    public TMP_Text pinDisplayText;
    public TMP_Text errorText;

    public GameObject keypadPanel;
    public GameObject bgMejaOpen2;

    public EvidenceUI evidenceUI;
    public string evidenceID;

    public string noteTitle;

    [TextArea]
    public string noteContent;

    string currentPin = "";
    const string correctPin = "793510";

    void Start()
    {
        pinDisplayText.text = "";

        if (errorText != null)
            errorText.gameObject.SetActive(false);
    }

    public void AddNumber(string number)
    {
        if (currentPin.Length >= 6)
            return;

        currentPin += number;
        pinDisplayText.text = currentPin;

        if (errorText != null)
            errorText.gameObject.SetActive(false);
    }

    public void ClearPin()
    {
        currentPin = "";
        pinDisplayText.text = "";

        if (errorText != null)
            errorText.gameObject.SetActive(false);
    }

    public void EnterPin()
    {
        if (currentPin.Length < 4)
            return;

        if (currentPin == correctPin)
        {
            keypadPanel.SetActive(false);

            bgMejaOpen2.SetActive(true);

            GameManager.Instance.CompleteEvidence(evidenceID);

            if (evidenceUI != null)
                evidenceUI.Dim();

            if (NoteManager.Instance != null)
                NoteManager.Instance.AddNote(noteTitle, noteContent);
        }
        else
        {
            if (errorText != null)
            {
                errorText.gameObject.SetActive(true);
                errorText.text = "PIN Salah";
            }

            currentPin = "";
            pinDisplayText.text = "";
        }
    }
}