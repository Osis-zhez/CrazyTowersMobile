using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatDebugKeys : MonoBehaviour
{
    private Button nextLevelButton;

    private void Awake()
    {
        nextLevelButton = GetComponent<Button>();
    }

    private void Start()
    {
        nextLevelButton.onClick.AddListener(() => {
            CheatKeys();
        });
    }

    private void CheatKeys()
    {
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            //Загрузить сцену окончания всей игры, а пока будет загрузка levelSelection
            FadeCanvasUI.Instance.FaderLoadString("MainMenu");
        }
        FadeCanvasUI.Instance.FaderLoadInt(nextSceneIndex);
    }
}
