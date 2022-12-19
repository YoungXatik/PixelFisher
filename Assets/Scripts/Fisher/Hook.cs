using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    [SerializeField] private int maxCountOfFish, countOfFish;
    [SerializeField] private HookController hookController;

    private Transform _hookTransform;

    [SerializeField] private Booster strengthBooster;

    private void Awake()
    {
        _hookTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        EventManager.OnStrengthValueChanged += UpdateStrengthValue;
        EventManager.OnGameEnded += RefreshCountOfFishValue;
        UpdateStrengthValue();
    }

    private void UpdateStrengthValue()
    {
        maxCountOfFish = strengthBooster.CurrentBoosterValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Fish fish;

        if (other.gameObject.TryGetComponent<Fish>(out fish))
        {
            hookController.CheckForFirstFishEntry();
            hookController.hookedFishes.Add(fish);
            countOfFish++;
            if (countOfFish >= maxCountOfFish)
            {
                hookController.HookCountIsOver();
            }
            fish.FishHasBeenHooked(_hookTransform);
        }
    }

    private void RefreshCountOfFishValue()
    {
        countOfFish = 0;
    }
}