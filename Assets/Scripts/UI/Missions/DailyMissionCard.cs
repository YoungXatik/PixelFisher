using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DailyMissionCard : MonoBehaviour
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
        CreateMissionCardValues();
        UpdateUI();
    }

    private void OnEnable()
    {
        if (FishType != null)
        {
            UpdateUI();
        }
    }

    private void CreateMissionCardValues()
    {
        FishType = MissionMenuValues.Instance.PickRandomFishType();
        description = $"Поймать {needCatchValue} {FishType.fishName}";
        coinsReward = Random.Range(needCatchValue * 5, needCatchValue * 10);
        fishCoinsReward = Random.Range(needCatchValue * 5, needCatchValue * 10);
    }

    [SerializeField] private float step;
    private void UpdateUI()
    {
        descriptionText.text = description;
        currentCatchValue = FishType.TotallyCatch;
        progressText.text = $"{currentCatchValue}/{needCatchValue}";
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        UpdateProgressBar();
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
