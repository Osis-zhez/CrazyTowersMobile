using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager Instance;

    public string lvlUnlock = "levelUnlock";

    public EventHandler OnWin;
    public EventHandler OnTimeIsUp;
    public EventHandler OnLoseGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1;

        AppodealCallbacks.Interstitial.OnLoaded += OnInterstitialLoaded;
    }

    private void OnInterstitialLoaded(object sender, AdLoadedEventArgs e)
    {
        
    }

    public void WinGame()
    {   
        int nextLevel = SceneManager.GetActiveScene().buildIndex;
        if (nextLevel > PlayerPrefs.GetInt(lvlUnlock, 0))
        {
            PlayerPrefs.SetInt(lvlUnlock, nextLevel);
        }
        Time.timeScale = 0;
        OnWin?.Invoke(this, EventArgs.Empty);
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        OnLoseGame?.Invoke(this, EventArgs.Empty);
    }
    
    public void TimeIsUp()
    {
        Time.timeScale = 0;
        OnTimeIsUp?.Invoke(this, EventArgs.Empty); 
    }

    public void AdLoseGame()
    {

    }

}
