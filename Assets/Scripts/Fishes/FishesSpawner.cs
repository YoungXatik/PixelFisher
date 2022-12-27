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
    
    private int _trueFishCount;
    private int _trueRareFishCount;
    

    private void Awake()
    {
        Instance = this;
        _trueMinimalYPosition = minYPosition;
        EventManager.OnGameStarted += StartSpawnFish;
        EventManager.OnGameEnded += DeleteAllFishes;
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
    }

    [ContextMenu("Test Spawn")]
    public void StartSpawnFish()
    {
        _offsetY = Random.Range(minimalSpawnOffsetY, maximalSpawnOffsetY);
        //fishCount = Mathf.RoundToInt(-maxYPosition / _offsetY);
        _trueFishCount = fishCount;
        _trueRareFishCount = rareFishCount;
        for (int i = 0; i < _trueFishCount; i++)
        {
            if (fishCount <= 0)
            {
                break;
            }
            else
            {
                SpawnFish();
            }
        }

        for (int i = 0; i < _trueRareFishCount; i++)
        {
            if (rareFishCount <= 0)
            {
                break;
            }
            else
            {
                SpawnRareFish();
            }
        }
    }

    private void SpawnFish()
    {
        float offsetX = Random.Range(minimalSpawnOffsetX, maximalSpawnOffsetX);
        float currentYOffset = Random.Range(minimalSpawnOffsetY, maximalSpawnOffsetY);
        float offsetY = minYPosition - currentYOffset;
        var clone = Instantiate(commonFishPrefabs[Random.Range(0, commonFishPrefabs.Count)], new Vector3(offsetX, offsetY, 0),
            Quaternion.identity);
        spawnedFish.Add(clone);
        
        
        int scaleIndex = Random.Range(0, moveAndScaleDirection.Length);
        clone.SetFishValues(moveAndScaleDirection[scaleIndex],moveAndScaleDirection[scaleIndex]);

        minYPosition -= currentYOffset;
        fishCount--;
    }

    private void SpawnRareFish()
    {
        float offsetX = Random.Range(minimalSpawnOffsetX, maximalSpawnOffsetX);
        float currentYOffset = Random.Range(minimalSpawnOffsetY, maximalSpawnOffsetY);
        float offsetY = minYPosition - currentYOffset;
        var clone = Instantiate(rareFishPrefabs[Random.Range(0, rareFishPrefabs.Count)], new Vector3(offsetX, offsetY, 0),
            Quaternion.identity);
        spawnedFish.Add(clone);
        
        
        int scaleIndex = Random.Range(0, moveAndScaleDirection.Length);
        clone.SetFishValues(moveAndScaleDirection[scaleIndex],moveAndScaleDirection[scaleIndex]);

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
    }

}
