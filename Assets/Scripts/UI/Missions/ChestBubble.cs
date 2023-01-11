using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestBubble : MonoBehaviour
{
    [SerializeField] private Transform chestInformationObject;

    [SerializeField] private GameObject coinsRewardObject;
    [SerializeField] private int coinsReward;
    [SerializeField] private TextMeshProUGUI coinsRewardText;

    [SerializeField] private GameObject gemRewardObject;
    [SerializeField] private int gemsReward;
    [SerializeField] private TextMeshProUGUI gemsRewardText;

    [SerializeField] private GameObject boosterRewardObject;
    [SerializeField] private Sprite boosterRewardSprite;
    [SerializeField] private Image boosterRewardImage;
    
    private enum RewardType
    {
        Money, Gems, Booster
    }

    [SerializeField] private RewardType rewardType;

    private void Start()
    {
        //UpdateUI();
    }

    private void UpdateUI()
    {
        switch (rewardType)
        {
            case RewardType.Money:
                coinsRewardText.text = $"{coinsReward}";
                ShowReward(coinsRewardObject);
                /*HideReward(gemRewardObject);
                HideReward(boosterRewardObject);*/
                break;
            /*case RewardType.Gems:
                gemsRewardText.text = $"{gemsReward}";
                ShowReward(gemRewardObject);
                HideReward(coinsRewardObject);
                HideReward(boosterRewardObject);
                break;
            case RewardType.Booster:
                ShowReward(boosterRewardObject);
                HideReward(coinsRewardObject);
                HideReward(gemRewardObject);
                break;*/
        }
    }

    private void ShowReward(GameObject rewardTransform)
    {
        rewardTransform.SetActive(true);   
    }

    private void HideReward(GameObject rewardTransform)
    {
        rewardTransform.SetActive(false);
    }
    
    public void OnChestClick()
    {
        UpdateUI();
        chestInformationObject.DOScale(1, 0.5f).From(0).SetEase(Ease.Linear);
    }

    public void OnOpenChestClick()
    {
        chestInformationObject.DOScale(0, 0.5f).From(1).SetEase(Ease.Linear);
    }
}
