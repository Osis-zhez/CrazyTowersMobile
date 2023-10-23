using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System;
using Analytics;
using UnityEngine.SceneManagement;

public class AppodealManager : MonoBehaviour, IRewardedVideoAdListener, IInterstitialAdListener
{
    public static AppodealManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int adTypes = AppodealAdType.RewardedVideo | AppodealAdType.Interstitial ;
        string appKey = "b6667efd0fd36a513bda29b598439b1aef8bab2d69eb7af4";
        AppodealCallbacks.Sdk.OnInitialized += OnInitilizationFinished;
        Appodeal.Initialize(appKey, adTypes);
        Appodeal.SetRewardedVideoCallbacks(this);
        Appodeal.SetInterstitialCallbacks(this);
        if (SceneManager.GetActiveScene().buildIndex >= 3)
            ShowInterstitial();
    }

    private void OnInitilizationFinished(object sender, SdkInitializedEventArgs e)
    {
        Debug.Log("Initialization Finished");
        
    }

    public void ShowInterstitial()
    {
        if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
        {
            Time.timeScale = 0;
            Appodeal.Show(AppodealShowStyle.Interstitial);
            GameAnalytics.Instance.InterstitialAd();
        }
    }
    
    public void ShowRewarded()
    {
        if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
        {
            Time.timeScale = 0;
            Appodeal.Show(AppodealShowStyle.RewardedVideo);
            GameAnalytics.Instance.RewardedAd();
        }
    }

    #region RewardedVideo

    public void OnRewardedVideoClicked()
    {
        throw new System.NotImplementedException();
    }

    public void OnRewardedVideoClosed(bool finished)
    {
        Time.timeScale = 1;
    }

    public void OnRewardedVideoExpired()
    {
        
    }

    public void OnRewardedVideoFailedToLoad()
    {
        
    }

    public void OnRewardedVideoFinished(double amount, string currency)
    {
        
    }

    public void OnRewardedVideoLoaded(bool isPrecache)
    {
        
    }

    public void OnRewardedVideoShowFailed()
    {
        
    }

    public void OnRewardedVideoShown()
    {
        
    }
    
    #endregion

    #region Interstitial

    public void OnInterstitialLoaded(bool isPrecache)
    {
        
    }

    public void OnInterstitialFailedToLoad()
    {
        
    }

    public void OnInterstitialShowFailed()
    {
        
    }

    public void OnInterstitialShown()
    {
        
    }

    public void OnInterstitialClosed()
    {
        Time.timeScale = 1;
    }

    public void OnInterstitialClicked()
    {
        
    }

    public void OnInterstitialExpired()
    {
        
    }

    #endregion



    // AppodealCallbacks.Interstitial.OnLoaded += (sender, args) => { };
    
    // public void SomeMethod()
    // {
    // AppodealCallbacks.Interstitial.OnLoaded += OnInterstitialLoaded;
    // AppodealCallbacks.Interstitial.OnFailedToLoad += OnInterstitialFailedToLoad;
    // AppodealCallbacks.Interstitial.OnShown += OnInterstitialShown;
    // AppodealCallbacks.Interstitial.OnShowFailed += OnInterstitialShowFailed;
    // AppodealCallbacks.Interstitial.OnClosed += OnInterstitialClosed;
    // AppodealCallbacks.Interstitial.OnClicked += OnInterstitialClicked;
    // AppodealCallbacks.Interstitial.OnExpired += OnInterstitialExpired;}

    // #region InterstitialAd Callbacks

    // // Called when interstitial was loaded (precache flag shows if the loaded ad is precache)
    // private void OnInterstitialLoaded(object sender, AdLoadedEventArgs e)
    // {
    //     Debug.Log("Interstitial loaded");
    // }

    // // Called when interstitial failed to load
    // private void OnInterstitialFailedToLoad(object sender, EventArgs e)
    // {
    //     Debug.Log("Interstitial failed to load");
    // }

    // // Called when interstitial was loaded, but cannot be shown (internal network errors, placement settings, etc.)
    // private void OnInterstitialShowFailed(object sender, EventArgs e)
    // {
    //     Debug.Log("Interstitial show failed");
    // }

    // // Called when interstitial is shown
    // private void OnInterstitialShown(object sender, EventArgs e)
    // {
    //     Debug.Log("Interstitial shown");
    // }

    // // Called when interstitial is closed
    // private void OnInterstitialClosed(object sender, EventArgs e)
    // {
    //     Debug.Log("Interstitial closed");
    // }

    // // Called when interstitial is clicked
    // private void OnInterstitialClicked(object sender, EventArgs e)
    // {
    //     Debug.Log("Interstitial clicked");
    // }

    // // Called when interstitial is expired and can not be shown
    // private void OnInterstitialExpired(object sender, EventArgs e)
    // {
    //     Debug.Log("Interstitial expired");
    // }

    // #endregion
}
