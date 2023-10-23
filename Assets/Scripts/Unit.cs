using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;
    private float damageAmount;
    private float healthAmountMax;
    private float moveSpeed;
    private float shieldAmount;
    [SerializeField] private float manaGain;

    private UnitTypeSO unitType;

    private int indexPath = 0;

    private HealthSystem healthSystem;

    private void Awake() 
    {
        unitType = GetComponent<UnitTypeHolder>().unitType;

        pathPoints = PathPoints.Instance.GetPathPoints();
        healthSystem = GetComponent<HealthSystem>();

        damageAmount = unitType.GetDamageAmount();
        healthAmountMax = unitType.GetHealthAmountMax();
        moveSpeed = unitType.GetMoveSpeed();
        shieldAmount = unitType.GetShieldAmount();
        manaGain = unitType.GetManaGain();

        healthSystem.SetHealthAmountMax(healthAmountMax);
        healthSystem.SetShield(shieldAmount);
    }

    private void Start()
    {
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        MovePath();
    }

    private void MovePath()
    {
        if (indexPath < pathPoints.Length)
        {
            if (Vector3.Distance(transform.position, pathPoints[indexPath].position) > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, pathPoints[indexPath].position, moveSpeed * Time.deltaTime);
            }
            else
            {
                indexPath++;
            }
        }
    }

    public float GetDamageAmount()
    {
        return damageAmount;
    }


    private void FixedUpdate() {
        //Вычисление должны происходить на апдейте, а физика в фикс апдейте.
    }
}
