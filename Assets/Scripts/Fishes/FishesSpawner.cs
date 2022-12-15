using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishesSpawner : MonoBehaviour
{
    #region Singleton

    public static FishesSpawner Instance;

    #endregion
    
    public List<Fish> fishPrefabs = new List<Fish>();
    public List<Fish> spawnedFish = new List<Fish>();

    [SerializeField] private float minYPosition, maxYPosition;

    private float _trueMinimalYPosition;

    [SerializeField] private float minimalSpawnOffsetX, maximalSpawnOffsetX;
    [SerializeField] private float minimalSpawnOffsetY, maximalSpawnOffsetY;

    private float _offsetY;

    [SerializeField] private Vector2[] moveAndScaleDirection;

    [SerializeField] private int fishCount;
    private int _trueFishCount;

    public void SetLevelValues(List<Fish> _fishPrefabs, float _maxYPosition, int _maxFishCount)
    {
        fishPrefabs = _fishPrefabs;
        maxYPosition = _maxYPosition;
        fishCount = _maxFishCount;
    }

    private void Awake()
    {
        Instance = this;
        _trueMinimalYPosition = minYPosition;
        EventManager.OnGameStarted += StartSpawnFish;
        EventManager.OnGameEnded += DeleteAllFishes;
    }

    [ContextMenu("Test Spawn")]
    public void StartSpawnFish()
    {
        _offsetY = Random.Range(minimalSpawnOffsetY, maximalSpawnOffsetY);
        fishCount = Mathf.RoundToInt(-maxYPosition / _offsetY);
        _trueFishCount = fishCount;
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
    }

    private void SpawnFish()
    {
        float offsetX = Random.Range(minimalSpawnOffsetX, maximalSpawnOffsetX);
        float currentYOffset = Random.Range(minimalSpawnOffsetY, maximalSpawnOffsetY);
        float offsetY = minYPosition - currentYOffset;
        Fish clone = Instantiate(fishPrefabs[Random.Range(0, fishPrefabs.Count)], new Vector3(offsetX, offsetY, 0),
            Quaternion.identity);
        spawnedFish.Add(clone);
        
        
        int scaleIndex = Random.Range(0, moveAndScaleDirection.Length);
        clone.SetFishValues(moveAndScaleDirection[scaleIndex],moveAndScaleDirection[scaleIndex]);

        minYPosition -= currentYOffset;
        fishCount--;
    }

    private void DeleteAllFishes()
    {
        for (int i = 0; i < spawnedFish.Count; i++)
        {
            Destroy(spawnedFish[i].gameObject);
        }
        spawnedFish.Clear();
        minYPosition = _trueMinimalYPosition;
    }

}
