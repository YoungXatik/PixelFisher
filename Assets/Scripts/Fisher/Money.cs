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
    [field: SerializeField] public float DiamondsCount { get; private set; }
    
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI fishBookMoneyText;
    [SerializeField] private TextMeshProUGUI shopMoneyText;
    
    [SerializeField] private TextMeshProUGUI shopDiamondsText;
    [SerializeField] private TextMeshProUGUI missionDiamondsText;

    private Tween _addTween;
    private Tween _removeTween;

    private Tween _diamondsAddTween;
    private Tween _diamondsRemoveTween;

    private void Start()
    { 
        UpdateUI();
    }

    private void UpdateUI()
    {
        moneyText.text = $"{MoneyCount}";
        fishBookMoneyText.text = $"{MoneyCount}";
        shopMoneyText.text = $"{MoneyCount}";
        shopDiamondsText.text = $"{DiamondsCount}";
        missionDiamondsText.text = $"{DiamondsCount}";
    }

    public void AddDiamonds(int value)
    {
        DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount + value, 1f)
            .OnUpdate((() => shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}")).OnComplete(delegate
            {
                shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
            });

        if (_diamondsAddTween == null)
        {
            _diamondsAddTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount + value, 1f)
                .OnUpdate((() => missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}")).OnComplete(delegate
                {
                    missionDiamondsText.text = $"{DiamondsCount}";
                });
        }
        else
        {
            if (_diamondsAddTween.IsActive())
            {
                _diamondsAddTween.Complete();
                _diamondsAddTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount + value, 1f)
                    .OnUpdate((() => missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}")).OnComplete(delegate
                    {
                        missionDiamondsText.text = $"{DiamondsCount}";
                    });
            }
            else
            {
                _diamondsAddTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount + value, 1f)
                    .OnUpdate((() => missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}")).OnComplete(delegate
                    {
                        missionDiamondsText.text = $"{DiamondsCount}";
                    });
            }
        }
    }
    
    public void RemoveDiamonds(int value)
    {
        if (_diamondsRemoveTween == null)
        {
            _diamondsRemoveTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount - value, 1f).
                OnUpdate((() => shopDiamondsText.text =  $"{Mathf.RoundToInt(DiamondsCount)}")).OnComplete(delegate
                {
                    missionDiamondsText.text = $"{DiamondsCount}";
                    shopDiamondsText.text = $"{DiamondsCount}";
                });
        }
        else
        {
            if (_diamondsRemoveTween.IsActive())
            {
                _diamondsRemoveTween.Complete();
                _diamondsRemoveTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount - value, 1f).
                    OnUpdate((() => shopDiamondsText.text =  $"{Mathf.RoundToInt(DiamondsCount)}")).OnComplete(delegate
                    {
                        missionDiamondsText.text = $"{DiamondsCount}";
                        shopDiamondsText.text = $"{DiamondsCount}";
                    });
            }
            else
            {
                _diamondsRemoveTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount - value, 1f).
                    OnUpdate((() => shopDiamondsText.text =  $"{Mathf.RoundToInt(DiamondsCount)}")).OnComplete(delegate
                    {
                        missionDiamondsText.text = $"{DiamondsCount}";
                        shopDiamondsText.text = $"{DiamondsCount}";
                    });
            }
        }
    }
    
    public void AddMoney(int value)
    {
         DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount + value, 1f)
            .OnUpdate((() => fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
            {
                EventManager.OnMoneyAddedInvoke();
                fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                shopMoneyText.text = $"{MoneyCount}";
            });
        
        if (_addTween == null)
        {
            _addTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount + value, 1f)
                .OnUpdate((() => moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
                {
                    EventManager.OnMoneyAddedInvoke();
                    shopMoneyText.text = $"{MoneyCount}";
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
                        shopMoneyText.text = $"{MoneyCount}";
                    });
            }
            else
            {
                _addTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount + value, 1f)
                    .OnUpdate((() => moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
                    {
                        EventManager.OnMoneyAddedInvoke();
                        shopMoneyText.text = $"{MoneyCount}";
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
                    fishBookMoneyText.text = $"{MoneyCount}";
                    shopMoneyText.text = $"{MoneyCount}";
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
                        fishBookMoneyText.text = $"{MoneyCount}";
                        shopMoneyText.text = $"{MoneyCount}";
                    });
            }
            else
            {
                _removeTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount - value, 1f).
                    OnUpdate((() => moneyText.text =  $"{Mathf.RoundToInt(MoneyCount)}")).OnComplete(delegate
                    {
                        EventManager.OnMoneyRemovedInvoke(); 
                        fishBookMoneyText.text = $"{MoneyCount}";
                        shopMoneyText.text = $"{MoneyCount}";
                    });
            }
        }
    }

    [ContextMenu("Test")]
    private void TestAdd()
    {
        AddMoney(1000);
    }
    [ContextMenu("TestDiamonds")]
    private void TestDiamondsAdd()
    {
        AddDiamonds(1000);
    }
}
