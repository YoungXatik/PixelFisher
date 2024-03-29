using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        //costText.text = costValue[currentValueLevel + 1].ToString();
        //CurrentBoosterValue = boostedValue[currentValueLevel];
        
        UpdateCurrentValueLevel();
    }

    public void UpdateCurrentValueLevel()
    {
        switch (boostValue)
        {
            case BoosterValue.Length:
                if (currentValueLevel >= boostedValue.Count - 1)
                {
                    currentValueLevel = boostedValue.Count - 1;
                    PlayerPrefs.SetInt("HookLengthLevel" + SceneManager.GetActiveScene().name,currentValueLevel);
                }
                else
                {
                    currentValueLevel = PlayerPrefs.GetInt("HookLengthLevel"+ SceneManager.GetActiveScene().name);
                }
                CurrentBoosterValue = boostedValue[currentValueLevel];
                currentValue.text = $"{boostedValue[currentValueLevel]}/m";
                if (PlayerPrefs.GetInt("HookLengthLevel"+ SceneManager.GetActiveScene().name) != boostedValue.Count - 1)
                {
                    costText.text = costValue[currentValueLevel + 1].ToString();
                }
                else if(PlayerPrefs.GetInt("HookLengthLevel"+ SceneManager.GetActiveScene().name) == boostedValue.Count - 1)
                {
                   UpdateUI();
                }
                break;
            case BoosterValue.Strength:
                if (currentValueLevel >= boostedValue.Count - 1)
                {
                    currentValueLevel = boostedValue.Count - 1;
                    PlayerPrefs.SetInt("HookStrengthLevel"+ SceneManager.GetActiveScene().name,currentValueLevel);
                }
                else
                {
                    currentValueLevel = PlayerPrefs.GetInt("HookStrengthLevel"+ SceneManager.GetActiveScene().name);
                }
                CurrentBoosterValue = boostedValue[currentValueLevel];
                currentValue.text = $"{boostedValue[PlayerPrefs.GetInt("HookStrengthLevel"+ SceneManager.GetActiveScene().name)]}/fishes";
                if (PlayerPrefs.GetInt("HookStrengthLevel"+ SceneManager.GetActiveScene().name) != boostedValue.Count - 1)
                {
                    costText.text = costValue[currentValueLevel + 1].ToString();
                }
                else
                {
                    UpdateUI();
                }
                break;
            case BoosterValue.OfflineMoney:
                if (currentValueLevel >= boostedValue.Count - 1)
                {
                    currentValueLevel = boostedValue.Count - 1;
                    PlayerPrefs.SetInt("OfflineMoneyLevel"+ SceneManager.GetActiveScene().name,currentValueLevel);
                }
                else
                {
                    currentValueLevel = PlayerPrefs.GetInt("OfflineMoneyLevel"+ SceneManager.GetActiveScene().name);
                }
                CurrentBoosterValue = boostedValue[currentValueLevel];
                currentValue.text = $"{boostedValue[PlayerPrefs.GetInt("OfflineMoneyLevel"+ SceneManager.GetActiveScene().name)]}/min";
                if (PlayerPrefs.GetInt("OfflineMoneyLevel"+ SceneManager.GetActiveScene().name) != boostedValue.Count - 1)
                {
                    costText.text = costValue[currentValueLevel + 1].ToString();
                }
                else
                {
                    UpdateUI();
                }
                break;
        }
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
            BoosterSwitch();
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
                currentValue.text = $"{boostedValue[currentValueLevel]}/m";
                PlayerPrefs.SetInt("HookLengthLevel"+ SceneManager.GetActiveScene().name,currentValueLevel);
                break;
            case BoosterValue.Strength:
                EventManager.OnStrengthValueChangedInvoke();
                currentValue.text = $"{boostedValue[currentValueLevel]}/fishes";
                PlayerPrefs.SetInt("HookStrengthLevel"+ SceneManager.GetActiveScene().name,currentValueLevel);
                break;
            case BoosterValue.OfflineMoney:
                EventManager.OnOfflineMoneyValueChangedInvoke();
                currentValue.text = $"{boostedValue[currentValueLevel]}/min";
                PlayerPrefs.SetInt("OfflineMoneyLevel"+ SceneManager.GetActiveScene().name,currentValueLevel);
                break;
        }
    }
}
