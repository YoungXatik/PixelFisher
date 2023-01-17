using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    [SerializeField] private Image boosterRewardImage;

    [SerializeField] private Button openChestButton;
    [SerializeField] private Button chestTakeButton;

    private List<IBoostable> _boostersList = new List<IBoostable>();
    [SerializeField] private List<GameObject> _boostersObjects = new List<GameObject>();
    private int _boosterIndex;

    private IBoostable _currentBooster;

    private bool _isReached;


    private void Awake()
    {
        for (int i = 0; i < _boostersObjects.Count; i++)
        {
            _boostersList.Add(_boostersObjects[i].GetComponent<IBoostable>());
        }

        _boosterIndex = Random.Range(0, _boostersObjects.Count);
    }

    private enum RewardType
    {
        Money, Gems, Booster
    }

    [SerializeField] private RewardType rewardType;

    private void Start()
    {
        chestTakeButton.interactable = false;
        if (PlayerPrefs.GetInt("Chest" + gameObject.name) == 1)
        {
            ChestReached();
        }
        else
        {
            UpdateUI();
        }
        if (PlayerPrefs.GetInt("ChestRewardTaken" + gameObject.name) == 1)
        {
            RewardTaken();
        }
    }

    private void UpdateUI()
    {
        switch (rewardType)
        {
            case RewardType.Money:
                coinsRewardText.text = $"{coinsReward}";
                ShowReward(coinsRewardObject);
                HideReward(gemRewardObject);
                HideReward(boosterRewardObject);
                break;
            case RewardType.Gems:
                gemsRewardText.text = $"{gemsReward}";
                ShowReward(gemRewardObject);
                HideReward(coinsRewardObject);
                HideReward(boosterRewardObject);
                break;
            case RewardType.Booster:
                boosterRewardImage.sprite = _boostersList[_boosterIndex].GetBoosterImage();
                _currentBooster = _boostersList[_boosterIndex];
                ShowReward(boosterRewardObject);
                HideReward(coinsRewardObject);
                HideReward(gemRewardObject);
                break;
        }
    }
    
    public void ClearChestReach()
    {
        _isReached = false;
    }

    private void ShowReward(GameObject rewardTransform)
    {
        rewardTransform.SetActive(true);   
    }

    private void HideReward(GameObject rewardTransform)
    {
        rewardTransform.SetActive(false);
    }
    
    public void ChestReached()
    {
        _isReached = true;
        chestTakeButton.interactable = true;
        PlayerPrefs.SetInt("Chest" + gameObject.name, true ? 1 : 0);
    }
    
    public void OnChestClick()
    {
        UpdateUI();
        openChestButton.interactable = false;
        OpenChestInformation();
        StartCoroutine(CloseChestCoroutine());
    }

    public void OnOpenChestClick()
    {
        openChestButton.gameObject.SetActive(false);
        chestInformationObject.DOScale(0, 0.2f).From(1).SetEase(Ease.Linear).OnComplete(TakeReward);
    }

    private void OpenChestInformation()
    {
        chestInformationObject.DOScale(1, 0.2f).From(0).SetEase(Ease.Linear);
    }

    private void CloseChestInformation()
    {
        chestInformationObject.DOScale(0, 0.2f).From(1).SetEase(Ease.Linear);
    }

    private void RewardTaken()
    {
        Destroy(gameObject);
    }
    
    private void TakeReward()
    {
        switch (rewardType)
        {
            case RewardType.Money:
                Money.Instance.AddMoney(coinsReward);
                PlayerPrefs.SetInt("ChestRewardTaken" + gameObject.name, true ? 1 : 0);
                Destroy(gameObject);
                break;
            case RewardType.Gems:
                Money.Instance.AddDiamonds(gemsReward);
                PlayerPrefs.SetInt("ChestRewardTaken" + gameObject.name, true ? 1 : 0);
                Destroy(gameObject);
                break;
            case RewardType.Booster:
                _currentBooster.StartBooster();
                PlayerPrefs.SetInt("ChestRewardTaken" + gameObject.name, true ? 1 : 0);
                Destroy(gameObject);
                break;
        }
    }

    [SerializeField] private float timeToCloseChest;
    private IEnumerator CloseChestCoroutine()
    {
        yield return new WaitForSeconds(timeToCloseChest);
        CloseChestInformation();
        openChestButton.interactable = true;
    }
}
