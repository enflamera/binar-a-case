using UnityEngine;

public class PengadilanDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(false, "Sidang pengadilan kasus ini resmi dibuka."),
            new DialogueData(false, "Berdasarkan seluruh bukti yang telah dikumpulkan, terdakwa Jodi didakwa sebagai pelaku dalam kasus ini."),
            new DialogueData(false, "Bukti forensik dan hasil interogasi menunjukkan keterkaitan langsung terdakwa dengan kejadian ini."),
            new DialogueData(true, "Itu tidak benar! Aku tidak melakukan apa-apa!"),
            new DialogueData(false, "Semua fakta yang ditemukan di lapangan mengarah pada satu kesimpulan yang sama, Saudara Jodi."),
            new DialogueData(true, "Kalian salah! Harusnya kalian mencurigai Baskoro, bukan saya!"),
            new DialogueData(false, "Baskoro dalam pengawasan KPK."),
            new DialogueData(true, "Dia yang menyuruh saya! Dia yang tahu semuanya, kenapa tidak diselidiki?!"),
            new DialogueData(false, "Yang ada di hadapan pengadilan hari ini adalah bukti-bukti yang mengarah padamu, bukan tuduhan tanpa dasar."),
            new DialogueData(true, "..."),
            new DialogueData(false, "Majelis hakim telah mempertimbangkan seluruh bukti dan keterangan yang diajukan dalam persidangan ini."),
            new DialogueData(false, "Dengan ini, pengadilan menyatakan terdakwa Jodi terbukti secara sah dan meyakinkan bersalah atas kasus ini."),
            new DialogueData(false, "Terdakwa dijatuhi hukuman pidana penjara selama 15 tahun."),
            new DialogueData(true, "Tidak... tidak mungkin..."),
            new DialogueData(false, "Sidang dinyatakan ditutup."),
        };

        dialogueManager.SetDialogue(data);
    }
}