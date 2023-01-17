using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyCollectRareFish : MonoBehaviour, ICatchable
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
        EventManager.OnRareFishHooked += UpdateAchievementValue;
        UpdateUI();
    }

    private void OnDisable()
    {
        EventManager.OnRareFishHooked -= UpdateAchievementValue;
        if (PlayerPrefs.HasKey("DailyHookedRareFish"))
        {
            currentCatchValue = PlayerPrefs.GetInt("DailyHookedRareFish");
        }
    }

    private void Start()
    {
        currentCatchValue = PlayerPrefs.GetInt("DailyHookedRareFish");
        coinsRewardText.text = $"{_coinsReward}";
        getRewardButton.interactable = false;

        if (!PlayerPrefs.HasKey("DailyHookedRareFishNeedValue") && !PlayerPrefs.HasKey("DailyHookedRareFishMoneyReward"))
        {
            _needCatchValue = Random.Range(minCatchValue, maxCatchValue);
            PlayerPrefs.SetInt("DailyHookedRareFishNeedValue",_needCatchValue);
            _coinsReward = Random.Range(minMoneyReward, maxMoneyReward);
            PlayerPrefs.SetInt("DailyHookedRareFishMoneyReward",_coinsReward);
            description = $"Catch rare fishes";
            UpdateUI();
        }
        else
        {
            _needCatchValue = PlayerPrefs.GetInt("DailyHookedRareFishNeedValue");
            _coinsReward = PlayerPrefs.GetInt("DailyHookedRareFishMoneyReward");
            description = $"Catch rare fishes";
            UpdateUI();
        }

        if (PlayerPrefs.HasKey("DailyHookedRareFish"))
        {
            currentCatchValue = PlayerPrefs.GetInt("DailyHookedRareFish");
        }
        else
        {
            currentCatchValue = 0;
        }

        if (PlayerPrefs.GetInt("DailyRareFishCollected") == 1)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue++;
        PlayerPrefs.SetInt("DailyHookedRareFish", currentCatchValue);
        CheckForReward();
        UpdateUI();
    }

    public void UpdateUI()
    {
        _step = 1f / _needCatchValue;
        currentCatchValue = PlayerPrefs.GetInt("DailyHookedRareFish");
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
        PlayerPrefs.SetInt("DailyRareFishCollected", true ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("DailyRareFishCollected"));
        Destroy(gameObject);
    }

    public void UpdateReward()
    {
    }
}