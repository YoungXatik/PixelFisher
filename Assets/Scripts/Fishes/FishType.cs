using UnityEngine;

[CreateAssetMenu(fileName = "Fish",menuName = "Create New Fish")]
public class FishType : ScriptableObject
{
    [field: SerializeField] public string fishName { get; private set; }

    [TextArea(minLines: 3, maxLines: 6)] 
    [field: SerializeField] public string fishDescription;
    [field: SerializeField] public Sprite fishSprite { get; private set; }
    [field: SerializeField] public Sprite lockedFishSprite { get; private set; }
    public bool isCollected;
    [field: SerializeField] public FishQuality FishQuality { get; private set; }
    [field: SerializeField] public int TotallyCatch { get; private set; }
    
    public bool isAchieved;
    
    [SerializeField] private float minSpawnDepth, maxSpawnDepth;
    [field: SerializeField] public float SpawnDepth { get; private set; }

    public float GetSpawnDepth()
    {
        SpawnDepth = Random.Range(minSpawnDepth, maxSpawnDepth);
        return SpawnDepth;
    }
    public void IncreaseCatchValue()
    {
        TotallyCatch++;
    }

    public void ResetCatchValue()
    {
        TotallyCatch = 0;
    }

}
public enum FishQuality
{
    Common, Rare
}
