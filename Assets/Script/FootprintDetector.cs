using UnityEngine;

public class FootprintDetector : MonoBehaviour
{
    public RectTransform safeTrigger;
    public GameObject openButton;
    public JodiDialogueManager jodiManager;

    RectTransform openButtonRect;
    bool isInside;
    bool collected;

    void Awake()
    {
        openButtonRect = openButton.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (collected)
            return;

        bool nowInside = RectTransformUtility.RectangleContainsScreenPoint(safeTrigger, transform.position);

        if (nowInside && !isInside)
        {
            isInside = true;

            if (openButtonRect != null)
                openButtonRect.position = transform.position;

            openButton.SetActive(true);
            jodiManager.ChangeJodiExpression(JodiState.Confused);
        }
        else if (!nowInside && isInside)
        {
            isInside = false;

            openButton.SetActive(false);
            jodiManager.ChangeJodiExpression(JodiState.Poker);
        }
    }

    public void MarkCollected()
    {
        collected = true;
        openButton.SetActive(false);

        if (jodiManager != null)
            jodiManager.ChangeJodiExpression(JodiState.Poker);
    }

    public void ResetDetector()
    {
        collected = false;
        isInside = false;
        openButton.SetActive(false);

        if (jodiManager != null)
            jodiManager.ChangeJodiExpression(JodiState.Poker);
    }
}