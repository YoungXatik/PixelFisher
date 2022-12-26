using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{

    [SerializeField] private Button cellBuyButton;
    [SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private int cost;

    private void Start()
    {
        costText.text = $"{cost}";
        EventManager.OnDiamondsChanged += CheckCost;
        CheckCost();
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