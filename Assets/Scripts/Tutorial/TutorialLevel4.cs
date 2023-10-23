using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Analytics;

public class TutorialLevel4 : MonoBehaviour
{
    [SerializeField] private GameObject firstTutorialScreen;
    [SerializeField] private Button unit1Button;
    [SerializeField] private Button unit2Button;
    [SerializeField] private Button unit3Button;
    [SerializeField] private Button unit4Button;

    private void Start()
    {
        firstTutorialScreen.SetActive(true);
        unit3Button.enabled = false;
        unit4Button.onClick.AddListener(EndFirstTutorial);
    }

    private void EndFirstTutorial()
    {
        GameAnalytics.Instance.LogEvent("Level4_Unit4_TutorialEnd");
        firstTutorialScreen.SetActive(false);
        unit3Button.enabled = true;
        unit4Button.onClick.RemoveListener(EndFirstTutorial);
    }

}
