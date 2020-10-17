using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

}
