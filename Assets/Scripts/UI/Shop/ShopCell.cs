using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{

    [field: SerializeField] public Button cellBuyButton { get; private set; }
    [SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private int cost;

    private void Start()
    {
        costText.text = $"{cost}";
        
        CheckCost();
    }

    private void OnEnable()
    {
        EventManager.OnDiamondsChanged += CheckCost;
    }

    private void OnDisable()
    {
        EventManager.OnDiamondsChanged -= CheckCost;
    }

    public void Buy()
    {
        Money.Instance.RemoveDiamonds(cost);
    }

    public void CheckCost()
    {
        if (Money.Instance.DiamondsCount >= cost)
        {
            cellBuyButton.interactable = true;
        }
        else
        {
            cellBuyButton.interactable = false;
        }
    }
}