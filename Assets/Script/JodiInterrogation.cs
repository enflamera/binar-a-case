using UnityEngine;

public class JodiInterrogation : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [Header("Evidence Popup")]
    public Sprite photoPopup;
    public Sprite sdCardPopup;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(true, "Brigadir Jodi, saya akan bertanya sekali lagi."),
            new DialogueData(true, "Apa yang sebenarnya terjadi malam itu?"),

            new DialogueData(false, "Saya sudah menjelaskan semuanya, Pak."),
            new DialogueData(false, "Saya hanya datang setelah menerima laporan dari warga."),

            new DialogueData(true, "Masih bersikeras dengan jawaban itu?"),

            new DialogueData(false, "Karena memang itu yang terjadi."),

            new DialogueData(true, "Kalau begitu, lihat bukti ini."),

            new InterrogationEvidenceData(
                true,
                "Foto korban yang ditemukan dalam keadaan sudah disobek, tetapi berhasil kami susun kembali.",
                photoPopup,
                "Foto Sobek"
            ),

            new DialogueData(true, "Korban sedang menyelidiki Baskoro atas dugaan korupsi yang selama ini berusaha ditutupi."),
            new DialogueData(true, "Meskipun KPK menyatakan Baskoro tidak bersalah, Binar tidak pernah menghentikan penyelidikannya."),
            new DialogueData(true, "Dia yakin masih ada sesuatu yang disembunyikan."),

            new DialogueData(false, "..."),
            new DialogueData(false, "Itu belum membuktikan apa pun."),

            new DialogueData(true, "Baik. Berarti kita lanjut ke bukti berikutnya."),

            new InterrogationEvidenceData(
                true,
                "Kartu SD yang ditemukan tersembunyi di lokasi kejadian.",
                sdCardPopup,
                "SD Card"
            ),

            new DialogueData(true, "Binar membawa kamera ke mana pun dia pergi."),
            new DialogueData(true, "Sesaat sebelum meninggal, dia secara refleks menekan tombol rana kameranya."),
            new DialogueData(true, "Foto-foto di kartu SD ini menangkap detik-detik terakhir sebelum kematiannya."),

            new DialogueData(true, "Dan lihat wajah yang tertangkap di foto terakhir."),

            new DialogueData(false, "..."),

            new DialogueData(true, "Itu kamu, Jodi."),

            new DialogueData(false, "..."),
            new DialogueData(false, "......"),

            new DialogueData(true, "Masih mau menyangkal?"),

            new DialogueData(false, "Saya..."),

            new DialogueData(false, "Saya memang datang malam itu sebelum ada laporan warga."),
            new DialogueData(false, "Baskoro yang menyuruh saya menemui Binar."),

            new DialogueData(true, "Kenapa?"),

            new DialogueData(false, "Karena Binar terus mengusut dugaan korupsinya."),
            new DialogueData(false, "Dia satu-satunya wartawan yang belum menyerah."),
            new DialogueData(false, "Walaupun KPK sudah menyatakan Baskoro tidak bersalah, Binar tetap mencari bukti baru."),
            new DialogueData(false, "Baskoro takut semuanya akan terbongkar."),

            new DialogueData(true, "Lalu apa yang terjadi?"),

            new DialogueData(false, "Saya hanya diperintahkan mengambil semua bukti yang dimiliki Binar dan membuatnya berhenti."),
            new DialogueData(false, "Tapi dia melawan."),
            new DialogueData(false, "Dia bahkan sempat mengambil kameranya."),

            new DialogueData(false, "Kami berkelahi."),
            new DialogueData(false, "Saya kehilangan kendali."),

            new DialogueData(false, "Saya mencekiknya... sampai dia berhenti bergerak."),

            new DialogueData(true, "Setelah itu kamu membuat semuanya terlihat seperti bunuh diri."),

            new DialogueData(false, "...Iya."),
            new DialogueData(false, "Saya merapikan TKP agar kematiannya terlihat seperti bunuh diri."),

            new DialogueData(true, "Jadi kamu mengaku telah membunuh Binar atas perintah Baskoro?"),

            new DialogueData(false, "...Saya mengaku."),
            new DialogueData(false, "Saya yang membunuh Binar."),
            new DialogueData(false, "Dan saya melakukannya atas perintah Baskoro.")
        };

        dialogueManager.SetDialogue(data);
    }
}