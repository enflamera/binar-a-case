using UnityEngine;

public class ForensicDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [Header("Popup Sprites")]
    public Sprite neckPopup;
    public Sprite handPopup;
    public Sprite fingerPopup;
    public Sprite shirtPopup;
    public Sprite shoulderPopup;

    [Header("TV Sprites")]
    public Sprite neckTV;
    public Sprite handTV;
    public Sprite fingerTV;
    public Sprite shirtTV;
    public Sprite shoulderTV;

    void OnEnable()
    {
        if (dialogueManager != null)
            dialogueManager.OnDialogueEnded += UnlockForensic;
    }

    void OnDisable()
    {
        if (dialogueManager != null)
            dialogueManager.OnDialogueEnded -= UnlockForensic;
    }

    void UnlockForensic()
    {
        ForensicManager.Instance?.UnlockForensicMenu();

        MenuTab menuTab = FindFirstObjectByType<MenuTab>();

        if (menuTab != null)
            menuTab.UpdateForensicLock();
    }

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(true, "Dok, apa hasil pemeriksaan korban sudah keluar?"),

            new DialogueData(false, "Sudah. Sekilas korban memang terlihat meninggal akibat jeratan di leher."),
            new DialogueData(false, "Namun ada beberapa temuan yang membuat kasus ini tidak sesederhana dugaan bunuh diri."),

            new DialogueData(true, "Temuan seperti apa?"),

            new EvidenceDialogueData(
                false,
                "Bekas jeratan memang ada, tetapi terdapat juga tanda-tanda kekerasan lain pada tubuh korban.",
                neckPopup,
                neckTV,
                "Bekas Jeratan",
                "bukti_leher"
            ),

            new DialogueData(false, "Lihat pergelangan tangan korban."),

            new EvidenceDialogueData(
                false,
                "Memar ini menunjukkan korban sempat dicengkeram dengan kuat oleh seseorang.",
                handPopup,
                handTV,
                "Bekas Cengkeraman",
                "bukti_cengkeram"
            ),

            new DialogueData(false, "Kami juga menemukan serpihan kulit manusia di bawah kuku korban."),

            new EvidenceDialogueData(
                false,
                "Korban kemungkinan mencakar pelaku saat berusaha melawan.",
                fingerPopup,
                fingerTV,
                "Kulit di Bawah Kuku",
                "bukti_kuku"
            ),

            new DialogueData(false, "Pakaian korban robek di bagian kerah dan lengan."),
            new DialogueData(false, "Pola sobekannya menunjukkan adanya tarikan paksa dari depan."),

            new EvidenceDialogueData(
                false,
                "Selain itu terdapat serat kain asing yang diduga berasal dari pakaian pelaku.",
                shirtPopup,
                shirtTV,
                "Serat Kain Asing",
                "bukti_baju"
            ),

            new DialogueData(false, "Korban juga mengalami memar akibat hantaman benda tumpul di bagian pundak."),

            new EvidenceDialogueData(
                false,
                "Luka ini tidak mematikan, tetapi cukup kuat untuk melemahkan korban.",
                shoulderPopup,
                shoulderTV,
                "Memar Pundak",
                "bukti_pundak"
            ),

            new DialogueData(true, "Jadi korban sempat melakukan perlawanan sebelum meninggal?"),

            new DialogueData(false, "Benar."),
            new DialogueData(false, "Semua bukti menunjukkan adanya kontak fisik dan pergulatan sebelum kematian terjadi."),
            new DialogueData(false, "Seseorang berusaha menahan, menyerang, dan kemungkinan memaksa korban."),

            new DialogueData(true, "Saya mengerti."),
            new DialogueData(true, "Maaf sudah merepotkan Anda dengan pemeriksaan yang mendadak."),

            new DialogueData(false, "Tidak masalah. Itu sudah menjadi tugas saya."),

            new DialogueData(true, "Tetap saja, terima kasih."),
            new DialogueData(true, "Anda berhasil menyelesaikan seluruh analisis laboratorium kurang dari 24 jam."),
            new DialogueData(true, "Informasi ini akan sangat membantu penyelidikan kami."),

            new DialogueData(false, "Semoga kalian bisa menemukan pelakunya, Detektif.")
        };

        dialogueManager.SetDialogue(data);
    }
}