using UnityEngine;

public class PartnerDialogue1 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(false, "Pak Dani, aku baru selesai periksa rekaman CCTV di kontrakan."),
            new DialogueData(true, "Ada yang menarik?"),
            new DialogueData(false, "Ada satu orang yang sering keliatan mampir ke kontrakan Binar. Namanya Bagas, kata Bu Lestari."),
            new DialogueData(true, "Siapa lagi?"),
            new DialogueData(false, "Preman setempat. Biasanya keliaran di kecamatan ini."),
            new DialogueData(true, "Hubungannya sama korban apa?"),
            new DialogueData(false, "Menurut warga, Bagas sering nagih uang keamanan ke penghuni kontrakan. Bisa dibilang pungli."),
            new DialogueData(false, "Bukan penghuni kontrakan doang, semua warga juga katanya dia tagihin"),
            new DialogueData(true, "Jadi dia sering mampir ke Binar?"),
            new DialogueData(false, "Akhir-akhir ini iya. Dari rekaman CCTV, sekitar seminggu terakhir dia hampir setiap hari mampir ke Binar."),
            new DialogueData(true, "Gimana reaksi Binar pas itu?"),
            new DialogueData(false, "Fun fact, Pak. Binar sama sekali nggak keliatan takut."),
            new DialogueData(false, "Setiap kali bukain pintu, yang kejadian justru sebaliknya. Binar keliatan lagi marahin Bagas soalnya dia sering buat ribut di kontrakan."),
            new DialogueData(true, "Jadi bukan Bagas yang neken korban?"),
            new DialogueData(false, "Ya, setidaknya, dari rekaman CCTV, nggak keliatan gitu."),
            new DialogueData(true, "Ada hal lain, Vin?"),
            new DialogueData(false, "Ada satu rekaman yang cukup menarik. Bagas terus bicara di depan pintu kamar Binar sampai akhirnya Binar keluar dan nendang kakinya."),
            new DialogueData(true, "Hah? Serius?"),
            new DialogueData(false, "Iya, Pak. Terus habis itu Bagas langsung pergi."),
            new DialogueData(true, "Berarti Binar memang bukan tipe orang yang gampang diintimidasi."),
            new DialogueData(false, "Aku juga mikir begitu. Dari sikapnya, dia keliatan berani ngadepin orang yang ganggu dia."),
            new DialogueData(true, "Kalau begitu aku perlu ngomong langsung sama Bagas, nih."),
            new DialogueData(false, "Kebetulan dia lagi ada di ujung gang deket pos ronda. Warga bilang dia hampir tiap hari nongkrong di sana kalo siang atau sore."),
            new DialogueData(true, "Sip. Good job, Vin."),
            new DialogueData(false, "Aku bakalan cek rekaman CCTV lainnya. Siapa tahu ada sesuatu yang kelewat.")
        };

        dialogueManager.SetDialogue(data);
    }
}