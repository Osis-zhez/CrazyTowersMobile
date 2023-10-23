using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualTowerRadiusButton : MonoBehaviour
{
    public static VisualTowerRadiusButton Instance;
    
    public event EventHandler OnActivateTowerRadius;

    private Button button;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => {
            OnActivateTowerRadius?.Invoke(this, EventArgs.Empty);
        });
    }

}
