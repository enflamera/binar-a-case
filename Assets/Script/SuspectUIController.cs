using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuspectUIController : MonoBehaviour
{
    public static SuspectUIController Instance;

    public GameObject[] suspectPanels;
    public GameObject[] interrogateButtons;

    [Header("Animation Settings")]
    public float animDuration = 0.25f;
    public RectTransform nextButtonRect;
    public RectTransform prevButtonRect;

    private int currentIndex;
    private Coroutine activeTransitionRoutine;
    
    private Vector2 nextBtnOriginalPos;
    private Vector2 prevBtnOriginalPos;
    private bool initializedButtons;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (nextButtonRect != null && !initializedButtons) nextBtnOriginalPos = nextButtonRect.anchoredPosition;
        if (prevButtonRect != null && !initializedButtons) prevBtnOriginalPos = prevButtonRect.anchoredPosition;
        if (nextButtonRect != null || prevButtonRect != null) initializedButtons = true;

        OpenFirstUnlocked();
    }

    public void OpenFirstUnlocked()
    {
        if (SuspectManager.Instance == null)
            return;

        bool foundAny = false;

        for (int i = suspectPanels.Length - 1; i >= 0; i--)
        {
            if (SuspectManager.Instance.IsUnlocked(i))
            {
                currentIndex = i;
                foundAny = true;
                break;
            }
        }

        if (foundAny)
        {
            ShowCurrent();
        }
        else
        {
            HideAll();
        }
    }

    void HideAll()
    {
        foreach (GameObject panel in suspectPanels)
        {
            panel.SetActive(false);
        }
    }

    void ShowCurrent()
    {
        for (int i = 0; i < suspectPanels.Length; i++)
        {
            bool active = i == currentIndex;
            suspectPanels[i].SetActive(active);

            if (active)
            {
                CanvasGroup canvasGroup = suspectPanels[i].GetComponent<CanvasGroup>();
                if (canvasGroup == null) canvasGroup = suspectPanels[i].AddComponent<CanvasGroup>();
                canvasGroup.alpha = 1f;

                bool showButton = SuspectManager.Instance.suspects[i].canInterrogate &&
                                  !SuspectManager.Instance.suspects[i].interrogated;

                if (interrogateButtons.Length > i && interrogateButtons[i] != null)
                {
                    interrogateButtons[i].SetActive(showButton);
                }
            }
        }

        if (nextButtonRect != null) nextButtonRect.anchoredPosition = nextBtnOriginalPos;
        if (prevButtonRect != null) prevButtonRect.anchoredPosition = prevBtnOriginalPos;
    }

    IEnumerator ChangeSuspectRoutine(int targetIndex)
    {
        GameObject currentPanel = suspectPanels[currentIndex];
        CanvasGroup currentGroup = currentPanel.GetComponent<CanvasGroup>();
        if (currentGroup == null) currentGroup = currentPanel.AddComponent<CanvasGroup>();

        GameObject targetPanel = suspectPanels[targetIndex];
        CanvasGroup targetGroup = targetPanel.GetComponent<CanvasGroup>();
        if (targetGroup == null) targetGroup = targetPanel.AddComponent<CanvasGroup>();
        
        targetGroup.alpha = 0f;

        Vector2 nextBtnOffset = nextBtnOriginalPos + new Vector2(40f, 0);
        Vector2 prevBtnOffset = prevBtnOriginalPos - new Vector2(40f, 0);

        float elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / animDuration);

            currentGroup.alpha = 1f - t;

            if (nextButtonRect != null) nextButtonRect.anchoredPosition = Vector2.Lerp(nextBtnOriginalPos, nextBtnOffset, t);
            if (prevButtonRect != null) prevButtonRect.anchoredPosition = Vector2.Lerp(prevBtnOriginalPos, prevBtnOffset, t);

            yield return null;
        }

        currentPanel.SetActive(false);
        currentIndex = targetIndex;
        targetPanel.SetActive(true);

        bool showButton = SuspectManager.Instance.suspects[currentIndex].canInterrogate &&
                          !SuspectManager.Instance.suspects[currentIndex].interrogated;

        if (interrogateButtons.Length > currentIndex && interrogateButtons[currentIndex] != null)
        {
            interrogateButtons[currentIndex].SetActive(showButton);
        }

        elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / animDuration);

            targetGroup.alpha = t;

            if (nextButtonRect != null) nextButtonRect.anchoredPosition = Vector2.Lerp(nextBtnOffset, nextBtnOriginalPos, t);
            if (prevButtonRect != null) prevButtonRect.anchoredPosition = Vector2.Lerp(prevBtnOffset, prevBtnOriginalPos, t);

            yield return null;
        }

        targetGroup.alpha = 1f;
        if (nextButtonRect != null) nextButtonRect.anchoredPosition = nextBtnOriginalPos;
        if (prevButtonRect != null) prevButtonRect.anchoredPosition = prevBtnOriginalPos;
        
        activeTransitionRoutine = null;
    }

    public void Next()
    {
        if (activeTransitionRoutine != null) return;

        int nextIndex = currentIndex;
        int start = currentIndex;

        do
        {
            nextIndex++;

            if (nextIndex >= suspectPanels.Length)
                nextIndex = 0;

            if (SuspectManager.Instance.IsUnlocked(nextIndex))
            {
                activeTransitionRoutine = StartCoroutine(ChangeSuspectRoutine(nextIndex));
                return;
            }

        } while (nextIndex != start);
    }

    public void Prev()
    {
        if (activeTransitionRoutine != null) return;

        int prevIndex = currentIndex;
        int start = currentIndex;

        do
        {
            prevIndex--;

            if (prevIndex < 0)
                prevIndex = suspectPanels.Length - 1;

            if (SuspectManager.Instance.IsUnlocked(prevIndex))
            {
                activeTransitionRoutine = StartCoroutine(ChangeSuspectRoutine(prevIndex));
                return;
            }

        } while (prevIndex != start);
    }
}