using UnityEngine;

public class BagasDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(true, "Permisi, Bagas? Saya Dani, penyidik. Saya ingin bertanya beberapa hal soal Binar jika Anda tidak keberatan."),
            new DialogueData(false, "Bertanya mulu dah. Dari tadi orang pada nyari gue. Kalau gue keberatan gimane?"),
            new DialogueData(true, "Gue gak peduli lu keberatan atau kagak. Lu mau kerja sama jawab pertanyaan gue, atau gue angkut lu ke kantor hari ini juga gara-gara kasus pungli duit keamanan?"),
            new DialogueData(false, "Buset, Bang, jangan main angkut aje. Iya dah iya, gue jawab. Polisi sekarang mulutnye pedes bener."),
            new DialogueData(true, "Bagus. Lu sering ke kontrakan Binar. Hubungan kalian gimana?"),
            new DialogueData(false, "Berantem mulu, tuh cewek kaga ada takutnya. Di CCTV pas gue ketok pintunya kekerasan, gue malah ditendang."),
            new DialogueData(true, "Pernah ada momen kalian ngobrol baik-baik?"),
            new DialogueData(false, "Pernah pas motornya mogok 3 hari lalu (Senin). Gue bantuin dorong ke bengkel."),
            new DialogueData(true, "Ada obrolan penting sepanjang jalan?"),
            new DialogueData(false, "Dia ngomel ngatain pejabat korup yang baru-baru ini, siapa ya, lupa gue..."),
            new DialogueData(true, "Menko Bidang Infrastruktur dan Pembangunan Kewilayahan? Baskoro Edi Haris?"),
            new DialogueData(true, "Nah! Iya, iya bener."),
            new DialogueData(true, "Setelah dari bengkel, dia pulang sama siapa?"),
            new DialogueData(false, "Gue tawarin anter tapi ditolak. Dia milih dijemput temen ceweknya yang bela-belain dateng jauh dari Depok."),
            new DialogueData(true, "Oke. Berarti Binar ini emang keras kepala."),
            new DialogueData(true, "Itu aja dulu, jangan kabur lu kalo gue mau nanya lagi."),
            new DialogueData(false, "Udah kepala batu, rada gila lagi. Santai, gue tiap hari nongkrong di sini kok.")
        };

        dialogueManager.SetDialogue(data);
    }
}