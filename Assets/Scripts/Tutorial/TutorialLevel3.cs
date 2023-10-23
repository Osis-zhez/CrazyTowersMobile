using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Analytics;

public class TutorialLevel3 : MonoBehaviour
{
    [SerializeField] private GameObject firstTutorialScreen;
    // [SerializeField] private Button unit1Button;
    [SerializeField] private Button unit2Button;
    [SerializeField] private Button unit3Button;
    // [SerializeField] private Button unit4Button;

    private void Start()
    {
        firstTutorialScreen.SetActive(true);
        unit2Button.enabled = false;
        unit3Button.onClick.AddListener(EndFirstTutorial);
    }

    private void EndFirstTutorial()
    {
        GameAnalytics.Instance.LogEvent("Level3_Unit3_TutorialEnd");
        firstTutorialScreen.SetActive(false);
        unit2Button.enabled = true;
        unit3Button.onClick.RemoveListener(EndFirstTutorial);
    }

}
