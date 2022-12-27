using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoostedHookDeep : MonoBehaviour
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
        HookController.Instance.BoostCameraFollowLenght(lengthMultiplier,newSpeedValueMultiplier);
        ActivateTimer();
    }

    private void CancelBoost()
    {
        Debug.Log("BoostFinished");
        _available = false;
        buyButton.interactable = true;
        HookController.Instance.CancelBoostCameraFollowLength();
        DeactivateTimer();
    }

    private void ActivateTimer()
    {
        boosterTimer.ActivateTimer(duration,boosterIcon);
    }

    private void DeactivateTimer()
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

    private void StopBoost()
    {
        _available = false;
    }

    private void ContinueBoost()
    {
        _available = true;
    }
}
