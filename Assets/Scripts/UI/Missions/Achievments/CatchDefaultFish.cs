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
    [SerializeField] private int currentCatchValue, needCatchValue;

    [SerializeField] private int coinsReward, fishCoinsReward;

    private float _step;

    private void OnEnable()
    {
        EventManager.OnCommonFishHooked += UpdateAchievementValue;
        UpdateAchievementValue();
    }

    private void OnDisable()
    {
        EventManager.OnCommonFishHooked -= UpdateAchievementValue;
    }

    private void Start()
    {
        currentCatchValue = PlayerPrefs.GetInt("HookedCommonFish");
        coinsRewardText.text = $"{coinsReward}";
        fishCoinsRewardText.text = $"{fishCoinsReward}";
        _step = 1f / needCatchValue;
        getRewardButton.interactable = false;
        UpdateAchievementValue();

        if (PlayerPrefs.GetInt("CommonFishRewardTaken") == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }

        currentCatchValue = 0;
    }

    public void UpdateAchievementValue()
    {
        currentCatchValue++;
        PlayerPrefs.SetInt("HookedCommonFish",currentCatchValue);
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
        PlayerPrefs.SetInt("CommonFishRewardTaken", true ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("CommonFishRewardTaken"));
        Destroy(gameObject);
    }
}