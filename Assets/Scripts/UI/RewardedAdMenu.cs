using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdMenu : MonoBehaviour
{
    [SerializeField] private Image menuImage;
    
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI upgradeRewardText;

    [SerializeField] private Button getRewardButton;
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        menuImage.transform.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnUpdate(delegate
        {
            rewardText.text = $"{Hook.Instance.HookedFishCost}";
            upgradeRewardText.text = $"{Hook.Instance.HookedFishCost * 2}";
        }).OnComplete(delegate
        {
            getRewardButton.interactable = true;
            closeButton.interactable = true;
        });
    }

    public void CloseMenu()
    {
        getRewardButton.interactable = false;
        closeButton.interactable = false;
        menuImage.transform.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear);
    }
}
