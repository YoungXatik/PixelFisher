using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFishCards : MonoBehaviour
{
    public List<FishCard_UI> fishCards = new List<FishCard_UI>();

    [SerializeField] private bool common;
    [SerializeField] private bool rare;

    private void Start()
    {
        if (common)
        {
            for (int i = 0; i < fishCards.Count; i++)
            {
                if (fishCards[i].FishType.FishQuality == FishQuality.Common)
                {
                    fishCards[i].UnlockCommonCard(); 
                }
                else
                {
                    Destroy(fishCards[i].gameObject);
                }
            }
        }
        else if(rare)
        {
            for (int i = 0; i < fishCards.Count; i++)
            {
                if (fishCards[i].FishType.FishQuality == FishQuality.Rare)
                {
                    fishCards[i].UnlockRareCard();
                }
                else
                {
                    Destroy(fishCards[i].gameObject);
                }
            }
        }
    }
}
