using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentValue;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI costText;
    
    public List<int> boostedValue = new List<int>();
    public List<int> costValue = new List<int>();

    public int currentValueLevel;
    
    [field: SerializeField] public int CurrentBoosterValue { get; private set; }

    private enum BoosterValue
    {
        Length, Strength, OfflineMoney
    }

    [SerializeField] private BoosterValue boostValue;

    private void OnEnable()
    {
        EventManager.OnMoneyChanged += CheckForMoney;
    }

    private void OnDisable()
    {
        EventManager.OnMoneyChanged -= CheckForMoney;
    }

    private void Start()
    {
        costText.text = costValue[currentValueLevel + 1].ToString();
        CurrentBoosterValue = boostedValue[currentValueLevel];

        UpdateCurrentValueLevel();
    }

    public void UpdateCurrentValueLevel()
    {
        switch (boostValue)
        {
            case BoosterValue.Length:
                currentValueLevel = PlayerPrefs.GetInt("HookLengthLevel");
                currentValue.text = $"{boostedValue[PlayerPrefs.GetInt("HookLengthLevel")]}m";
                costText.text = costValue[currentValueLevel + 1].ToString();
                break;
            case BoosterValue.Strength:
                currentValueLevel = PlayerPrefs.GetInt("HookStrengthLevel");
                currentValue.text = $"{boostedValue[PlayerPrefs.GetInt("HookStrengthLevel")]}fishes";
                costText.text = costValue[currentValueLevel + 1].ToString();
                break;
            case BoosterValue.OfflineMoney:
                currentValueLevel = PlayerPrefs.GetInt("OfflineMoneyLevel");
                currentValue.text = $"{boostedValue[PlayerPrefs.GetInt("OfflineMoneyLevel")]}min";
                costText.text = costValue[currentValueLevel + 1].ToString();
                break;
        }
        CheckForMoney();
    }

    private void CheckForMoney()
    {
        if (currentValueLevel + 1 != boostedValue.Count)
        {
            if (costValue[currentValueLevel + 1] > Money.Instance.MoneyCount)
            {
                buyButton.interactable = false;
            }
            else if (costValue[currentValueLevel + 1] <= Money.Instance.MoneyCount)
            {
                buyButton.interactable = true;
            }
        }
    }

    public void UpgradeCurrentValue()
    {
        if (currentValueLevel + 1 != boostedValue.Count)
        {
            if (Money.Instance.MoneyCount >= costValue[currentValueLevel + 1])
            {
                currentValueLevel++;
                Money.Instance.RemoveMoney(costValue[currentValueLevel]);
                CurrentBoosterValue = boostedValue[currentValueLevel];
                UpdateUI();
                CheckForMoney();
            }
        }
        else
        {
            return;
        }
    }
    

    private void UpdateUI()
    {
        if (currentValueLevel + 1 != boostedValue.Count)
        {
            costText.text = costValue[currentValueLevel +1].ToString();
            BoosterSwitch();
        }
        else
        {
            costText.text = "-";
            buyButton.interactable = false;
            currentValue.text = $"{boostedValue[boostedValue.Count - 1]}";
        }
    }

    private void BoosterSwitch()
    {
        switch (boostValue)
        {
            case BoosterValue.Length:
                EventManager.OnLengthValueChangedInvoke();
                currentValue.text = $"{boostedValue[currentValueLevel]}m";
                break;
            case BoosterValue.Strength:
                EventManager.OnStrengthValueChangedInvoke();
                currentValue.text = $"{boostedValue[currentValueLevel]}fishes";
                break;
            case BoosterValue.OfflineMoney:
                EventManager.OnOfflineMoneyValueChangedInvoke();
                currentValue.text = $"{boostedValue[currentValueLevel]}/min";
                break;
        }
    }
}
