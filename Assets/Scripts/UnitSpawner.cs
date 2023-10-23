using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner Instance;

    [SerializeField] private Transform spawnPoint;

    private UnitTypeListSO unitTypeList;

    private int nextSortingOrder = 5;

    private void Awake() 
    {
        Instance = this;

        unitTypeList = Resources.Load<UnitTypeListSO>("UnitTypeListSO");
        foreach (UnitTypeSO unitType in unitTypeList.unitList)
        {
            unitType.StartUnitTypeSO();
        }
    }

    public void CreateUnit(int unitIndex)
    {
        GameObject unit = unitTypeList.unitList[unitIndex].SpawnUnit(spawnPoint.position);
        EnergyManager.Instance.TakeEnergyForUnit(unitTypeList.unitList[unitIndex].unitEnergyCoast);
        unit.transform.Find("sprite").GetComponent<SpriteRenderer>().sortingOrder = nextSortingOrder;
        nextSortingOrder += 1;
    }

    public void UpgradeUnit(int unitIndex, out bool isDone)
    {
        if (ManaManager.Instance.GetCurrentMana() >= unitTypeList.unitList[unitIndex].manaUpgradeCoastBase)
        {
            ManaManager.Instance.TakeManaForUpgrade(unitTypeList.unitList[unitIndex].manaUpgradeCoastBase);
            unitTypeList.unitList[unitIndex].UpgradeUnit();
            isDone = true;
        }
        else   
            isDone = false;
    }

    public UnitTypeListSO GetUnitTypeList()
    {
        return unitTypeList;
    }

    // public void RegisterEnergySystem(EnergySystem energySystem)
    // {
    //     this.energySystem = energySystem;
    // }
}
