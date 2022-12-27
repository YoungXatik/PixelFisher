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

    public int _currentValueLevel { get; private set; }
    
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
        costText.text = costValue[_currentValueLevel + 1].ToString();
        CurrentBoosterValue = boostedValue[_currentValueLevel];
        BoosterSwitch();
        CheckForMoney();
    }

    private void CheckForMoney()
    {
        if (CurrentBoosterValue != costValue[costValue.Count - 1])
        {
            if (costValue[_currentValueLevel + 1] > Money.Instance.MoneyCount)
            {
                buyButton.interactable = false;
            }
            else if (costValue[_currentValueLevel + 1] <= Money.Instance.MoneyCount)
            {
                buyButton.interactable = true;
            }
        }
    }

    public void UpgradeCurrentValue()
    {
        
        if (CurrentBoosterValue != boostedValue[boostedValue.Count - 1])
        {
            if (Money.Instance.MoneyCount >= costValue[_currentValueLevel+1])
            {
                _currentValueLevel++;
                Money.Instance.RemoveMoney(costValue[_currentValueLevel]);
                CurrentBoosterValue = boostedValue[_currentValueLevel];
                UpdateUI();
                CheckForMoney();
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    private void UpdateUI()
    {
        if (CurrentBoosterValue != boostedValue[boostedValue.Count - 1])
        {
            costText.text = costValue[_currentValueLevel +1].ToString();
            BoosterSwitch();
        }
        else
        {
            return;
        }
    }

    private void BoosterSwitch()
    {
        switch (boostValue)
        {
            case BoosterValue.Length:
                EventManager.OnLengthValueChangedInvoke();
                currentValue.text = $"{boostedValue[_currentValueLevel]}m";
                break;
            case BoosterValue.Strength:
                EventManager.OnStrengthValueChangedInvoke();
                currentValue.text = $"{boostedValue[_currentValueLevel]}fishes";
                break;
            case BoosterValue.OfflineMoney:
                EventManager.OnOfflineMoneyValueChangedInvoke();
                currentValue.text = $"{boostedValue[_currentValueLevel]}/min";
                break;
        }
    }
}
