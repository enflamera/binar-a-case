using UnityEngine;
using UnityEngine.EventSystems;

public class ExamineObject : MonoBehaviour, IPointerClickHandler
{
    public string sceneName;

    public GameObject panelToOpen;

    public ExamineManager manager;
    public EvidenceUI evidenceUI;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.ShowButton(
            transform.position + new Vector3(0, 100, 0),
            sceneName,
            transform,
            panelToOpen
        );

        if (evidenceUI != null)
            evidenceUI.Dim();
    }

    public void SetSelected()
    {
        transform.localScale = originalScale * 1.03f;
    }

    public void SetNormal()
    {
        transform.localScale = originalScale;
    }
}