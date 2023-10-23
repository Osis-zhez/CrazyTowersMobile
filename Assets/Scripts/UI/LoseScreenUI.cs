using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenUI : MonoBehaviour
{
    private readonly EndGameManager endGameManager;
    private readonly FadeCanvasUI fadeCanvasUI;

    private Button endLevelButton;
    private Button retryButton;

    public LoseScreenUI()
    {
        this.endGameManager = EndGameManager.Instance;
        this.fadeCanvasUI = FadeCanvasUI.Instance;
    }

    private void Awake()
    {
        endLevelButton = transform.Find("EndLevelButton").GetComponent<Button>();
        retryButton = transform.Find("RetryButton").GetComponent<Button>();
    }

    private void Start()
    {
        endLevelButton.onClick.AddListener(() => {
            MainMenu();
        });
        retryButton.onClick.AddListener(() => {
            ReloadLevel();
        });
        EndGameManager.Instance.OnLoseGame += EndGameManager_OnLoseGame;
        FadeCanvasUI.Instance.OnStartLevel += FadeCanvasUI_OnStartLevel;
        gameObject.SetActive(false);
    }

    private void EndGameManager_OnLoseGame(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }

    private void FadeCanvasUI_OnStartLevel(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        FadeCanvasUI.Instance.FaderLoadString("MainMenu");
    }
    
    public void ReloadLevel()
    {
        Time.timeScale = 1;
        FadeCanvasUI.Instance.FaderLoadInt(FadeCanvasUI.Instance.GetCurrentLevelIndex());
    }
}
