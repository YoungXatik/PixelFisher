using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreasedFishCost : MonoBehaviour
{
    [SerializeField] private float duration;
    private float _durationCounter;

    [Range(0.25f,0.5f)] 
    [SerializeField] private float increasedCostMultiplier;

    [SerializeField] private Button buyButton;

    private bool _available;
    private ShopCell _shopCell;

    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject boosterTimerPrefab;

    private Image _boosterTimerImage;
}
