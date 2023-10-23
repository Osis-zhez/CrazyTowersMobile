using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeisupUI : MonoBehaviour
{
    private Button nextLevelButton;
    private Button endLevelButton;

    private readonly EndGameManager endGameManager;
    private readonly FadeCanvasUI fadeCanvasUI;
    private Timer timerUI;

    public TimeisupUI()
    {
        this.endGameManager = EndGameManager.Instance;
        this.fadeCanvasUI = FadeCanvasUI.Instance;
    }

    private void Awake()
    {
        timerUI = FindObjectOfType<Timer>();
        nextLevelButton = transform.Find("NextLevelButton").GetComponent<Button>();
        endLevelButton = transform.Find("EndLevelButton").GetComponent<Button>();
    }

    private void Start()
    {
        nextLevelButton.onClick.AddListener(ContinueRewardedGame);
        endLevelButton.onClick.AddListener(() => {
            EndGameManager.Instance.LoseGame();
        });
        FadeCanvasUI.Instance.OnStartLevel += FadeCanvasUI_OnStartLevel;
        EndGameManager.Instance.OnTimeIsUp += EndGameManager_OnTimeIsUp;
        EndGameManager.Instance.OnLoseGame += EndGameManager_OnLoseGame;
        gameObject.SetActive(false);
    }

    private void FadeCanvasUI_OnStartLevel(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void EndGameManager_OnLoseGame(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void EndGameManager_OnTimeIsUp(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        FadeCanvasUI.Instance.FaderLoadString("MainMenu");
    }
    
    public void ContinueRewardedGame()
    {
        Time.timeScale = 1;
        timerUI.SetTimer(1, 0);
        gameObject.SetActive(false);
    }
}
