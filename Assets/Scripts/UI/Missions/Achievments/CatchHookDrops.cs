using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchHookDrops : MonoBehaviour, ICatchable
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
        EventManager.OnGameStarted += UpdateAchievementValue;
        _currentAchievementStage = PlayerPrefs.GetInt("CatchHookDropsStage");
        UpdateUI();
        CheckForReward();
    }

    private void OnDisable()
    {
        EventManager.OnGameStarted -= UpdateAchievementValue;
    }

    private void Start()
    {
        currentCatchValue = PlayerPrefs.GetInt("HookDrops");
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        getRewardButton.interactable = false;
        CheckForReward();
        
        if (PlayerPrefs.GetInt("HookDropsRewardTaken") == 1)
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("HookDrops"))
        {
            currentCatchValue = PlayerPrefs.GetInt("HookDrops");
        }
        else
        {
            currentCatchValue = 0;
        }
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue++;
        PlayerPrefs.SetInt("HookDrops",currentCatchValue);
        CheckForReward();
        UpdateUI();
    }

    public void UpdateUI()
    {
        _needCatchValue = neededCatchValues[_currentAchievementStage];
        _step = 1f / _needCatchValue;
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        currentCatchValue = PlayerPrefs.GetInt("HookDrops");
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
            PlayerPrefs.SetInt("HookDropsRewardTaken", true ? 1 : 0);
            Debug.Log(PlayerPrefs.GetInt("HookDropsRewardTaken"));
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
        PlayerPrefs.SetInt("CatchHookDropsStage",_currentAchievementStage);
        coinsReward = coinsReward * (_currentAchievementStage);
        fishCoinsReward = fishCoinsReward * (_currentAchievementStage);
        _step = 1f / _needCatchValue;
        UpdateUI();
    }
}
