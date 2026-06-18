using UnityEngine;
using UnityEngine.EventSystems;

public class WardrobeTrigger : MonoBehaviour, IPointerClickHandler
{
    public ExamineManager manager;

    public Transform wardrobeVisual;

    public string sceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.ShowButton(
            transform.position + new Vector3(0,100,0),
            sceneName,
            wardrobeVisual
        );
    }
}