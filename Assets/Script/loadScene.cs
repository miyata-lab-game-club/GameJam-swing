using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("NewGame");
    }
}
