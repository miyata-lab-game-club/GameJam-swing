using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode
{
    Story,
    StageSelect
}

public class GameManager : MonoBehaviour
{
    public GameMode gameMode = GameMode.Story;
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = (GameManager)FindObjectOfType(typeof(GameManager));
                if (null == instance)
                {
                    Debug.Log("GameManager Instance Error");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SelectStoryMode()
    {
        gameMode = GameMode.Story;
        Debug.Log(gameMode);
    }

    public void SelectStageMode()
    {
        gameMode = GameMode.StageSelect;
        Debug.Log(gameMode);
    }
}