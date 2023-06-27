using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public Button yourButton;

    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        SceneManager.LoadScene("GameSelect");
    }
}