using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MissionMenuValues : MonoBehaviour
{
    #region Singleton

    public static MissionMenuValues Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    public List<FishType> commonFishTypes = new List<FishType>();
    
    public List<FishType> rareFishTypes = new List<FishType>();
    
    public List<FishType> fishTypes = new List<FishType>();
    
    public List<FishType> availableFishTypes = new List<FishType>();

    private void OnEnable()
    {
        EventManager.OnLengthValueChanged += UpdateAvailableFishList;
    }

    private void OnDisable()
    {
        EventManager.OnLengthValueChanged -= UpdateAvailableFishList;
    }

    private void Start()
    {
        UpdateAvailableFishList();
    }

    private void UpdateAvailableFishList()
    {
        availableFishTypes.Clear();
        for (int i = 0; i < fishTypes.Count; i++)
        {
            if (fishTypes[i].minSpawnDepth < HookController.Instance.GetHookLength())
            {
                //
            }
            else
            {
                availableFishTypes.Add(fishTypes[i]);
            }
        }
    }

    public int Index { get; private set; }
    
    public FishType PickRandomFishType()
    {
        Index = Random.Range(0, fishTypes.Count);
        var fish =  fishTypes[Index];
        fishTypes.Remove(fish);
        return fish;
    }

    public FishType PickRandomAvailableFish()
    {
        Index = Random.Range(0, availableFishTypes.Count);
        var fish = availableFishTypes[Index];
        availableFishTypes.Remove(fish);
        return fish;
    }
}
