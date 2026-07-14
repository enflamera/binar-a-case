using UnityEngine;

public class PartnerDialogue5 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(true, "Vin, aku baru selesai ngobrol sama Jodi."),
            new DialogueData(true, "Ada yang aneh. Dia keliatan nyembunyiin sesuatu."),
            new DialogueData(false, "Jelas-jelas ada foto dia sama Baskoro di kantornya. Dia pasti punya hubungan sama Baskoro."),
            new DialogueData(true, "Ya kalo nggak apa coba? Masa kita mengibaratkan Baskoro Jodi sekelas Prabowo Teddy???"),
            new DialogueData(false, "Masuk akal nggak sih, Vin? Haha."),
            new DialogueData(false, "By the way, dia mau buka mulut nggak, Pak?"),
            new DialogueData(true, "Nggak. Dia nolak cerita soal apa pun yang nyangkut Baskoro."),
            new DialogueData(true, "Tapi dia sempat keceplosan satu hal yang bikin aku kepikiran."),
            new DialogueData(true, "Dia bilang Binar punya bekas luka trauma fisik di bahu kiri."),
            new DialogueData(false, "Bukannya itu hasil pemeriksaan forensik ya?"),
            new DialogueData(true, "Iya."),
            new DialogueData(true, "Laporan laboratorium forensik belum pernah dikirim ke Unit Polantas, dan nggak akan pernah."),
            new DialogueData(false, "Berarti Jodi nggak mungkin tahu informasi itu dari jalur resmi..."),
            new DialogueData(true, "Persis."),
            new DialogueData(true, "Artinya ada seseorang yang kasih dia informasi, atau dia memang terlibat lebih jauh dari yang dia akui."),
            new DialogueData(true, "Aku bakal cari tahu dari mana dia dapet informasi itu, kalo bisa lho.")
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