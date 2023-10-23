using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    private float healthAmountMax;
    private float healthAmount;
    private float shieldAmount;
    [SerializeField] private bool hasShield = false;

    private void Awake() 
    {
        
    }

    private void Start() 
    {
        healthAmount = healthAmountMax;
    }

    public void Damage(float damageAmount) 
    {
        if (hasShield)
            healthAmount -= damageAmount - (damageAmount * shieldAmount);
        else
            healthAmount -= damageAmount;     
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead()) {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead() {
        return healthAmount == 0;
    }

    public bool IsFullHealth() {
        return healthAmount == healthAmountMax;
    }

    public float GetHealthAmount() {
        return healthAmount;
    }

    public float GetHealthAmountNormalized() {
        return (float)healthAmount / healthAmountMax;
    }

    public void HealUnit(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnDamaged?.Invoke(this, EventArgs.Empty);
    }
    

    public void SetHealthAmountMax(float healthAmountMax) 
    {
        this.healthAmountMax = healthAmountMax;

        // if (updateHealthAmount) {
        //     healthAmount = healthAmountMax;
        // }
    }

    public void SetShield(float shieldAmount)
    {
        this.shieldAmount = shieldAmount;
    }


}
