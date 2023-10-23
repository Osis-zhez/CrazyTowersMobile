using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.XR.OpenVR;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;

    [SerializeField] private TMP_Text energyText;
    [SerializeField] private float energyMultiplier;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energy;
    [SerializeField] private float energyUpgradeCoast;
    [SerializeField] private float energyUpgradeAmount;
    [SerializeField] private Image energyFill;
    [SerializeField] private float timerMax;
    private float timer;

    private void Awake()
    {   
        Instance = this;
    }

    private void Start() 
    {
        energy = maxEnergy;
        energyFill.fillAmount = energy / maxEnergy;

        timer = timerMax;
    }

    private void Update() 
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            energy += maxEnergy * energyMultiplier;
            energy = Mathf.Clamp(energy, 0, maxEnergy);

            energyText.text = $"{Mathf.FloorToInt(energy).ToString()} / {Mathf.FloorToInt(maxEnergy).ToString()}"; //Mathf.FloorToInt(currentEnergy).ToString();
            energyFill.fillAmount = energy / maxEnergy;

            timer = timerMax;
        }
        
    }

    public void UpgradeEnergy(out bool UpIsDone)
    {
        if (ManaManager.Instance.GetCurrentMana() >= energyUpgradeCoast)
        {
            maxEnergy += energyUpgradeAmount;
            energy = Mathf.Clamp(energy, 0, maxEnergy);

            energyText.text = $"{Mathf.FloorToInt(energy).ToString()} / {Mathf.FloorToInt(maxEnergy).ToString()}"; //Mathf.FloorToInt(currentEnergy).ToString();
            energyFill.fillAmount = energy / maxEnergy;
            ManaManager.Instance.TakeManaForUpgrade(energyUpgradeCoast);
            energyUpgradeCoast += 10;
            UpIsDone = true;
        }
        else
            UpIsDone = false;
    }

    public void TakeEnergyForUnit(float energyAmount)
    {
        energy -= energyAmount;
        energy = Mathf.Clamp(energy, 0, maxEnergy);

        energyText.text = $"{Mathf.FloorToInt(energy).ToString()} / {Mathf.FloorToInt(maxEnergy).ToString()}"; //Mathf.FloorToInt(currentEnergy).ToString();
        energyFill.fillAmount = energy / maxEnergy;
    }

    public float GetCurrentEnergy()
    {
        return energy;
    }

    

    public float GetManaUpgradeCoast()
    {
        return energyUpgradeCoast;
    }

    
}
