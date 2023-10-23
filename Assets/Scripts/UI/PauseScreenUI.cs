using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PauseScreenUI : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    private Button continueButton;
    private Button mainMenuButton;

    private void Start()
    {
        pauseButton = transform.parent.transform.parent.Find("PauseButton").GetComponent<Button>();
        continueButton = transform.Find("ContinueButton").GetComponent<Button>();
        mainMenuButton = transform.Find("EndLevelButton").GetComponent<Button>();;
        pauseButton.onClick.AddListener(PauseGame);
        continueButton.onClick.AddListener(ContinueGame);
        mainMenuButton.onClick.AddListener(MainMenu);
        gameObject.SetActive(false);
        
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void MainMenu()
    {
        Time.timeScale = 1;
        FadeCanvasUI.Instance.FaderLoadString("MainMenu");
    }
}
