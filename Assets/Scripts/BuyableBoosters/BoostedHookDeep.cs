using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoostedHookDeep : MonoBehaviour, IBoostable
{
    [SerializeField] private float duration;
    private float _durationCounter;

    [SerializeField] private Sprite boosterIcon;
    
    [Range(0.1f,1f)]
    [SerializeField] private float lengthMultiplier;
    [Range(2,4)]
    [SerializeField] private int newSpeedValueMultiplier;

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
        HookController.Instance.BoostCameraFollowLenght(lengthMultiplier,newSpeedValueMultiplier);
        ActivateTimer();
    }

    public void CancelBoost()
    {
        _available = false;
        buyButton.interactable = true;
        HookController.Instance.CancelBoostCameraFollowLength();
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
