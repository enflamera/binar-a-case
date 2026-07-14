using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public class IntroDialogue
{
    public bool isDani;

    [TextArea(2, 5)]
    public string text;
}

[Serializable]
public class ChoiceData
{
    public string title;

    [TextArea(2, 5)]
    public string daniDialogue;

    [TextArea(2, 5)]
    public string jodiDialogue;

    public bool correct;
}

[Serializable]
public class TopicData
{
    public ChoiceData[] choices;
}

public enum JodiState
{
    Poker,
    Nervous,
    Confused
}

public class JodiDialogueManager : MonoBehaviour
{
    public enum State
    {
        Intro,
        Question,
        DaniTalking,
        JodiTalking,
        Ending,
        Finished
    }

    [Header("Dialogue")]
    public IntroDialogue[] introDialogues;

    [Header("Ending")]
    public IntroDialogue[] endingDialogues;

    [Header("Question")]
    public TopicData[] topics;

    [Header("Dialogue UI")]
    public GameObject daniBox;
    public GameObject jodiBox;

    public TMP_Text daniText;
    public TMP_Text jodiText;

    public Button nextButton;

    [Header("Question UI")]
    public GameObject questionPanel;

    public Button buttonA;
    public Button buttonB;
    public Button buttonC;

    public TMP_Text textA;
    public TMP_Text textB;
    public TMP_Text textC;

    [Header("Typing")]
    public float typingSpeed = 0.03f;

    [Header("Jodi Expression")]
    public Image jodiImage;
    public GameObject jodiCharacter;
    public Sprite pokerSprite;
    public Sprite nervousSprite;
    public Sprite confusedSprite;

    [Header("Transition")]
    public RectTransform dialoguePanel;
    public CanvasGroup blur;
    public RectTransform jodiRect;
    public Vector2 dialogueHidePosition;
    public Vector2 jodiSearchPosition;
    public Vector3 jodiSearchScale = Vector3.one;
    public CanvasGroup sliderCanvasGroup;
    public CanvasGroup searchUIGroup;
    public GameObject notulensiButton;
    public GameObject evidenceButton;
    public float transitionDuration = .75f;
    public UnityEvent OnSearchStarted;

    public Action<int> OnPressureIncrease;
    public Action OnDialogueFinished;

    private bool inputLocked;

    private Coroutine typingRoutine;

    private bool isTyping;

    private string currentFullText;

    private int introIndex;
    private int endingIndex;

    protected int topicIndex;

    protected ChoiceData currentChoice;

    protected int currentChoiceIndex;

    protected State currentState;

    void Start()
    {
        buttonA.onClick.AddListener(() => SelectChoice(0));
        buttonB.onClick.AddListener(() => SelectChoice(1));
        buttonC.onClick.AddListener(() => SelectChoice(2));

        questionPanel.SetActive(false);

        daniBox.SetActive(false);
        jodiBox.SetActive(false);

        currentState = State.Intro;

        ShowIntroDialogue();
    }

    void ShowIntroDialogue()
    {
        if (introIndex >= introDialogues.Length)
        {
            ShowQuestion();
            return;
        }

        IntroDialogue dialogue = introDialogues[introIndex];

        daniBox.SetActive(dialogue.isDani);
        jodiBox.SetActive(!dialogue.isDani);
        jodiCharacter.SetActive(!dialogue.isDani);

        daniText.text = "";
        jodiText.text = "";

        currentFullText = dialogue.text;

        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(
            TypeText(
                dialogue.isDani ? daniText : jodiText,
                dialogue.text
            )
        );
    }

