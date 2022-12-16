using UnityEngine;

[CreateAssetMenu(fileName = "Fish",menuName = "Create New Fish")]
public class FishType : ScriptableObject
{
    [field: SerializeField] public string fishName { get; private set; }
    [field: SerializeField] public Sprite fishSprite { get; private set; }
    [field: SerializeField] public Sprite lockedFishSprite { get; private set; }
}
