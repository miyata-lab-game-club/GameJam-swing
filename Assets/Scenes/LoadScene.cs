using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Button newGameButton;  // 初めからのボタン
    public Button continueButton;  // 続きからのボタン

    private void Start()
    {
        // 各ボタンにリスナーを追加
        newGameButton.onClick.AddListener(() => LoadSceneByName("BossStage1"));
        continueButton.onClick.AddListener(() => LoadSceneByName("selectStage"));
    }

    // シーン名を引数にとるメソッド
    private void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}