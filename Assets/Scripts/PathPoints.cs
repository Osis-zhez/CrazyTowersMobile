using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoints : MonoBehaviour
{
    public static PathPoints Instance;

    [SerializeField] private Transform[] pathPoints;

    private void Awake() 
    {
        Instance = this;
    }
    
    public Transform[] GetPathPoints()
    {
        return pathPoints;
    }
}
