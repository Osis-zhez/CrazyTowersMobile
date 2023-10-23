using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private Transform barTransform;
    private TextMeshPro healthText;

    private void Awake() {
        barTransform = transform.Find("bar");
        healthText = transform.Find("HealthText").GetComponent<TextMeshPro>();
    }

    private void Start() {
        enemy.OnDamaged += Enemy_OnDamaged;
        UpdateBar();
        UpdateText();
    }

  

    private void Enemy_OnDamaged(object sender, System.EventArgs e) {
        UpdateBar();
        UpdateText();
    }

    private void UpdateBar() {
            barTransform.localScale = new Vector3 (enemy.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateText()
    {
        healthText.text = enemy.GetHealthText();
    }

}
