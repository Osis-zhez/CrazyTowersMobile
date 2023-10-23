using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Analytics;

public class TutorialLevel2 : MonoBehaviour
{
    public delegate void TutorialDelegate();
    private TutorialDelegate changeTutorial;
    [SerializeField] private GameObject firstTutorialScreen;
    [SerializeField] private GameObject secondTutorialScreen;
    [SerializeField] private Button unit1Button;
    [SerializeField] private Button unit2Button;
    
    private void Start()
    {
        firstTutorialScreen.SetActive(true);
        secondTutorialScreen.SetActive(false);
        unit1Button.enabled = false;
        unit2Button.onClick.AddListener(EndFirstTutorial);
        // changeTutorial = SecondTutorial;
    }

    private void EndFirstTutorial()
    {
        GameAnalytics.Instance.LogEvent("Level2_Unit2_TutorialEnd");
        firstTutorialScreen.SetActive(false);
        unit2Button.onClick.RemoveListener(EndFirstTutorial);
        Debug.Log("1");
        SecondTutorial();
    }

    private void SecondTutorial()
    {
        StartCoroutine(StartSecondTutorial());
    }
    
    private void EndSecondTutorial()
    {
        GameAnalytics.Instance.LogEvent("Level2_SummonsUnit_TutorialEnd");
        secondTutorialScreen.SetActive(false);
        unit1Button.enabled = true;
        unit2Button.onClick.RemoveListener(EndSecondTutorial);
    }

    private IEnumerator StartSecondTutorial()
    {
        yield return new WaitForSeconds(5.5f);
        secondTutorialScreen.SetActive(true);
        unit2Button.onClick.AddListener(EndSecondTutorial);
    }

    
}
