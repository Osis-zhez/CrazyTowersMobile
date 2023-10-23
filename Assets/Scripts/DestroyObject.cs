using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
