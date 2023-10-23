using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ManaManager : MonoBehaviour
{
    public static ManaManager Instance;
    public EventHandler OnTutorialManaCheck;

    [SerializeField] private TMP_Text manaText;
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] float mana = 0;


    public void Awake()
    {
        Instance = this;
        manaText.text = $"{Mathf.FloorToInt(mana).ToString()}";
    }

    public void TakeManaForUpgrade(float manaAmount)
    {
        mana -= manaAmount;
        manaText.text = $"{Mathf.FloorToInt(mana).ToString()}";
    }

    public void AddMana(float manaAmount = 10)
    {
        mana += manaAmount;
        manaText.text = $"{Mathf.FloorToInt(mana).ToString()}";
        OnTutorialManaCheck?.Invoke(this, EventArgs.Empty);
    }

    public float GetCurrentMana()
    {
        return mana;
    }

    public void ShowAddManaText(string text, Vector3 position)
    {
        if (floatingTextPrefab != null)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, new Vector3 (position.x, position.y + 1.1f, position.z), Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }

    }
}
