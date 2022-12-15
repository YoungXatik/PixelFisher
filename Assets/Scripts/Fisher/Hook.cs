using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public List<Fish> hookedFish = new List<Fish>();

    [SerializeField] private int maxCountOfFish, countOfFish;
    [SerializeField] private HookController hookController;

    private Transform _hookTransform;

    private void Awake()
    {
        _hookTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Fish fish;

        if (other.gameObject.TryGetComponent<Fish>(out fish))
        {
            hookController.hookedFishes.Add(fish);
            countOfFish++;
            if (countOfFish >= maxCountOfFish)
            {
                hookController.HookCountIsOver();
            }
            fish.FishHasBeenHooked(_hookTransform);
        }
    }
}