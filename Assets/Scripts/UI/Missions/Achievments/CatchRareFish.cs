using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchRareFish : MonoBehaviour, ICatchable
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
        EventManager.OnRareFishHooked += UpdateAchievementValue;
        _step = 1f / needCatchValue;
        UpdateUI();
    }

    private void OnDisable()
    {
        EventManager.OnRareFishHooked -= UpdateAchievementValue;
        if (PlayerPrefs.HasKey("HookedRareFish"))
        {
            currentCatchValue = PlayerPrefs.GetInt("HookedRareFish");
        }
    }

    private void Start()
    {
        currentCatchValue = PlayerPrefs.GetInt("HookedRareFish");
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
        
        if (PlayerPrefs.HasKey("HookedRareFish"))
        {
            currentCatchValue = PlayerPrefs.GetInt("HookedRareFish");
        }
        else
        {
            currentCatchValue = 0;
        }
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue++;
        PlayerPrefs.SetInt("HookedRareFish",currentCatchValue);
        if (currentCatchValue >= needCatchValue)
        {
            UnlockReward();
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        currentCatchValue = PlayerPrefs.GetInt("HookedRareFish");
        descriptionText.text = description;
        progressText.text = $"{currentCatchValue}/{needCatchValue}";
        UpdateProgressBar();
    }

    public void UpdateProgressBar()
    {
        progressImage.fillAmount = _step * currentCatchValue;
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
        PlayerPrefs.SetInt("RareFishRewardTaken", true ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("RareFishRewardTaken"));
        EventManager.OnAchievementCollectedInvoke();
        Destroy(gameObject);
    }
}
