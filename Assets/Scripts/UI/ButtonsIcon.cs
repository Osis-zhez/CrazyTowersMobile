using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonsIcon : MonoBehaviour
{
    [SerializeField] private Button[] lvlButtons;
    [SerializeField] private Sprite unlockedIcon;
    [SerializeField] private Sprite lockedIcon;
    [SerializeField] private int firstLevelIndex;

    private void Awake()
    {
        int unlockedLvl = PlayerPrefs.GetInt(EndGameManager.Instance.lvlUnlock, firstLevelIndex);
        for (int i = 0; i < lvlButtons.Length; i ++)
        {
            if (i + firstLevelIndex <= unlockedLvl)
            {
                lvlButtons[i].interactable = true;
                lvlButtons[i].transform.Find("Icon").GetComponent<Image>().sprite = unlockedIcon;
                // TextMeshProUGUI textButton = lvlButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                // textButton.text = (i+1).ToString();
                // textButton.enabled = true;
            }
            else
            {
                lvlButtons[i].interactable = false;
                lvlButtons[i].transform.Find("Icon").GetComponent<Image>().sprite = lockedIcon;
                // lvlButtons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }
}
