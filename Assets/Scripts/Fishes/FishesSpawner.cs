using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishesSpawner : MonoBehaviour
{
    #region Singleton

    public static FishesSpawner Instance;

    #endregion
    
    public List<Fish> commonFishPrefabs = new List<Fish>();
    public List<Fish> rareFishPrefabs = new List<Fish>();
    public List<Fish> spawnedFish = new List<Fish>();

    [SerializeField] private float minYPosition, maxYPosition;

    private float _trueMinimalYPosition;

    [SerializeField] private float minimalSpawnOffsetX, maximalSpawnOffsetX;
    [SerializeField] private float minimalSpawnOffsetY, maximalSpawnOffsetY;

    private float _offsetY;

    [SerializeField] private Vector2[] moveAndScaleDirection;

    [SerializeField] private int fishCount;
    [SerializeField] private int rareFishCount;

    [SerializeField] private float rareFishMultiplier;

    private float _trueRareFishMultiplier;
    private int _trueFishCount;
    private int _trueRareFishCount;
    

    private void Awake()
    {
        Instance = this;
        _trueMinimalYPosition = minYPosition;
        _trueRareFishMultiplier = rareFishMultiplier;
        
    }

    private void OnEnable()
    {
        EventManager.OnGameStarted += StartSpawnFish;
        EventManager.OnGameEnded += DeleteAllFishes;
    }

    private void OnDisable()
    {
        EventManager.OnGameStarted -= StartSpawnFish;
        EventManager.OnGameEnded -= DeleteAllFishes;
    }

    private void Start()
    {
        for (int i = 0; i < commonFishPrefabs.Count; i++)
        {
            if (commonFishPrefabs[i].fishType.FishQuality == FishQuality.Rare)
            {
                rareFishPrefabs.Add(commonFishPrefabs[i]);
                commonFishPrefabs.Remove(commonFishPrefabs[i]);
            }
        }
        _trueRareFishCount = Mathf.RoundToInt(rareFishCount * rareFishMultiplier);
        _trueFishCount = fishCount;
    }

    public void IncreaseRareFishCount(float multiplier)
    {
        rareFishMultiplier = multiplier;
    }

    public void DecreaseRareFishCount()
    {
        rareFishMultiplier = _trueRareFishMultiplier;
        rareFishCount = _trueRareFishCount;
    }
    

    [ContextMenu("Test Spawn")]
    public void StartSpawnFish()
    {
        _offsetY = Random.Range(minimalSpawnOffsetY, maximalSpawnOffsetY);
        
        rareFishCount *= Mathf.RoundToInt(rareFishMultiplier);
        

        while (fishCount >= 1)
        {
            SpawnFish(commonFishPrefabs[Random.Range(0,commonFishPrefabs.Count)]);
        }

        while (rareFishCount >= 1)
        {
            SpawnRareFish(rareFishPrefabs[Random.Range(0,rareFishPrefabs.Count)]);
        }
    }

    private void SpawnFish(Fish fish)
    {
        float offsetX = Random.Range(minimalSpawnOffsetX, maximalSpawnOffsetX);
        float currentYOffset = Random.Range(minimalSpawnOffsetY, maximalSpawnOffsetY);
        var clone = Instantiate(fish, new Vector3(offsetX, fish.fishType.GetSpawnDepth() - currentYOffset, 0),
            Quaternion.identity);
        spawnedFish.Add(clone);
        int scaleIndex = Random.Range(0, moveAndScaleDirection.Length);
        clone.SetFishValues(moveAndScaleDirection[scaleIndex], moveAndScaleDirection[scaleIndex]);

        minYPosition -= currentYOffset;
        fishCount--;
    }

    private void SpawnRareFish(Fish fish)
    {
        float offsetX = Random.Range(minimalSpawnOffsetX, maximalSpawnOffsetX);
        float currentYOffset = Random.Range(minimalSpawnOffsetY, maximalSpawnOffsetY);
        var clone = Instantiate(fish, new Vector3(offsetX, fish.fishType.GetSpawnDepth() - currentYOffset, 0),
            Quaternion.identity);
        spawnedFish.Add(clone);
        int scaleIndex = Random.Range(0, moveAndScaleDirection.Length);
        clone.SetFishValues(moveAndScaleDirection[scaleIndex], moveAndScaleDirection[scaleIndex]);

        minYPosition -= currentYOffset;
        rareFishCount--;
    }

    private void DeleteAllFishes()
    {
        for (int i = 0; i < spawnedFish.Count; i++)
        {
            Destroy(spawnedFish[i].gameObject);
        }
        spawnedFish.Clear();
        minYPosition = _trueMinimalYPosition;

        fishCount = _trueFishCount;
        rareFishCount = _trueRareFishCount;
        DecreaseRareFishCount();
    }

}
