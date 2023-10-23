using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private int secMax;
    [SerializeField] private int minMax;
    [SerializeField] private int delta = 1;
    [SerializeField] private TextMeshProUGUI timerText;

    private int sec;
    private int min;
    private bool isWin;

    private void Awake()
    {
        timerText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    void Start() 
    {
        // timerText = GameObject.Find("Timer").GetComponent<TMP_Text>();
        sec = secMax;
        min = minMax;
        StartCoroutine(TimerFlow());
    }

    IEnumerator TimerFlow()
    {
        while (true)
        {
            if (sec == 0)
            {
                min --;
                sec = 60;
            }
            sec -= delta;
            // UIController.Instance.timerText.text = min.ToString("D2") + " : " + sec.ToString("D2");
            timerText.text = min.ToString("D2") + " : " + sec.ToString("D2");

            if (min == 0 && sec == 0)
            {
                // sec = 1;
                // Time.timeScale = 0;
                EndGameManager.Instance.TimeIsUp();
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void Update() 
    {
        
    }

    public void RestartTimer() {
        sec = secMax;
        min = minMax;
    }

    public void SetTimer(int setMin, int setSec) {
        sec = setSec;
        min = setMin;
        StartCoroutine(TimerFlow());
    }

    public void StartStop(int delta)
    {
        this.delta = delta;
    }

    public bool GetIsWin() {
        return isWin;
    }
}
