using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UnitType")]
public class UnitTypeSO : ScriptableObject
{
    [Header("Base Unit settings")]
    public string nameString;
    public GameObject prefab;
    public float unitEnergyCoast;
    public float manaGainBase;
    public float callDawnSpawn;
    public float damageAmountBase;
    public float healthAmountMaxBase;
    public float moveSpeedBase;
    public float shieldAmountBase;
    public float manaUpgradeCoastBase;

    [Header("Upgrade settings")]
    [SerializeField] private float damageAmountUpgrade;
    [SerializeField] private float healthAmountMaxUpgrade;
    [SerializeField] private float moveSpeedUpgrade;
    [SerializeField] private float shieldAmountUpgrade; 
    [SerializeField] private float manaGainUpgrade;

    private float damageAmount;
    private float moveSpeed;
    private float healthAmountMax;
    private float manaGain;
    private float shieldAmount;
    
    public void StartUnitTypeSO() 
    {
        damageAmount = damageAmountBase;
        moveSpeed = moveSpeedBase;
        healthAmountMax = healthAmountMaxBase;
        manaGain = manaGainBase;
        shieldAmount = shieldAmountBase;
    }

    public GameObject SpawnUnit(Vector3 position)
    {
        GameObject unit = Instantiate(prefab, position, Quaternion.identity);
        return unit;
    }

    public void UpgradeUnit()
    {
        this.manaGain += manaGainUpgrade;
        this.damageAmount += this.damageAmountBase * damageAmountUpgrade;
        this.healthAmountMax += this.healthAmountMaxBase * healthAmountMaxUpgrade;
        this.moveSpeed += this.moveSpeedBase * moveSpeedUpgrade;
        this.shieldAmount += shieldAmountUpgrade;
    }

    public void UpgradeUnit1(float manaGain = 0, float healthAmountMax = 0) //Здесь нужно добавить параметры для всех переменных, которые нужно апгрейдить
    {
        this.manaGain += manaGain;
        this.healthAmountMax += this.healthAmountMaxBase * healthAmountMax;
    }

    public void UpgradeUnit2(float healthAmountMax = 0)
    {
        this.healthAmountMax += this.healthAmountMaxBase * healthAmountMax;
    }

    public void UpgradeUnit3(float damageAmount = 0, float healthAmountMax = 0,
    float moveSpeed = 0, float shieldAmount = 0)
    {
        this.damageAmount += this.damageAmountBase * damageAmount;
        this.healthAmountMax += this.healthAmountMaxBase * healthAmountMax;
        this.moveSpeed += this.moveSpeedBase * moveSpeed;
        this.shieldAmount += shieldAmount;
    }

    public void UpgradeUnit4(float healthAmountMax = 0)
    {
        this.healthAmountMax += this.healthAmountMaxBase * healthAmountMax;
    }

    public float GetEnergyCoast()
    {
        return unitEnergyCoast;
    }
    public float GetManaGain()
    {
        return manaGain;
    }

    public float GetDamageAmount()
    {
        return damageAmount;
    }

    public float GetHealthAmountMax()
    {
        return healthAmountMax;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetShieldAmount()
    {
        return shieldAmount;
    }

    public float GetManaUpgradeCoast()
    {
        return manaUpgradeCoastBase;
    }
}
