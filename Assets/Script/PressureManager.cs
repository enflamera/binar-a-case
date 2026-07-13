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

    [Header("Gradient")]
    public Gradient gradient;

    [Header("Jodi")]
    public Image jodiImage;
    public Sprite pokerFace;
    public Sprite confused;
    public Sprite nervous;

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

        fillImage.color = gradient.Evaluate(0);

        dialogue.OnPressureIncrease += AddPressure;
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
            nervousUsed = true;
            jodiImage.sprite = confused;
        }

        if (slider.value >= 75 && !nervousUsed)
        {
            confusedUsed = true;
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