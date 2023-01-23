using System;
using DG.Tweening;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI descriptionText;
    
    private void Start()
    {
        _shopCell = GetComponent<ShopCell>();
        descriptionText.text = boosterDescription;
        if (PlayerPrefs.HasKey("boosterDuration" + gameObject.name))
        {
            StartSavedBooster();
        }
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

    private void StartSavedBooster()
    {
        _available = true;
        _durationCounter = duration;
        buyButton.interactable = false;
        HookController.Instance.BoostCameraFollowLenght(lengthMultiplier,newSpeedValueMultiplier);
        boosterTimer.ActivateTimer(PlayerPrefs.GetFloat("boosterDuration"),boosterIcon);
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
        PlayerPrefs.DeleteKey("boosterDuration");
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

    private void OnApplicationQuit()
    {
        if (_available)
        {
            PlayerPrefs.SetFloat("boosterDuration" + gameObject.name,_durationCounter);
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
