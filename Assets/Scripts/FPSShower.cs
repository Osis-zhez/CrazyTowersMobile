using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSShower : MonoBehaviour
{

    public static float fps;

    public TMP_Text text;
    // private void Update()

    // {
    //     fps = 1.0f / Time.deltaTime;
    // }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        // StartCoroutine(fpsC());
    }
    
    public IEnumerator fpsC()
    {

        yield return new WaitForSeconds(0.5f);
        var fpsDone = Mathf.Round(fps);

        text.text = "FPS: " + fpsDone;
        StartCoroutine(fpsC());

    }
}