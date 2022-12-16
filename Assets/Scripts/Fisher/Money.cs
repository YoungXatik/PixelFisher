using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    #region MyRegion

    public static Money Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [field: SerializeField] public float MoneyCount { get; private set; }
    [SerializeField] private TextMeshProUGUI moneyText;

    private Tween _addTween;
    private Tween _removeTween;

    private void Start()
    { 
        UpdateUI();
    }

    private void UpdateUI()
    {
        moneyText.text = $"{MoneyCount}";
    }

    public void AddMoney(int value)
    {
        if (_addTween == null)
        {
            _addTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount + value, 1f)
                .OnUpdate((() => moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
                {
                    EventManager.OnMoneyAddedInvoke();
                });
        }
        else
        {
            if (_addTween.IsActive())
            {
                _addTween.Complete();
                _addTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount + value, 1f)
                    .OnUpdate((() => moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
                    {
                        EventManager.OnMoneyAddedInvoke();
                    });
            }
            else
            {
                _addTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount + value, 1f)
                    .OnUpdate((() => moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
                    {
                        EventManager.OnMoneyAddedInvoke();
                    });
            }
        }
        
    }

    public void RemoveMoney(int value)
    {
        if (_removeTween == null)
        {
            _removeTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount - value, 1f).
                OnUpdate((() => moneyText.text =  $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
                {
                    EventManager.OnMoneyRemovedInvoke(); 
                });
        }
        else
        {
            if (_removeTween.IsActive())
            {
                _removeTween.Complete();
                _removeTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount - value, 1f).
                    OnUpdate((() => moneyText.text =  $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
                    {
                        EventManager.OnMoneyRemovedInvoke(); 
                    });
            }
            else
            {
                _removeTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount - value, 1f).
                    OnUpdate((() => moneyText.text =  $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
                    {
                        EventManager.OnMoneyRemovedInvoke(); 
                    });
            }
        }
        
    }

    [ContextMenu("Test")]
    private void TestAdd()
    {
        AddMoney(1000);
    }
}
