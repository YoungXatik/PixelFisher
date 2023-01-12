using System;
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

    [SerializeField] private Button chestOpenButton;

    private List<IBoostable> _boostersList = new List<IBoostable>();
    [SerializeField] private List<GameObject> _boostersObjects = new List<GameObject>();

    private IBoostable _currentBooster;

    private bool _isReached;


    private void Awake()
    {
        for (int i = 0; i < _boostersObjects.Count; i++)
        {
            _boostersList.Add(_boostersObjects[i].GetComponent<IBoostable>());
        }
    }

    private enum RewardType
    {
        Money, Gems, Booster
    }

    [SerializeField] private RewardType rewardType;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Chest" + gameObject.name) == 1)
        {
            ChestReached();
        }
        else
        {
            UpdateUI();
            chestOpenButton.interactable = false;   
        }
        if (PlayerPrefs.GetInt("ChestRewardTaken" + gameObject.name) == 1)
        {
            RewardTaken();
        }
        else
        {
            UpdateUI();
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
                boosterRewardImage.sprite = _boostersList[Random.Range(0, _boostersList.Count)].GetBoosterImage();
                _currentBooster = _boostersList[Random.Range(0, _boostersList.Count)];
                ShowReward(boosterRewardObject);
                HideReward(coinsRewardObject);
                HideReward(gemRewardObject);
                break;
        }
    }

    public void ChestReached()
    {
        _isReached = true;
        chestOpenButton.interactable = true;
        PlayerPrefs.SetInt("Chest" + gameObject.name, true ? 1 : 0);
    }

    public void ClearChestReach()
    {
        _isReached = false;
        chestOpenButton.interactable = false;
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
        chestOpenButton.interactable = false;
        chestInformationObject.DOScale(1, 0.2f).From(0).SetEase(Ease.Linear);
        chestOpenButton.transform.DOPunchScale(chestOpenButton.transform.position, 0.2f);
    }

    public void OnOpenChestClick()
    {
        chestOpenButton.interactable = false;
        chestInformationObject.DOScale(0, 0.2f).From(1).SetEase(Ease.Linear);
        TakeReward();
    }

    private void RewardTaken()
    {
        chestOpenButton.gameObject.SetActive(false);
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
}
