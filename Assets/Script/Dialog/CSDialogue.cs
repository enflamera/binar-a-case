using UnityEngine;

public class CSDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public void StartDialogue()
    {
        DialogueData[] data =
        {
            new DialogueData(true, "Selamat siang. Saya Detektif Dani dari kepolisian."),
            new DialogueData(false, "Selamat siang, Pak. Ada yang bisa saya bantu?"),
            new DialogueData(true, "Kami sedang melakukan penyelidikan dan perlu mengakses kantor Pak Baskoro Edi Haris."),
            new DialogueData(false, "Oh, apakah ini terkait wartawan yang seharusnya mewawancarai Pak Baskoro minggu depan?"),
            new DialogueData(true, "Anda tahu soal itu?"),
            new DialogueData(false, "Saya hanya sempat melihat namanya di jadwal kunjungan."),
            new DialogueData(true, "Baik. Informasi itu cukup membantu."),
            new DialogueData(false, "Apakah bapak sudah memiliki izin pemeriksaan?"),
            new DialogueData(true, "Sudah. Semua dokumen ada di sini."),
            new DialogueData(false, "Baik, Pak. Sebentar saya periksa."),
            new DialogueData(false, "Sudah sesuai."),
            new DialogueData(false, "Namun Pak Baskoro sedang tidak berada di kantor saat ini."),
            new DialogueData(true, "Tidak masalah. Kami hanya perlu memeriksa ruang kerjanya."),
            new DialogueData(false, "Baik, Pak."),
            new DialogueData(false, "Kantor beliau berada di lantai lima, koridor sebelah barat."),
            new DialogueData(false, "Saya akan menghubungi petugas keamanan agar bapak dapat masuk."),
            new DialogueData(true, "Terima kasih."),
            new DialogueData(false, "Semoga penyelidikannya lancar, Pak."),
            new DialogueData(true, "Terima kasih.")
        };

        dialogueManager.SetDialogue(data);
    }
}