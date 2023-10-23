using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ManaButtonController : MonoBehaviour
{
    [SerializeField] private float calldownMaxTime;
    [SerializeField] private float separatorTimerСoefficient;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private int unitIndex;

    private float separatorTimer;
    private float calldownTimer = 0;
    private int levelUnit = 1;


    private void Awake()
    {
        fadeImage = transform.Find("FadeImage").GetComponent<Image>();
        manaText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
        // levelText = transform.GetComponentInParent<RectTransform>().transform.Find("Level").transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        float manaUpgradeCoast = UnitSpawner.Instance.GetUnitTypeList().unitList[unitIndex].GetManaUpgradeCoast();
        fadeImage.gameObject.SetActive(false);
        separatorTimer = calldownMaxTime * separatorTimerСoefficient;
        manaText.text = manaUpgradeCoast.ToString();
        button.GetComponent<Button>().onClick.AddListener(() => {
            UnitSpawner.Instance.UpgradeUnit(unitIndex, out bool UpIsDone);
            if (UpIsDone)
            {
                ClickButtonFadeOut(unitIndex);
                manaUpgradeCoast += 10;
                manaText.text = manaUpgradeCoast.ToString();
            }
        });

        // button.GetComponent<Button>().onClick.AddListener(() => {
        //     ClickButtonFadeOut(unitIndex);
        // });
    }


    private void Update() 
    {
        if (calldownTimer > 0)
        {
            calldownTimer -= Time.deltaTime;
            // if(calldownTimer <= 0)
            // {
            //     calldownTimer = calldownMaxTime;
            // }
        }
    }

    public void ClickButtonFadeOut(int unitIndex)
    {
            calldownTimer = calldownMaxTime;
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1;
            button.GetComponent<Button>().enabled = false;
            levelUnit += 1;
            levelText.text = $"L.{levelUnit}";
            StartCoroutine(CallDownButtonCO());
    }

    IEnumerator CallDownButtonCO()
    {
        while (calldownTimer > 0)
        {
            fadeImage.fillAmount = calldownTimer / calldownMaxTime;
            yield return new WaitForSeconds(separatorTimer);
        }
        fadeImage.gameObject.SetActive(false);
        button.GetComponent<Button>().enabled = true;
    }
}
