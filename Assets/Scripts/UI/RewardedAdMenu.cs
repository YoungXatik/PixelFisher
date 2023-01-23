using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RewardedAdMenu : MonoBehaviour
{
    [SerializeField] private Image menuImage;
    
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI upgradeRewardText;

    [SerializeField] private Button getRewardButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private List<GameObject> boostersObjects = new List<GameObject>();
    private List<IBoostable> _boostersList = new List<IBoostable>();

    private IBoostable _currentBooster;
    private int _boosterIndex;

    private void Awake()
    {
        for (int i = 0; i < boostersObjects.Count; i++)
        {
            _boostersList.Add(boostersObjects[i].GetComponent<IBoostable>());
        }

        _boosterIndex = Random.Range(0, boostersObjects.Count);
        _currentBooster = _boostersList[_boosterIndex];
    }

    public void OpenMenu()
    {
        menuImage.transform.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnUpdate(delegate
        {
            rewardText.text = $"{Hook.Instance.HookedFishCost}";
            upgradeRewardText.text = $"{Hook.Instance.HookedFishCost * 2}";
        }).OnComplete(delegate
        {
            getRewardButton.gameObject.SetActive(true);
            closeButton.interactable = true;
        });
    }

    public void OpenMenuWithoutTakeButton()
    {
        getRewardButton.gameObject.SetActive(false);
        menuImage.transform.DOScale(1, 0.25f).From(0).SetEase(Ease.Linear).OnUpdate(delegate
        {
            rewardText.text = $"{Hook.Instance.HookedFishCost}";
            upgradeRewardText.text = $"{Hook.Instance.HookedFishCost * 2}";
        }).OnComplete(delegate
        {
            closeButton.interactable = true;
        });
    }

    public void CloseMenu()
    {
        getRewardButton.gameObject.SetActive(false);
        closeButton.interactable = false;
        menuImage.transform.DOScale(0, 0.25f).From(1).SetEase(Ease.Linear);
    }

    public void TakeReward()
    {
        int chance = Random.Range(1, 10);
        if (chance >= 7)
        {
            _currentBooster.StartBooster();
            Money.Instance.AddMoney(Hook.Instance.HookedFishCost * 2);
            CloseMenu();
        }
        else if(chance < 7)
        {
            //Start AD
            Money.Instance.AddMoney(Hook.Instance.HookedFishCost * 2);
            CloseMenu(); 
        }
    }
}
