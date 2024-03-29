using System;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    #region Singleton

    public static Hook Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    #endregion
    
    [SerializeField] private int maxCountOfFish, countOfFish;
    [SerializeField] private HookController hookController;

    private Transform _hookTransform;

    [SerializeField] private Booster strengthBooster;

    public int HookedFishCost { get; private set; }
    [SerializeField] private float fishCostMultiplier = 1;
    private float _trueFishCostMultiplier;
    
    private List<Fish> _hookedFish = new List<Fish>();

    private void Start()
    {
        _hookTransform = GetComponent<Transform>();
        
        UpdateStrengthValue();

        _trueFishCostMultiplier = fishCostMultiplier;
    }

    private void OnEnable()
    {
        EventManager.OnStrengthValueChanged += UpdateStrengthValue;
        EventManager.OnGameEnded += RefreshCountOfFishValue;
        EventManager.OnGameEnded += SellHookedFish;
        EventManager.OnGameStarted += ClearMoneyValue;
    }

    private void OnDisable()
    {
        EventManager.OnStrengthValueChanged -= UpdateStrengthValue;
        EventManager.OnGameEnded -= RefreshCountOfFishValue;
        EventManager.OnGameEnded -= SellHookedFish;
        EventManager.OnGameStarted -= ClearMoneyValue;
    }

    private void ClearMoneyValue()
    {
        HookedFishCost = 0;
    }
    
    private void UpdateStrengthValue()
    {
        maxCountOfFish = strengthBooster.CurrentBoosterValue;
    }

    [field: SerializeField] public FishType currentHookedFish;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Fish fish;

        if (other.gameObject.TryGetComponent<Fish>(out fish))
        {
            hookController.CheckForFirstFishEntry();
            currentHookedFish = fish.fishType;
            CheckForFishQuality(fish);
            CheckForUnCollectedFish(fish);
            hookController.hookedFish.Add(fish);
            hookController.FishDepthValueCounter(fish);
            _hookedFish.Add(fish);
            fish.fishType.IncreaseCatchValue();
            countOfFish++;
            if (countOfFish >= maxCountOfFish)
            {
                hookController.HookCountIsOver();
            }
            fish.FishHasBeenHooked(_hookTransform);
            HookedFishCost += Mathf.RoundToInt((fish.fishCost * fishCostMultiplier));
        }
    }

    private void CheckForFishQuality(Fish fish)
    {
        if (fish.fishType.FishQuality == FishQuality.Common)
        {
            EventManager.OnCommonFishHookedInvoke();
        }
        else
        {
            EventManager.OnRareFishHookedInvoke();
        }
    }
    
    private void CheckForUnCollectedFish(Fish fish)
    {
        if (!fish.fishType.isCollected)
        {
            EventManager.OnFishCollectedInvoke();
        }
    }

    public void IncreaseFishCost(float multiplier)
    {
        fishCostMultiplier += multiplier;
    }

    public void DecreaseFishCost()
    {
        fishCostMultiplier = _trueFishCostMultiplier;
    }

    private void RefreshCountOfFishValue()
    {
        countOfFish = 0;
    }

    private void SellHookedFish()
    {
        if (_hookedFish.Count != 0)
        {
            Money.Instance.AddMoney(HookedFishCost);
            for (int i = 0; i < _hookedFish.Count; i++)
            {
                Destroy(_hookedFish[i].gameObject);
            }

            _hookedFish.Clear();
        }
    }
}