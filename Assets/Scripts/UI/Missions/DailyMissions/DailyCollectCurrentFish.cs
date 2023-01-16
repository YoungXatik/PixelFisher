using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyCollectCurrentFish : MonoBehaviour, ICatchable
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI coinsRewardText;
    [SerializeField] private Button getRewardButton;

    [SerializeField] private string description;
    [SerializeField] private int currentCatchValue;
    private int _needCatchValue;

    [SerializeField] private int minCatchValue, maxCatchValue;
    [SerializeField] private int minMoneyReward, maxMoneyReward;

    private int _coinsReward;
    private float _step;

    private void OnEnable()
    {
        EventManager.OnRareFishHooked += CheckFish;
        EventManager.OnCommonFishHooked += CheckFish;
        UpdateUI();
    }

    private void OnDisable()
    {
        EventManager.OnRareFishHooked -= CheckFish;
        EventManager.OnCommonFishHooked -= CheckFish;
        if (PlayerPrefs.HasKey("DailyHookedCurrentFish" + gameObject.name))
        {
            currentCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFish"+ gameObject.name);
        }
    }

    private FishType _currentFishType;
    [SerializeField] private FishType neededFish;
    
    private void CheckFish()
    {
        _currentFishType = Hook.Instance.currentHookedFish;
        if (_currentFishType == neededFish)
        {
            UpdateAchievementValue();
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("CurrentFishIndex"+ gameObject.name))
        {
            neededFish = MissionMenuValues.Instance.availableFishTypes[PlayerPrefs.GetInt("CurrentFishIndex"+ gameObject.name)];
        }
        else
        {
            neededFish = MissionMenuValues.Instance.PickRandomAvailableFish();
            PlayerPrefs.SetInt("CurrentFishIndex"+ gameObject.name,MissionMenuValues.Instance.Index);
        }
        currentCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFish"+ gameObject.name);
        coinsRewardText.text = $"{_coinsReward}";
        getRewardButton.interactable = false;

        if (!PlayerPrefs.HasKey("DailyHookedCurrentFishNeedValue"+ gameObject.name) && !PlayerPrefs.HasKey("DailyHookedCurrentFishMoneyReward"+ gameObject.name))
        {
            _needCatchValue = Random.Range(minCatchValue, maxCatchValue);
            PlayerPrefs.SetInt("DailyHookedCurrentFishNeedValue"+ gameObject.name,_needCatchValue);
            _coinsReward = Random.Range(minMoneyReward, maxMoneyReward);
            PlayerPrefs.SetInt("DailyHookedCurrentFishMoneyReward"+ gameObject.name,_coinsReward);
            description = $"Поймайте {neededFish.fishName}";
            UpdateUI();
        }
        else
        {
            _needCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFishNeedValue"+ gameObject.name);
            _coinsReward = PlayerPrefs.GetInt("DailyHookedCurrentFishMoneyReward"+ gameObject.name);
            description = $"Поймайте {neededFish.fishName}";
            UpdateUI();
        }

        if (PlayerPrefs.HasKey("DailyHookedCurrentFish"+ gameObject.name))
        {
            currentCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFish"+ gameObject.name);
        }
        else
        {
            currentCatchValue = 0;
        }

        if (PlayerPrefs.GetInt("DailyCurrentFishCollected"+ gameObject.name) == 1)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue++;
        PlayerPrefs.SetInt("DailyHookedCurrentFish"+ gameObject.name, currentCatchValue);
        CheckForReward();
        UpdateUI();
    }

    public void UpdateUI()
    {
        _step = 1f / _needCatchValue;
        currentCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFish"+ gameObject.name);
        descriptionText.text = description;
        progressText.text = $"{currentCatchValue}/{_needCatchValue}";
        coinsRewardText.text = $"{_coinsReward}";
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
        Money.Instance.AddMoney(_coinsReward);
        getRewardButton.interactable = false;
        PlayerPrefs.SetInt("DailyCurrentFishCollected"+ gameObject.name, true ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("DailyCurrentFishCollected"+ gameObject.name));
        Destroy(gameObject);
    }

    public void UpdateReward()
    {
    }
}
