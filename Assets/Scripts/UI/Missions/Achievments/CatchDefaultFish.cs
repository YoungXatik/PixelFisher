using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchDefaultFish : MonoBehaviour, ICatchable
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
    
    public List<int> neededCatchValues = new List<int>();
    private int _currentAchievementStage;

    private float _step;

    private void OnEnable()
    {
        EventManager.OnCommonFishHooked += UpdateAchievementValue;
        _currentAchievementStage = PlayerPrefs.GetInt("CatchCommonFishStage");
        UpdateUI();
    }

    private void OnDisable()
    {
        EventManager.OnCommonFishHooked -= UpdateAchievementValue;
        if (PlayerPrefs.HasKey("HookedCommonFish"))
        {
            currentCatchValue = PlayerPrefs.GetInt("HookedCommonFish");
        }
    }

    private void Start()
    {
        currentCatchValue = PlayerPrefs.GetInt("HookedCommonFish");
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        getRewardButton.interactable = false;

        if (PlayerPrefs.GetInt("CommonFishRewardTaken") == 1)
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("HookedCommonFish"))
        {
            currentCatchValue = PlayerPrefs.GetInt("HookedCommonFish");
        }
        else
        {
            currentCatchValue = 0;
        }
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue++;
        PlayerPrefs.SetInt("HookedCommonFish",currentCatchValue);
        CheckForReward();
        UpdateUI();
    }

    public void UpdateUI()
    {
        _needCatchValue = neededCatchValues[_currentAchievementStage];
        _step = 1f / _needCatchValue;
        currentCatchValue = PlayerPrefs.GetInt("HookedCommonFish");
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
            PlayerPrefs.SetInt("CommonFishRewardTaken", true ? 1 : 0);
            Debug.Log(PlayerPrefs.GetInt("CommonFishRewardTaken"));
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
        PlayerPrefs.SetInt("CatchCommonFishStage",_currentAchievementStage);
        coinsReward *= _currentAchievementStage;
        fishCoinsReward *= _currentAchievementStage;
        _step = 1f / _needCatchValue;
        UpdateUI();
    }
}