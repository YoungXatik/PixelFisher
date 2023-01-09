using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AchievementCardValues : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI coinsRewardText, fishCoinsRewardText;
    [SerializeField] private Button getRewardButton;

    [SerializeField] private string description;
    [SerializeField] private int currentCatchValue, needCatchValue;

    [SerializeField] private int coinsReward, fishCoinsReward;
    

    [field: SerializeField] public FishType FishType { get; private set; }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateAchievementValue()
    {
        
    }
    
    [SerializeField] private float step;
    private void UpdateUI()
    {
        
        if (currentCatchValue == needCatchValue)
        {
            UnlockReward();
        }
        else
        {
            getRewardButton.interactable = false;
        }
    }

    private void UpdateProgressBar()
    {
        var step = 1f / needCatchValue;
        progressImage.fillAmount = step * FishType.TotallyCatch;
    }

    private void UnlockReward()
    {
        if (!FishType.isAchieved)
        {
            getRewardButton.interactable = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeReward()
    {
        Money.Instance.AddMoney(coinsReward);
        MissionChestReward.Instance.AddFishCoins(fishCoinsReward);
        FishType.isAchieved = true;
        Destroy(gameObject);
    }
    
}
