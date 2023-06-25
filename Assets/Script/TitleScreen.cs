using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public string sceneToLoad;  // インスペクタからロードするシーンを指定します

    // ゲームをスタートする
    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}