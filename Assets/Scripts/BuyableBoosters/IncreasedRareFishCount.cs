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

    private void Start()
    {
        _shopCell = GetComponent<ShopCell>();
    }
    
    public void StartBooster()
    {
        Debug.Log("BoostStarted");
        _available = true;
        _durationCounter = duration;
        buyButton.interactable = false;
        FishesSpawner.Instance.IncreaseRareFishCount(increasedRareFishMultiplier);
        ActivateTimer();
    }

    public void CancelBoost()
    {
        Debug.Log("BoostFinished");
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
    }

    public void ContinueBoost()
    {
        _available = true;
    }
    
    public Sprite GetBoosterImage()
    {
        return boosterIcon;
    }
}
