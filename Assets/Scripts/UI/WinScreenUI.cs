using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour
{
    private Button endLevelButton;
    private Button nextLevelButton;

    private void Awake()
    {
        endLevelButton = transform.Find("EndLevelButton").GetComponent<Button>();
        nextLevelButton = transform.Find("NextLevelButton").GetComponent<Button>();
    }

    private void Start()
    {
        endLevelButton.onClick.AddListener(() => {
            LoadMainMenu();
        });
        nextLevelButton.onClick.AddListener(() => {
            LoadNextLevel();
        });
        FadeCanvasUI.Instance.OnStartLevel += FadeCanvasUI_OnStartLevel;
        EndGameManager.Instance.OnWin += EndGameManager_OnWin;
        gameObject.SetActive(false);
    }

    private void FadeCanvasUI_OnStartLevel(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void EndGameManager_OnWin(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }


    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        FadeCanvasUI.Instance.FaderLoadString("MainMenu");
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            //Загрузить сцену окончания всей игры, а пока будет загрузка levelSelection
            FadeCanvasUI.Instance.FaderLoadString("MainMenu");
        }
        FadeCanvasUI.Instance.FaderLoadInt(nextSceneIndex);
    }
}
