using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance;
    public GameObject gameOverPanel;


    void Start()
    {
        Instance = this;
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartLevel(string levelName)
    {
        //SceneManager.LoadScene(levelName);
    }
}
