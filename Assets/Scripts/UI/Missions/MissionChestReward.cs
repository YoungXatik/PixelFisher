using System;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionChestReward : MonoBehaviour
{
    #region Singleton

    public static MissionChestReward Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    [field: SerializeField] public float FishCoins { get; private set; }
    
    [SerializeField] private TextMeshProUGUI fishCoinsCountText;

    [SerializeField] private TextMeshProUGUI[] textReaches;
    [SerializeField] private int[] textReachesValues;

    [SerializeField] private Image progressBarImage;

    private Tween _addTween, _removeTween;

    private void Start()
    {
        for (int i = 0; i < textReaches.Length; i++)
        {
            textReaches[i].text = $"{textReachesValues[i]}";
        }
        _step = 1f / textReachesValues.Last();
    }

    public void AddFishCoins(int value)
    {
        if (_addTween == null)
        {
            _addTween = DOTween.To(x => FishCoins = x, FishCoins, FishCoins + value, 1f).OnUpdate(UpdateUI);
        }
        else
        {
            if (_addTween.IsComplete())
            {
                _addTween = DOTween.To(x => FishCoins = x, FishCoins,  FishCoins + value, 1f).OnUpdate(UpdateUI);
            }
            else
            {
                _addTween.Complete();
                _addTween = DOTween.To(x => FishCoins = x, FishCoins,  FishCoins + value, 1f).OnUpdate(UpdateUI);
            }
        }
    }

    public void RemoveFishCoins(int value)
    {
        if (_removeTween == null)
        {
            _removeTween = DOTween.To(x => FishCoins = x, FishCoins,  FishCoins - value, 1f).OnUpdate((() => fishCoinsCountText.text = $"{Mathf.RoundToInt(FishCoins)}"));    
        }
        else
        {
            if (_removeTween.IsComplete())
            {
                _removeTween = DOTween.To(x => FishCoins = x, FishCoins,  FishCoins - value, 1f).OnUpdate((() => fishCoinsCountText.text = $"{Mathf.RoundToInt(FishCoins)}"));
            }
            else
            {
                _removeTween.Complete();
                _removeTween = DOTween.To(x => FishCoins = x, FishCoins,  FishCoins - value, 1f).OnUpdate((() => fishCoinsCountText.text = $"{Mathf.RoundToInt(FishCoins)}"));
            }
        }
    }

    private float _step;
    private void UpdateProgressBar()
    {
        progressBarImage.fillAmount = _step * FishCoins;
    }

    private void UpdateUI()
    {
        fishCoinsCountText.text = $"{Mathf.RoundToInt(FishCoins)}";
        UpdateProgressBar();
    }

    private void OnEnable()
    {
        UpdateUI();
    }
}
