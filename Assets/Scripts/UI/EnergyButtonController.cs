using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyButtonController : MonoBehaviour
{
    [SerializeField] private float calldownMaxTime;
    [SerializeField] private float separatorTimerСoefficient;
    [SerializeField] private int unitIndex;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI manaCoastText;
  
    private float separatorTimer;
    private float calldownTimer = 0;

    private void Awake()
    {
        fadeImage = transform.Find("FadeImage").GetComponent<Image>();
        manaCoastText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }

    private void Start() 
    {
        manaCoastText.text = EnergyManager.Instance.GetManaUpgradeCoast().ToString();
        fadeImage.gameObject.SetActive(false);
        separatorTimer = calldownMaxTime * separatorTimerСoefficient;

        button.GetComponent<Button>().onClick.AddListener(() => {
                EnergyManager.Instance.UpgradeEnergy(out bool UpIsDone);
                if (UpIsDone)
                    ClickButtonFadeOut(unitIndex);
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
        manaCoastText.text = EnergyManager.Instance.GetManaUpgradeCoast().ToString();
        // levelUnit += 1;
        // levelText.text = $"L.{levelUnit}";
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
