using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardedButtonUI : MonoBehaviour
{
    [SerializeField] private float timerMax;
    [SerializeField] private float timerHideButton;
    [SerializeField] private float manaGain; 
    private Button rewardButton;
    private float timer;
    

    private void Start()
    {
        timer = timerMax;
        rewardButton = transform.Find("Button").GetComponent<Button>();
        rewardButton.gameObject.SetActive(false);
        rewardButton.onClick.AddListener(ClickButton);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                rewardButton.gameObject.SetActive(true);
                StartCoroutine(TimerHideButton());
                timer = timerMax;
            }
        }
    }

    private IEnumerator TimerHideButton()
    {
        yield return new WaitForSeconds(timerHideButton);
        gameObject.SetActive(false);
    }

    public void ClickButton()
    {
        rewardButton.gameObject.SetActive(false);
        ManaManager.Instance.AddMana(manaGain);
        AppodealManager.Instance.ShowRewarded();
    }

}
