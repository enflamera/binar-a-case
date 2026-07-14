using TMPro;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public TMP_Text pinDisplayText;
    public TMP_Text errorText;

    public GameObject keypadPanel;
    public GameObject laptopContentImage;

    private string currentPin = "";
    private const string correctPin = "2105";

    void Start()
    {
        pinDisplayText.text = "";

        if (errorText != null)
            errorText.gameObject.SetActive(false);

        if (laptopContentImage != null)
            laptopContentImage.SetActive(false);
    }

    public void AddNumber(string number)
    {
        if (currentPin.Length >= 4)
            return;

        currentPin += number;
        pinDisplayText.text = currentPin;

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
            laptopContentImage.SetActive(true);
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