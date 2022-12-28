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

    public FishType PickRandomFishType()
    {
        var fish =  fishTypes[0];
        fishTypes.Remove(fish);
        return fish;
    }
}
