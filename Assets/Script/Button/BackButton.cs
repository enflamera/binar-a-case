using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public string previousScene;

    public void GoBack()
    {
        SceneManager.LoadScene(previousScene);
    }
}