    IEnumerator TypeText(TMP_Text target, string text)
    {
        inputLocked = true;
        isTyping = true;

        target.text = "";

        yield return new WaitForSeconds(0.15f);

        foreach (char c in text)
        {
            target.text += c;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        inputLocked = false;
        typingRoutine = null;
    }

    public void Next()
    {
        if (inputLocked && !isTyping)
            return;

        if (isTyping)
        {
            if (typingRoutine != null)
                StopCoroutine(typingRoutine);

            if (daniBox.activeSelf)
                daniText.text = currentFullText;
            else
                jodiText.text = currentFullText;

            isTyping = false;
            inputLocked = false;
            typingRoutine = null;

            return;
        }

        switch (currentState)
        {
            case State.Intro:

                introIndex++;
                ShowIntroDialogue();

                break;

            case State.DaniTalking:

                ShowJodiDialogue();

                break;

            case State.JodiTalking:

                CheckChoice();

                break;

            case State.Ending:

                endingIndex++;
                ShowEndingDialogue();

                break;
        }
    }

    void ShowQuestion(bool reset = true)
    {
        currentState = State.Question;

        nextButton.gameObject.SetActive(false);

        questionPanel.SetActive(true);

        EnableQuestionButtons(true);

        TopicData topic = topics[topicIndex];

        if (reset)
        {
            buttonA.gameObject.SetActive(true);
            buttonB.gameObject.SetActive(true);
            buttonC.gameObject.SetActive(true);
        }

        textA.text = "A. " + topic.choices[0].title;
        textB.text = "B. " + topic.choices[1].title;
        textC.text = "C. " + topic.choices[2].title;
    }

    void SelectChoice(int index)
    {
        if (inputLocked)
            return;

        if (!buttonA.gameObject.activeSelf && index == 0)
            return;

        if (!buttonB.gameObject.activeSelf && index == 1)
            return;

        if (!buttonC.gameObject.activeSelf && index == 2)
            return;

        currentChoiceIndex = index;

        currentChoice = topics[topicIndex].choices[index];

        questionPanel.SetActive(false);

        EnableQuestionButtons(false);

        nextButton.gameObject.SetActive(true);

        currentState = State.DaniTalking;

        daniBox.SetActive(true);
        jodiBox.SetActive(false);
        jodiCharacter.SetActive(false);

        daniText.text = "";
        jodiText.text = "";

        currentFullText = currentChoice.daniDialogue;

        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(
            TypeText(
                daniText,
                currentChoice.daniDialogue
            )
        );
    }

    void ShowJodiDialogue()
    {
        currentState = State.JodiTalking;

        daniBox.SetActive(false);
        jodiBox.SetActive(true);
        jodiCharacter.SetActive(true);

        daniText.text = "";
        jodiText.text = "";

        currentFullText = currentChoice.jodiDialogue;

        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(
            TypeText(
                jodiText,
                currentChoice.jodiDialogue
            )
        );
    }

    void ShowEndingDialogue()
    {
        nextButton.gameObject.SetActive(true);

        if (endingIndex >= endingDialogues.Length)
        {
            FinishDialogue();
            return;
        }

        IntroDialogue dialogue = endingDialogues[endingIndex];

        daniBox.SetActive(dialogue.isDani);
        jodiBox.SetActive(!dialogue.isDani);
        jodiCharacter.SetActive(!dialogue.isDani);

        daniText.text = "";
        jodiText.text = "";

        currentFullText = dialogue.text;

        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(
            TypeText(
                dialogue.isDani ? daniText : jodiText,
                dialogue.text
            )
        );
    }

    void CheckChoice()
    {
        if (currentChoice.correct)
        {
            OnPressureIncrease?.Invoke(15);

            ScoreManager.Instance?.AddScore(
                100,
                ScoreCategory.JodiChoice,
                $"Jodi_Choice_{topicIndex}"
            );

            topicIndex++;

            if (topicIndex >= topics.Length)
            {
                currentState = State.Ending;

                daniBox.SetActive(false);
                jodiBox.SetActive(false);

                ShowEndingDialogue();
            }
            else
            {
                ShowQuestion();
            }
        }
        else
        {
            HideWrongChoice();

            ShowQuestion(false);
        }
    }

    void HideWrongChoice()
    {
        switch (currentChoiceIndex)
        {
            case 0:
                buttonA.gameObject.SetActive(false);
                break;

            case 1:
                buttonB.gameObject.SetActive(false);
                break;

            case 2:
                buttonC.gameObject.SetActive(false);
                break;
        }
    }

    void EnableQuestionButtons(bool value)
    {
        buttonA.interactable = value;
        buttonB.interactable = value;
        buttonC.interactable = value;
    }

    void FinishDialogue()
    {
        nextButton.gameObject.SetActive(false);

        questionPanel.SetActive(false);

        daniBox.SetActive(false);
        jodiBox.SetActive(false);
        jodiCharacter.SetActive(true);

        OnDialogueFinished?.Invoke();

        StartCoroutine(StartSearchTransition());
    }

    IEnumerator StartSearchTransition()
    {
        float t = 0;

        Vector2 startDialogue = dialoguePanel.anchoredPosition;
        Vector2 startJodi = jodiRect.anchoredPosition;
        Vector3 startJodiScale = jodiRect.localScale;

        float startBlur = blur.alpha;
        float startSliderAlpha = sliderCanvasGroup != null ? sliderCanvasGroup.alpha : 0f;

        if (searchUIGroup != null)
        {
            searchUIGroup.gameObject.SetActive(true);
            searchUIGroup.alpha = 0f;
            searchUIGroup.interactable = false;
            searchUIGroup.blocksRaycasts = false;
        }

        while (t < 1)
        {
            t += Time.deltaTime / transitionDuration;

            dialoguePanel.anchoredPosition =
                Vector2.Lerp(startDialogue, dialogueHidePosition, t);

            jodiRect.anchoredPosition =
                Vector2.Lerp(startJodi, jodiSearchPosition, t);

            jodiRect.localScale =
                Vector3.Lerp(startJodiScale, jodiSearchScale, t);

            blur.alpha = Mathf.Lerp(startBlur, 0, t);

            if (sliderCanvasGroup != null)
                sliderCanvasGroup.alpha = Mathf.Lerp(startSliderAlpha, 0f, t);

            if (searchUIGroup != null)
                searchUIGroup.alpha = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        blur.blocksRaycasts = false;

        if (sliderCanvasGroup != null)
        {
            sliderCanvasGroup.alpha = 0f;
            sliderCanvasGroup.interactable = false;
            sliderCanvasGroup.blocksRaycasts = false;
        }

        if (searchUIGroup != null)
        {
            searchUIGroup.alpha = 1f;
            searchUIGroup.interactable = true;
            searchUIGroup.blocksRaycasts = true;
        }

        if (notulensiButton != null)
            notulensiButton.SetActive(true);

        if (evidenceButton != null)
            evidenceButton.SetActive(true);

        OnSearchStarted?.Invoke();
    }

    public void AddPressure(int amount)
    {
        OnPressureIncrease?.Invoke(amount);
    }

    public void ChangeJodiExpression(JodiState state)
    {
        switch (state)
        {
            case JodiState.Poker:
                jodiImage.sprite = pokerSprite;
                break;

            case JodiState.Confused:
                jodiImage.sprite = confusedSprite;
                break;

            case JodiState.Nervous:
                jodiImage.sprite = nervousSprite;
                break;
        }
    }

    public void ChangeJodiExpression(Sprite sprite, Image image)
    {
        image.sprite = sprite;
    }

    public void DisableNext()
    {
        nextButton.gameObject.SetActive(false);
    }

    public void EnableNext()
    {
        nextButton.gameObject.SetActive(true);
    }

    public bool IsEndingOrFinished()
    {
        return currentState == State.Ending || currentState == State.Finished;
    }
}