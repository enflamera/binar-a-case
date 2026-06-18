using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransisiLokal : MonoBehaviour
{
    public RectTransform panelAtas;
    public RectTransform panelBawah;
    
    public float yTerbukaAtas = 600f;
    public float yTerbukaBawah = -600f;
    public float yTertutup = 0f; 
    public float durasi = 0.5f;

    void Start()
    {
        StartCoroutine(AnimasiBukaLayar());
    }

    public void PindahScene(string namaSceneTujuan)
    {
        StartCoroutine(AnimasiTutupLayar(namaSceneTujuan));
    }

    IEnumerator AnimasiBukaLayar()
    {
        float timer = 0;
        while (timer < durasi)
        {
            timer += Time.deltaTime;
            float t = timer / durasi;
            t = t * t * (3f - 2f * t);

            float newYAtas = Mathf.Lerp(yTertutup, yTerbukaAtas, t);
            float newYBawah = Mathf.Lerp(yTertutup, yTerbukaBawah, t);

            panelAtas.anchoredPosition = new Vector2(panelAtas.anchoredPosition.x, newYAtas);
            panelBawah.anchoredPosition = new Vector2(panelBawah.anchoredPosition.x, newYBawah);
            yield return null;
        }
    }

    IEnumerator AnimasiTutupLayar(string sceneTujuan)
    {
        float timer = 0;
        while (timer < durasi)
        {
            timer += Time.deltaTime;
            float t = timer / durasi;
            t = t * t * (3f - 2f * t);

            float newYAtas = Mathf.Lerp(yTerbukaAtas, yTertutup, t);
            float newYBawah = Mathf.Lerp(yTerbukaBawah, yTertutup, t);

            panelAtas.anchoredPosition = new Vector2(panelAtas.anchoredPosition.x, newYAtas);
            panelBawah.anchoredPosition = new Vector2(panelBawah.anchoredPosition.x, newYBawah);
            yield return null;
        }

        AsyncOperation operasiAsinkron = SceneManager.LoadSceneAsync(sceneTujuan);
        while (!operasiAsinkron.isDone)
        {
            yield return null;
        }
    }
}