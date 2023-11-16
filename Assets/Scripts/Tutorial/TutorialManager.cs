using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Analytics;

public class TutorialManager : MonoBehaviour
{
    public delegate void TutorialDelegate();
    private TutorialDelegate manaCheckDelegate;

    [SerializeField] private GameObject firstTutorialObject;
    [SerializeField] private GameObject secondTutorialObject;
    [SerializeField] private GameObject thirdTutorialObject;
    [SerializeField] private Button unit1Button;
    [SerializeField] private Button energyButton;
    [SerializeField] private Button manaButton;
    [SerializeField] private Button[] fadeButtons;

    private void Start()
    {
        firstTutorialObject.SetActive(true);
        secondTutorialObject.SetActive(false);
        thirdTutorialObject.SetActive(false);
        ManaManager.Instance.OnTutorialManaCheck += ManaManager_OnTutorialManaCheck;
        unit1Button.onClick.AddListener(EndFirstTutorial);
        manaCheckDelegate = StartSecondTutorial;

        transform.Find("Unit1Button").transform.Find("ManaButton").GetComponent<Button>().enabled = false;
        ActivateFadeButtons(false);
    }

    private void ManaManager_OnTutorialManaCheck(object sender, EventArgs e)
    {
        if(manaCheckDelegate == null) { return; }
        manaCheckDelegate();
    }

    private void ActivateFadeButtons(bool isActive)
    {
        foreach (Button button in fadeButtons)
        {
            button.enabled = isActive;
        }
    }

    public void EndFirstTutorial()
    {
        ActivateFadeButtons(true);
        firstTutorialObject.SetActive(false);
        unit1Button.onClick.RemoveListener(EndFirstTutorial);
        GameAnalytics.Instance.LogEvent("Level1_FirstUnit_tutorialEnd");
    }

    public void StartSecondTutorial()
    {
        if (ManaManager.Instance.GetCurrentMana() >= 40)
        {
            ActivateFadeButtons(false);
            unit1Button.enabled = false;
            secondTutorialObject.SetActive(true);

            energyButton.onClick.AddListener(EndSecondTutorial);
            Time.timeScale = 0;
        }
    }

    public void EndSecondTutorial()
    {
        ActivateFadeButtons(true);
        unit1Button.enabled = true;
        secondTutorialObject.SetActive(false);
        manaCheckDelegate = StartThirdTutorial;
        Time.timeScale = 1;
        GameAnalytics.Instance.LogEvent("Level1_UpgradeEnergy_TutorialEnd");
    }

    public void StartThirdTutorial()
    {
        if (ManaManager.Instance.GetCurrentMana() >= 40)
        {
            ActivateFadeButtons(false);
            unit1Button.enabled = false;
            thirdTutorialObject.SetActive(true);
            transform.Find("Unit1Button").transform.Find("ManaButton").GetComponent<Button>().enabled = true;
            unit1Button.enabled = false;
            energyButton.enabled = false;
            manaCheckDelegate = null;

            manaButton.onClick.AddListener(EndThirdTutorial);
            Time.timeScale = 0;
        }
    }

    public void EndThirdTutorial()
    {
        ActivateFadeButtons(true);
        unit1Button.enabled = true;
        GameAnalytics.Instance.LogEvent("Level1_UpgradeUnit_TutorialEnd");
        thirdTutorialObject.SetActive(false);
        transform.Find("Unit1Button").transform.Find("ManaButton").GetComponent<Button>().enabled = true;
        unit1Button.enabled = true;
        energyButton.enabled = true;
        manaCheckDelegate = null;

        manaButton.onClick.RemoveListener(EndThirdTutorial);
        ManaManager.Instance.OnTutorialManaCheck -= ManaManager_OnTutorialManaCheck;
        Time.timeScale = 1;
    }
}
