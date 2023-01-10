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
    [SerializeField] private int currentCatchValue, needCatchValue;

    [SerializeField] private int coinsReward, fishCoinsReward;

    private float _step;

    private void OnEnable()
    {
        EventManager.OnGameStarted += UpdateAchievementValue;
        _step = 1f / needCatchValue;
        UpdateUI();
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

        if (PlayerPrefs.GetInt("RareFishRewardTaken") == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
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
        if (currentCatchValue >= needCatchValue)
        {
            UnlockReward();
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        currentCatchValue = PlayerPrefs.GetInt("HookDrops");
        descriptionText.text = description;
        progressText.text = $"{currentCatchValue}/{needCatchValue}";
        UpdateProgressBar();
    }

    public void UpdateProgressBar()
    {
        progressImage.fillAmount = _step * currentCatchValue;
    }

    public void CheckForReward()
    {
        if (currentCatchValue >= needCatchValue)
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
        PlayerPrefs.SetInt("HookDropsRewardTaken", true ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("HookDropsRewardTaken"));
        EventManager.OnAchievementCollectedInvoke();
        Destroy(gameObject);
    }

    public void UpdateReward()
    {
        
    }
}
