using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelValue",menuName = "Create Level Value")]
public class LevelValues : ScriptableObject
{
    public List<Fish> currentLevelFish = new List<Fish>();
    public Sprite currentLevelWaterSprite;
}
