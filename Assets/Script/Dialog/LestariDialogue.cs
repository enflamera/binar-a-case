using UnityEngine;

public class LestariDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [Header("Expression")]
    public Sprite defaultExpression;
    public Sprite takutExpression;
    public Sprite kagetExpression;

    public void StartDialogue()
    {
        DialogueData[] data =
        {
            new DialogueData(true, "Selamat siang, Bu. Saya Dani, penyidik yang menangani kasus ini.", expression: takutExpression),
            new DialogueData(false, "Iya Nak. Aduh, saya masih gemeteran sampe sekarang. Kagak nyangka dah kejadian begini di kontrakan saya."),
            new DialogueData(true, "Saya mengerti. Saya hanya ingin menanyakan beberapa hal."),
            new DialogueData(false, "Tanya aja Nak. Kalo saya tau pasti saya jawab."),
            new DialogueData(true, "Ibu yang pertama kali menemukan Binar?"),
            new DialogueData(false, "Iya. Tadinya saya cuma mau nagih uang air. Binar udah nunggak dua bulan. Eh pas saya dateng malah nemu yang begituan."),
            new DialogueData(true, "Saat itu kondisi pintu bagaimana?"),
            new DialogueData(false, "Kagak dikunci rapet. Saya ketok-ketok kaga ada jawaban. Pas saya buka dikit... Ya Allah, saya langsung teriak.", expression: defaultExpression),
            new DialogueData(true, "Setelah itu Ibu langsung melapor?"),
            new DialogueData(false, "Iya. Saya langsung lari ke perempatan depan. Untung ketemu polisi yang lagi jaga."),
            new DialogueData(true, "Brigadir Jodi?"),
            new DialogueData(false, "Nah iya, Pak Jodi itu. Saya langsung bilang ada orang gantung diri di kontrakan saya."),
            new DialogueData(true, "Sudah berapa lama Binar tinggal di sini?"),
            new DialogueData(false, "Kurang lebih dua tahun lah. Anaknya masih muda juga. Baru dua puluh empat tahun kalo ga salah."),
            new DialogueData(true, "Selama itu, bagaimana kesehariannya?"),
            new DialogueData(false, "Anaknya baik sih. Sopan juga. Cuma berisik."),
            new DialogueData(true, "Berisik?"),
            new DialogueData(false, "Main gitar mulu, Nak. Kadang nyanyi, kadang rekaman. Suaranya suka kedengeran sampe kamar sebelah."),
            new DialogueData(true, "Dia sering merekam dirinya sendiri?"),
            new DialogueData(false, "Sering banget. Kamera sama gitar mah kaya kaga bisa dipisahin dari dia."),
            new DialogueData(true, "Untuk media sosial?"),
            new DialogueData(false, "Katanya sih buat YouTube. Lumayan tuh yang nonton. Dua ribuan orang ada kali."),
            new DialogueData(true, "Binar bekerja sebagai apa?"),
            new DialogueData(false, "Jurnalis, Nak. Dulu katanya pernah jadi penyiar berita juga, tapi sekarang udah engga."),
            new DialogueData(true, "Kenapa pindah profesi?"),
            new DialogueData(false, "Katanya capek nyiar terus. Jadi milih kerja lapangan aja."),
            new DialogueData(true, "Kalau soal kontrakan ini sendiri, sudah berdiri berapa lama?"),
            new DialogueData(false, "Waduh, udah lama banget. Lima belas tahunan lebih ada kali."),
            new DialogueData(true, "Ada berapa petak kamar di sini?"),
            new DialogueData(false, "Delapan petak."),
            new DialogueData(true, "Semuanya ukuran sama?"),
            new DialogueData(false, "Kagak. Kamar Binar malah paling gede."),
            new DialogueData(true, "Selain ruangan utama, apakah tiap kamar punya akses ke balkon atau halaman belakang untuk menjemur pakaian?"),
            new DialogueData(false, "Kagak ada.", expression: kagetExpression),
            new DialogueData(true, "Tidak ada sama sekali?"),
            new DialogueData(false, "Kagak ada. Paling jemur depan kamar masing-masing aja."),
            new DialogueData(true, "Baik. Saya hanya memastikan denah bangunan."),
            new DialogueData(false, "Oh gitu. Yaudah Nak, semoga cepet ketemu jawabannya.", expression: defaultExpression),
            new DialogueData(true, "Terima kasih atas waktunya, Bu."),
            new DialogueData(false, "Sama-sama. Tolong ya Nak, cari tau sebenernya ada apa sama Binar.")
        };

        dialogueManager.SetDialogue(data);
        dialogueManager.OnDialogueEnded += OnDialogueComplete;
    }

    private void OnDialogueComplete()
    {
        dialogueManager.OnDialogueEnded -= OnDialogueComplete;

        if (SuspectManager.Instance != null)
        {
            SuspectManager.Instance.UnlockSuspect(0, false);
        }
    }
}