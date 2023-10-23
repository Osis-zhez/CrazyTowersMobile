using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EventHandler OnDied;
    public EventHandler OnDamaged;

    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Awake()
    {
        health = maxHealth;
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Damage(other.GetComponent<Unit>().GetDamageAmount());
        Destroy(other.gameObject);
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = $"{health} / {maxHealth}";
    }

    public void Damage(float damageAmount)
    {
        bool winGame = false;
        if (health > 0)
        {
            health -= damageAmount;
            UpdateHealthBar();
            winGame = true;
            UpdateHealthBar();
            OnDamaged?.Invoke(this, EventArgs.Empty);
            if (health <= 0)
            {
                health = 0;
                EndGameManager.Instance.WinGame();
            }
        }

        if (health <= 0 & winGame == false)
        {
            
            // gameObject.SetActive(false);
        }
        

        // if (isDead())
        // {
        //     // OnDied?.Invoke(this, EventArgs.Empty);
        //     if (OnDied != null)
        //     {
        //         OnDied(this, EventArgs.Empty);
        //     }
        // }
    }

    public float GetHealthAmountNormalized() {
        return (float)health / maxHealth;
    }

    public bool isDead()
    {
        return health == 0;
    }

    public string GetHealthText()
    {
        string healthText = $"{health} / {maxHealth}";
        return healthText;
    }
}
