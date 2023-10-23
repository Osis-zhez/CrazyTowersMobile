using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private Transform[] path;
    [SerializeField] [Range(0f, 5f)]float speed = 1f;


    private void Start()
    {
        StartCoroutine(FollowPaths());
    }

    IEnumerator FollowPaths()
    {
        foreach(Transform waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.position;
            float travelPercent = 0f;

            // transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }
        
    }
}
