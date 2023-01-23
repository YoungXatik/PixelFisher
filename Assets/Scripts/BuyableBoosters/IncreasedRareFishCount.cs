using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreasedRareFishCount : MonoBehaviour, IBoostable
{
    [SerializeField] private float duration;
    private float _durationCounter;

    [SerializeField] private Sprite boosterIcon;

    [Range(2,4)] 
    [SerializeField] private int increasedRareFishMultiplier;

    [SerializeField] private Button buyButton;

    private bool _available;
    private ShopCell _shopCell;

    [SerializeField] private BoosterTimer boosterTimer;

    [SerializeField] private string boosterDescription;
    private void Start()
    {
        _shopCell = GetComponent<ShopCell>();
    }
    
    private void OnEnable()
    {
        EventManager.OnUIMenuEnter += StopBoost;
        EventManager.OnUIMenuExit += ContinueBoost;
    }

    private void OnDisable()
    {
        EventManager.OnUIMenuEnter -= StopBoost;
        EventManager.OnUIMenuExit -= ContinueBoost;
    }
    
    public void StartBooster()
    {
        _available = true;
        _durationCounter = duration;
        buyButton.interactable = false;
        FishesSpawner.Instance.IncreaseRareFishCount(increasedRareFishMultiplier);
        ActivateTimer();
    }

    public void CancelBoost()
    {
        _available = false;
        buyButton.interactable = true;
        FishesSpawner.Instance.DecreaseRareFishCount();
        DeactivateTimer();
    }

    public void ActivateTimer()
    {
        boosterTimer.ActivateTimer(duration,boosterIcon);
    }

    public void DeactivateTimer()
    {
        boosterTimer.DeactivateTimer();
    }

    private void Update()
    {
        if (_available)
        {
            _shopCell.cellBuyButton.interactable = false;
            _durationCounter -= Time.deltaTime;
            if (_durationCounter <= 0)
            {
                CancelBoost();
            }
        }
    }

    public void StopBoost()
    {
        _available = false;
        boosterTimer.StopTimer();
    }

    public void ContinueBoost()
    {
        _available = true;
        boosterTimer.ContinueTimer();
    }
    
    public Sprite GetBoosterImage()
    {
        return boosterIcon;
    }
    
    public string GetBoosterDescription()
    {
        return boosterDescription;
    }
}
