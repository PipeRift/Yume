using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {

    public void LoadLevel(int l) {
        SceneManager.LoadScene(l);
    }

    public void LoadLevel(string l)
    {
        SceneManager.LoadScene(l);
    }


    public void LoadLevelAsync(int l)
    {
        SceneManager.LoadSceneAsync(l);
    }

    public void LoadLevelAsync(string l)
    {
        SceneManager.LoadSceneAsync(l);
    }

    public void Quit() {
        Application.Quit();
    }
}
