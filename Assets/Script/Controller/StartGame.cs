using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string nextScene;
    public TransisiLokal transisi;

    public void StartGameButton()
    {
        transisi.PindahScene(nextScene);
    }
}