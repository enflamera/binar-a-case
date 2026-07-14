using UnityEngine;

public class PartnerDialogue6 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(true, "Vin, kamu tahu siapa Jodi? Sekarang dia di mana?"),
            new DialogueData(false, "Brigadir Jodi, Pak. Anggota Unit Polantas yang pertama kali datang ke lokasi setelah warga melapor."),
            new DialogueData(true, "Berarti dia salah satu petugas yang pertama kali lihat kondisi TKP?"),
            new DialogueData(false, "Iya, Pak. Dia sempat bantu mengamankan area sebelum tim Inafis datang."),
            new DialogueData(true, "Sekarang dia di mana?"),
            new DialogueData(false, "Harusnya di TKP, Pak. Katanya lagi jaga perimeter hari ini.")
        };

        dialogueManager.SetDialogue(data);
        dialogueManager.OnDialogueEnded += OnDialogueComplete;
    }

    private void OnDialogueComplete()
    {
        dialogueManager.OnDialogueEnded -= OnDialogueComplete;

        if (SuspectManager.Instance != null)
        {
            SuspectManager.Instance.UnlockSuspect(3);
            SuspectManager.Instance.pendingPopupSuspect = 3;
        }
    }
}