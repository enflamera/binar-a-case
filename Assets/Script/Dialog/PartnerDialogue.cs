using UnityEngine;

public class PartnerDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(false, "Selamat sore, Pak Dan."),
            new DialogueData(true, "Kasus baru lagi?"),
            new DialogueData(false, "Iya. Laporan masuk sekitar satu jam yang lalu."),
            new DialogueData(true, "Korbannya?"),
            new DialogueData(false, "Perempuan. Ditemukan meninggal di kamar kontrakannya."),
            new DialogueData(true, "Siapa yang melaporkan?"),
            new DialogueData(false, "Brigadir Jodi. Dia petugas pertama yang nerima laporan."),
            new DialogueData(false, "Kasusnya dialihin ke kita soalnya yang lain sibuk ngurus kasus lain."),
            new DialogueData(true, "Terus kamu terima, Vin?"),
            new DialogueData(false, "Iya, hehe."),
            new DialogueData(true, "Yaudah, ayo ke TKP."),
            new DialogueData(false, "Aku sudah nyiapin akses ke lokasi. Kita berangkat sekarang.")
        };

        dialogueManager.SetDialogue(data);
    }
}