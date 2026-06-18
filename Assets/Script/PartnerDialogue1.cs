using UnityEngine;

public class PartnerDialogue1 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(false, "Pak Dani, saya baru selesai memeriksa rekaman CCTV di sekitar kontrakan."),
            new DialogueData(true, "Ada sesuatu yang menarik?"),
            new DialogueData(false, "Ada satu orang yang cukup sering terlihat mengunjungi Binar. Namanya Bagas."),
            new DialogueData(true, "Siapa dia?"),
            new DialogueData(false, "Preman setempat. Biasanya berkeliaran di sekitar gang kontrakan."),
            new DialogueData(true, "Hubungannya dengan korban?"),
            new DialogueData(false, "Menurut warga, Bagas sering menagih uang keamanan ke penghuni kontrakan. Bisa dibilang pungutan liar."),
            new DialogueData(true, "Dan dia sering mendatangi Binar?"),
            new DialogueData(false, "Akhir-akhir ini iya. Dari rekaman CCTV, selama sekitar seminggu terakhir dia hampir setiap hari terlihat datang ke kamar Binar."),
            new DialogueData(true, "Bagaimana reaksi Binar saat itu?"),
            new DialogueData(false, "Menariknya, Binar sama sekali tidak terlihat takut."),
            new DialogueData(true, "Maksudmu?"),
            new DialogueData(false, "Setiap kali membukakan pintu, yang terjadi justru sebaliknya. Binar terlihat memarahi Bagas karena sering membuat keributan di lingkungan kontrakan."),
            new DialogueData(true, "Jadi bukan Bagas yang menekan korban?"),
            new DialogueData(false, "Setidaknya dari rekaman CCTV, tidak terlihat seperti itu."),
            new DialogueData(true, "Ada hal lain?"),
            new DialogueData(false, "Ada satu rekaman yang cukup menarik. Bagas terus berbicara di depan pintu kamar sampai akhirnya Binar keluar dan menendang kakinya."),
            new DialogueData(true, "Menendang?"),
            new DialogueData(false, "Iya. Dan setelah itu Bagas langsung pergi."),
            new DialogueData(true, "Berarti Binar memang bukan tipe orang yang mudah diintimidasi."),
            new DialogueData(false, "Saya juga berpikir begitu. Dari sikapnya, dia terlihat cukup berani menghadapi orang yang mengganggunya."),
            new DialogueData(true, "Kalau begitu saya perlu bicara langsung dengan Bagas."),
            new DialogueData(false, "Kebetulan dia sedang berada di ujung gang dekat pos ronda. Beberapa warga bilang dia hampir selalu nongkrong di sana setiap siang."),
            new DialogueData(true, "Baik. Saya akan menemuinya sekarang."),
            new DialogueData(false, "Saya akan tetap memeriksa rekaman CCTV lainnya. Siapa tahu ada sesuatu yang terlewat.")
        };

        dialogueManager.SetDialogue(data);
    }
}