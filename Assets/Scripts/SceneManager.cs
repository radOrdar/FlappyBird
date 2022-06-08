using UnityEngine;

public class SceneManager : SingletonMonoBehaviour<SceneManager> {

    public void Restart() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
}