using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectTile : MonoBehaviour
{
    public static ArrowProjectTile Create(GameObject prefab, Vector3 position, Unit unit, Vector3 moveDir) {
        Transform pfArrowProjectTile = prefab.transform;
        Transform arrowTransform = Instantiate(pfArrowProjectTile, position, Quaternion.identity);

        ArrowProjectTile ArrowProjectTile = arrowTransform.GetComponent<ArrowProjectTile>();
        ArrowProjectTile.SetTarget(unit);
        ArrowProjectTile.transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));
        return ArrowProjectTile;
    }

    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private bool isAreaDamage = false;

    private Unit targetUnit;
    private Vector3 lastMoveDir;
    private Vector3 moveDir; 
    private HealthSystem targerHealUnit;
    private Collider2D[] damageArea;
    
    private float timeToDie = 2f;

    private void Update()
    {
        if (targetUnit != null)
        {
            moveDir = (targetUnit.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDir));

        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(Unit targetEnemy)
    {
        this.targetUnit = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(isAreaDamage == false)
        {
            Unit unit = other.GetComponent<Unit>();
            if (unit != null)
            {
                //Hit enemy
                unit.GetComponent<HealthSystem>().Damage(damageAmount);
                Destroy(gameObject);
            }   
        }
        else
        {
            float targetMaxRadius = 1.8f;
            damageArea = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
            foreach (Collider2D unitCollider in damageArea)
            {
                // if (unitCollider.tag == "Priest") continue; 
                HealthSystem healthUnitSystem = unitCollider.GetComponent<HealthSystem>();
                if (healthUnitSystem != null)
                {
                    healthUnitSystem.Damage(damageAmount);

                    Debug.Log(healthUnitSystem.name);
                }
                
            }
            Destroy(gameObject);
        }
        
    }

    public void SetDamageAmount(float damageAmount)
    {
        this.damageAmount = Mathf.FloorToInt(damageAmount);
    }
}
