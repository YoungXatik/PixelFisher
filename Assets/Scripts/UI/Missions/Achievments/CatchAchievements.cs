using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchAchievements : MonoBehaviour, ICatchable
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI coinsRewardText, fishCoinsRewardText;
    [SerializeField] private Button getRewardButton;

    [SerializeField] private string description;
    [SerializeField] private int currentCatchValue, needCatchValue;

    [SerializeField] private int coinsReward, fishCoinsReward;

    private float _step;

    private void OnEnable()
    {
        EventManager.OnAchievementCollected += UpdateAchievementValue;
        _step = 1f / needCatchValue;
        UpdateUI();
    }

    private void OnDisable()
    {
        EventManager.OnAchievementCollected -= UpdateAchievementValue;
        UpdateUI();
    }

    private void Start()
    {
        currentCatchValue = PlayerPrefs.GetInt("AchievementsCollected");
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        getRewardButton.interactable = false;

        if (PlayerPrefs.GetInt("AchievementsCollectedRewardTaken") == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
        
        if (PlayerPrefs.HasKey("AchievementsCollected"))
        {
            currentCatchValue = PlayerPrefs.GetInt("AchievementsCollected");
        }
        else
        {
            currentCatchValue = 0;
        }
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue++;
        PlayerPrefs.SetInt("AchievementsCollected",currentCatchValue);
        if (currentCatchValue >= needCatchValue)
        {
            UnlockReward();
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        currentCatchValue = PlayerPrefs.GetInt("AchievementsCollected");
        descriptionText.text = description;
        progressText.text = $"{currentCatchValue}/{needCatchValue}";
        UpdateProgressBar();
    }

    public void UpdateProgressBar()
    {
        progressImage.fillAmount = _step * currentCatchValue;
    }

    public void CheckForReward()
    {
        if (currentCatchValue >= needCatchValue)
        {
            UnlockReward();
        }
    }
    
    public void UnlockReward()
    {
        getRewardButton.interactable = true;
    }

    public void TakeReward()
    {
        Money.Instance.AddMoney(coinsReward);
        MissionChestReward.Instance.AddFishCoins(fishCoinsReward);
        getRewardButton.interactable = false;
        PlayerPrefs.SetInt("AchievementsCollectedRewardTaken", true ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("AchievementsCollectedRewardTaken"));
        Destroy(gameObject);
    }

    public void UpdateReward()
    {
        
    }
}
