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
        if (PlayerPrefs.HasKey("DailyHookedCurrentFish"))
        {
            currentCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFish");
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
        if (PlayerPrefs.HasKey("CurrentFishIndex" ))
        {
            neededFish = MissionMenuValues.Instance.fishTypes[PlayerPrefs.GetInt("CurrentFishIndex")];
        }
        else
        {
            neededFish = MissionMenuValues.Instance.PickRandomFishType();
            PlayerPrefs.SetInt("CurrentFishIndex",MissionMenuValues.Instance.Index); 
        }
        currentCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFish");
        coinsRewardText.text = $"{_coinsReward}";
        getRewardButton.interactable = false;

        if (!PlayerPrefs.HasKey("DailyHookedCurrentFishNeedValue") && !PlayerPrefs.HasKey("DailyHookedCurrentFishMoneyReward"))
        {
            _needCatchValue = Random.Range(minCatchValue, maxCatchValue);
            PlayerPrefs.SetInt("DailyHookedCurrentFishNeedValue",_needCatchValue);
            _coinsReward = Random.Range(minMoneyReward, maxMoneyReward);
            PlayerPrefs.SetInt("DailyHookedCurrentFishMoneyReward",_coinsReward);
            description = $"Поймайте {neededFish.fishName}";
            UpdateUI();
        }
        else
        {
            _needCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFishNeedValue");
            _coinsReward = PlayerPrefs.GetInt("DailyHookedCurrentFishMoneyReward");
            description = $"Поймайте {neededFish.fishName}";
            UpdateUI();
        }

        if (PlayerPrefs.HasKey("DailyHookedCurrentFish"))
        {
            currentCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFish");
        }
        else
        {
            currentCatchValue = 0;
        }

        if (PlayerPrefs.GetInt("DailyCurrentFishCollected") == 1)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue++;
        PlayerPrefs.SetInt("DailyHookedCurrentFish", currentCatchValue);
        CheckForReward();
        UpdateUI();
    }

    public void UpdateUI()
    {
        _step = 1f / _needCatchValue;
        currentCatchValue = PlayerPrefs.GetInt("DailyHookedCurrentFish");
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
        PlayerPrefs.SetInt("DailyCurrentFishCollected", true ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("DailyCurrentFishCollected"));
        Destroy(gameObject);
    }

    public void UpdateReward()
    {
    }
}
