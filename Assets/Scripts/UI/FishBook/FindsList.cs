using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindsList : MonoBehaviour
{
    public List<FishCard_UI> fishCards = new List<FishCard_UI>();

    public void CheckFishForCollect(List<Fish> hookedFish)
    {
        for (int i = 0; i < fishCards.Count; i++)
        {
            if (hookedFish[i].fishType == fishCards[i].FishType)
            {
                if (!hookedFish[i].fishType.isCollected)
                {
                    fishCards[i].FishType.isCollected = true;
                }
            }
        }
    }
}
