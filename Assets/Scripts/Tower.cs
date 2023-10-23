using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject visualRadius; // Соотношение спрайта радиуса и настройки дальности стрельбы 1/4.
    [SerializeField] private float shootTimerMax;
    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float angelShift = 90;
    [SerializeField] private bool isActiveRadius = false;

    private Unit targetUnit;
    private float shootTimer;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = 0.2f;
    private Vector3 projecttileSpawnPosition;
    private Vector3 moveDir;
    private Transform towerHead;


    private void Awake()
    {
        towerHead = transform.Find("sprite");
        projecttileSpawnPosition = transform.Find("sprite").transform.Find("projecttileSpawnPosition").position; 
    }
    
    private void Start()
    {
        visualRadius.SetActive(isActiveRadius);
        visualRadius.transform.localScale = new Vector3(attackRadius * 0.25f, attackRadius * 0.25f);
        VisualTowerRadiusButton.Instance.OnActivateTowerRadius += VisualTowerRadiusButton_OnActivateTowerRadius;
    }

    private void VisualTowerRadiusButton_OnActivateTowerRadius(object sender, EventArgs e)
    {
        isActiveRadius = !isActiveRadius;
        visualRadius.SetActive(isActiveRadius);
    }

    private void Update() 
    {
        HandleTargetting(); 
        HandleShooting();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        ManaManager.Instance.AddMana(other.GetComponent<UnitTypeHolder>().unitType.GetManaGain());
        float unitManaGain = other.GetComponent<UnitTypeHolder>().unitType.GetManaGain();  
        ManaManager.Instance.ShowAddManaText($"+{unitManaGain}", transform.position);
    }

    private void UpdateVisualRadius()
    {
        // visualRadius.transform.localScale = new Vector3(attackRadius / 1.66f, attackRadius / 1.66f, 1);
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer += shootTimerMax;
            if (targetUnit != null && Vector3.Distance(transform.position, 
            targetUnit.transform.position) < attackRadius)
            {
                moveDir = (targetUnit.transform.position - transform.position).normalized;
                towerHead.eulerAngles = new Vector3 (0, 0, UtilsClass.GetAngleFromVector(moveDir) - angelShift);
                ArrowProjectTile.Create(bulletPrefab ,projecttileSpawnPosition, targetUnit, moveDir);
            }
        }
        
    }

    private void HandleTargetting() 
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0f) 
        { 
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void LookForTargets() {
        float targetMaxRadius = attackRadius;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            Unit unit = collider2D.GetComponent<Unit>();
            if (unit != null) {
                //It's enemy;
                if (this.targetUnit == null) {
                    this.targetUnit = unit;
                }
                else {
                    if(Vector3.Distance(transform.position, unit.transform.position) <
                    Vector3.Distance(transform.position, this.targetUnit.transform.position)) {
                        //Closer;
                        this.targetUnit = unit;
                    }
                }
            }
        }
    }
}
