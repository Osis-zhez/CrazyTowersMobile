using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] childrenObjects;

    private void Awake()
    {
        for (int i = 0; i < childrenObjects.Length; i++)
        {
            childrenObjects[i].SetActive(true);
        }

    }
}
