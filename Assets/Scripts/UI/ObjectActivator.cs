using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("CHEATBUTTON");
    }
}
