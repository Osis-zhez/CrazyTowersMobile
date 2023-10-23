using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Button playButton;
    private TextMeshProUGUI levelText;

    private void Awake()
    {
        playButton = transform.Find("PlayButton").GetComponent<Button>();
        levelText = playButton.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("lastLevelIndex"))
            levelText.text = $"level {PlayerPrefs.GetInt("lastLevelIndex", 3) - 1}";
        playButton.onClick.AddListener(() => {
            FadeCanvasUI.Instance.FaderLoadInt(PlayerPrefs.GetInt("lastLevelIndex", 2));
        });
    }
}
