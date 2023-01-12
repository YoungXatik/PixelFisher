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
    [SerializeField] private TextMeshProUGUI missionMoneyText;

    [SerializeField] private TextMeshProUGUI shopDiamondsText;
    [SerializeField] private TextMeshProUGUI missionDiamondsText;

    private Tween _addTween;
    private Tween _removeTween;

    private Tween _diamondsAddTween;
    private Tween _diamondsRemoveTween;

    private void Start()
    { 
        UpdateUI();

        if (PlayerPrefs.HasKey("Money"))
        {
            MoneyCount = PlayerPrefs.GetFloat("Money");
            UpdateUI();
        }

        if (PlayerPrefs.HasKey("Diamonds"))
        {
            DiamondsCount = PlayerPrefs.GetFloat("Diamonds");
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
        fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
        shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
        missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
        shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
        missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
    }

    public void AddDiamonds(int value)
    {
        if (_diamondsAddTween == null)
        {
            _diamondsAddTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount + value, 0.5f).OnUpdate(delegate
            {
                shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
            }).OnComplete(delegate
            {
                EventManager.OnDiamondsChangedInvoke();
                missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                PlayerPrefs.SetFloat("Diamonds", DiamondsCount);
            });
        }
        else
        {
            if (_diamondsAddTween.IsActive())
            {
                _diamondsAddTween.Complete();
                _diamondsAddTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount + value, 0.5f).OnUpdate(delegate
                {
                    shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                }).OnComplete(delegate
                {
                    EventManager.OnDiamondsChangedInvoke();
                    missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    PlayerPrefs.SetFloat("Diamonds", DiamondsCount);
                });
            }
            else
            {
                _diamondsAddTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount + value, 0.5f).OnUpdate(delegate
                {
                    shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                }).OnComplete(delegate
                {
                    EventManager.OnDiamondsChangedInvoke();
                    missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    PlayerPrefs.SetFloat("Diamonds", DiamondsCount);
                });
            }
        }
    }

    public void RemoveDiamonds(int value)
    {
        if (_diamondsRemoveTween == null)
        {
            _diamondsRemoveTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount - value, 0.5f).OnUpdate(delegate
            {
                shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
            }).OnComplete(delegate
            {
                EventManager.OnDiamondsChangedInvoke();
                missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                PlayerPrefs.SetFloat("Diamonds", DiamondsCount);
            });
        }
        else
        {
            if (_diamondsRemoveTween.IsActive())
            {
                _diamondsRemoveTween.Complete();
                _diamondsRemoveTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount - value, 0.5f).OnUpdate(delegate
                {
                    shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                }).OnComplete(delegate
                {
                    EventManager.OnDiamondsChangedInvoke();
                    missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    PlayerPrefs.SetFloat("Diamonds", DiamondsCount);
                });
            }
            else
            {
                _diamondsRemoveTween = DOTween.To(x => DiamondsCount = x, DiamondsCount, DiamondsCount - value, 0.5f).OnUpdate(delegate
                {
                    shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                }).OnComplete(delegate
                {
                    EventManager.OnDiamondsChangedInvoke();
                    missionDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    shopDiamondsText.text = $"{Mathf.RoundToInt(DiamondsCount)}";
                    PlayerPrefs.SetFloat("Diamonds", DiamondsCount);
                });
            }
        }
    }

    public void AddMoney(int value)
    {
        if (_addTween == null)
        {
            _addTween =  DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount + value, 0.5f).OnUpdate(delegate
            {
                moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
            }).OnComplete(delegate
            {
                EventManager.OnMoneyChangedInvoke();
                moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                PlayerPrefs.SetFloat("Money", MoneyCount);
            });
        }
        else
        {
            if (_addTween.IsActive())
            {
                _addTween.Complete();
                _addTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount + value, 0.5f).OnUpdate(delegate
                {
                    moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                }).OnComplete(delegate
                {
                    EventManager.OnMoneyChangedInvoke();
                    moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    PlayerPrefs.SetFloat("Money", MoneyCount);
                });
            }
            else
            {
                _addTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount + value, 0.5f).OnUpdate(delegate
                {
                    moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                }).OnComplete(delegate
                {
                    EventManager.OnMoneyChangedInvoke();
                    moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    PlayerPrefs.SetFloat("Money", MoneyCount);
                });
            }
        }
    }

    public void RemoveMoney(int value)
    {
        if (_removeTween == null)
        {
            _removeTween =  DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount - value, 0.5f).OnUpdate(delegate
            {
                moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
            }).OnComplete(delegate
            {
                EventManager.OnMoneyChangedInvoke();
                moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                PlayerPrefs.SetFloat("Money", MoneyCount);
            });
        }
        else
        {
            if (_removeTween.IsActive())
            {
                _removeTween.Complete();
                _removeTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount - value, 0.5f).OnUpdate(delegate
                {
                    moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                }).OnComplete(delegate
                {
                    EventManager.OnMoneyChangedInvoke();
                    moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    PlayerPrefs.SetFloat("Money", MoneyCount);
                });
            }
            else
            {
                _removeTween = DOTween.To(x => MoneyCount = x, MoneyCount, MoneyCount - value, 0.5f).OnUpdate(delegate
                {
                    moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                }).OnComplete(delegate
                {
                    EventManager.OnMoneyChangedInvoke();
                    moneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    missionMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    shopMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    fishBookMoneyText.text = $"{Mathf.RoundToInt(MoneyCount)}";
                    PlayerPrefs.SetFloat("Money", MoneyCount);
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
        AddDiamonds(10);
    }
    [ContextMenu("TestRemove")]
    private void TestRemove()
    {
        RemoveMoney(1000);
    }
    [ContextMenu("TestDiamondsRemove")]
    private void TestDiamondRemove()
    {
        RemoveDiamonds(10);
    }
}
