using UnityEngine;

public class PartnerDialogue3 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        DialogueData[] data =
        {
            new DialogueData(false, "Gimana hasilnya?"),
            new DialogueData(true, "Cocok sama CCTV. Dia sering dateng ke kontrakan buat nagih uang keamanan atau apalah itu."),
            new DialogueData(true, "Emang tabiat dia juga, preman pungli. Satu pasar juga dia tagihin."),
            new DialogueData(false, "Jadi sesuai dugaan kita."),
            new DialogueData(true, "Iya."),
            new DialogueData(false, "Dan di CCTV juga gak ada hal baru lagi."),
            new DialogueData(false, "Kayaknya kita harus langsung masuk TKP."),
            new DialogueData(true, "Oke. Kita cek langsung di dalam.")
        };

        dialogueManager.SetDialogue(data);
    }
}