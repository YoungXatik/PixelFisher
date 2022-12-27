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

    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        descriptionText.text = description;
        progressText.text = $"{currentCatchValue}/{needCatchValue}";
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        var step = progressImage.fillAmount / needCatchValue;
        progressImage.fillAmount = step * currentCatchValue;
    }
}
