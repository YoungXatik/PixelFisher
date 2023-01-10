using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchHookUpgrade : MonoBehaviour,ICatchable
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

    [SerializeField] private Image completeBanner;

    private void OnEnable()
    {
        EventManager.OnLengthValueChanged += UpdateAchievementValue;
        _currentAchievementStage = PlayerPrefs.GetInt("CatchHookLengthStage");
        UpdateUI();
        CheckForReward();
    }

    private void OnDisable()
    {
        EventManager.OnLengthValueChanged -= UpdateAchievementValue;
    }

    private void Start()
    {
        currentCatchValue = PlayerPrefs.GetInt("HookLengthLevel");
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        _step = 1f / _needCatchValue;
        getRewardButton.interactable = false;
        CheckForReward();

        if (PlayerPrefs.GetInt("LengthRewardTaken") == 1)
        {
            completeBanner.gameObject.SetActive(true);
            //Destroy(gameObject);
        }
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue++;
        PlayerPrefs.SetInt("HookLengthLevel",currentCatchValue);
        CheckForReward();
        UpdateUI();
    }

    public void UpdateUI()
    {
        _needCatchValue = neededCatchValues[_currentAchievementStage];
        _step = 1f / _needCatchValue;
        currentCatchValue = PlayerPrefs.GetInt("HookLengthLevel");
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
            PlayerPrefs.SetInt("LengthRewardTaken", true ? 1 : 0);
            Debug.Log(PlayerPrefs.GetInt("LengthRewardTaken"));
            completeBanner.gameObject.SetActive(true);
        }
        else
        {
            UpdateReward();
        }
        EventManager.OnAchievementCollectedInvoke();
    }

    public void UpdateReward()
    {
        _currentAchievementStage++;
        _needCatchValue = neededCatchValues[_currentAchievementStage];
        PlayerPrefs.SetInt("CatchHookLengthStage",_currentAchievementStage);
        coinsReward *= _currentAchievementStage;
        fishCoinsReward *= _currentAchievementStage;
        _step = 1f / _needCatchValue;
        UpdateUI();
    }
}
