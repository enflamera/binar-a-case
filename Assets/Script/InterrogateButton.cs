using UnityEngine;
using UnityEngine.SceneManagement;

public class InterrogateButton : MonoBehaviour
{
    public string sceneName;
    public TransisiLokal transisi;
    
    public void StartInterrogation()
    {
        if (transisi != null)
        {
            transisi.PindahScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}