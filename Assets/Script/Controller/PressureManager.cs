using UnityEngine;
using UnityEngine.UI;

public class PressureManager : MonoBehaviour
{
    public JodiDialogueManager dialogue;

    [Header("UI")]
    public Slider slider;
    public Image fillImage;

    [Header("Animation")]
    public float animationSpeed = 2f;

    [Header("Jodi")]
    public Image jodiImage;
    public Sprite pokerFace;
    public Sprite confused;
    public Sprite nervous;

    private Gradient gradient;
    private float targetValue;

    private bool confusedUsed;
    private bool nervousUsed;
    private bool pressureLocked;

    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 100;

        slider.value = 0;
        targetValue = 0;

        BuildGradient();

        fillImage.color = gradient.Evaluate(0);

        dialogue.OnPressureIncrease += AddPressure;
    }

    void BuildGradient()
    {
        gradient = new Gradient();

        GradientColorKey[] colorKeys = new GradientColorKey[4];
        colorKeys[0] = new GradientColorKey(Color.green, 0f);
        colorKeys[1] = new GradientColorKey(Color.yellow, 0.33f);
        colorKeys[2] = new GradientColorKey(new Color(1f, 0.5f, 0f), 0.66f);
        colorKeys[3] = new GradientColorKey(Color.red, 1f);

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1f, 0f);
        alphaKeys[1] = new GradientAlphaKey(1f, 1f);

        gradient.SetKeys(colorKeys, alphaKeys);
    }

    void Update()
    {
        slider.value = Mathf.MoveTowards(
            slider.value,
            targetValue,
            animationSpeed * Time.deltaTime * 20f);

        fillImage.color = gradient.Evaluate(slider.normalizedValue);

        if (slider.value >= 45 && !confusedUsed)
        {
            confusedUsed = true;
            jodiImage.sprite = confused;
        }

        if (slider.value >= 75 && !nervousUsed)
        {
            nervousUsed = true;
            jodiImage.sprite = nervous;
        }

        if (slider.value >= 100 && !pressureLocked && !dialogue.IsEndingOrFinished())
        {
            pressureLocked = true;
            dialogue.DisableNext();
        }
    }

    void AddPressure(int value)
    {
        targetValue += value;

        if (targetValue > 100)
            targetValue = 100;
    }

    public bool IsFull()
    {
        return targetValue >= 100;
    }
}