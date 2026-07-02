using UnityEngine;

public class WitnessTestimony : MonoBehaviour
{
    [SerializeField] private Sprite testimonyPage;

    private bool saved;

    public void SaveTestimony()
    {
        if (saved)
            return;

        if (NoteManager.Instance == null)
            return;

        if (testimonyPage == null)
            return;

        NoteManager.Instance.AddTestimony(testimonyPage);

        saved = true;
    }

    public void ResetTestimony()
    {
        saved = false;
    }

    public bool IsSaved()
    {
        return saved;
    }
}