using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Button newGameButton;  // 初めからのボタン
    public Button continueButton;  // 続きからのボタン

    void Start()
    {
        // 各ボタンにリスナーを追加
        newGameButton.onClick.AddListener(() => LoadSceneByName("MapStage1"));
        continueButton.onClick.AddListener(() => LoadSceneByName("choiseScene"));
    }

    // シーン名を引数にとるメソッド
    void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}