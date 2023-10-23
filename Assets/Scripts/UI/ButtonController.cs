using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private float calldownMaxTime;
    private float calldownTimer = 0;

    [SerializeField] private float separatorTimerСoefficient;
    private float separatorTimer;

    [SerializeField] private Image fadeImage;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private int unitIndex;

    private UnitTypeListSO unitTypeListSO;
    [SerializeField] private bool isIndividual = false;

    private void Awake()
    {
        unitTypeListSO = Resources.Load<UnitTypeListSO>(typeof(UnitTypeListSO).Name);
        if (isIndividual == false)
            calldownMaxTime = unitTypeListSO.unitList[unitIndex].callDawnSpawn;
        fadeImage = transform.Find("FadeImage").GetComponent<Image>();
        button = GetComponent<Button>();
        energyText = transform.Find("Energy").GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start() 
    {
        fadeImage.gameObject.SetActive(false);
        separatorTimer = calldownMaxTime * separatorTimerСoefficient;
        energyText.text = unitTypeListSO.unitList[unitIndex].GetEnergyCoast().ToString();

        button.GetComponent<Button>().onClick.AddListener(() => {
            if (EnergyManager.Instance.GetCurrentEnergy() >= UnitSpawner.Instance.GetUnitTypeList().unitList[unitIndex].unitEnergyCoast)
            {
                UnitSpawner.Instance.CreateUnit(unitIndex);
                ClickButtonFadeOut(unitIndex);
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
