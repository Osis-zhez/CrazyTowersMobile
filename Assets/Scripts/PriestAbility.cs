using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PriestAbility : MonoBehaviour
{
    [SerializeField] private GameObject healthTextPrefab;
    [SerializeField] private float timeCalldownHealAbility;
    private float timerHealAbility;
    
    [SerializeField] private float healAmount;

    private Collider2D[] healTargetColliders;
    private HealthSystem targerHealUnit;
    private TextMeshPro healthText;

    private void Start()
    {
        timerHealAbility = timeCalldownHealAbility;
        healthText = healthTextPrefab.transform.Find("FloatingText").GetComponent<TextMeshPro>();
        healthText.text = $"+ {healAmount}";
    }

    private void Update()
    {
        timerHealAbility -= Time.deltaTime;
        if (timerHealAbility <= 0)
        {
            FindHealTarget();
            // if (targerHealUnit != null)
            //     HealTarget();
            timerHealAbility = timeCalldownHealAbility;
        }
    }
    

    private void FindHealTarget()
    {
        float targetMaxRadius = 3f;
        healTargetColliders = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D unitCollider in healTargetColliders)
        {
            if (unitCollider.tag == "Priest") continue; 
            HealthSystem healthUnitSystem = unitCollider.GetComponent<HealthSystem>();
            if (healthUnitSystem != null)
            {
                healthUnitSystem.HealUnit(healAmount);
                Transform healUnitTransform = healthUnitSystem.transform;
                Debug.Log(unitCollider.name);
                GameObject prefab = Instantiate(healthTextPrefab, new Vector3(healUnitTransform.position.x,
                healUnitTransform.position.y + 2f, healUnitTransform.position.z), Quaternion.identity);
                // if (this.targerHealUnit == null)
                // {
                //     this.targerHealUnit = healthUnitSystem;
                // }
                // else
                // {
                //     if (Vector3.Distance(transform.position, healthUnitSystem.transform.position) <
                //     Vector3.Distance(transform.position, this.targerHealUnit.transform.position))
                //     {
                //         this.targerHealUnit = healthUnitSystem;
                //     }
                // }
            }
            // if(targerHealUnit != null)
            //     Debug.Log(targerHealUnit.name);
        }
    }

    public void HealTarget()
    {
        // targerHealUnit.HealUnit(healAmount);
        // Transform HealUnitTransform = targerHealUnit.transform;
        // GameObject prefab = Instantiate(healthTextPrefab, new Vector3(HealUnitTransform.position.x,
        // HealUnitTransform.position.y + 2f, HealUnitTransform.position.z), Quaternion.identity);
        // prefab.GetComponent<TextMeshProUGUI>().text = $"+ {healAmount}";
    }
}
