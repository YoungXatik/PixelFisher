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
    [SerializeField] private int currentCatchValue;
    private int _needCatchValue;

    [SerializeField] private int coinsReward, fishCoinsReward;

    private float _step;
    
    public List<int> neededCatchValues = new List<int>();
    private int _currentAchievementStage;

    private void OnEnable()
    {
        EventManager.OnAchievementCollected += UpdateAchievementValue;
        _currentAchievementStage = PlayerPrefs.GetInt("CatchAchievementsStage");
        UpdateUI();
        CheckForReward();
    }

    private void OnDisable()
    {
        EventManager.OnAchievementCollected -= UpdateAchievementValue;
    }

    private void Start()
    {
        currentCatchValue = PlayerPrefs.GetInt("AchievementsCollected");
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        getRewardButton.interactable = false;
        CheckForReward();
        
        if (PlayerPrefs.GetInt("AchievementsCollectedRewardTaken") == 1)
        {
            Destroy(gameObject);
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
        CheckForReward();
        UpdateUI();
    }

    public void UpdateUI()
    {
        _needCatchValue = neededCatchValues[_currentAchievementStage];
        _step = 1f / _needCatchValue;
        currentCatchValue = PlayerPrefs.GetInt("AchievementsCollected");
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        descriptionText.text = description;
        progressText.text = $"{currentCatchValue}/{_needCatchValue}";
        UpdateProgressBar();
        CheckForReward();
    }

    public void UpdateProgressBar()
    {
        progressImage.fillAmount = _step * currentCatchValue;
    }

    public void CheckForReward()
    {
        if (currentCatchValue >= _needCatchValue)
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
        if(_currentAchievementStage == (neededCatchValues.Count - 1))
        {
            PlayerPrefs.SetInt("AchievementsCollectedRewardTaken", true ? 1 : 0);
            Debug.Log(PlayerPrefs.GetInt("AchievementsCollectedRewardTaken"));
        }
        else
        {
            UpdateReward();
        }
    }

    public void UpdateReward()
    {
        _currentAchievementStage++;
        _needCatchValue = neededCatchValues[_currentAchievementStage];
        PlayerPrefs.SetInt("CatchAchievementsStage",_currentAchievementStage);
        coinsReward = coinsReward * (_currentAchievementStage);
        fishCoinsReward = fishCoinsReward * (_currentAchievementStage);
        _step = 1f / _needCatchValue;
        UpdateUI();   
    }
}
