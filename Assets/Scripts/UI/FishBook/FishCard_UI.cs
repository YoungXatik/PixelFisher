using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishCard_UI : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image fishImage;
    [SerializeField] private Button collectRewardButton;

    [Header("Values")] 
    [SerializeField] private int rewardCount;
    [SerializeField] private string lockedFishDescription;
    [field: SerializeField] public FishType FishType { get; private set; }

    private bool _rewardHasBeenCollected;

    private void OnEnable()
    {
        UnlockFishCard();
    }

    public void UnlockCommonCard()
    {
        if (FishType.FishQuality == FishQuality.Common)
        {
            if (!FishType.isCollected)
            {
                descriptionText.text = lockedFishDescription;
                fishImage.sprite = FishType.lockedFishSprite;
                collectRewardButton.gameObject.SetActive(false);
            }
            else
            {
                descriptionText.text = FishType.fishDescription;
                fishImage.sprite = FishType.fishSprite;
                collectRewardButton.gameObject.SetActive(true);
            }
        }
    }

    public void UnlockRareCard()
    {
        if(FishType.FishQuality == FishQuality.Rare)
        {
            if (!FishType.isCollected)
            {
                descriptionText.text = lockedFishDescription;
                fishImage.sprite = FishType.lockedFishSprite;
                collectRewardButton.gameObject.SetActive(false);
            }
            else
            {
                descriptionText.text = FishType.fishDescription;
                fishImage.sprite = FishType.fishSprite;
                collectRewardButton.gameObject.SetActive(true);
            }
        }
    }

    public void UnlockFishCard()
    {
        if (!_rewardHasBeenCollected)
        {
            if (FishType.isCollected)
            {
                descriptionText.text = FishType.fishDescription;
                fishImage.sprite = FishType.fishSprite;
                collectRewardButton.gameObject.SetActive(true);
                Debug.Log(FishType.fishName);
            }
            else
            {
                descriptionText.text = lockedFishDescription;
                fishImage.sprite = FishType.lockedFishSprite;
                collectRewardButton.gameObject.SetActive(false);
            }
        }
        else
        {
            if (collectRewardButton != null)
            {
                Destroy(collectRewardButton);
            }
            else
            {
                return;
            }
        }
    }

    public void GetReward()
    {
        _rewardHasBeenCollected = true;
        Money.Instance.AddMoney(rewardCount);
        collectRewardButton.gameObject.SetActive(false);
    }
}
