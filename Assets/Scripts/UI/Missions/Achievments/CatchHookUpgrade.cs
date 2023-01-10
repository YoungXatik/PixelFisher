using System;
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
    [SerializeField] private int currentCatchValue, needCatchValue;

    [SerializeField] private int coinsReward, fishCoinsReward;

    [SerializeField] private Booster booster;
    
    private float _step;

    private void OnEnable()
    {
        EventManager.OnLengthValueChanged += UpdateAchievementValue;
        UpdateAchievementValue();
    }

    private void OnDisable()
    {
        EventManager.OnLengthValueChanged -= UpdateAchievementValue;
    }

    private void Start()
    {
        currentCatchValue = PlayerPrefs.GetInt("HookLengthLevel");
        needCatchValue = booster.boostedValue.Count - 1;
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        _step = 1f / needCatchValue;
        getRewardButton.interactable = false;
        UpdateAchievementValue();

        if (PlayerPrefs.GetInt("LengthRewardTaken") == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue = PlayerPrefs.GetInt("HookLengthLevel") + 1;
        if (currentCatchValue >= needCatchValue)
        {
            UnlockReward();
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
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
        PlayerPrefs.SetInt("LengthRewardTaken",true ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("LengthRewardTaken"));
        EventManager.OnAchievementCollectedInvoke();
        Destroy(gameObject);
    }

    public void UpdateReward()
    {
        
    }
}
