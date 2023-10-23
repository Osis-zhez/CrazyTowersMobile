using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Analytics;

public class FadeCanvasUI : MonoBehaviour
{
    public static FadeCanvasUI Instance;

    public EventHandler OnStartLevel;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingBar;
    [SerializeField] private float changeValue;
    [SerializeField] private float waitTime;
    [SerializeField] private bool fadeStarted = false;

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
        canvasGroup.alpha = 1;
    }


    private void Start() 
    {
        StartCoroutine(FadeIn());
    }

    public void FaderLoadString(string levelName)
    {
        StartCoroutine(FadeOutString(levelName));
    }

    public void FaderLoadInt(int levelIndex)
    {
        StartCoroutine(FadeOutInt(levelIndex));
    }

    public int GetCurrentLevelIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator FadeIn()
    {
        loadingScreen.SetActive(false);
        OnStartLevel?.Invoke(this, EventArgs.Empty);
        fadeStarted = false;
        while (canvasGroup.alpha > 0)
        {
            // if (fadeStarted)
            //     yield break;
            canvasGroup.alpha -= changeValue;
            yield return new WaitForSeconds(waitTime);
        }

    }

    IEnumerator FadeOutString(string levelName)
    {
        if (fadeStarted)
            yield break;
        fadeStarted = true;
        GameAnalytics.Instance.LogEvent($"Start_{levelName}");
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }
        if (levelName != "MainMenu" & levelName != "LevelSelection")
            PlayerPrefs.SetString("lastLevel", levelName);
        Debug.Log(PlayerPrefs.GetString("lastLevel", "level1"));
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName);
        asyncOperation.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        loadingBar.fillAmount = 0;
        while (asyncOperation.isDone == false)
        {
            loadingBar.fillAmount = asyncOperation.progress / 0.9f;
            if (asyncOperation.progress == 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOutInt(int levelIndex)
    {
        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }
        // PlayerPrefs.SetString("lastLevel", $"level {levelIndex - 2}");
        if (levelIndex >= 3)
        {
            PlayerPrefs.SetInt("lastLevelIndex", levelIndex - 1);
            GameAnalytics.Instance.LogEvent($"Start_level_{levelIndex - 1}");
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelIndex);
        asyncOperation.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        loadingBar.fillAmount = 0;
        while (asyncOperation.isDone == false)
        {
            loadingBar.fillAmount = asyncOperation.progress / 0.9f;
            if (asyncOperation.progress == 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
        StartCoroutine(FadeIn());
    }
}
