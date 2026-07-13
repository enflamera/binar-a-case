using UnityEngine;

public class PartnerDialogue7 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(false, "Pak Dani... ini bukan orangnya. Kita salah tangkap."),
            new DialogueData(true, "Apa? Nggak mungkin, aku yakin dengan buktinya."),
            new DialogueData(false, "Bukti yang kita kumpulkan ternyata belum cukup kuat untuk memastikan pelaku sebenarnya."),
            new DialogueData(true, "Aku... benar-benar gagal kali ini."),
        };

        dialogueManager.SetDialogue(data);
    }
}