using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class chosseScene : MonoBehaviour
{
    public Button stage1Button;
    public Button boss1Button;
    public Button stage2Button;
    public Button boss2Button;
    public Button stage3Button;
    public Button boss3Button;

    void Start()
    {
        stage1Button.onClick.AddListener(() => LoadScene("MapStage1"));
        boss1Button.onClick.AddListener(() => LoadScene("BossStage1"));
        stage2Button.onClick.AddListener(() => LoadScene("MapStage2"));
        boss2Button.onClick.AddListener(() => LoadScene("BossStage2"));
        stage3Button.onClick.AddListener(() => LoadScene("MapStage3"));
        boss3Button.onClick.AddListener(() => LoadScene("BossStage3"));
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
