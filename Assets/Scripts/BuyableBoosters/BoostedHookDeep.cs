using System;
using UnityEngine;
using UnityEngine.UI;

public class BoostedHookDeep : MonoBehaviour
{
    [SerializeField] private float duration;
    private float _durationCounter;

    [Range(0.1f,1f)]
    [SerializeField] private float lengthMultiplier;
    [Range(2,4)]
    [SerializeField] private int newSpeedValueMultiplier;

    [SerializeField] private Button buyButton;
    
    private bool _available;
    
    public void StartBooster()
    {
        Debug.Log("BoostStarted");
        _available = true;
        _durationCounter = duration;
        buyButton.interactable = false;
        HookController.Instance.BoostCameraFollowLenght(lengthMultiplier,newSpeedValueMultiplier);
    }

    private void CancelBoost()
    {
        Debug.Log("BoostFinished");
        _available = false;
        buyButton.interactable = true;
        HookController.Instance.CancelBoostCameraFollowLength();
    }

    private void Update()
    {
        if (_available)
        {
            _durationCounter -= Time.deltaTime;
            if (_durationCounter <= 0)
            {
                CancelBoost();
            }
        }
        else
        {
            return;
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
