using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;

    private void Awake() {
        barTransform = transform.Find("bar");
    }

    private void Start() {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        // enemy.OnDamaged += Enemy_OnDamage;
        UpdateBar();
        UpdateHealthBarVisible();
    }

    // private void Enemy_OnDamage(object sender, EventArgs e)
    // {
    //     UpdateBar();
    //     UpdateHealthBarVisible();
    // }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar() {
        // if(healthSystem != null)
            barTransform.localScale = new Vector3 (healthSystem.GetHealthAmountNormalized(), 1, 1);
        // else
        //     barTransform.localScale = new Vector3 (enemy.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        // if (healthSystem == null) { return; }
        if (healthSystem.IsFullHealth()) {
            gameObject.SetActive(false);
        }else {
            gameObject.SetActive(true);
        }
    }
}
