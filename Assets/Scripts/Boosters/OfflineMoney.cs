using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfflineMoney : MonoBehaviour
{
    private Booster _offlineBooster;

    [SerializeField] private Image offlineBoosterImage;
    [SerializeField] private TextMeshProUGUI timeSpendText;
    [SerializeField] private TextMeshProUGUI rewardedMoney;
    [SerializeField] private Button getRewardButton;
    
    private float maxRewardValue;
    private float _reward;
    
    private void Awake()
    {
        _offlineBooster = GetComponent<Booster>();
        getRewardButton.interactable = false;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("OfflineMoneyLevel"))
        {
            maxRewardValue = 60f;
        }
        else
        {
            maxRewardValue = _offlineBooster.boostedValue[PlayerPrefs.GetInt("OfflineMoneyLevel")] * 60f;   
        }
        CheckOffline();
    }

    private void CheckOffline()
    {
        TimeSpan ts;
        if (PlayerPrefs.HasKey("LastGameSession"))
        {
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastGameSession"));
            if (ts.Minutes >= 1)
            {
                ShowOfflineMoneyScreen(ts);
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastGameSession",DateTime.Now.ToString());
    }

    [ContextMenu("DeleteLastGameSessionTime")]
    private void DeletePrefs()
    {
        PlayerPrefs.DeleteKey("LastGameSession");
    }
    
    
    private void ShowOfflineMoneyScreen(TimeSpan ts)
    {
        float hours = ts.Hours;
        float minutes = ts.Minutes;
        float seconds = ts.Seconds;

        offlineBoosterImage.transform.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnComplete(delegate
        {
            DOTween.To(x => hours = x, 0, hours, 0.75f).SetEase(Ease.Linear)
                .OnUpdate((() => timeSpendText.text = $"{Mathf.RoundToInt(hours)}ч:{Mathf.RoundToInt(minutes)}м:{Mathf.RoundToInt(seconds)}с"));
            DOTween.To(x => minutes = x, 0, minutes, 0.75f).SetEase(Ease.Linear)
                .OnUpdate((() => timeSpendText.text = $"{Mathf.RoundToInt(hours)}ч:{Mathf.RoundToInt(minutes)}м:{Mathf.RoundToInt(seconds)}с"));
            DOTween.To(x => seconds = x, 0, seconds, 0.75f).SetEase(Ease.Linear)
                .OnUpdate((() => timeSpendText.text = $"{Mathf.RoundToInt(hours)}ч:{Mathf.RoundToInt(minutes)}м:{Mathf.RoundToInt(seconds)}с")).OnComplete(
                    delegate
                    {
                        _reward = ((hours * 60) + minutes) * _offlineBooster.CurrentBoosterValue;
                        if (_reward > maxRewardValue)
                        {
                            _reward = maxRewardValue;
                        }
                        DOTween.To(x => _reward = x, 0, _reward, 0.75f).SetEase(Ease.Linear)
                            .OnUpdate((() => rewardedMoney.text = $"{Mathf.RoundToInt(_reward)}")).OnComplete(delegate
                            {
                                getRewardButton.interactable = true;
                            });
                    });

        });
    }

    public void TakeReward()
    {
        Money.Instance.AddMoney((int)_reward);
        getRewardButton.interactable = false;
        _reward = 0;
        offlineBoosterImage.transform.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear);
    }
}
