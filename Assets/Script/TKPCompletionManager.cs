using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TKPCompletionManager : MonoBehaviour
{
    public static TKPCompletionManager Instance;

    [Header("Evidence")]
    public string[] evidenceIDs;

    [Header("Police Line")]
    public RectTransform policeLineLeft;
    public RectTransform policeLineRight;
    public float lineSpeed = 2f;

    [Header("Scene")]
    public string homeSceneName = "Beranda";
    public float delayBeforeLoad = 1f;

    Vector2 leftOpenPos;
    Vector2 rightOpenPos;
    Vector2 leftClosedPos;
    Vector2 rightClosedPos;

    bool animationPlaying = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Instance.RebindSceneReferences(policeLineLeft, policeLineRight);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        CachePoliceLinePositions();
    }

    void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnEvidenceCompleted += CheckCompletion;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnEvidenceCompleted -= CheckCompletion;
    }

    void Start()
    {
        CheckCompletion();
    }

    public void RebindSceneReferences(RectTransform left, RectTransform right)
    {
        policeLineLeft = left;
        policeLineRight = right;
        CachePoliceLinePositions();
    }

    void CachePoliceLinePositions()
    {
        if (policeLineLeft != null && policeLineRight != null)
        {
            leftOpenPos = policeLineLeft.anchoredPosition;
            rightOpenPos = policeLineRight.anchoredPosition;
            leftClosedPos = Vector2.zero;
            rightClosedPos = Vector2.zero;
        }
    }

    void CheckCompletion()
    {
        if (GameManager.Instance.tkp2Completed) return;
        if (animationPlaying) return;

        foreach (string id in evidenceIDs)
        {
            if (!GameManager.Instance.IsEvidenceCompleted(id))
                return;
        }

        animationPlaying = true;
        StartCoroutine(FinishTKP2());
    }

    IEnumerator FinishTKP2()
    {
        if (policeLineLeft != null && policeLineRight != null)
        {
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * lineSpeed;
                float eased = Mathf.SmoothStep(0f, 1f, t);

                policeLineLeft.anchoredPosition = Vector2.Lerp(leftOpenPos, leftClosedPos, eased);
                policeLineRight.anchoredPosition = Vector2.Lerp(rightOpenPos, rightClosedPos, eased);

                yield return null;
            }
        }

        GameManager.Instance.tkp2Completed = true;

        yield return new WaitForSeconds(delayBeforeLoad);

        Instance = null;
        Destroy(gameObject);
        SceneManager.LoadScene(homeSceneName);
    }
}