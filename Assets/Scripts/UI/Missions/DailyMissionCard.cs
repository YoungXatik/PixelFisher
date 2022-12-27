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
        FishType.ResetCatchValue();
        description = $"Поймать {needCatchValue} {FishType.fishName}";
        coinsReward = Random.Range(needCatchValue * 5, needCatchValue * 10);
        fishCoinsReward = Random.Range(needCatchValue * 5, needCatchValue * 10);
    }

    [SerializeField] private float step;
    private void UpdateUI()
    {
        descriptionText.text = description;
        progressText.text = $"{FishType.TotallyCatch}/{needCatchValue}";
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        step = progressImage.fillAmount / needCatchValue;
        progressImage.fillAmount = step * FishType.TotallyCatch;
    }
}
