using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationCard : MonoBehaviour
{
    public TMP_Text locationText;
    public Image blackPanel;

    public GameObject blackScreen;
    public GameObject dialogueUI;

    public CanvasGroup dialogueCanvas;
    public LestariDialogue lestariDialogue;

    public float fadeDuration = 1f;
    public float displayDuration = 3f;

    IEnumerator Start()
    {
        dialogueUI.SetActive(false);

        locationText.text =
            "09.12 WIB\n\n" +
            "Tempat Kejadian Perkara\n\n" +
            "Kontrakan Kemuning\n" +
            "Palmerah, Jakarta Barat";

        Color textColor = locationText.color;
        textColor.a = 0;
        locationText.color = textColor;

        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            textColor.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            locationText.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(displayDuration);

        timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            textColor.a = Mathf.Lerp(1, 0, timer / fadeDuration);
            locationText.color = textColor;
            yield return null;
        }

        Color panelColor = blackPanel.color;

        timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            panelColor.a = Mathf.Lerp(1, 0, timer / fadeDuration);
            blackPanel.color = panelColor;
            yield return null;
        }

        blackScreen.SetActive(false);

        dialogueCanvas.alpha = 0;

        dialogueUI.SetActive(true);

        lestariDialogue.StartDialogue();

        yield return null;

        timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            dialogueCanvas.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }
    }
}