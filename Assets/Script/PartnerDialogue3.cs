using UnityEngine;

public class PartnerDialogue4 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(false, "Pak Dani, hasil dari laboratorium forensik sudah selesai keluar."),
            new DialogueData(false, "Sekarang bapak sudah bisa langsung menemui dokter Nana."),
            new DialogueData(true, "Oke, sip deh. Thank you infonya, Vin."),
            new DialogueData(true, "Oiya, kayaknya habis ini kita perlu mampir ke Kementerian Bidang Infrastruktur dan Pembangunan Kewilayahan."),
            new DialogueData(false, "Loh? Sejak kapan kita punya urusan sama kementerian?"),
            new DialogueData(true, "Aku baru nemu informasi soal korban."),
            new DialogueData(true, "Korban ternyata lagi ngerjain liputan tentang dugaan korupsi Baskoro Edi Haris yang itu lho, Vin."),
            new DialogueData(false, "Serius? Dari mana bapak dapet itu?"),
            new DialogueData(true, "Dari catatan kerjanya. Korban jadi penanggung jawab pemberitaan kasus itu."),
            new DialogueData(true, "Bahkan harusnya dia wawancara Baskoro hari Selasa besok."),
            new DialogueData(false, "Jadi sekarang kita mau cari tahu sejauh apa hubungan korban sama pihak kementerian?"),
            new DialogueData(true, "Kurang lebih begitu."),
            new DialogueData(true, "Aku nggak mau langsung loncat ke kesimpulan, tapi waktunya terlalu pas buat diabaikan."),
            new DialogueData(false, "Oke. Habis ini kita ke sana aja."),
        };

        dialogueManager.SetDialogue(data);
        dialogueManager.OnDialogueEnded += OnDialogueComplete;
    }

    private void OnDialogueComplete()
    {
        dialogueManager.OnDialogueEnded -= OnDialogueComplete;

        if (SuspectManager.Instance != null)
        {
            SuspectManager.Instance.UnlockSuspect(2);
            SuspectManager.Instance.pendingPopupSuspect = 2;
        }
    }
}